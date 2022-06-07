using Melanchall.DryWetMidi.Common;

namespace Ukulele.Controllers.Midi.Alesis;

public record AlesisEvent(AlesisDials Dial, SevenBitNumber Value);