using Graphics3D.Model;
using System.Diagnostics;
using System.Numerics;

namespace Graphics3D.Rendering
{
    internal class ColorPicker
    {
        public Vector3 lightDirection;
        public RGB lightColor;
        public InterpolantType interpolantType;

        public ColorPicker()
        {
            lightDirection = new Vector3(0, 0, 1);
            lightColor = new RGB(1, 1, 1);
            interpolantType = InterpolantType.NormalVector;
        }

        // color in point (x, y) for uniformly colored object
        public (int, int, int) GetColor(int x, int y, List<VertexInfo> vertices, Shape shape, float depth)
        {
            RGB color;
            if (interpolantType == InterpolantType.Color)
            {
                var colors = vertices.Select(v => ApplyLighting(shape, v.normal, depth)).ToList();
                color = Utils.Interpolate(vertices, colors, x, y); // interpolate color
            }
            else if (interpolantType == InterpolantType.NormalVector)
            {
                Vector3 interpolatedNormal
                    = Utils.Interpolate(vertices, vertices.Select(v => v.normal).ToList(), x, y); // interpolate normal
                color = ApplyLighting(shape, interpolatedNormal, depth);
            }
            else if (interpolantType == InterpolantType.Constant)
            {
                color = ApplyLighting(shape, vertices[0].normal, depth);
            }
            else
            {
                throw new NotSupportedException("Unknown interpolant type");
            }

            return color.ToRGB255();
        }

        private RGB ApplyLighting(Shape shape, Vector3 normal, float depth)
        {
            Vector3 vert = new(0, 0, 1);
            Vector3 N = Vector3.Normalize(normal);

            Vector3 R = 2 * Vector3.Dot(N, lightDirection) * N - lightDirection;

            float cos1 = Vector3.Dot(N, lightDirection);
            if (cos1 < 0)
                cos1 = 0;

            float cos2 = Vector3.Dot(vert, R);
            if (cos2 < 0)
                cos2 = 0;

            var c = lightColor * shape.color;
            var coef1 = shape.kd * c;
            var coef2 = shape.ks * c;
            return Fog(depth) * (cos1 * coef1 + MathF.Pow(cos2, shape.m) * coef2) + (1 - Fog(depth)) * new RGB(Color.White);
        }

        private static float Fog(float depth)
        {
            if (depth > 30)
                return 0;
            if (depth < 0.1)
                return 1;
            return (30 - depth) / (30 - 0.1f);
            
        }
    }
}
