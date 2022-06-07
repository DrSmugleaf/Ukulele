using Melanchall.DryWetMidi.Common;

namespace Ukulele.Controllers.Alesis;

public record AlesisEvent(AlesisDials Dial, SevenBitNumber Value);