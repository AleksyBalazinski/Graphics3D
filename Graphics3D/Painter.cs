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
        //private float[,] zBuffer;
        public float[,] zBuffer;

        public Painter(int canvasWidth, int canvasHeight, float zoom)
        {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            Zoom = zoom;
            CameraPosition = new Vector3(1, 1, 1);
            zBuffer = new float[canvasWidth, canvasHeight];
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

        public void Paint(Shape shape, DirectBitmap canvas)
        {
            // clear z-buffer
            /*for(int y=0;y< canvasHeight; y++)
            {
                for(int x = 0;x<canvasWidth;x++)
                {
                    canvas.SetPixel(x, y, Color.White);
                    zBuffer[x, y] = float.MaxValue;
                }
            }*/

            foreach(var f in shape.Faces)
            {
                FillFace(f, shape, canvas);
            }
        }

        private float InterpolateZ(List<(float, float, float)> vertices, float x, float y)
        {
            (float wa, float wb, float wc) = GetWeights(x, y,
                vertices[0].Item1, vertices[0].Item2,
                vertices[1].Item1, vertices[1].Item2,
                vertices[2].Item1, vertices[2].Item2);

            float interpolatedZ = wa * vertices[0].Item3 + wb * vertices[1].Item3 + wc * vertices[2].Item3;
            return interpolatedZ;
        }

        private (float, float, float) GetWeights(float x, float y, float ax, float ay, float bx, float by, float cx, float cy)
        {
            float detT = GetDetT(ax, ay, bx, by, cx, cy);
            float wa = ((by - cy) * (x - cx) + (cx - bx) * (y - cy)) / detT;
            float wb = ((cy - ay) * (x - cx) + (ax - cx) * (y - cy)) / detT;
            float wc = 1 - wa - wb;

            return (wa < 0 ? 0 : wa, wb < 0 ? 0 : wb, wc < 0 ? 0 : wc);
        }

        private float GetDetT(float ax, float ay, float bx, float by, float cx, float cy)
        {
            return (by - cy) * (ax - cx) + (cx - bx) * (ay - cy);
        }

        private void FillFace(Face face, Shape shape, DirectBitmap canvas)
        {
            List<(float, float, float)> screenPoints = face.Vertices.Select(v => RenderWithZ(v.Location, shape.ModelMatrix)).ToList();

            List<(float, float, float)> ascY = screenPoints.OrderBy(v => v.Item2).ToList();
            int ymin = (int)ascY[0].Item2, ymax = (int)ascY[^1].Item2;
            List<(float, float, float)> scanned = new();
            List<ActiveEdge> activeEdges = new();
            int k = 0; int verticesCount = screenPoints.Count;
            for (int y = ymin; y <= ymax; y++)
            {
                while (k < verticesCount && y == (int)ascY[k].Item2)
                {
                    scanned.Add(ascY[k]);
                    k++;
                }

                foreach (var s in scanned)
                {
                    int si = screenPoints.IndexOf(s);
                    (float, float, float) prev = screenPoints[si == 0 ? verticesCount - 1 : si - 1];
                    (float, float, float) next = screenPoints[si == verticesCount - 1 ? 0 : si + 1];

                    if (prev.Item2 > s.Item2)
                        activeEdges.Add(new ActiveEdge(prev, s, Slope(prev, s), (int)s.Item1));
                    if (prev.Item2 < s.Item2)
                        activeEdges.RemoveAll(ae => ae.start == prev && ae.end == s);
                    if (next.Item2 > s.Item2)
                        activeEdges.Add(new ActiveEdge(s, next, Slope(s, next), (int)s.Item1));
                    if (next.Item2 < s.Item2)
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
                    FillBetween(canvas, (int)activeEdges[i].xIntersect, (int)activeEdges[i + 1].xIntersect, y, new ColorPicker(), shape, screenPoints);
                }

                foreach (var ae in activeEdges)
                    ae.xIntersect += 1 / ae.slope;

                scanned = new();
            }
        }

        private static float Slope((float, float, float) start, (float, float, float) end)
        {
            return (end.Item2 - start.Item2) / (end.Item1 - start.Item1);
        }

        private void FillBetween(DirectBitmap canvas, int xStart, int xEnd, int y, ColorPicker colorPicker, Shape shape, List<(float, float, float)> vertices)
        {
            for (int x = xStart + 1; x <= xEnd; x++)
            {
                /*int red, green, blue;
                if (shape.Texture == null && shape.NormalMap == null && shape.Color != null) // uniform object color
                {
                    (red, green, blue) = colorPicker.GetColor(x, y, Interpolant, shape.Color);
                }
                else if (shape.Texture != null)
                {
                    (red, green, blue) = colorPicker.GetColor(x, y, Interpolant, shape.Texture, shape.NormalMap);
                }
                else if (shape.Color != null && shape.NormalMap != null)
                {
                    (red, green, blue) = colorPicker.GetColor(x, y, shape.Color, shape.NormalMap);
                }
                else return;

                canvas.SetPixel(x, y, Color.FromArgb(red, green, blue));*/
                float z = InterpolateZ(vertices, x, y);
                if (z > zBuffer[x, y])
                    return;
                int r = (int)(shape.color.r * 255);
                int g = (int)(shape.color.g * 255);
                int b = (int)(shape.color.b * 255);
                canvas.SetPixel(x, y, Color.FromArgb(r, g, b));
                zBuffer[x, y] = z;
            }
        }

        private class ActiveEdge
        {
            public (float, float, float) start;
            public (float, float, float) end;
            public float slope;
            public float xIntersect;
            public ActiveEdge((float, float, float) start, (float, float, float) end, float slope, float xIntersect)
            {
                this.start = start;
                this.end = end;
                this.slope = slope;
                this.xIntersect = xIntersect;
            }
        }

        private bool IsBackFace(Face f, Shape shape)
        {
            return false; // not used
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

        private (float, float, float) RenderWithZ(Vector3 point, Matrix4x4 modelMatrix)
        {
            Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(CameraPosition, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            Matrix4x4 projMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, 1, 5, 100);

            Vector4 location = new(point.X, point.Y, point.Z, 1);
            var worldSpaceCoordinates = Vector4.Transform(location, modelMatrix);
            var eyeCoordinates = Vector4.Transform(worldSpaceCoordinates, viewMatrix);
            var clipCoordinates = Vector4.Transform(eyeCoordinates, projMatrix);
            Vector3 normalized = new(clipCoordinates.X / clipCoordinates.W, clipCoordinates.Y / clipCoordinates.W, clipCoordinates.Z / clipCoordinates.W);

            (float screenX, float screenY) = ToScreen(normalized);
            
            return (screenX, screenY, normalized.Z);
        }
    }
}
