using NetDaemon.Helpers;
using Niemand.Helpers;

namespace Niemand.Dining;

[NetDaemonApp]
//[Focus]
public class DiningApp
{
    private readonly IAlexa             _alexa;
    private readonly IHaContext         _haContext;
    private readonly ILogger<DiningApp> _logger;
    private          string             _selectedPerson;

    public DiningApp(IHaContext haContext, IAlexa alexa, ILogger<DiningApp> logger)
    {
        _haContext = haContext;
        _alexa     = alexa;
        _logger    = logger;

        haContext.Entity("input_button.diningseating").StateChanges().Subscribe(_ =>
            {
                logger.LogInformation("Button Pressed");
                GetSeatingArrangement();
                logger.LogInformation("Making Announcement");
                alexa.Announce(new Alexa.Config { Entity = "media_player.downstairs", Message = $"It is {_selectedPerson}'s turn to sit next to mummy" });
            }
        );
    }

    private void GetSeatingArrangement()
    {
        switch (DateTime.Now.DayOfWeek)
        {
            case DayOfWeek.Monday:
            case DayOfWeek.Wednesday:
            case DayOfWeek.Saturday:
                _selectedPerson = "Jayden";
                break;
            case DayOfWeek.Tuesday:
            case DayOfWeek.Thursday:
            case DayOfWeek.Sunday:
                _selectedPerson = "Aaron";
                break;
        }

        _logger.LogInformation($"Selected Person is: {_selectedPerson}");
    }
}