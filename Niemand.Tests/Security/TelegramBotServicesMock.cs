using Niemand.Helpers.Notifications;
using NetDaemon.HassModel;

namespace Niemand.Tests.Security;

public class TelegramBotServicesMock(IHaContext haContext) : TelegramBotServices(haContext)
{
    // Simple mock - inherits all behavior from TelegramBotServices but won't actually send messages
    // since we're using mock IHaContext which won't process the calls
}
