namespace Healthcare.Edi837.Models.Common
{
    public class DtpSegment
    {
        public string DateTimeQualifier { get; set; } = "";   // 431=Onset, 050=Received, etc.
        public string DateTimePeriodFormat { get; set; } = "D8"; // D8=CCYYMMDD, RD8=Range, etc.
        public string DateTimePeriod { get; set; } = "";
    }
}