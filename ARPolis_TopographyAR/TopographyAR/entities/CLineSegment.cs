using UnityEngine;

namespace ARPolis.TopographyAR
{
    public class CLineSegment
    {
        public Vector2 StartOfLine { get; set; }

        public Vector2 EndOfLine { get; set; }

        public bool HasPathFreeMove { get; set; }

        public bool MoveOutOfPathWithLimits { get; set; }

        public CLineSegment() { }

        public CLineSegment(Vector2 s, Vector2 e)
        {
            StartOfLine = s;
            EndOfLine = e;
        }
    }
}