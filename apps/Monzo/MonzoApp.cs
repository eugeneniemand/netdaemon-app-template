// NetDaemon App
using NetDaemon.HassModel.Integration;
using System;
using System.IO;
using System.Threading;
using System.Reactive.Concurrency;

[NetDaemonApp]
//[Focus]
public class MonzoApp : IAsyncInitializable
{
    private readonly IHaContext _ha;
    private readonly ILogger<MonzoApp> _logger;
    private readonly IScheduler _scheduler;
    private readonly MonzoClient _monzoClient;
    private readonly string _accountId = "acc_0000AJeEzK6iaxm13KsTGz"; // From Monzo API
    private readonly string _sourcePotId = "pot_0000AO5mGcxSUSc0QAKauX"; // Source pot ID
    private readonly string _targetPotId = "pot_target_id"; // Target pot ID


    private decimal PoundsToPence(decimal amount) => (amount * 100);
    private decimal PenceToPounds(decimal amount) => (amount / 100);


    public MonzoApp(IHaContext ha, ILogger<MonzoApp> logger, IScheduler scheduler)
    {
        _ha = ha;
        _logger = logger;
        _scheduler = scheduler;
        _monzoClient = new MonzoClient(_logger);
    }

    // Initialize with initial token (you'd need to store this securely)
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        // For initial setup, you'll need to complete OAuth flow manually first
        // and store the auth code or initial tokens somewhere secure
        // This is a one-time setup step
        _ha.Events.Filter<MonzoOAuthCallback>("monzo_oauth_redirect_received").SubscribeAsync(async e => await HandleOAuthRedirect(e.Data));

        _monzoClient.GetAuthorizeUrl();

        try
        {
            await _monzoClient.WhoAmIAsync(cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Unable to reach Monzo during initialization; will retry on the next scheduled run.");
        }

        await ReplensihBalance();

        _scheduler.SchedulePeriodic(TimeSpan.FromMinutes(5), async () => await ReplensihBalance());
    }

    private async Task HandleOAuthRedirect(MonzoOAuthCallback data)
    {
        try
        {
            string authCode = data.Code;
            string state = data.State;

            if (!string.IsNullOrEmpty(authCode))
            {
                await _monzoClient.AcquireTokenAsync(authCode);

            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }

    public async Task ReplensihBalance()
    {
        try
        {
            var balanceResponse = await _monzoClient.GetBalanceAsync(_accountId);
            if (balanceResponse != null && PenceToPounds(balanceResponse.Balance) < 100)
            {
                _logger.LogInformation("Account balance below threshhold of £100 moving money");

                await _monzoClient.WithdrawPotAsync(_accountId, _sourcePotId, PoundsToPence(100) - balanceResponse.Balance);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occured: {ex}", ex);
        }
    }

}