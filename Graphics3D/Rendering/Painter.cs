using Graphics3D.Model;
using System.Numerics;

namespace Graphics3D.Rendering
{
    /// <summary>
    /// Encapsulates the graphics pipeline.
    /// </summary>
    internal class Painter
    {
        public VertexProcessor vertexProcessor;
        public Rasterizer rasterizer;

        public Painter(DirectBitmap canvas)
        {
            vertexProcessor = new VertexProcessor(canvas.Width, canvas.Height);
            rasterizer = new Rasterizer(canvas);
        }

        public void Paint(Shape shape)
        {
            var modelMatrix = shape.ModelMatrix;
            List<VertexInfo>[] faceInfos = new List<VertexInfo>[shape.Faces.Count];
            Parallel.For(0, shape.Faces.Count, index =>
            {
                faceInfos[index] = vertexProcessor.ProcessFace(shape.Faces[index], modelMatrix);
            });

            for (int i = 0; i < shape.Faces.Count; i++)
            {
                rasterizer.FillFace(faceInfos[i], shape);
            }
        }

        public void DrawMesh(Shape shape)
        {
            var modelMatrix = shape.ModelMatrix;
            foreach (var face in shape.Faces)
            {
                List<VertexInfo> vertexInfos
                    = face.Vertices.Select(v => vertexProcessor.Process(v, modelMatrix)).ToList();

                rasterizer.DrawFaceBoundary(vertexInfos);
            }
        }

        private void DrawAxis(Vector3 start, Vector3 end, Color color)
        {
            Vertex startVertex = new(start, new Vector3());
            Vertex endVertex = new(end, new Vector3());
            var startInfo = vertexProcessor.Process(startVertex, Matrix4x4.Identity);
            var endInfo = vertexProcessor.Process(endVertex, Matrix4x4.Identity);

            rasterizer.DrawArrow(startInfo, endInfo, color);
        }

        public void DrawCoordinateSystem()
        {
            float a = 1;
            DrawAxis(new Vector3(-a, 0, 0), new Vector3(a, 0, 0), Color.Red); // x
            DrawAxis(new Vector3(0, -a, 0), new Vector3(0, a, 0), Color.Green); // y
            DrawAxis(new Vector3(0, 0, -a), new Vector3(0, 0, a), Color.Blue); // z
        }
    }
}
