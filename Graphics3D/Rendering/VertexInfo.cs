using System.Numerics;

namespace Graphics3D.Rendering
{
    internal struct VertexInfo
    {
        public float X;
        public float Y;
        public float depth;
        public Vector3 normal; // normal vector in world coordinates
        public VertexInfo(float X, float Y, float depth, Vector3 normal)
        {
            this.X = X;
            this.Y = Y;
            this.depth = depth;
            this.normal = normal;
        }

        public static bool operator ==(VertexInfo vi1, VertexInfo vi2)
        {
            return vi1.X == vi2.X && vi1.Y == vi2.Y;
        }

        public static bool operator !=(VertexInfo vi1, VertexInfo vi2)
        {
            return !(vi1 == vi2);
        }
    }
}
