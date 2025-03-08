//using HomeAssistantGenerated;
//using Niemand.Helpers.Notifications;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Niemand.SecurityApps;

//public class DoorWatchdogService
//{
//    private readonly BinarySensorEntity[] _doors;
//    private readonly ILogger _logger;
//    private readonly INotifyService _notifyService;
//    private readonly IScheduler _scheduler;
//    private readonly List<BinarySensorEntity> _doorsOpened;
//    private readonly Entity<AlarmControlPanelAttributes> _alarmPanel;
//    private readonly HomeAssistantEvents _haEvents;
//    private readonly TelegramBotService _telegramBot;

//    public DoorWatchdogService(
//        ILogger logger,
//        INotifyService notifyService,
//        IScheduler scheduler,
//        List<BinarySensorEntity> doorsOpened,
//        Entity<AlarmControlPanelAttributes> alarmPanel,
//        HomeAssistantEvents haEvents,
//        TelegramBotService telegramBot,
//        BinarySensorEntity[] doors)
//    {
//        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        _notifyService = notifyService ?? throw new ArgumentNullException(nameof(notifyService));
//        _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
//        _doorsOpened = doorsOpened ?? throw new ArgumentNullException(nameof(doorsOpened));
//        _alarmPanel = alarmPanel ?? throw new ArgumentNullException(nameof(alarmPanel));
//        _haEvents = haEvents ?? throw new ArgumentNullException(nameof(haEvents));
//        _telegramBot = telegramBot ?? throw new ArgumentNullException(nameof(telegramBot));
//        _doors = doors ?? throw new ArgumentNullException(nameof(doors));
//    }

//    public void Start()
//    {
//        // Subscribe to door open events
//        _doors.StateChanges()
//            .Where(e => e.New?.IsOn() ?? true)
//            .Subscribe(e =>
//            {
//                if (!_doorsOpened.Contains(e.Entity))
//                {
//                    _doorsOpened.Add(e.Entity);
//                    _logger.LogInformation("Added open door: {door}", e.Entity.EntityId);
//                }
//            });

//        // Subscribe to door close events
//        _doors.StateChanges()
//            .Where(e => e.New?.IsOff() ?? false)
//            .Subscribe(e => NotifyUncheckedDoors());

//        // Subscribe to alarm armed events
//        _alarmPanel.StateChanges()
//            .Where(e => e.Entity.IsArmed())
//            .Subscribe(e => NotifyUncheckedDoors());

//        // Telegram callback handlers
//        _haEvents.HandleTelegramCallback(_telegramBot, _logger)
//            .On("/locked", HandleLockedCommand)
//            .On("/defer", HandleDeferCommand)
//            .On("/ignore", HandleIgnoreCommand);
//    }

//    private void NotifyUncheckedDoors()
//    {
//        foreach (var door in _doorsOpened)
//        {
//            _logger.LogInformation("Notifying door check: {door}", door.EntityId);
//            _notifyService.Eugene(new NotifyEugeneParameters
//            {
//                Message = $"Is the {door.Attributes.FriendlyName} locked?",
//                Data = new
//                {
//                    inline_keyboard = new List<string>
//                    {
//                        $"🔒Locked:/locked {door.EntityId}, 🙈Ignore:/ignore {door.EntityId}",
//                        $"⏳Defer 5 min:/defer {door.EntityId} 5, ⏳Defer 30 min:/defer {door.EntityId} 30",
//                        $"⏳Defer 1 hour:/defer {door.EntityId} 60, ⏳Defer 6 hours:/defer {door.EntityId} 360"
//                    }
//                }
//            });
//        }
//    }

//    private bool TryGetDoorFromCallback(TelegramCallback e, out BinarySensorEntity? door)
//    {
//        door = _doorsOpened.FirstOrDefault(d => d.EntityId == e?.Args[0]);
//        return door != null;
//    }

//    private void HandleLockedCommand(TelegramCallback e, TelegramCallbackHandler handler)
//    {
//        if (!TryGetDoorFromCallback(e, out var door)) return;

//        _doorsOpened.Remove(door);
//        handler.EditMessage(e, $"Acknowledged {door.Attributes?.FriendlyName} is locked");
//    }

//    private void HandleDeferCommand(TelegramCallback e, TelegramCallbackHandler handler)
//    {
//        if (!TryGetDoorFromCallback(e, out var door) || !int.TryParse(e.Args[1], out int minutes)) return;

//        handler.EditMessage(e, $"I'll remind you in {minutes} minutes to check {door.Attributes?.FriendlyName}");
//        _scheduler.Schedule(TimeSpan.FromMinutes(minutes), () => NotifyUncheckedDoors());
//    }

//    private void HandleIgnoreCommand(TelegramCallback e, TelegramCallbackHandler handler)
//    {
//        if (!TryGetDoorFromCallback(e, out var door)) return;

//        _doorsOpened.Remove(door);
//        handler.EditMessage(e, $"{door.Attributes?.FriendlyName} check ignored🤦‍");
//    }
//}