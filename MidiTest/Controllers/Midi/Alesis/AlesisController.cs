using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Melanchall.DryWetMidi.Core;

namespace Ukulele.Controllers.Midi.Alesis;

public class AlesisController
{
    private static readonly ImmutableHashSet<AlesisDials> Dials = ImmutableHashSet.Create(Enum.GetValues<AlesisDials>());

    public int Duration { get; private set; } = 1;
    public int Intensity { get; private set; } = 50;
    public int MinimumWarning { get; private set; } = 1;
    public int MaximumWarning { get; private set; } = 5;

    public bool Read(MidiEvent ev)
    {
        if (!TryParseEvent(ev, out var alesisEvent))
        {
            return false;
        }

        switch (alesisEvent.Dial)
        {
            case AlesisDials.Duration:
                Duration = alesisEvent.Value.ToDuration();
                break;
            case AlesisDials.Intensity:
                Intensity = alesisEvent.Value.ToIntensity();
                break;
            case AlesisDials.MinimumWarning:
                MinimumWarning = alesisEvent.Value.ToMinimumWarning();
                break;
            case AlesisDials.MaximumWarning:
                MaximumWarning = alesisEvent.Value.ToMaximumWarning();
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
