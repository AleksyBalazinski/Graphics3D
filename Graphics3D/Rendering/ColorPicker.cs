using Graphics3D.Model;
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
        public (int, int, int) GetColor(int x, int y, List<VertexInfo> vertices, Shape shape)
        {
            RGB color;
            if (interpolantType == InterpolantType.Color)
            {
                var colors = vertices.Select(v => ApplyLighting(shape, v.normal)).ToList();
                color = Utils.Interpolate(vertices, colors, x, y); // interpolate color
            }
            else if (interpolantType == InterpolantType.NormalVector)
            {
                Vector3 interpolatedNormal
                    = Utils.Interpolate(vertices, vertices.Select(v => v.normal).ToList(), x, y); // interpolate normal
                color = ApplyLighting(shape, interpolatedNormal);
            }
            else if (interpolantType == InterpolantType.Constant)
            {
                color = shape.color;
            }
            else
            {
                throw new NotSupportedException("Unknown interpolant type");
            }

            return color.ToRGB255();
        }

        private RGB ApplyLighting(Shape shape, Vector3 normal)
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
            return cos1 * coef1 + MathF.Pow(cos2, shape.m) * coef2;
        }
    }
}
