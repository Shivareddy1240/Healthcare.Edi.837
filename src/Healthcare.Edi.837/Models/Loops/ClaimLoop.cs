namespace Healthcare.Edi837.Models.Loops
{
    public class ClaimLoop
    {
        public string HierarchicalId { get; set; } = "";
        public string ClaimNumber { get; set; } = "";
        public decimal TotalChargeAmount { get; set; }
        public string ClaimType { get; set; } = "";           // 1=Hospital, 2=Professional etc.
        public string PlaceOfService { get; set; } = "";
    }
}