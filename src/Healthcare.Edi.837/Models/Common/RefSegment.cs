using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Edi837.Models.Common;

public class RefSegment
{
    public string ReferenceIdQualifier { get; set; } = ""; // EI=Employer ID, XX=NPI, SY=Social Security, etc.
    public string ReferenceId { get; set; } = "";
    public string Description { get; set; } = "";
}
