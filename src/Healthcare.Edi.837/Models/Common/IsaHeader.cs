namespace Healthcare.Edi837.Models.Common;

public class IsaHeader
{
    public string AuthorizationInfoQualifier { get; set; } = "00";
    public string AuthorizationInfo { get; set; } = "";
    public string SecurityInfoQualifier { get; set; } = "00";
    public string SecurityInfo { get; set; } = "";
    public string InterchangeIdQualifierSender { get; set; } = "";
    public string InterchangeSenderId { get; set; } = "";
    public string InterchangeIdQualifierReceiver { get; set; } = "";
    public string InterchangeReceiverId { get; set; } = "";
    public DateTime InterchangeDateTime { get; set; }
    public char RepetitionSeparator { get; set; } = '^';
    public string Version { get; set; } = "00501";
    public string InterchangeControlNumber { get; set; } = "";
    public bool AcknowledgmentRequested { get; set; }
    public string UsageIndicator { get; set; } = "P"; // P=Production, T=Test
    public char ComponentElementSeparator { get; set; } = ':';
}