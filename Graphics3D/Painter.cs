namespace Graphics3D
{
    internal class Painter
    {
        readonly int canvasWidth;
        readonly int canvasHeight;
        float scale;

        public Painter(int canvasWidth, int canvasHeight, float scale)
        {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.scale = scale;
        }

        public void DrawMesh(Shape shape, DirectBitmap canvas)
        {
            foreach (var f in shape.Faces)
            {
                for (int i = 0; i < f.Vertices.Count; i++)
                {
                    (float x, float y) = ToScreen(f.Vertices[i].Location);
                    using var brush = new SolidBrush(Color.Black);
                    using var pen = new Pen(Color.Orange, 1);
                    (float prevX, float prevY) = ToScreen(i == 0 ? f.Vertices[^1].Location : f.Vertices[i - 1].Location);
                    using Graphics g = Graphics.FromImage(canvas.Bitmap);
                    g.DrawLine(pen, prevX, prevY, x, y);
                }
            }
        }

        private (float, float) ToScreen(Point3D point)
        {
            float x = scale * point.X + canvasWidth / 2;
            float y = canvasHeight / 2 - scale * point.Y;
            return (x, y);
        }
    }
}
