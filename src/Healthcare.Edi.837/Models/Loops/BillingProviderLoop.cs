using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models.Loops
{
    public class BillingProviderLoop
    {
        public string HierarchicalId { get; set; } = "";
        public Nm1Segment ProviderName { get; set; } = new();
        public N3Segment Address { get; set; } = new();
        public N4Segment CityStateZip { get; set; } = new();
        public RefSegment TaxIdentification { get; set; } = new();
        public RefSegment Npi { get; set; } = new();
    }
}