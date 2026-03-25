namespace Healthcare.Edi837.Models.Common
{
    public class StHeader
    {
        public string TransactionSetIdentifierCode { get; set; } = "837";
        public string TransactionSetControlNumber { get; set; } = "";
        public string ImplementationConventionReference { get; set; } = "005010X222A1"; // or X223A2 for Institutional
    }
}