using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Edi837.Models.Common;
public class GsHeader
{
    public string FunctionalIdentifierCode { get; set; } = "HC"; // HC = Health Care Claim
    public string ApplicationSenderCode { get; set; } = "";
    public string ApplicationReceiverCode { get; set; } = "";
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string GroupControlNumber { get; set; } = "";
    public string Version { get; set; } = "005010X222A1"; // or X223A2 for I
}
