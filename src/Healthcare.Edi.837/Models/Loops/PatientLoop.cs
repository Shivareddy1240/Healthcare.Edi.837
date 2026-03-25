using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models.Loops
{
    public class PatientLoop
    {
        public string HierarchicalId { get; set; } = "";
        public Nm1Segment PatientName { get; set; } = new();
        public N3Segment Address { get; set; } = new();
        public N4Segment CityStateZip { get; set; } = new();
        public string Gender { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
    }
}