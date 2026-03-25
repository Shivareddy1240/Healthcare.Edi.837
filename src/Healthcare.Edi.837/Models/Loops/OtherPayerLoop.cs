using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models.Loops
{
    public class OtherPayerLoop
    {
        public Nm1Segment PayerName { get; set; } = new();
        public RefSegment PayerId { get; set; } = new();
        public decimal PaidAmount { get; set; }
    }
}