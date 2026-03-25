using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Edi837.Models.Common;

public class Nm1Segment
{
    public string EntityIdentifierCode { get; set; } = "";   // 41=Submitter, 85=Billing Provider, IL=Insured, PR=Payer, etc.
    public string EntityTypeQualifier { get; set; } = "";    // 1=Person, 2=Non-Person Entity
    public string LastNameOrOrganizationName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string NamePrefix { get; set; } = "";
    public string NameSuffix { get; set; } = "";
    public string IdentificationCodeQualifier { get; set; } = ""; // XX=NPI, 24=EIN, MI=Member ID, etc.
    public string IdentificationCode { get; set; } = "";         // Actual NPI / Tax ID / Member ID
}
