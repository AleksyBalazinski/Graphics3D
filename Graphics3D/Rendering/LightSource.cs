using Graphics3D.Utility;
using System.Numerics;

namespace Graphics3D.Rendering
{
    internal class LightSource
    {
        public LightSource(Type type, Vector3 lightDirection, RGB lightColor)
        {
            this.lightDirection = lightDirection;
            this.lightColor = lightColor;
            this.type = type;
        }

        public LightSource(Type type, Vector3 lightDirection, RGB lightColor, float e, float cutoff, Vector3 location)
        {
            this.lightDirection = lightDirection;
            this.lightColor = lightColor;
            this.type = type;
            this.e = e;
            this.cutoff = cutoff;
            this.location = location;
        }

        public Type type;
        public Vector3 lightDirection;
        public RGB lightColor;

        // spotlight characteristics
        public float e;
        public float cutoff;
        public Vector3 location;

        public enum Type
        {
            Point,
            Spotlight
        }
    }
}
