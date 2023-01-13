using Graphics3D.Model;
using Graphics3D.Utility;
using System.Numerics;

namespace Graphics3D.Rendering
{
    internal class ColorPicker
    {
        public InterpolantType Interpolant { get; set; }
        public RGB Ambient { get; set; }
        public List<LightSource> LightSources { get; set; }
        public RGB FogColor { get; set; }

        public ColorPicker()
        {
            Interpolant = InterpolantType.NormalVector;
            Ambient = new RGB(0.05f, 0.05f, 0.05f);
            LightSources = new List<LightSource>();
            FogColor = new RGB(1, 1, 1);
        }

        /// <summary>
        /// Calculates color of a pixel (x, y) for uniformly colored object
        /// </summary>
        /// <param name="x">First coordinate of the pixel</param>
        /// <param name="y">Second coordinate of the pixel</param>
        /// <param name="vertices">Vertices of the face this pixel belongs to</param>
        /// <param name="shape">Shape this pixel belongs to</param>
        /// <param name="depth">Depth of the pixel from viewer's perspective</param>
        /// <param name="worldSpaceLocation">Coordinates of a point that maps to this pixel</param>
        /// <returns>Color of this pixel as an RGB triple with each color in range 0..255</returns>
        /// <exception cref="NotSupportedException">Thrown when ColorPicker is set to use an unknown interpolation method</exception>
        public (int, int, int) GetColor(int x, int y, List<VertexInfo> vertices, Shape shape, float depth, Vector4 worldSpaceLocation)
        {
            RGB color;
            if (Interpolant == InterpolantType.Color)
            {
                var colors = vertices.Select(v => ApplyLighting(shape, v.normal, v.depth, v.worldSpaceLocation)).ToList();
                color = MathUtils.Interpolate(vertices, colors, x, y);
            }
            else if (Interpolant == InterpolantType.NormalVector)
            {
                Vector3 interpolatedNormal
                    = MathUtils.Interpolate(vertices, vertices.Select(v => v.normal).ToList(), x, y);
                color = ApplyLighting(shape, interpolatedNormal, depth, worldSpaceLocation);
            }
            else if (Interpolant == InterpolantType.Constant)
            {
                color = ApplyLighting(shape, vertices[0].normal, depth, worldSpaceLocation);
            }
            else
            {
                throw new NotSupportedException("Unknown interpolant type");
            }

            return color.ToRGB255();
        }

        private RGB ApplyLighting(Shape shape, Vector3 normal, float depth, Vector4 worldSpaceLocation)
        {
            if (LightSources.Count == 0)
            {
                return shape.Ka * Ambient;
            }

            Vector3 vert = new(0, 0, 1);
            Vector3 N = Vector3.Normalize(normal);

            var Rs = LightSources.Select(
                ls => 2 * Vector3.Dot(N, ls.LightDirection) * N - ls.LightDirection).ToList();

            var cos1s = LightSources.Select(ls =>
            {
                float cos1 = Vector3.Dot(N, ls.LightDirection);
                return cos1 < 0 ? 0 : cos1;
            }).ToList();

            var cos2s = Rs.Select(R =>
            {
                float cos2 = Vector3.Dot(vert, R);
                return cos2 < 0 ? 0 : cos2;
            }).ToList();

            var cs = LightSources.Select(ls => ls.LightColor * shape.Color).ToList();
            RGB I = new();

            for (int i = 0; i < LightSources.Count; i++)
            {
                if (LightSources[i].Type == LightSource.LightSourceType.Spotlight)
                {
                    var spotlight = LightSources[i];
                    Vector3 P = new(worldSpaceLocation.X, worldSpaceLocation.Y, worldSpaceLocation.Z);
                    float cosTheta = Vector3.Dot(Vector3.Normalize(spotlight.Location - P), spotlight.LightDirection);
                    if (cosTheta > spotlight.Cutoff)
                        cs[i] = MathF.Pow(cosTheta, spotlight.E) * cs[i];
                    else
                        cs[i] = new RGB(0, 0, 0);
                }

                I += shape.Ka * Ambient
                    + (shape.Kd * cos1s[i] + shape.Ks * MathF.Pow(cos2s[i], shape.M)) * cs[i];
            }

            return Fog(depth) * I + (1 - Fog(depth)) * FogColor;
        }

        private static float Fog(float depth)
        {
            float max = 30;
            float min = 0.1f;
            if (depth > max)
                return 0;
            if (depth < min)
                return 1;

            return (max - depth) / (max - min);
        }
    }
}
