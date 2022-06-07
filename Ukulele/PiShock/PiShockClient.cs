using System.Net.Http.Json;

namespace Ukulele.PiShock;

public class PiShockClient
{
    private const string Url = "https://do.pishock.com/api/apioperate";
    private readonly HttpClient _client = new();

    public PiShockClient(string username, string apiKey, string code, string name)
    {
        Username = username;
        ApiKey = apiKey;
        Code = code;
        Name = name;
    }

    public string Username { get; }
    public string ApiKey { get; }
    public string Code { get; }
    public string Name { get; }

    private async void PrintIfError(Task<HttpResponseMessage> responseTask)
    {
        var response = await responseTask;
        if (!response.IsSuccessStatusCode)
        {
            await Console.Error.WriteLineAsync($"Failed request:\n{response}");
        }
    }

    /// <param name="duration">1-15</param>
    /// <param name="intensity">1-100</param>
    public void Shock(int duration, int intensity)
    {
        var request = new ShockRequest(Username, ApiKey, Code, Name, PiShockOps.Shock, duration, intensity);
        PrintIfError(_client.PostAsJsonAsync(Url, request));
    }

    /// <param name="duration">In seconds</param>
    /// <param name="intensity">1-100</param>
    public void Vibrate(int duration, int intensity)
    {
        var request = new VibrateRequest(Username, ApiKey, Code, Name, PiShockOps.Vibrate, duration, intensity);
        PrintIfError(_client.PostAsJsonAsync(Url, request));
    }

    /// <param name="duration">In seconds</param>
    public void Beep(int duration)
    {
        var request = new BeepRequest(Username, ApiKey, Code, Name, PiShockOps.Beep, duration);
        PrintIfError(_client.PostAsJsonAsync(Url, request));
    }
}
