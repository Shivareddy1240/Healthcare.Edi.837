using Healthcare.Edi837.Models;
using Healthcare.Edi837.Models.Common;
using Healthcare.Edi837.Models.Loops;
using Healthcare.Edi837.Models.Professional;
using Healthcare.Edi837.Parsers.Core;
using System;

namespace Healthcare.Edi837.Parsers
{
    public class Edi837PReader : Edi837BaseParser
    {
        public override IEnumerable<Claim837Base> ParseClaims()
        {
            int index = 0;
            Claim837P? currentClaim = null;
            string currentLoop = "";

            // Skip to first ST
            while (index < Segments.Count && Segments[index].SegmentId != "ST")
                index++;

            while (index < Segments.Count)
            {
                var segment = Segments[index];

                switch (segment.SegmentId)
                {
                    case "ST":
                        if (currentClaim != null)
                            yield return currentClaim;

                        currentClaim = new Claim837P();
                        currentClaim.TransactionSetControlNumber = segment.Elements.Length > 0 ? segment.Elements[0] : "";
                        currentLoop = "Header";
                        index++;
                        break;

                    case "BHT":
                        if (currentClaim != null)
                            currentClaim.Claim.ClaimNumber = segment.Elements.Length > 1 ? segment.Elements[1] : "";
                        index++;
                        break;

                    case "HL":
                        currentLoop = ParseHlSegment(currentClaim, segment, ref index);
                        break;

                    case "NM1":
                        ParseNm1(currentClaim, segment, currentLoop, ref index);
                        break;

                    case "N3":
                        ParseN3(currentClaim, segment, currentLoop, ref index);
                        break;

                    case "N4":
                        ParseN4(currentClaim, segment, currentLoop, ref index);
                        break;

                    case "REF":
                        ParseRef(currentClaim, segment, currentLoop, ref index);
                        break;

                    case "CLM":
                        if (currentClaim != null)
                        {
                            currentClaim.Claim.TotalChargeAmount = segment.Elements.Length > 1
                                && decimal.TryParse(segment.Elements[1], out var amt) ? amt : 0m;
                            currentClaim.Claim.ClaimType = segment.Elements.Length > 2 ? segment.Elements[2] : "";
                        }
                        index++;
                        break;

                    case "LX": // Service Line Start
                        if (currentClaim != null)
                        {
                            var serviceLine = ParseServiceLine(ref index);
                            currentClaim.ServiceLines.Add(serviceLine);
                        }
                        break;

                    case "SE":
                        if (currentClaim != null)
                            yield return currentClaim;
                        currentClaim = null;
                        index++;
                        break;

                    case "GE":
                    case "IEA":
                        index++;
                        break;

                    default:
                        // Store unknown segments for user extension
                        if (currentClaim != null)
                        {
                            if (!currentClaim.CustomSegments.ContainsKey(segment.SegmentId))
                                currentClaim.CustomSegments[segment.SegmentId] = new();
                            currentClaim.CustomSegments[segment.SegmentId].Add(segment.Elements);
                        }
                        index++;
                        break;
                }
            }

            if (currentClaim != null)
                yield return currentClaim;
        }

        private string ParseHlSegment(Claim837P? claim, EdiSegment segment, ref int index)
        {
            if (claim == null)
            {
                index++;
                return "";
            }

            string levelCode = segment.Elements.Length > 3 ? segment.Elements[3] : "";

            switch (levelCode)
            {
                case "20": // Billing Provider Level
                    claim.BillingProvider.HierarchicalId = segment.Elements.Length > 0 ? segment.Elements[0] : "";
                    return "2000A";
                case "22": // Subscriber Level
                    claim.Subscriber.HierarchicalId = segment.Elements.Length > 0 ? segment.Elements[0] : "";
                    return "2000B";
                case "23": // Patient Level (when different from subscriber)
                    claim.Patient = new PatientLoop();
                    claim.Patient.HierarchicalId = segment.Elements.Length > 0 ? segment.Elements[0] : "";
                    return "2000C";
                default:
                    index++;
                    return "";
            }
        }

