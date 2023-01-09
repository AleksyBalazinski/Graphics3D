using Graphics3D.Model;
using System.Numerics;

namespace Graphics3D.Rendering
{
    internal class ColorPicker
    {
/*        public Vector3 lightDirection;
        public RGB lightColor;*/
        public InterpolantType interpolantType;
        public List<LightSource> lightSources;

        public ColorPicker()
        {
            /*lightDirection = new Vector3(0, 0, 1);
            lightColor = new RGB(1, 1, 1);*/
            interpolantType = InterpolantType.NormalVector;
            lightSources = new List<LightSource>();
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
            if(lightSources.Count== 0)
            {
                return new RGB(0, 0, 0);
            }
            Vector3 vert = new(0, 0, 1);
            Vector3 N = Vector3.Normalize(normal);

            //Vector3 R = 2 * Vector3.Dot(N, lightDirection) * N - lightDirection;
            var Rs = lightSources.Select(ls => 2 * Vector3.Dot(N, ls.lightDirection) * N - ls.lightDirection).ToList();

            /*float cos1 = Vector3.Dot(N, lightDirection);
            if (cos1 < 0)
                cos1 = 0;*/

            var cos1s = lightSources.Select(ls =>
            {
                float cos1 = Vector3.Dot(N, ls.lightDirection);
                return cos1 < 0 ? 0 : cos1;
            }).ToList();

            /*float cos2 = Vector3.Dot(vert, R);
            if (cos2 < 0)
                cos2 = 0;*/

            var cos2s = Rs.Select(R =>
            {
                float cos2 = Vector3.Dot(vert, R);
                return cos2 < 0 ? 0 : cos2;
            }).ToList();

            //var c = lightColor * shape.color;
            var cs = lightSources.Select(ls => ls.lightColor * shape.color).ToList();
            //var coef1 = shape.kd * c;
            //var coef2 = shape.ks * c;
            RGB I = new();
            for(int i = 0; i < lightSources.Count; i++)
            {
                I += (shape.kd * cos1s[i] + shape.ks * MathF.Pow(cos2s[i], shape.m)) * cs[i];
            }
            return Fog(depth) * I  + (1 - Fog(depth)) * new RGB(Color.White);
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
