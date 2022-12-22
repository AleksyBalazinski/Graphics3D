using Graphics3D.Model;
using System.Numerics;

namespace Graphics3D.Rendering
{
    internal class VertexProcessor
    {
        public float FieldOfView
        {
            set
            {
                fieldOfView = value;
                CalculateProjection();
            }
            get => fieldOfView;
        }

        public Vector3 CameraPosition
        {
            set
            {
                cameraPosition = value;
                CalculateView();
            }
            get => cameraPosition;
        }

        public float Zoom { get; set; }
        public bool CullBackFaces = false;

        public RGB lightColor;
        public Vector3 lightDirection;

        private Vector3 cameraPosition;
        private float fieldOfView;

        private readonly int canvasWidth;
        private readonly int canvasHeight;
        private Matrix4x4 viewMatrix;
        private Matrix4x4 projectionMatrix;

        public VertexProcessor(int canvasWidth, int canvasHeight)
        {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            CameraPosition = new Vector3(1, 1, 1);
            FieldOfView = 1;
            Zoom = 1;
            lightColor = new RGB(1, 1, 1);
            lightDirection = new Vector3(0, 0, 1);
        }

        private void CalculateView()
        {
            viewMatrix = Matrix4x4.CreateLookAt(CameraPosition, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
        }

        private void CalculateProjection()
        {
            projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, 1, 5, 100);
        }

        public VertexInfo Process(Vertex vertex, Matrix4x4 modelMatrix)
        {
            Vector4 location = new(vertex.Location.X, vertex.Location.Y, vertex.Location.Z, 1);
            var worldSpaceCoordinates = Vector4.Transform(location, modelMatrix);
            var eyeCoordinates = Vector4.Transform(worldSpaceCoordinates, viewMatrix);
            var clipCoordinates = Vector4.Transform(eyeCoordinates, projectionMatrix);
            Vector3 normalized = new(clipCoordinates.X / clipCoordinates.W, clipCoordinates.Y / clipCoordinates.W, clipCoordinates.Z / clipCoordinates.W);

            (float screenX, float screenY) = ToScreen(normalized);
            var worldSpaceNormal = Vector3.TransformNormal(vertex.NormalVector, modelMatrix);

            return new VertexInfo(screenX, screenY, normalized.Z, worldSpaceNormal);
        }

        private (float, float) ToScreen(Vector3 point)
        {
            float x = Zoom * point.X + canvasWidth / 2;
            float y = canvasHeight / 2 - Zoom * point.Y;
            return (x, y);
        }
    }
}
