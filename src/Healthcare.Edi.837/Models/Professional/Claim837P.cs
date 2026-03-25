using Healthcare.Edi837.Models.Common;
using Healthcare.Edi837.Models.Loops;

namespace Healthcare.Edi837.Models.Professional
{
    public class Claim837P : Claim837Base
    {
        // 837P specific service lines (SV1 segment)
        public override List<ServiceLineBase> ServiceLines { get; set; } = new();

        // 837P often has more detailed provider loops
        public RenderingProviderLoop? RenderingProvider { get; set; }
        public ReferringProviderLoop? ReferringProvider { get; set; }
        public ServiceFacilityLoop? ServiceFacility { get; set; }

        // COB - Other Payer information
        public List<OtherPayerLoop> OtherPayers { get; set; } = new();
    }
}