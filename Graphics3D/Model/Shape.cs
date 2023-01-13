using Graphics3D.Rendering;
using Graphics3D.Utility;
using System.Numerics;

namespace Graphics3D.Model
{
    internal class Shape
    {
        public List<Face> Faces { get; set; }
        public Matrix4x4 ModelMatrix { get; set; } = Matrix4x4.Identity;
        public RGB Color { get; set; }

        /// <summary>
        /// ambient reflection constant
        /// </summary>
        public float Ka { get; set; } = 0.5f;

        /// <summary>
        /// specular reflection constant
        /// </summary>
        public float Ks { get; set; } = 0.5f;

        /// <summary>
        /// diffuse reflection constant
        /// </summary>
        public float Kd { get; set; } = 0.5f;

        /// <summary>
        /// shininess constant
        /// </summary>
        public float M { get; set; } = 1;

        Vector3 position;

        public Vector3 direction;
        private Vector3 initialDirection;

        // current position is WS coordinates. Object's position can be modified via the Translate() method
        public Vector3 Position
        {
            get => position;
        }
        public Vector3 InitialDirection
        {
            get => initialDirection;
        }

        public Shape(List<Face> faces, RGB color, Vector3 initialDirection)
        {
            Faces = faces;
            Color = color;
            this.initialDirection = initialDirection;
        }

        public void DrawMesh(Painter painter)
        {
            painter.DrawMesh(this);
        }

        public void PaintShape(Painter painter)
        {
            painter.Paint(this);
            //painter.PaintOnSingleThread(this);
        }

        public void RotateX(float radians)
        {
            ModelMatrix *= Matrix4x4.CreateRotationX(radians);
        }

        public void Translate(float x, float y, float z)
        {
            ModelMatrix *= Matrix4x4.CreateTranslation(x, y, z);

            position.X = x;
            position.Y = y;
            position.Z = z;
        }

        public void ApplyGeneralRotation(Matrix4x4 rotation)
        {
            ModelMatrix *= rotation;
        }

        public void Scale(float factor)
        {
            ModelMatrix *= Matrix4x4.CreateScale(factor);
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
