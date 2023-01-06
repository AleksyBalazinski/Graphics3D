using Graphics3D.Rendering;
using System.Numerics;

namespace Graphics3D.Model
{
    internal class Shape
    {
        public List<Face> Faces { get; set; }
        public int ShapeId { get; }
        public Matrix4x4 ModelMatrix { get; set; } = Matrix4x4.Identity;
        public RGB color;

        public float ks = 0.5f;
        public float kd = 0.5f;
        public float m = 1;

        private float x;
        private float y;
        private float z;

        public float X
        {
            get => x;
        }
        public float Y
        {
            get => y;
        }
        public float Z
        {
            get => z;
        }

        public Shape(List<Face> faces, int shapeId, RGB color)
        {
            Faces = faces;
            ShapeId = shapeId;
            this.color = color;
        }

        public void DrawMesh(Painter painter)
        {
            painter.DrawMesh(this);
        }

        public void PaintShape(Painter painter)
        {
            painter.Paint(this);
        }

        public void Rotate(float radians)
        {
            ModelMatrix *= Matrix4x4.CreateRotationX(radians);
        }

        public void Translate(float x, float y, float z)
        {
            ModelMatrix *= Matrix4x4.CreateTranslation(x, y, z);

            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void ApplyGeneralRotation(Matrix4x4 rotation)
        {
            ModelMatrix *= rotation;
        }

        public void ResetPosition()
        {
            ModelMatrix = Matrix4x4.Identity;
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
