using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPolis.TopographyAR
{
    public class CDeadSpot
    {
        public List<CLineSegment> DeadPerimetros { get; set; }

        public string DeadAreaName { get; set; }

        public Vector2 Center { get; set; }

        public float Radius { get; set; }

        public List<Vector3> Points { get; set; }
    }
}