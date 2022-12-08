using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    internal class Face
    {
        public List<Vertex> Vertices { get; set; }
        public Face()
        {
            Vertices = new List<Vertex>();
        }

        public void AddVertex(Vertex vertex)
        {
            Vertices.Add(vertex);
        }

        public override string ToString()
        {
            string result = String.Empty;
            foreach (var v in Vertices)
            {
                result += "{" + v.ToString() + "} ";
            }
            return result;
        }
    }
}
