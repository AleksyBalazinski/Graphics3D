#define NOTUSED
using System.Numerics;

#if USED

namespace Graphics3D
{
    internal class ColorPicker
    {
        readonly float detT;
        readonly Face face;
        readonly RGB lightColor;
        readonly Vector3 lightDirection;
        readonly float ks;
        readonly float kd;
        readonly float m;

        public ColorPicker(Face face, RGB lightColor, float ks, float kd, float m, Point3D lightDirection)
        {
            this.face = face;
            this.lightColor = lightColor;
            this.ks = ks;
            this.kd = kd;
            this.m = m;
            this.lightDirection = lightDirection;

            detT = Utility.GetDetT(face.Vertices[0].Location, face.Vertices[1].Location, face.Vertices[2].Location);
        }

        private RGB GetBaseColorAtPoint(int x, int y, Bitmap texture)
        {
            Point3D a = face.Vertices[0].Location;
            Point3D b = face.Vertices[1].Location;
            Point3D c = face.Vertices[2].Location;
            (float wa, float wb, float wc) = GetWeights(x, y, a.X, a.Y, b.X, b.Y, c.X, c.Y);

            Point2D aTextureVec = face.Vertices[0].UVCoodinate;
            Point2D bTextureVec = face.Vertices[1].UVCoodinate;
            Point2D cTextureVec = face.Vertices[2].UVCoodinate;

            Point2D interpolatedUV = wa * aTextureVec + wb * bTextureVec + wc * cTextureVec;

            int px = (int)(interpolatedUV.X * 2000) % texture.Width;
            int py = (int)(interpolatedUV.Y * 2000) % texture.Height;

            Color color = texture.GetPixel(px, py);
            return new RGB(color.R / 255f, color.G / 255f, color.B / 255f);
        }

        private Point3D InterpolateNormal(int x, int y)
        {
            Point3D a = face.Vertices[0].Location;
            Point3D b = face.Vertices[1].Location;
            Point3D c = face.Vertices[2].Location;
            Point3D na = face.Vertices[0].NormalVector;
            Point3D nb = face.Vertices[1].NormalVector;
            Point3D nc = face.Vertices[2].NormalVector;

            (float wa, float wb, float wc) = GetWeights(x, y, a.X, a.Y, b.X, b.Y, c.X, c.Y);

            Point3D interpolatedNormal = wa * na + wb * nb + wc * nc;

            return interpolatedNormal;
        }

        private RGB InterpolateColor(int x, int y, RGB col1, RGB col2, RGB col3)
        {
            Point3D a = face.Vertices[0].Location;
            Point3D b = face.Vertices[1].Location;
            Point3D c = face.Vertices[2].Location;
            (float wa, float wb, float wc) = GetWeights(x, y, a.X, a.Y, b.X, b.Y, c.X, c.Y);

            RGB color = wa * col1 + wb * col2 + wc * col3;

            return color;
        }

        // color in point (x, y) for uniformly colored object
        public (int, int, int) GetColor(int x, int y, InterpolantType interpolant, RGB objectColor)
        {
            RGB color;
            if (interpolant == InterpolantType.Color)
            {
                var (c1, c2, c3) = ApplyLightingToVertexColors(objectColor);
                color = InterpolateColor(x, y, c1, c2, c3);
            }
            else // interpolating normal vectors
            {
                Point3D interpolatedNormal = InterpolateNormal(x, y);
                color = ApplyLighting(objectColor, x, y, interpolatedNormal, null);
            }

            return ToStandardRGB(color);
        }

        // color in point (x, y) for an object whose color is given by a texture; normal mapping is optional
        public (int, int, int) GetColor(int x, int y, InterpolantType interpolant, Bitmap texture, Bitmap? normalMap)
        {
            RGB color;
            if (interpolant == InterpolantType.NormalVector)
            {
                RGB baseColor = GetBaseColorAtPoint(x, y, texture);

                color = ApplyLighting(baseColor, x, y, InterpolateNormal(x, y), normalMap);
            }
            else
            {
                var (c1, c2, c3) = ApplyLightingToVertexColors(texture);
                color = InterpolateColor(x, y, c1, c2, c3);
            }

            return ToStandardRGB(color);
        }

