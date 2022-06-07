namespace Ukulele.PiShock;

public record BeepRequest(string Username, string Apikey, string Code, string Name, PiShockOps Op, int Duration);