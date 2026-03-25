using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models.Loops
{
    public class ReferringProviderLoop
    {
        public Nm1Segment ProviderName { get; set; } = new();
        public RefSegment Npi { get; set; } = new();
    }
}