using Melanchall.DryWetMidi.Common;

namespace Ukulele.Controllers.Midi;

public static class MidiExtensions
{
    private const int MinimumDuration = 1;
    private const int MaximumDuration = 15;
    private const int MinimumWarning = 2;
    private const int MaximumWarning = 10;
    private const int MinimumIntensity = 0;
    private const int MaximumIntensity = 100;
    
    public static int ToDuration(this SevenBitNumber number)
    {
        return MathExtensions.MapSevenBitNumberTo(number, MinimumDuration, MaximumDuration);
    }

    public static int ToMinimumWarning(this SevenBitNumber number)
    {
        return MathExtensions.MapSevenBitNumberTo(number, MinimumWarning, MaximumWarning);
    }

    public static int ToMaximumWarning(this SevenBitNumber number)
    {
        return MathExtensions.MapSevenBitNumberTo(number, MinimumWarning, MaximumWarning);
    }

    public static int ToIntensity(this SevenBitNumber number)
    {
        return MathExtensions.MapSevenBitNumberTo(number, MinimumIntensity, MaximumIntensity);
    }

    public static string ToDurationMeter(this int number)
    {
        return MathExtensions.MapToMeter(number, "s", MinimumDuration, MaximumDuration);
    }

    public static string ToMinimumWarningMeter(this int number)
    {
        return MathExtensions.MapToMeter(number, "s", MinimumWarning, MaximumWarning);
    }

    public static string ToMaximumWarningMeter(this int number)
    {
        return MathExtensions.MapToMeter(number, "s", MinimumWarning, MaximumWarning);
    }

    public static string ToIntensityMeter(this int number)
    {
        return MathExtensions.MapToMeter(number, "%", MinimumIntensity, MaximumIntensity);
    }
}
