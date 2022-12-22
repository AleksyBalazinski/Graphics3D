namespace Graphics3D.Model
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
            string result = string.Empty;
            foreach (var v in Vertices)
            {
                result += "{" + v.ToString() + "} ";
            }
            return result;
        }
    }
}
