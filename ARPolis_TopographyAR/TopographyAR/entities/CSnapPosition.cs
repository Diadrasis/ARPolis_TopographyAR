using UnityEngine;

namespace ARPolis.TopographyAR
{
    public class CSnapPosition
    {
        public Vector2 Position { get; set; }
        public float SqrDistance { get; set; }
        public bool LimitsOn { get; set; }
    }
}