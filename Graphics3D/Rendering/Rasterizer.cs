using Graphics3D.Model;
using Graphics3D.Utility;
using System.Numerics;

namespace Graphics3D.Rendering
{
    /// <summary>
    /// Effectively draws pixels on the screen.
    /// Visibility of individual points is determined through the depth test.
    /// </summary>
    internal class Rasterizer
    {
        public ColorPicker colorPicker;
        private readonly int canvasWidth;
        private readonly int canvasHeight;
        private readonly DirectBitmap canvas;
        private readonly float[,] zBuffer;
        private readonly object[,] locks;

        public Rasterizer(DirectBitmap canvas)
        {
            this.canvas = canvas;
            canvasWidth = canvas.Width;
            canvasHeight = canvas.Height;
            zBuffer = new float[canvasWidth, canvasHeight];
            colorPicker = new ColorPicker();

            locks = new object[canvasWidth, canvasHeight];
            for (int i = 0; i < canvasWidth; i++)
            {
                for (int j = 0; j < canvasHeight; j++)
                {
                    locks[i, j] = new object();
                }
            }
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

        public void ClearCanvas(Color color)
        {
            using var g = Graphics.FromImage(canvas.Bitmap);
            g.Clear(color);
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
            if (screenPoints.Count == 0)
                return;

            bool containedInCanvas = screenPoints.All(p => p.X >= 0 && p.X <= canvasWidth && p.Y >= 0 && p.Y <= canvasHeight);
            if (!containedInCanvas)
                return;

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
                        activeEdges.Add(new ActiveEdge(prev, s, MathUtils.Slope(prev, s), (int)s.X));
                    if (prev.Y < s.Y)
                        activeEdges.RemoveAll(ae => ae.start == prev && ae.end == s);
                    if (next.Y > s.Y)
                        activeEdges.Add(new ActiveEdge(s, next, MathUtils.Slope(s, next), (int)s.X));
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
            for (int x = xStart; x <= xEnd; x++)
            {
                float z = MathUtils.Interpolate(vertices, vertices.Select(v => v.depth).ToList(), x, y);
                Vector4 worldSpaceLocation = MathUtils.Interpolate(vertices, vertices.Select(v => v.worldSpaceLocation).ToList(), x, y);

                lock (locks[x, y])
                {
                    if (z > zBuffer[x, y] || float.IsNaN(z))
                        continue;

                    var (r, g, b) = colorPicker.GetColor(x, y, vertices, shape, z, worldSpaceLocation);
                    canvas.SetPixel(x, y, Color.FromArgb(r, g, b));
                    zBuffer[x, y] = z;
                }
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

        public void DrawArrow(VertexInfo startInfo, VertexInfo endInfo, Color color)
        {
            using Pen pen = new(color);
            pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

            using Graphics g = Graphics.FromImage(canvas.Bitmap);
            g.DrawLine(pen, startInfo.X, startInfo.Y, endInfo.X, endInfo.Y);
        }
    }
}
