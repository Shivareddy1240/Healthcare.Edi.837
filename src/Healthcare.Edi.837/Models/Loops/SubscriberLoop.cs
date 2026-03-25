using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models.Loops
{
    public class SubscriberLoop
    {
        public string HierarchicalId { get; set; } = "";
        public Nm1Segment SubscriberName { get; set; } = new();
        public N3Segment Address { get; set; } = new();
        public N4Segment CityStateZip { get; set; } = new();
        public RefSegment MemberId { get; set; } = new();
    }
}