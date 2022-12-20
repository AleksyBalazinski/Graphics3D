using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    internal class RGB
    {
        public float r;
        public float g;
        public float b;
        public RGB(float r, float g, float b)
        {
            this.r = r; this.g = g; this.b = b;
        }
        public static RGB operator *(float c, RGB color)
        {
            return new RGB(c * color.r, c * color.g, c * color.b);
        }
        public static RGB operator +(RGB color1, RGB color2)
        {
            return new RGB(color1.r + color2.r, color1.g + color2.g, color1.b + color2.b);
        }
        public static RGB operator *(RGB color1, RGB color2)
        {
            return new RGB(color1.r * color2.r, color1.g * color2.g, color1.b * color2.b);
        }
        public static implicit operator Color(RGB color)
        {
            return Color.FromArgb((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
        }
    }
}
