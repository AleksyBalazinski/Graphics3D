using System.Numerics;

namespace Graphics3D
{
    internal class Vertex
    {
        public Vector3 Location { get; set; }
        public Vertex(Vector3 location)
        {
            Location = location;
        }

        public override string ToString()
        {
            return $"v = {Location}";
        }
    }
}
