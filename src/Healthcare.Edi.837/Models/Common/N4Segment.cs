using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Edi837.Models.Common;

public class N4Segment
{
    public string CityName { get; set; } = "";
    public string StateOrProvinceCode { get; set; } = "";
    public string PostalCode { get; set; } = "";
    public string CountryCode { get; set; } = "US";
}
