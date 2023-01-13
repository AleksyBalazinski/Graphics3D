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
        /// <summary>
        /// Exponent; specifies how intensity changes depending on radial distance from the center of the casted beam of light  
        /// </summary>
        public float E { get; set; }

        /// <summary>
        /// Specifies maximal angle of the light cone
        /// </summary>
        public float Cutoff { get; set; }

        /// <summary>
        /// Location of the source
        /// </summary>
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

        /// <summary>
        /// Type of the light source
        /// </summary>
        public enum LightSourceType
        {
            /// <summary>
            /// Type of light source emitting light from a single point in all directions
            /// </summary>
            Point,

            /// <summary>
            /// Type of light source producing a directed cone of light
            /// </summary>
            Spotlight
        }
    }
}
