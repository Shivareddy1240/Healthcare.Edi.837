using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models
{
    public abstract class ServiceLineBase
    {
        public string LineNumber { get; set; } = "";
        public string ProcedureCode { get; set; } = "";
        public string Modifier1 { get; set; } = "";
        public string Modifier2 { get; set; } = "";
        public string Modifier3 { get; set; } = "";
        public string Modifier4 { get; set; } = "";
        public decimal ChargedAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string Units { get; set; } = "";
        public List<DtpSegment> Dates { get; set; } = new();
        public List<RefSegment> References { get; set; } = new();
    }
}