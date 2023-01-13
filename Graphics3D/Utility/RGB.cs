namespace Graphics3D.Utility
{
    internal struct RGB
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public RGB()
        {
            R = G = B = 0f;
        }

        public RGB(float r, float g, float b)
        {
            this.R = r; this.G = g; this.B = b;
        }

        public RGB(Color color)
        {
            R = color.R / 255f;
            G = color.G / 255f;
            B = color.B / 255f;
        }

        public (int, int, int) ToRGB255()
        {
            int r255, g255, b255;
            if (R > 1) r255 = 255;
            else if (R < 0) r255 = 0;
            else r255 = (int)(R * 255);

            if (G > 1) g255 = 255;
            else if (G < 0) g255 = 0;
            else g255 = (int)(G * 255);

            if (B > 1) b255 = 255;
            else if (B < 0) b255 = 0;
            else b255 = (int)(B * 255);

            return (r255, g255, b255);
        }

        public static RGB operator *(float c, RGB color)
        {
            return new RGB(c * color.R, c * color.G, c * color.B);
        }

        public static RGB operator +(RGB color1, RGB color2)
        {
            return new RGB(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B);
        }

        public static RGB operator -(RGB color1, RGB color2)
        {
            return new RGB(color1.R - color2.R, color1.G - color2.G, color1.B - color2.B);
        }

        public static RGB operator *(RGB color1, RGB color2)
        {
            return new RGB(color1.R * color2.R, color1.G * color2.G, color1.B * color2.B);
        }

        public static implicit operator Color(RGB color)
        {
            var (r255, g255, b255) = color.ToRGB255();
            return Color.FromArgb(r255, g255, b255);
        }
    }
}
