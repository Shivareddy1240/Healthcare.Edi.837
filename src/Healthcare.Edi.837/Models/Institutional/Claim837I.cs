using Healthcare.Edi837.Models.Common;
using Healthcare.Edi837.Models.Loops;

namespace Healthcare.Edi837.Models.Institutional
{
    public class Claim837I : Claim837Base
    {
        // 837I uses SV2 instead of SV1
        public override List<ServiceLineBase> ServiceLines { get; set; } = new();

        // Institutional specific
        public string PatientControlNumber { get; set; } = "";
        public string AdmissionDate { get; set; } = "";
        public string DischargeDate { get; set; } = "";

        public RenderingProviderLoop? RenderingProvider { get; set; }
        public List<OtherPayerLoop> OtherPayers { get; set; } = new();
    }
}