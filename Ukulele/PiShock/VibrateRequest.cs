namespace Ukulele.PiShock;

// Intensity: 1-100
public record VibrateRequest(string Username, string Apikey, string Code, string Name, PiShockOps Op, int Duration, int Intensity);