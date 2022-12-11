namespace Graphics3D
{
    internal class Shape
    {
        public List<Face> Faces { get; set; }

        public Shape(List<Face> faces)
        {
            Faces = faces;
        }

        public void DrawMesh(Painter painter, DirectBitmap canvas)
        {
            painter.DrawMesh(this, canvas);
        }

        public override string ToString()
        {
            string s = "{ ";
            foreach(Face f in Faces)
            {
                s += f.ToString();
            }
            s += " }";

            return s;
        }
    }
}
