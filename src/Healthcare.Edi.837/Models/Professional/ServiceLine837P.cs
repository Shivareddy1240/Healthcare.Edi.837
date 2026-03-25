using Healthcare.Edi837.Models.Common;

namespace Healthcare.Edi837.Models.Professional
{
    public class ServiceLine837P : ServiceLineBase
    {
        public string DiagnosisPointer { get; set; } = "";
        public string UnitOrBasisForMeasurementCode { get; set; } = "UN";
        public List<HiSegment> DiagnosisCodes { get; set; } = new();
    }
}