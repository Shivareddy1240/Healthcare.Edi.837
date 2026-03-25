using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models.Loops
{
    public class ServiceFacilityLoop
    {
        public Nm1Segment FacilityName { get; set; } = new();
        public N3Segment Address { get; set; } = new();
        public N4Segment CityStateZip { get; set; } = new();
    }
}