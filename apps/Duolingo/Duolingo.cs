using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace Niemand.Duolingo;

[NetDaemonApp]
//[Focus]
public class Duolingo : IAsyncInitializable
{
    private async Task<Root> LoginAsync(string username, string jwt)
    {
        string loginUrl = $"https://www.duolingo.com/2017-06-30/users/{username}?fields=username,totalXp,sessionCount,weeklyXp,streak,practiceReminderSettings,currentCourse,health,streakData{{currentStreak,longestStreak,previousStreak}}";
        using var handler = new HttpClientHandler();
        handler.UseCookies = true; // This is true by default
        using var httpClient = new HttpClient(handler);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36");
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt);
        var response       = await httpClient.GetAsync(loginUrl);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Root>(responseString) ?? throw new Exception("Failed to deserialize response");
    }

    private Dictionary<string, string> Logins = new Dictionary<string, string>
    {
        // Eugene
        { "3030808", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjYzMDcyMDAwMDAsImlhdCI6MCwic3ViIjozMDMwODA4fQ.GOfCC7tWjanVNGcURgdvNUfUXe8JRRQbF-fPIBItfPU" },
        // Jayden
        { "847129330", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjYzMDcyMDAwMDAsImlhdCI6MCwic3ViIjo4NDcxMjkzMzB9.e7TB8LY0EIom4MQvUl-9ar-khe3775pQ3YTuvSNaImE" }
    };

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        // Login to Duolingo for each user
        var roots = Logins.Select(async l => await LoginAsync(l.Key, l.Value)).ToList();
        await Task.WhenAll(roots);
        var users = roots.Select(r => r.Result).ToList();
    }
}
