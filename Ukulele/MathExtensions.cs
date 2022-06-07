using Melanchall.DryWetMidi.Common;

namespace Ukulele;

public static class MathExtensions
{
    public static int MapSevenBitNumberTo(int value, int low2, int high2)
    {
        var low1 = SevenBitNumber.MinValue;
        var high1 = SevenBitNumber.MaxValue;
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
}