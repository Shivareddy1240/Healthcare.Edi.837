namespace Healthcare.Edi837.Models.Common
{
    public class HiSegment
    {
        public string CodeListQualifierCode { get; set; } = "";   // BK=Principal Diagnosis, ABK=Admitting, etc.
        public string IndustryCode { get; set; } = "";            // Actual ICD-10 code
        public string DateTimePeriod { get; set; } = "";
        public string MonetaryAmount { get; set; } = "";
    }
}