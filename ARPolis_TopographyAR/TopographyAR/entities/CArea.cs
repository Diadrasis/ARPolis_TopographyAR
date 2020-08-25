using System.Collections.Generic;
using UnityEngine;

namespace ARPolis.TopographyAR
{
    public class CArea
    {
        public List<CLineSegment> PerimeterLines { get; set; }
        //dead areas inside this area
        public List<CLineSegment> DeadLines { get; set; }
        public string AreaName { get; set; }
        public Vector2 CenterOfArea { get; set; }
        public float Radius { get; set; }
        public List<Vector3> Points { get; set; }
    }
}