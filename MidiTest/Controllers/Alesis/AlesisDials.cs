namespace Ukulele.Controllers.Alesis;

public enum AlesisDials
{
#if DEBUG
    Duration = 1,
    Intensity = 2,
    MinimumWarning = 4,
    MaximumWarning = 5
#else
    Duration = 20,
    Intensity = 21,
    MinimumWarning = 22,
    MaximumWarning = 23
#endif
}