using Graphics3D.Utility;
using System.Numerics;

namespace Graphics3D.Rendering
{
    internal class LightSource
    {
        public LightSourceType Type { get; set; }
        public Vector3 LightDirection { get; set; }
        public RGB LightColor { get; set; }

        // spotlight characteristics
        public float E { get; set; }
        public float Cutoff { get; set; }
        public Vector3 Location { get; set; }
        public LightSource(LightSourceType type, Vector3 lightDirection, RGB lightColor)
        {
            LightDirection = lightDirection;
            LightColor = lightColor;
            Type = type;
        }

        public LightSource(LightSourceType type, Vector3 lightDirection, RGB lightColor, float e, float cutoff, Vector3 location)
        {
            LightDirection = lightDirection;
            LightColor = lightColor;
            Type = type;
            E = e;
            Cutoff = cutoff;
            Location = location;
        }

        public enum LightSourceType
        {
            Point,
            Spotlight
        }
    }
}
