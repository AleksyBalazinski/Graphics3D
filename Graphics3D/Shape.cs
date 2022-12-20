using System.Numerics;

namespace Graphics3D
{
    internal class Shape
    {
        public List<Face> Faces { get; set; }
        public int ShapeId { get; }
        public Matrix4x4 ModelMatrix { get; set; } = Matrix4x4.Identity;
        public RGB color;

        public Shape(List<Face> faces, int shapeId, RGB color)
        {
            Faces = faces;
            ShapeId = shapeId;
            this.color = color;
        }

        public void DrawMesh(Painter painter, DirectBitmap canvas)
        {
            painter.DrawMesh(this, canvas);
            painter.PutId(this, canvas);
        }

        public void PaintShape(Painter painter, DirectBitmap canvas)
        {
            painter.Paint(this, canvas);
        }

        public void Rotate(float radians)
        {
            ModelMatrix = Matrix4x4.CreateRotationX(radians);
        }

        public override string ToString()
        {
            string s = "{ ";
            foreach (Face f in Faces)
            {
                s += f.ToString();
            }
            s += " }";

            return s;
        }
    }
}
