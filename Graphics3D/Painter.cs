using System.Numerics;

namespace Graphics3D
{
    internal class Painter
    {
        readonly int canvasWidth;
        readonly int canvasHeight;
        public float FieldOfView { get; set; } = 1;
        public Vector3 CameraPosition;
        public float Zoom { get; set; }
        public bool CullBackFaces = false;

        public Painter(int canvasWidth, int canvasHeight, float zoom)
        {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            Zoom = zoom;
            CameraPosition = new Vector3(1, 1, 1);
        }

        public void DrawMesh(Shape shape, DirectBitmap canvas)
        {
            foreach (var f in shape.Faces)
            {
                DrawFace(f, shape, canvas);
            }
        }

        private void DrawFace(Face f, Shape shape, DirectBitmap canvas)
        {
            List<(float, float)> screenPoints = f.Vertices.Select(v => Render(v.Location, shape.ModelMatrix)).ToList();
            bool containedInCanvas = screenPoints.All(p => p.Item1 >= 0 && p.Item1 <= canvasWidth && p.Item2 >= 0 && p.Item2 <= canvasHeight);
            if (!containedInCanvas || (CullBackFaces && IsBackFace(f, shape)))
            {
                return;
            }

            for (int i = 0; i < f.Vertices.Count; i++)
            {
                var (x, y) = screenPoints[i];
                var (prevX, prevY) = screenPoints[i == 0 ? ^1 : i - 1];
                using var pen = new Pen(Color.Black, 1);
                using Graphics g = Graphics.FromImage(canvas.Bitmap);
                g.DrawLine(new Pen(Color.Black, 1), prevX, prevY, x, y);
            }
        }

        private bool IsBackFace(Face f, Shape shape)
        {
            Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(CameraPosition, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            Matrix4x4 projMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, 1, 5, 100);
            var locations = f.Vertices.Select(v => new Vector4(v.Location.X, v.Location.Y, v.Location.Z, 1));
            var worldSpaceCoords = locations.Select(l => Vector4.Transform(l, shape.ModelMatrix));
            List<Vector3> ws3 = worldSpaceCoords.Select(ws => new Vector3(ws.X, ws.Y, ws.Z)).ToList();
            var normal = Vector3.Cross(ws3[1] - ws3[0], ws3[2] - ws3[0]);

            return Vector3.Dot(ws3[0] - CameraPosition, normal) >= 0;
        }

        public void PutId(Shape shape, DirectBitmap canvas)
        {
            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            SolidBrush brush = new(Color.Orange);
            Font font = new("Arial", 14);
            (float x, float y) = ToScreen(shape.Faces[0].Vertices[0].Location);
            g.DrawString(shape.ShapeId.ToString(), font, brush, x, y);
        }

        public void DrawCoordinateSystem(DirectBitmap canvas)
        {
            DrawXAxis(canvas);
            DrawYAxis(canvas);
            DrawZAxis(canvas);
        }

        public void DrawXAxis(DirectBitmap canvas)
        {
            DrawAxis(canvas, new Vector3(-1, 0, 0), new Vector3(1, 0, 0), Color.Red);
        }

        public void DrawYAxis(DirectBitmap canvas)
        {
            DrawAxis(canvas, new Vector3(0, -1, 0), new Vector3(0, 1, 0), Color.Green);
        }

        public void DrawZAxis(DirectBitmap canvas)
        {
            DrawAxis(canvas, new Vector3(0, 0, -1), new Vector3(0, 0, 1), Color.Blue);
        }

        private (float, float) ToScreen(Vector3 point)
        {
            float x = Zoom * point.X + canvasWidth / 2;
            float y = canvasHeight / 2 - Zoom * point.Y;
            return (x, y);
        }

        private void DrawAxis(DirectBitmap canvas, Vector3 start, Vector3 end, Color color)
        {
            (float startX, float startY) = Render(start, Matrix4x4.Identity);
            (float endX, float endY) = Render(end, Matrix4x4.Identity);

            using Pen pen = new(color);
            pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            g.DrawLine(pen, startX, startY, endX, endY);
        }

        private (float, float) Render(Vector3 point, Matrix4x4 modelMatrix)
        {
            Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(CameraPosition, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            Matrix4x4 projMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, 1, 5, 100);

            Vector4 location = new(point.X, point.Y, point.Z, 1);
            var worldSpaceCoordinates = Vector4.Transform(location, modelMatrix);
            var eyeCoordinates = Vector4.Transform(worldSpaceCoordinates, viewMatrix);
            var clipCoordinates = Vector4.Transform(eyeCoordinates, projMatrix);
            Vector3 normalized = new(clipCoordinates.X / clipCoordinates.W, clipCoordinates.Y / clipCoordinates.W, clipCoordinates.Z / clipCoordinates.W);

            return ToScreen(normalized);
        }
    }
}
