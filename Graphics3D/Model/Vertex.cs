using System.Numerics;

namespace Graphics3D.Model
{
    internal class Vertex
    {
        public Vector3 Location { get; set; }
        public Vector3 NormalVector { get; set; }
        public Vertex(Vector3 location, Vector3 normalVector)
        {
            Location = location;
            NormalVector = normalVector;
        }

        public override string ToString()
        {
            return $"v = {Location}";
        }
    }
}
