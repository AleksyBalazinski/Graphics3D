namespace Graphics3D
{
    internal struct RGB
    {
        public float r;
        public float g;
        public float b;

        public RGB()
        {
            r = g = b = 0f;
        }

        public RGB(float r, float g, float b)
        {
            this.r = r; this.g = g; this.b = b;
        }

        public RGB(Color color)
        {
            r = color.R / 255f;
            g = color.G / 255f;
            b = color.B / 255f;
        }

        public (int, int, int) ToRGB255()
        {
            int r255, g255, b255;
            if (r > 1) r255 = 255;
            else if (r < 0) r255 = 0;
            else r255 = (int)(r * 255);

            if (g > 1) g255 = 255;
            else if (g < 0) g255 = 0;
            else g255 = (int)(g * 255);

            if (b > 1) b255 = 255;
            else if (b < 0) b255 = 0;
            else b255 = (int)(b * 255);

            return (r255, g255, b255);
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
