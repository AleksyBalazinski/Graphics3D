using Graphics3D.Rendering;

namespace Graphics3D
{
    internal class Utils
    {
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

            return (wa < 0 ? 0 : wa, wb < 0 ? 0 : wb, wc < 0 ? 0 : wc);
        }
    }
}
