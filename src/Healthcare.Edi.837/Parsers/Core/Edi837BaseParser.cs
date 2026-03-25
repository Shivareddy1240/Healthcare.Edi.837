using Healthcare.Edi837.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Healthcare.Edi837.Parsers.Core
{
    public abstract class Edi837BaseParser
    {
        protected char ElementSeparator = '*';
        protected char ComponentSeparator = ':';
        protected char RepetitionSeparator = '^';
        protected char SegmentTerminator = '~';

        protected List<EdiSegment> Segments { get; } = new();

        /// <summary>
        /// Loads the raw EDI text and automatically detects the correct delimiters from ISA segment
        /// </summary>
        public virtual void Load(string ediText)
        {
            if (string.IsNullOrWhiteSpace(ediText) || ediText.Length < 106)
                throw new ArgumentException("Invalid EDI file - too short for ISA segment");

            // Extract ISA segment (first 106 characters)
            string isa = ediText.Substring(0, 106);

            ElementSeparator = isa[3];
            RepetitionSeparator = isa[82];
            ComponentSeparator = isa[103];
            SegmentTerminator = isa[105];

            // Split the entire EDI into segments
            var segmentParts = ediText.Split(SegmentTerminator,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            Segments.Clear();

            foreach (var part in segmentParts)
            {
                if (string.IsNullOrWhiteSpace(part)) continue;

                var elements = part.Split(ElementSeparator, StringSplitOptions.None);
                if (elements.Length > 0)
                {
                    string segmentId = elements[0].Trim();
                    string[] elementValues = elements.Skip(1).ToArray();

                    Segments.Add(new EdiSegment(segmentId, elementValues));
                }
            }
        }

        /// <summary>
        /// Main parsing method - Must be implemented by 837P and 837I readers.
        /// Returns claims one by one (streaming / yield return) for low memory usage.
        /// </summary>
        public abstract IEnumerable<Claim837Base> ParseClaims();

        /// <summary>
        /// Helper to safely peek at a segment by index
        /// </summary>
        protected EdiSegment? Peek(int index)
            => index < Segments.Count ? Segments[index] : null;

        /// <summary>
        /// Helper to get current segment and move forward (used in parsing loops)
        /// </summary>
        protected EdiSegment? GetCurrentAndMove(ref int index)
        {
            if (index >= Segments.Count) return null;
            return Segments[index++];
        }
    }
}