        private void ParseNm1(Claim837P? claim, EdiSegment segment, string currentLoop, ref int index)
        {
            if (claim == null) { index++; return; }

            var nm1 = new Nm1Segment
            {
                EntityIdentifierCode = segment.Elements.Length > 0 ? segment.Elements[0] : "",
                EntityTypeQualifier = segment.Elements.Length > 1 ? segment.Elements[1] : "",
                LastNameOrOrganizationName = segment.Elements.Length > 2 ? segment.Elements[2] : "",
                FirstName = segment.Elements.Length > 3 ? segment.Elements[3] : "",
                IdentificationCodeQualifier = segment.Elements.Length > 8 ? segment.Elements[8] : "",
                IdentificationCode = segment.Elements.Length > 9 ? segment.Elements[9] : ""
            };

            switch (currentLoop)
            {
                case "2000A":
                    claim.BillingProvider.ProviderName = nm1;
                    break;
                case "2000B":
                    claim.Subscriber.SubscriberName = nm1;
                    break;
                case "2000C":
                    if (claim.Patient != null)
                        claim.Patient.PatientName = nm1;
                    break;
            }
            index++;
        }

        private void ParseN3(Claim837P? claim, EdiSegment segment, string currentLoop, ref int index)
        {
            if (claim == null) { index++; return; }

            var n3 = new N3Segment
            {
                AddressLine1 = segment.Elements.Length > 0 ? segment.Elements[0] : "",
                AddressLine2 = segment.Elements.Length > 1 ? segment.Elements[1] : ""
            };

            if (currentLoop == "2000A")
                claim.BillingProvider.Address = n3;
            else if (currentLoop == "2000B")
                claim.Subscriber.Address = n3;
            else if (currentLoop == "2000C" && claim.Patient != null)
                claim.Patient.Address = n3;

            index++;
        }

        private void ParseN4(Claim837P? claim, EdiSegment segment, string currentLoop, ref int index)
        {
            if (claim == null) { index++; return; }

            var n4 = new N4Segment
            {
                CityName = segment.Elements.Length > 0 ? segment.Elements[0] : "",
                StateOrProvinceCode = segment.Elements.Length > 1 ? segment.Elements[1] : "",
                PostalCode = segment.Elements.Length > 2 ? segment.Elements[2] : ""
            };

            if (currentLoop == "2000A")
                claim.BillingProvider.CityStateZip = n4;
            else if (currentLoop == "2000B")
                claim.Subscriber.CityStateZip = n4;
            else if (currentLoop == "2000C" && claim.Patient != null)
                claim.Patient.CityStateZip = n4;

            index++;
        }

        private void ParseRef(Claim837P? claim, EdiSegment segment, string currentLoop, ref int index)
        {
            if (claim == null) { index++; return; }

            var refSeg = new RefSegment
            {
                ReferenceIdQualifier = segment.Elements.Length > 0 ? segment.Elements[0] : "",
                ReferenceId = segment.Elements.Length > 1 ? segment.Elements[1] : ""
            };

            if (currentLoop == "2000A")
                claim.BillingProvider.TaxIdentification = refSeg;

            index++;
        }

        private ServiceLine837P ParseServiceLine(ref int index)
        {
            var line = new ServiceLine837P();

            while (index < Segments.Count)
            {
                var seg = Segments[index];
                if (seg.SegmentId == "LX" || seg.SegmentId == "SE")
                    break;

                if (seg.SegmentId == "SV1")
                {
                    line.ProcedureCode = seg.Elements.Length > 1 ? seg.Elements[1] : "";
                    line.ChargedAmount = seg.Elements.Length > 2 && decimal.TryParse(seg.Elements[2], out var amt) ? amt : 0;
                }

                index++;
            }

            return line;
        }
    }
}