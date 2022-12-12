using System.Numerics;

namespace Graphics3D
{
    internal class Transformations
    {
        // Rotates a shape about the x axis
        public static void RotateX(Shape shape, float radians)
        {
            Matrix4x4 Rx = Matrix4x4.CreateRotationX(radians);

            foreach (var f in shape.Faces)
            {
                for (int i = 0; i < f.Vertices.Count; i++)
                {
                    f.Vertices[i].Location = Vector3.Transform(f.Vertices[i].Location, Rx);
                }
            }
        }

        // Rotates a shape about any axis
        public static void Rotate(Shape shape, float radians, Vector3 axis)
        {
            Vector3 u = Vector3.Normalize(axis);
            Matrix4x4 Ru = NumericsExtensions.CreateGeneralRotation(radians, u);

            foreach (var f in shape.Faces)
            {
                for (int i = 0; i < f.Vertices.Count; i++)
                {
                    f.Vertices[i].Location = Vector3.Transform(f.Vertices[i].Location, Ru);
                }
            }
        }
    }
}
