using System;
using System.Collections.Generic;
using System.Linq;
using NetDaemon.Common.Reactive;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace Niemand
{
    public class NotificationEngineImpl
    {
        private readonly INetDaemonRxApp                              _app;
        private readonly Dictionary<string, NotificationEngineConfig> _config;
        private readonly Dictionary<string, Action>                   _messageBuilders = new();
        public string InstantMessage { get; private set; }
        public string VoiceMessage { get; private set; }

        public NotificationEngineImpl(INetDaemonRxApp app, Dictionary<string, NotificationEngineConfig> config)
        {
            _app    = app;
            _config = config;
        }

        public void Initialize()
        {
            _app.Entities(_config.Select(c => c.Value.EntityId))
                .StateChanges
                .Subscribe(s => { });
        }

        public void Notify(List<string> options)
        {
            ClearMessages();

            foreach (var option in options) _messageBuilders[option].Invoke();

            _app.LogInformation($"VoiceMessage: {VoiceMessage}");
            _app.LogInformation($"InstantMessage: {InstantMessage}");

            SendNotifications();
        }

        private void BuildMessages((string voiceMessage, string instantMessage) caller)
        {
            VoiceMessage   += "<s>" + caller.voiceMessage + "</s>";
            InstantMessage += caller.instantMessage + Environment.NewLine;
        }

        private void ClearMessages()
        {
            VoiceMessage   = "";
            InstantMessage = "";
        }

        private bool GetEntityState(string entityId, out string? state)
        {
            state = _app.States.FirstOrDefault(e => e.EntityId == entityId)?.State?.ToString();
            return state != null;
        }

        private (string voiceMessage, string instantMessage) GetStateOnMessage(
            string entityId, string voiceMessage, string instantMessage)
        {
            return StateIsOn(entityId) ? ( voiceMessage, instantMessage ) : ( string.Empty, string.Empty );
        }

        private void SendNotifications()
        {
            _app.CallService("notify", "alexa_media", new
            {
                message = $"<voice name=\"Emma\">{VoiceMessage}</voice>",
                target  = new List<string> { "media_player.downstairs" },
                data = new
                {
                    type = "announce"
                }
            });

            _app.CallService("notify", "twinstead", new
            {
                message = InstantMessage
            });
        }

        private bool StateIsOn(string entityId, string onState = "on")
        {
            return GetEntityState(entityId, out var state) && state.ToLower() == onState;
        }
    }

    public class NotificationEngineConfig
    {
        public string EntityId { get; set; }
        public string? VoiceMsg { get; set; }
        public string? TextMsg { get; set; }
        public string State { get; set; } = "on";
        public int? Interval { get; set; }
    }
}