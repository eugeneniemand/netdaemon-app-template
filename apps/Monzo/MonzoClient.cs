using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NetDaemon.AppModel;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;

// Monzo API Client
public class MonzoClient
{
    private readonly HttpClient _httpClient;
    private string _accessToken;
    private string _refreshToken;
    private DateTime _tokenExpiry;
    private readonly string _clientId = "oauth2client_0000Arzoy9ucGWjr2v2KmH"; // From Monzo developer portal
    private readonly string _clientSecret = "mnzconf.ryPjukkjk63R0AQxTYBSW1pwpVHOxEFjITbNK2PxfNtgev21U+q1X6Y7RbVdXSIPNZrHKLRCt/yYbIz4d0PPPg=="; // From Monzo developer portal
    private readonly string _redirectUri = "https://hooks.nabu.casa/gAAAAABn2BpIC0uimfg4sjq2vn6tHhSmJvH45B37StDSKzOFQJ_1zkpk7rNNBbdOYyIwKHDR7vF1RVovZHyzRXPOOlHHGdMb3QGPaiblKlUDTJx4S3LTYA139WMfMMcB8TLGLPzAI5SR1usHU6nKyyTHByVKz6xUysvr3QB2q03PWJzlNbUFQC0="; // Your registered redirect URI
    private readonly string _tokenFilePath = Path.Combine(Directory.GetCurrentDirectory(), "monzo_tokens.json");
    private readonly ILogger<MonzoApp> _logger;

    // One Off manual oauth flow
    // https://auth.monzo.com/?client_id=oauth2client_0000Arzoy9ucGWjr2v2KmH&redirect_uri=https://32gfs8k52wonmyiohfgg4v73kkr7w6it.ui.nabu.casa/redirect/oauth&response_type=code&state=hass

