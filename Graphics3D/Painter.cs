using System.Numerics;

namespace Graphics3D
{
    internal class Painter
    {
        readonly int canvasWidth;
        readonly int canvasHeight;
        float scale;
        public float Radians { get; set; }
        public float FieldOfView { get; set; } = 1;
        public Vector3 CameraPosition;
        public float Scale 
        {
            get { return scale; }
            set { scale = value; }
        }

        public Painter(int canvasWidth, int canvasHeight, float scale)
        {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.scale = scale;
            CameraPosition = new Vector3(5, 5, 5);
        }

        public void DrawMesh(Shape shape, DirectBitmap canvas)
        {
            foreach (var f in shape.Faces)
            {
                for (int i = 0; i < f.Vertices.Count; i++)
                {
                    (float x, float y) = ToScreen2(f.Vertices[i].Location);
                    using var brush = new SolidBrush(Color.Black);
                    using var pen = new Pen(Color.Black, 1);
                    (float prevX, float prevY) = ToScreen2(i == 0 ? f.Vertices[^1].Location : f.Vertices[i - 1].Location);
                    using Graphics g = Graphics.FromImage(canvas.Bitmap);
                    g.DrawLine(pen, prevX, prevY, x, y);
                }
            }
        }

        public void PutId(Shape shape, DirectBitmap canvas)
        {
            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            SolidBrush brush = new(Color.Orange);
            Font font = new("Arial", 14);
            (float x, float y) = ToScreen(shape.Faces[0].Vertices[0].Location);
            g.DrawString(shape.ShapeId.ToString(), font, brush, x, y);
        }

        public void DrawXAxis(DirectBitmap canvas)
        {
            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            Vector3 start = new Vector3(-1, 0, 0);
            Vector3 end = new Vector3(1, 0, 0);
            (float startX, float startY) = ToScreen(start);
            (float endX, float endY) = ToScreen(end);

            g.DrawLine(new Pen(Color.Red), startX, startY, endX, endY);
        }

        public void DrawYAxis(DirectBitmap canvas)
        {
            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            Vector3 start = new Vector3(0, -1, 0);
            Vector3 end = new Vector3(0, 1, 0);
            (float startX, float startY) = ToScreen(start);
            (float endX, float endY) = ToScreen(end);

            g.DrawLine(new Pen(Color.Green), startX, startY, endX, endY);
        }

        private (float, float) ToScreen(Vector3 point)
        {
            float x = scale * point.X + canvasWidth / 2;
            float y = canvasHeight / 2 - scale * point.Y;
            return (x, y);
        }

        private (float, float) ToScreen2(Vector3 point)
        {
            Matrix4x4 Rx = Matrix4x4.CreateRotationX(Radians);
            Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(CameraPosition, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            Matrix4x4 projMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, 1, 5, 100);

            Vector4 location = new Vector4(point.X, point.Y, point.Z, 1);
            var worldSpace = Vector4.Transform(location, Rx);
            var eye = Vector4.Transform(worldSpace, viewMatrix);

            var clip = Vector4.Transform(eye, projMatrix);
            Vector3 normalized = new Vector3(clip.X / clip.W, clip.Y / clip.W, clip.Z / clip.W);

            return ToScreen(normalized);

        }
    }
}