        // color in point (x, y) for uniformly colored object with normal mapping applied
        public (int, int, int) GetColor(int x, int y, RGB objectColor, Bitmap normalMap)
        {
            RGB color = ApplyLighting(objectColor, x, y, InterpolateNormal(x, y), normalMap);

            return ToStandardRGB(color);
        }

        private (RGB, RGB, RGB) ApplyLightingToVertexColors(RGB objectColor)
        {
            Vertex v1 = face.Vertices[0];
            Vertex v2 = face.Vertices[1];
            Vertex v3 = face.Vertices[2];

            RGB c1 = ApplyLighting(objectColor, (int)v1.Location.X, (int)v1.Location.Y, v1.NormalVector, null);
            RGB c2 = ApplyLighting(objectColor, (int)v2.Location.X, (int)v2.Location.Y, v2.NormalVector, null);
            RGB c3 = ApplyLighting(objectColor, (int)v3.Location.X, (int)v3.Location.Y, v3.NormalVector, null);

            return (c1, c2, c3);
        }

        private (RGB, RGB, RGB) ApplyLightingToVertexColors(Bitmap texture)
        {
            Vertex v1 = face.Vertices[0];
            Vertex v2 = face.Vertices[1];
            Vertex v3 = face.Vertices[2];

            RGB c1 = ApplyLighting(GetBaseColorAtPoint((int)v1.Location.X, (int)v1.Location.Y, texture),
                (int)v1.Location.X, (int)v1.Location.Y, v1.NormalVector, null); // TODO add normal map here
            RGB c2 = ApplyLighting(GetBaseColorAtPoint((int)v2.Location.X, (int)v2.Location.Y, texture),
                (int)v2.Location.X, (int)v2.Location.Y, v2.NormalVector, null);
            RGB c3 = ApplyLighting(GetBaseColorAtPoint((int)v3.Location.X, (int)v3.Location.Y, texture),
                (int)v3.Location.X, (int)v1.Location.Y, v3.NormalVector, null);

            return (c1, c2, c3);
        }

        private RGB ApplyLighting(RGB objectColorAtPoint, int x, int y, Point3D normal, Bitmap? normalMap)
        {
            Point3D vert = new(0, 0, 1);

            Point3D N;
            if (normalMap != null)
            {
                RGB textureNormal = GetBaseColorAtPoint(x, y, normalMap);
                N = Utility.BumpNormal(Utility.Normalize(normal), textureNormal);
            }
            else
            {
                N = Utility.Normalize(normal);
            }

            Point3D R = 2 * Utility.DotProduct(N, lightDirection) * N - lightDirection;

            float cos1 = Utility.Cos(N, lightDirection);
            if (cos1 < 0)
                cos1 = 0;

            float cos2 = Utility.Cos(vert, R);
            if (cos2 < 0)
                cos2 = 0;

            var c = lightColor * objectColorAtPoint;
            var coef1 = kd * c;
            var coef2 = ks * c;
            return cos1 * coef1 + MathF.Pow(cos2, m) * coef2;
        }

        private (float, float, float) GetWeights(float x, float y, float ax, float ay, float bx, float by, float cx, float cy)
        {
            float wa = ((by - cy) * (x - cx) + (cx - bx) * (y - cy)) / detT;
            float wb = ((cy - ay) * (x - cx) + (ax - cx) * (y - cy)) / detT;
            float wc = 1 - wa - wb;

            return (wa < 0 ? 0 : wa, wb < 0 ? 0 : wb, wc < 0 ? 0 : wc);
        }

        private static (int, int, int) ToStandardRGB(RGB color)
        {
            return (color.r > 1 ? 255 : (int)(color.r * 255),
            color.g > 1 ? 255 : (int)(color.g * 255),
            color.b > 1 ? 255 : (int)(color.b * 255));
        }
    }
}

#else

namespace Graphics3D
{
    class ColorPicker
    {
    }
}

#endif
