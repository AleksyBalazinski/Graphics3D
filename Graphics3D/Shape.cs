namespace Graphics3D
{
    internal class Shape
    {
        public List<Face> Faces { get; set; }
        public int ShapeId { get; }

        public Shape(List<Face> faces, int shapeId)
        {
            Faces = faces;
            ShapeId = shapeId;
        }

        public void DrawMesh(Painter painter, DirectBitmap canvas)
        {
            painter.PutId(this, canvas);
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
