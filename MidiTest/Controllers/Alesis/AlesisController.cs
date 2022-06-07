using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Ukulele.Controllers.Alesis;

public class AlesisController
{
    private static readonly ImmutableHashSet<AlesisDials> Dials = ImmutableHashSet.Create(Enum.GetValues<AlesisDials>());

    public SevenBitNumber Duration { get; private set; } = new(1);
    public SevenBitNumber Intensity { get; private set; } = new(50);
    public SevenBitNumber MinimumWarning { get; private set; } = new(1);
    public SevenBitNumber MaximumWarning { get; private set; } = new(5);

    public bool Read(MidiEvent ev)
    {
        if (!TryParseEvent(ev, out var alesisEvent))
        {
            return false;
        }

        switch (alesisEvent.Dial)
        {
            case AlesisDials.Duration:
                Duration = alesisEvent.Value;
                break;
            case AlesisDials.Intensity:
                Intensity = alesisEvent.Value;
                break;
            case AlesisDials.MinimumWarning:
                MinimumWarning = alesisEvent.Value;
                break;
            case AlesisDials.MaximumWarning:
                MaximumWarning = alesisEvent.Value;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return true;
    }

    public override string ToString()
    {
        return $"Duration: {Duration}, Intensity: {Intensity}, Minimum warning: {MinimumWarning}, Maximum warning: {MaximumWarning}";
    }

    public static AlesisEvent? ParseEvent(MidiEvent ev)
    {
        if (ev is not ControlChangeEvent control)
        {
            return null;
        }

        var dialNumber = (AlesisDials) Convert.ToInt32(control.ControlNumber);
        if (!Dials.Contains(dialNumber))
        {
            return null;
        }

        return new AlesisEvent(dialNumber, control.ControlValue);
    }

    public static bool TryParseEvent(MidiEvent ev, [NotNullWhen(true)] out AlesisEvent? alesisEvent)
    {
        return (alesisEvent = ParseEvent(ev)) != null;
    }
}
