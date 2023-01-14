using Graphics3D.Rendering;
using System.Numerics;

namespace Graphics3D.Utility
{
    internal class MathUtils
    {
        /// <summary>
        /// Interpolates a set of three values over triangle surface
        /// </summary>
        /// <typeparam name="T">Type of the value to be interpolated</typeparam>
        /// <param name="vertices">Vertices of the triangle</param>
        /// <param name="values">Values in the traingle's vertices</param>
        /// <param name="x">First coordinate of the point where the value is to be interpolated</param>
        /// <param name="y">Second coordinate of the point where the value is to be interpolated</param>
        /// <returns></returns>
        public static T Interpolate<T>(List<VertexInfo> vertices, List<T> values, float x, float y)
        {
            (float wa, float wb, float wc) = GetWeights(x, y,
                vertices[0].X, vertices[0].Y,
                vertices[1].X, vertices[1].Y,
                vertices[2].X, vertices[2].Y);

            T interpolatedValue = wa * (values[0] as dynamic) + wb * (values[1] as dynamic) + wc * (values[2] as dynamic);
            return interpolatedValue;
        }

        public static float GetDetT(float ax, float ay, float bx, float by, float cx, float cy)
        {
            return (by - cy) * (ax - cx) + (cx - bx) * (ay - cy);
        }

        public static float Slope(VertexInfo start, VertexInfo end)
        {
            return (end.Y - start.Y) / (end.X - start.X);
        }

        public static (float, float, float) GetWeights(float x, float y, float ax, float ay, float bx, float by, float cx, float cy)
        {
            float detT = GetDetT(ax, ay, bx, by, cx, cy);
            float wa = ((by - cy) * (x - cx) + (cx - bx) * (y - cy)) / detT;
            float wb = ((cy - ay) * (x - cx) + (ax - cx) * (y - cy)) / detT;
            float wc = 1 - wa - wb;

            if (wa > 1) wa = 1;
            if (wa < 0) wa = 0;

            if (wb < 0) wb = 0;
            if (wb > 1) wb = 1;

            if (wc < 0) wc = 0;
            if (wc > 1) wc = 1;

            return (wa, wb, wc);
        }

        public static Matrix4x4 RotateOnto(Vector3 a, Vector3 b)
        {
            if (Vector3.Dot(a, b) == 1)
                return Matrix4x4.Identity;

            Vector3 axis = Vector3.Normalize(Vector3.Cross(a, b));
            float angle = MathF.Acos(Vector3.Dot(a, b) / (a.Length() * b.Length()));

            Quaternion q = Quaternion.CreateFromAxisAngle(axis, angle);
            return Matrix4x4.CreateFromQuaternion(q);
        }
    }
}
