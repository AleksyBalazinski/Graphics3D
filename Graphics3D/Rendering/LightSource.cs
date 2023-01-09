using System.Numerics;

namespace Graphics3D.Rendering
{
    internal class LightSource
    {
        public LightSource(Type type, Vector3 lightDirection, RGB lightColor) 
        { 
            this.lightDirection= lightDirection;
            this.lightColor= lightColor;
            this.type= type;
        }

        public LightSource(Type type, Vector3 lightDirection, RGB lightColor, float e)
        {
            this.lightDirection = lightDirection;
            this.lightColor = lightColor;
            this.type = type;
            this.e= e;
        }

        public Vector3 lightDirection;
        public RGB lightColor;
        public float e;
        public Type type;

        public enum Type
        {
            Point,
            Spotlight
        }
    }
}
