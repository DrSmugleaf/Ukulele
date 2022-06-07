namespace Ukulele.PiShock;

// Duration: 1-15
// Intensity: 1-100
public record ShockRequest(string Username, string Apikey, string Code, string Name, PiShockOps Op, int Duration, int Intensity);