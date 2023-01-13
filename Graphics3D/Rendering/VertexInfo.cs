using System.Numerics;

namespace Graphics3D.Rendering
{
    /// <summary>
    /// Internal structure used for passing information between rendering stages
    /// </summary>
    internal struct VertexInfo
    {
        public float X;
        public float Y;
        public float depth;
        public Vector3 normal; // normal vector in world coordinates
        public Vector4 worldSpaceLocation;
        public VertexInfo(float X, float Y, float depth, Vector3 normal, Vector4 worldSpaceLocation)
        {
            this.X = X;
            this.Y = Y;
            this.depth = depth;
            this.normal = normal;
            this.worldSpaceLocation = worldSpaceLocation;
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
