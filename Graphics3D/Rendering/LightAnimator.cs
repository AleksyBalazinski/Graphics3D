using System.Numerics;

namespace Graphics3D.Rendering
{
    internal class LightAnimator
    {
        Vector3 lightSource;
        readonly float sinAngle;
        readonly float cosAngle;
        bool increaseR = true;
        public float Z
        {
            set
            {
                lightSource = new Vector3(lightSource.X, lightSource.Y, value);
            }
        }
        public Vector3 LightSource
        {
            get { return lightSource; }
        }

        // angle in degrees
        public LightAnimator(Vector3 lightSource, float angle)
        {
            this.lightSource = lightSource;
            sinAngle = MathF.Sin(angle * MathF.PI / 180);
            cosAngle = MathF.Cos(angle * MathF.PI / 180);
        }

        public Vector3 MoveLightSource()
        {
            if (increaseR)
            {
                lightSource.X *= 1.01f;
                lightSource.Y *= 1.01f;
                if (lightSource.X * lightSource.X + lightSource.Y * lightSource.Y > 30)
                    increaseR = false;
            }
            else
            {
                lightSource.X /= 1.01f;
                lightSource.Y /= 1.01f;
                if (lightSource.X * lightSource.X + lightSource.Y * lightSource.Y < 5)
                    increaseR = true;
                if (lightSource.X * lightSource.X + lightSource.Y * lightSource.Y > 30)
                    increaseR = true;
            }

            RotateAboutZ(sinAngle, cosAngle);

            return lightSource;
        }

        private void RotateAboutZ(float sinAngle, float cosAngle)
        {
            lightSource.X = lightSource.X * cosAngle - lightSource.Y * sinAngle;
            lightSource.Y = lightSource.X * sinAngle + lightSource.Y * cosAngle;
        }
    }
}
