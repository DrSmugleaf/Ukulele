using Melanchall.DryWetMidi.Common;

namespace Ukulele;

public static class MathExtensions
{
    public static int MapTo(int value, int low1, int high1, int low2, int high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
    
    public static int MapSevenBitNumberTo(SevenBitNumber value, int low2, int high2)
    {
        return MapTo(value, SevenBitNumber.MinValue, SevenBitNumber.MaxValue, low2, high2);
    }

    public static string MapToMeter(int value, string suffix, int low, int high, int maxSquares = 10)
    {
        var squares = MapTo(value, low, high, 0, maxSquares);
        var spaces = maxSquares - squares;
        return $"[{new string('#', squares)}{new string(' ', spaces)}] ({value}{suffix})";
    }
}