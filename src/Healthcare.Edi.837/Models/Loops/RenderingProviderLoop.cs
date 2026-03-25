using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models.Loops
{
    public class RenderingProviderLoop
    {
        public Nm1Segment ProviderName { get; set; } = new();
        public RefSegment Npi { get; set; } = new();
        public RefSegment TaxonomyCode { get; set; } = new();
    }
}