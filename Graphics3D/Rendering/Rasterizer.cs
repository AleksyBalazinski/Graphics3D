using Graphics3D.Model;

namespace Graphics3D.Rendering
{
    internal class Rasterizer
    {
        public bool CullBackFaces = false; // unused
        public ColorPicker colorPicker;
        private readonly int canvasWidth;
        private readonly int canvasHeight;
        private readonly DirectBitmap canvas;
        private readonly float[,] zBuffer;

        public Rasterizer(DirectBitmap canvas)
        {
            this.canvas = canvas;
            canvasWidth = canvas.Width;
            canvasHeight = canvas.Height;
            zBuffer = new float[canvasWidth, canvasHeight];
            colorPicker = new ColorPicker();
        }

        public void ClearDepthBuffer()
        {
            for (int y = 0; y < canvasHeight; y++)
            {
                for (int x = 0; x < canvasWidth; x++)
                {
                    zBuffer[x, y] = float.MaxValue;
                }
            }
        }

        public void ClearCanvas()
        {
            using var g = Graphics.FromImage(canvas.Bitmap);
            g.Clear(Color.White);
        }

        public void DrawFaceBoundary(List<VertexInfo> screenPoints)
        {
            bool containedInCanvas = screenPoints.All(p => p.X >= 0 && p.X <= canvasWidth && p.Y >= 0 && p.Y <= canvasHeight);
            if (!containedInCanvas)
            {
                return;
            }

            for (int i = 0; i < screenPoints.Count; i++)
            {
                var point = screenPoints[i];
                var prevPoint = screenPoints[i == 0 ? ^1 : i - 1];
                using var pen = new Pen(Color.Black, 1);
                using Graphics g = Graphics.FromImage(canvas.Bitmap);
                g.DrawLine(new Pen(Color.Black, 1), prevPoint.X, prevPoint.Y, point.X, point.Y);
            }
        }

        public void FillFace(List<VertexInfo> screenPoints, Shape shape)
        {
            bool containedInCanvas = screenPoints.All(p => p.X >= 0 && p.X <= canvasWidth && p.Y >= 0 && p.Y <= canvasHeight);
            if (!containedInCanvas)
            {
                return;
            }
            List<VertexInfo> ascY = screenPoints.OrderBy(v => v.Y).ToList();
            int ymin = (int)ascY[0].Y, ymax = (int)ascY[^1].Y;
            List<VertexInfo> scanned = new();
            List<ActiveEdge> activeEdges = new();
            int k = 0; int verticesCount = screenPoints.Count;
            for (int y = ymin; y <= ymax; y++)
            {
                while (k < verticesCount && y == (int)ascY[k].Y)
                {
                    scanned.Add(ascY[k]);
                    k++;
                }

                foreach (var s in scanned)
                {
                    int si = screenPoints.IndexOf(s);
                    VertexInfo prev = screenPoints[si == 0 ? verticesCount - 1 : si - 1];
                    VertexInfo next = screenPoints[si == verticesCount - 1 ? 0 : si + 1];

                    if (prev.Y > s.Y)
                        activeEdges.Add(new ActiveEdge(prev, s, Utils.Slope(prev, s), (int)s.X));
                    if (prev.Y < s.Y)
                        activeEdges.RemoveAll(ae => ae.start == prev && ae.end == s);
                    if (next.Y > s.Y)
                        activeEdges.Add(new ActiveEdge(s, next, Utils.Slope(s, next), (int)s.X));
                    if (next.Y < s.Y)
                        activeEdges.RemoveAll(ae => ae.start == s && ae.end == next);
                }

                activeEdges.Sort(delegate (ActiveEdge ae1, ActiveEdge ae2)
                {
                    if (ae1.xIntersect < ae2.xIntersect)
                        return -1;
                    if (ae1.xIntersect > ae2.xIntersect)
                        return 1;
                    return 0;
                });

                for (int i = 0; i <= activeEdges.Count - 2; i += 2)
                {
                    FillBetween((int)activeEdges[i].xIntersect, (int)activeEdges[i + 1].xIntersect, y, shape, screenPoints);
                }

                foreach (var ae in activeEdges)
                    ae.xIntersect += 1 / ae.slope;

                scanned.Clear();
            }
        }

        private void FillBetween(int xStart, int xEnd, int y, Shape shape, List<VertexInfo> vertices)
        {
            for (int x = xStart + 1; x <= xEnd; x++)
            {
                float z = Utils.Interpolate(vertices, vertices.Select(v => v.depth).ToList(), x, y);
                if (z > zBuffer[x, y])
                    continue;

                var (r, g, b) = colorPicker.GetColor(x, y, vertices, shape);
                canvas.SetPixel(x, y, Color.FromArgb(r, g, b));

                zBuffer[x, y] = z;
            }
        }

        private class ActiveEdge
        {
            public VertexInfo start;
            public VertexInfo end;
            public float slope;
            public float xIntersect;
            public ActiveEdge(VertexInfo start, VertexInfo end, float slope, float xIntersect)
            {
                this.start = start;
                this.end = end;
                this.slope = slope;
                this.xIntersect = xIntersect;
            }
        }

        /*private bool IsBackFace(Face f, Shape shape)
        {
            return false; // not used
            Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(CameraPosition, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            Matrix4x4 projMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, 1, 5, 100);
            var locations = f.Vertices.Select(v => new Vector4(v.Location.X, v.Location.Y, v.Location.Z, 1));
            var worldSpaceCoords = locations.Select(l => Vector4.Transform(l, shape.ModelMatrix));
            List<Vector3> ws3 = worldSpaceCoords.Select(ws => new Vector3(ws.X, ws.Y, ws.Z)).ToList();
            var normal = Vector3.Cross(ws3[1] - ws3[0], ws3[2] - ws3[0]);

            return Vector3.Dot(ws3[0] - CameraPosition, normal) >= 0;
        }*/

        /*public void PutId(Shape shape, DirectBitmap canvas)
        {
            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            SolidBrush brush = new(Color.Orange);
            Font font = new("Arial", 14);
            (float x, float y) = ToScreen(shape.Faces[0].Vertices[0].Location);
            g.DrawString(shape.ShapeId.ToString(), font, brush, x, y);
        }*/

        public void DrawArrow(VertexInfo startInfo, VertexInfo endInfo, Color color)
        {
            using Pen pen = new(color);
            pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            g.DrawLine(pen, startInfo.X, startInfo.Y, endInfo.X, endInfo.Y);
        }
    }
}
