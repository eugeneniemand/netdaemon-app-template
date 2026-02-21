// NetDaemon App
using NetDaemon.HassModel.Integration;
using System;
using System.IO;
using System.Threading;
using System.Reactive.Concurrency;
using NetDaemon;
using Niemand.Helpers.Notifications;

[NetDaemonApp]
//[Focus]
public class MonzoApp : IAsyncInitializable
{
    private readonly IHaContext _ha;
    private readonly ILogger<MonzoApp> _logger;
    private readonly IScheduler _scheduler;
    private readonly MonzoClient _monzoClient;
    private readonly PushNotifier _pushNotifier;
    private readonly string _accountId = "acc_0000AJeEzK6iaxm13KsTGz"; // From Monzo API
    private readonly string _billsPotId = "pot_0000AO5mGcxSUSc0QAKauX"; // Source pot ID
    private readonly string _targetPotId = "pot_target_id"; // Target pot ID


    private decimal PoundsToPence(decimal amount) => (amount * 100);
    private decimal PenceToPounds(decimal amount) => (amount / 100);


    public MonzoApp(IHaContext ha, ILogger<MonzoApp> logger, IScheduler scheduler, PushNotifier pushNotifier)
    {
        _ha = ha;
        _logger = logger;
        _scheduler = scheduler;
        _monzoClient = new MonzoClient(_logger);
        _pushNotifier = pushNotifier;


    }

    // Initialize with initial token (you'd need to store this securely)
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        // For initial setup, you'll need to complete OAuth flow manually first
        // and store the auth code or initial tokens somewhere secure
        // This is a one-time setup step
        _ha.Events.Filter<MonzoOAuthCallback>("monzo_oauth_redirect_received").SubscribeAsync(async e => await HandleOAuthRedirect(e.Data));

        var autUrl = _monzoClient.GetAuthorizeUrl();
        _logger.LogDebug("Please authorize the app by visiting: {url}", autUrl);

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
        _scheduler.SchedulePeriodic(TimeSpan.FromMinutes(60), async () => await _monzoClient.RefreshTokenAsync());
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
            var billsBalance = await _monzoClient.GetBalanceAsync(_billsPotId);
            var accountBalance = await _monzoClient.GetBalanceAsync(_accountId);
            if (billsBalance != null && PenceToPounds(billsBalance.Balance) < 100)
            {
                _pushNotifier.Notify(PushNotifier.Recipient.All, "Bills Balance Depleted", "Pot does not have enough to replensih main account");
                return;
            }

            if (accountBalance != null && PenceToPounds(accountBalance.Balance) < 100)
            {
                _logger.LogInformation("Account balance below threshhold of £100 moving money");

                await _monzoClient.WithdrawPotAsync(_accountId, _billsPotId, PoundsToPence(100) - accountBalance.Balance);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occured: {ex}", ex);
        }
    }

}