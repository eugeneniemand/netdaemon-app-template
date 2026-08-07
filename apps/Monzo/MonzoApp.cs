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
    private int _warningCount = 0;


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
            //await _monzoClient.GetAccountsAsync(cancellationToken);
            //await _monzoClient.GetPotsAsync(_accountId, cancellationToken);

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
            var accountBalance = await _monzoClient.GetBalanceAsync(_accountId);
            var billsBalance = await _monzoClient.GetPotBalanceAsync(_accountId, "Bills");

            var accountBalanceInPounds = PenceToPounds(accountBalance.Balance);
            if (accountBalanceInPounds >= 150)
                return;

            var billsBalanceInPounds = PenceToPounds(billsBalance);
            if (billsBalanceInPounds < 150)
            {
                HandleInsufficientBillsBalance();
                return;
            }

            _warningCount = 0;
            _logger.LogInformation("Account balance below threshold of £150, moving money");

            var amountToWithdraw = PoundsToPence(150) - accountBalance.Balance;
            await _monzoClient.WithdrawPotAsync(_accountId, _billsPotId, amountToWithdraw);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred: {ex}", ex);
        }
    }

    private void HandleInsufficientBillsBalance()
    {
        if (_warningCount > 3)
            return;

        _pushNotifier.Notify(PushNotifier.Recipient.All, "Bills Balance Depleted", "Pot does not have enough to replenish main account");
        _warningCount++;
    }

}