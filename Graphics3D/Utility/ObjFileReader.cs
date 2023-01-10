using Graphics3D.Model;
using System.Numerics;

namespace Graphics3D.Utility
{
    internal class ObjFileReader
    {
        public static List<Face> Read(string path)
        {
            List<Vector3> points = new();
            List<Vector3> normalVectors = new();
            List<Vector2> uvCoordinates = new();
            List<Face> faces = new();

            foreach (string line in File.ReadLines(path))
            {
                if (line.StartsWith('#') || line.StartsWith('o') || line.StartsWith('s') || line.Length == 0)
                    continue;
                if (line.StartsWith("vn"))
                {
                    normalVectors.Add(ParsePoint3D(line));
                    continue;
                }
                if (line.StartsWith("vt"))
                {
                    uvCoordinates.Add(ParsePoint2D(line));
                    continue;
                }
                if (line.StartsWith('v'))
                {
                    points.Add(ParsePoint3D(line));
                    continue;
                }
                if (line.StartsWith('f'))
                {
                    faces.Add(ParseFace(line, points, normalVectors));
                    continue;
                }
            }

            return faces;
        }

        private static Vector3 ParsePoint3D(string line)
        {
            string[] subs = line.Trim().Split(' ');
            float x = float.Parse(subs[1], System.Globalization.CultureInfo.InvariantCulture);
            float y = float.Parse(subs[2], System.Globalization.CultureInfo.InvariantCulture);
            float z = float.Parse(subs[3], System.Globalization.CultureInfo.InvariantCulture);
            return new Vector3(x, y, z);
        }

        private static Vector2 ParsePoint2D(string line)
        {
            string[] subs = line.Split(' ');
            float x = float.Parse(subs[1], System.Globalization.CultureInfo.InvariantCulture);
            float y = float.Parse(subs[2], System.Globalization.CultureInfo.InvariantCulture);
            return new Vector2(x, y);
        }

        private static Face ParseFace(string line, List<Vector3> points, List<Vector3> normalVectors)
        {
            Face face = new();
            string[] vStrings = line.Trim().Split(' ').Skip(1).ToArray();
            foreach (var vString in vStrings)
            {
                string[] subs = vString.Split("/");
                int vIndx = int.Parse(subs[0], System.Globalization.CultureInfo.InvariantCulture);
                int vnIndx = int.Parse(subs[2], System.Globalization.CultureInfo.InvariantCulture);
                Vertex vertex = new(points[vIndx - 1], normalVectors[vnIndx - 1]);
                face.AddVertex(vertex);
            }
            return face;
        }
    }
}