    public MonzoClient(ILogger<MonzoApp> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.monzo.com/")
        };
        LoadTokens();        
    }

    private void LoadTokens()
    {
        try
        {
            if (File.Exists(_tokenFilePath))
            {
                string json = File.ReadAllText(_tokenFilePath);
                var tokens = JsonSerializer.Deserialize<TokenStorage>(json);
                if (tokens != null)
                {
                    _accessToken = tokens.AccessToken;
                    _refreshToken = tokens.RefreshToken;
                    _tokenExpiry = tokens.Expiry;
                    _logger.LogInformation("Monzo tokens loaded");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failed to load tokens: {ex.Message}");
        }
    }

    private void SaveTokens()
    {
        try
        {
            var tokens = new TokenStorage
            {
                AccessToken = _accessToken,
                RefreshToken = _refreshToken,
                Expiry = _tokenExpiry
            };
            string json = JsonSerializer.Serialize(tokens, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_tokenFilePath, json);
            _logger.LogInformation("Monzo tokens saved");
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failed to save tokens: {ex.Message}");
        }
    }

    // Acquire initial access token (you'd need to handle OAuth flow first)
    public async Task<bool> AcquireTokenAsync(string authCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "oauth2/token")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"client_id", _clientId},
                {"client_secret", _clientSecret},
                {"redirect_uri", _redirectUri},
                {"code", authCode}
            })
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return false;

        var json = await response.Content.ReadAsStringAsync();
        var tokenData = JsonSerializer.Deserialize<TokenResponse>(json);

        _accessToken = tokenData.access_token;
        _refreshToken = tokenData.refresh_token;
        _tokenExpiry = DateTime.UtcNow.AddSeconds(tokenData.expires_in);

        SaveTokens();

        return true;
    }

    // Refresh token when expired
    public async Task<bool> RefreshTokenAsync()
    {
        if (string.IsNullOrEmpty(_refreshToken)) return false;

        var request = new HttpRequestMessage(HttpMethod.Post, "oauth2/token")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"client_id", _clientId},
                {"client_secret", _clientSecret},
                {"refresh_token", _refreshToken}
            })
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return false;

        var json = await response.Content.ReadAsStringAsync();
        var tokenData = JsonSerializer.Deserialize<TokenResponse>(json);

        _accessToken = tokenData.access_token;
        _refreshToken = tokenData.refresh_token;
        _tokenExpiry = DateTime.UtcNow.AddSeconds(tokenData.expires_in);

        SaveTokens(); // Save after refreshing

        return true;
    }

    // Move money between pots
    public async Task<bool> MoveToPotAsync(string accountId, string potId, decimal amount)
    {
        // Check and refresh token if expired
        if (DateTime.UtcNow >= _tokenExpiry)
        {
            await RefreshTokenAsync();
        }

        var request = new HttpRequestMessage(HttpMethod.Put, $"pots/{potId}/deposit")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"account_id", accountId},
                {"amount", amount.ToString()}, // Convert to pence
                {"dedupe_id", Guid.NewGuid().ToString()} // Unique identifier for deduplication
            })
        };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to move money: {response.StatusCode}");
        }
        _logger.LogInformation("Withdrawn {amount} from pot {pot}", amount, potId);
        return response.IsSuccessStatusCode;
    }

    // Withdraw from pots
    public async Task<bool> WithdrawPotAsync(string accountId, string potId, decimal amount)
    {
        // Check and refresh token if expired
        if (DateTime.UtcNow >= _tokenExpiry)
        {
            await RefreshTokenAsync();
        }

        var request = new HttpRequestMessage(HttpMethod.Put, $"pots/{potId}/withdraw")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"destination_account_id", accountId},
                {"amount", amount.ToString()}, // Convert to pence
                {"dedupe_id", Guid.NewGuid().ToString()} // Unique identifier for deduplication
            })
        };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to withdraw money: {response.StatusCode}");
        }
        _logger.LogInformation("Withdrawn {amount} from pot {pot}", amount, potId);
        return response.IsSuccessStatusCode;
    }

    public async Task<MonzoBalanceResponse?> GetBalanceAsync(string accountId)
    {
        //Check and refresh token if expired
        if (DateTime.UtcNow >= _tokenExpiry)
        {
            await RefreshTokenAsync();
        }

        var request = new HttpRequestMessage(HttpMethod.Get, $"balance?account_id={accountId}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to get balance: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MonzoBalanceResponse>(json) ?? null;
    }

    public async Task WhoAmIAsync()
    {
        //Check and refresh token if expired
        if (DateTime.UtcNow >= _tokenExpiry)
        {
            await RefreshTokenAsync();
        }

        var request = new HttpRequestMessage(HttpMethod.Get, $"ping/whoami");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to get whoami: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        _logger.LogDebug("Monzo WhoAmI {whoAmI}", json);
    }

    private record TokenResponse
    {
        public string access_token { get; init; }
        public string refresh_token { get; init; }
        public int expires_in { get; init; }
    }
}

public class MonzoOAuthCallback
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }
}
public class TokenStorage
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonPropertyName("expiry")]
    public DateTime Expiry { get; set; }
}

public class MonzoWebhookPayload
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("data")]
    public TransactionData Data { get; set; }
}

public class TransactionData
{
    [JsonPropertyName("account_id")]
    public string AccountId { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }  // In pence/minor currency units

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("is_load")]
    public bool IsLoad { get; set; }

    [JsonPropertyName("settled")]
    public DateTime Settled { get; set; }

    [JsonPropertyName("merchant")]
    public Merchant Merchant { get; set; }
}

public class Merchant
{
    [JsonPropertyName("address")]
    public Address Address { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("group_id")]
    public string GroupId { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("logo")]
    public string Logo { get; set; }

    [JsonPropertyName("emoji")]
    public string Emoji { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }
}

public class Address
{
    [JsonPropertyName("address")]
    public string StreetAddress { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("postcode")]
    public string Postcode { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }
}

public class MonzoBalanceResponse
{
    [JsonPropertyName("balance")]
    public long Balance { get; set; }  // The currently available balance of the account

    [JsonPropertyName("total_balance")]
    public long TotalBalance { get; set; }  // The sum of the currently available balance of the account and the combined total of all the user's pots.

    [JsonPropertyName("currency")]
    public string Currency { get; set; }  // ISO 4217 currency code

    [JsonPropertyName("spend_today")]
    public long SpendToday { get; set; }  // The amount spent from this account today (considered from approx 4am onwards)
}