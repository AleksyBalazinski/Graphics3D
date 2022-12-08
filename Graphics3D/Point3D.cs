using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    internal struct Point3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Point3D(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }
        public static bool operator ==(Point3D a, Point3D b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator !=(Point3D a, Point3D b)
        {
            return !(a == b);
        }
        public static Point3D operator -(Point3D a)
            => new(-a.X, -a.Y, -a.Z);
        public static Point3D operator +(Point3D a, Point3D b)
            => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Point3D operator -(Point3D a, Point3D b)
            => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Point3D operator *(float c, Point3D a)
            => new(a.X * c, a.Y * c, a.Z * c);
        public static Point3D operator /(Point3D a, float c)
            => new(a.X / c, a.Y / c, a.Z / c);
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Point3D)
                return false;

            Point3D other = (Point3D)obj;
            return other == this;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }
    }
}
