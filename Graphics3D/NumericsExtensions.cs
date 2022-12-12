using System.Numerics;

namespace Graphics3D
{
    internal static class NumericsExtensions
    {
        public static Matrix4x4 CreateGeneralRotation(float radians, Vector3 u)
        {
            float c = MathF.Cos(radians);
            float s = MathF.Sin(radians);

            return new Matrix4x4(
                    c + u.X * u.X * (1 - c), u.X * u.Y * (1 - c) - u.Z * s, u.X * u.Z * (1 - c) + u.Y * s, 0,
                    u.Y * u.X * (1 - c) + u.Z * s, c + u.Y * u.Y * (1 - c), u.Y * u.Z * (1 - c) - u.X * s, 0,
                    u.Z * u.X * (1 - c) - u.Y * s, u.Z * u.Y * (1 - c) + u.X * s, c + u.Z * u.Z * (1 - c), 0,
                    0, 0, 0, 1
                );
        }
    }
}
