using Healthcare.Edi837.Models.Common;
using Healthcare.Edi837.Models.Loops;

namespace Healthcare.Edi837.Models
{
    public abstract class Claim837Base
    {
        public IsaHeader Isa { get; set; } = new();
        public GsHeader Gs { get; set; } = new();
        public StHeader St { get; set; } = new();

        public string InterchangeControlNumber { get; set; } = "";
        public string GroupControlNumber { get; set; } = "";
        public string TransactionSetControlNumber { get; set; } = "";

        // Hierarchical Loops
        public BillingProviderLoop BillingProvider { get; set; } = new();
        public SubscriberLoop Subscriber { get; set; } = new();
        public PatientLoop? Patient { get; set; }           // Only present when Patient is different from Subscriber

        public ClaimLoop Claim { get; set; } = new();

        public abstract List<ServiceLineBase> ServiceLines { get; set; }

        // Extensibility
        public Dictionary<string, List<string[]>> CustomSegments { get; set; } = new();
    }
}