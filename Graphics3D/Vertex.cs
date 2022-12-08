using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    internal class Vertex
    {
        Point3D location;
        public Point3D Location { get => location; }
        public Vertex(Point3D location)
        {
            this.location = location;
        }

        public override string ToString()
        {
            return $"v = {location}";
        }
    }
}
