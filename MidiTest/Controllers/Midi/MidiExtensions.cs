using Melanchall.DryWetMidi.Common;

namespace Ukulele.Controllers.Midi;

public static class MidiExtensions
{
    public static int ToDuration(this SevenBitNumber number)
    {
        return MathExtensions.MapSevenBitNumberTo(number, 1, 15);
    }

    public static int ToMinimumWarning(this SevenBitNumber number)
    {
        return MathExtensions.MapSevenBitNumberTo(number, 2, 10);
    }

    public static int ToMaximumWarning(this SevenBitNumber number)
    {
        return MathExtensions.MapSevenBitNumberTo(number, 2, 10);
    }

    public static int ToIntensity(this SevenBitNumber number)
    {
        return MathExtensions.MapSevenBitNumberTo(number, 0, 100);
    }
}
