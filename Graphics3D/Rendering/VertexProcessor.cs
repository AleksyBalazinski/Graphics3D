using Graphics3D.Model;
using System.Numerics;

namespace Graphics3D.Rendering
{
    /// <summary>
    /// Transforms vertices (and their normals) comprising a face.
    /// </summary>
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

        /// <summary>
        /// Gets and sets camera position
        /// </summary>
        /// <remarks>
        /// <note type="Cautionary">
        /// This property should be modified through Painter to ensure consistency of the pipeline
        /// </note>
        /// </remarks>
        public Vector3 CameraPosition
        {
            set
            {
                cameraPosition = value;
                CalculateView();
            }
            get => cameraPosition;
        }

        /// <summary>
        /// Gets and sets camera target
        /// </summary>
        /// <remarks>
        /// <note type="Cautionary">
        /// This property should be modified through Painter to ensure consistency of the pipeline
        /// </note>
        /// </remarks>
        public Vector3 CameraTarget
        {
            set
            {
                cameraTarget = value;
                CalculateView();
            }
            get => cameraTarget;
        }

        public float Zoom { get; set; }

        /// <summary>
        /// Enables / disables back-face culling; enabled by default
        /// </summary>
        public bool CullBackFaces { get; set; } = true;

        private Vector3 cameraPosition;
        private Vector3 cameraTarget;
        private float fieldOfView;

        private readonly int canvasWidth;
        private readonly int canvasHeight;
        private Matrix4x4 viewMatrix;
        private Matrix4x4 projectionMatrix;

        private readonly float near = 0.1f;
        private readonly float far = 1000f;

        public VertexProcessor(int canvasWidth, int canvasHeight)
        {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            CameraPosition = new Vector3(1, 1, 1);
            FieldOfView = 0.8f;
            Zoom = 1;
        }

        private void CalculateView()
        {
            viewMatrix = Matrix4x4.CreateLookAt(cameraPosition, cameraTarget, new Vector3(0, 0, 1));
        }

        private void CalculateProjection()
        {
            projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, (float)canvasWidth / canvasHeight, near, far);
        }

        public List<VertexInfo> ProcessFace(Face face, Matrix4x4 modelMatrix)
        {
            List<Vector3> ws3List = new();
            foreach (var vertex in face.Vertices)
            {
                Vector4 location = new(vertex.Location.X, vertex.Location.Y, vertex.Location.Z, 1);
                var worldSpaceCoordinates = Vector4.Transform(location, modelMatrix);
                ws3List.Add(new Vector3(worldSpaceCoordinates.X, worldSpaceCoordinates.Y, worldSpaceCoordinates.Z));
            }
            var normal = Vector3.Cross(ws3List[1] - ws3List[0], ws3List[2] - ws3List[0]);

            float dot = Vector3.Dot(ws3List[0] - CameraPosition, normal);
            if (Vector3.Dot(ws3List[0] - CameraPosition, normal) == 0 || (dot > 0 && CullBackFaces))
                return new List<VertexInfo>();

            List<VertexInfo> faceInfo = new();
            foreach (var vertex in face.Vertices)
            {
                Vector4 location = new(vertex.Location.X, vertex.Location.Y, vertex.Location.Z, 1);
                var worldSpaceCoordinates = Vector4.Transform(location, modelMatrix);
                var eyeCoordinates = Vector4.Transform(worldSpaceCoordinates, viewMatrix);
                var clipCoordinates = Vector4.Transform(eyeCoordinates, projectionMatrix);

                if (clipCoordinates.Z < 0 || clipCoordinates.Z > far)
                    return new List<VertexInfo>();

                Vector3 normalized = new(clipCoordinates.X / clipCoordinates.W, clipCoordinates.Y / clipCoordinates.W, clipCoordinates.Z / clipCoordinates.W);

                (float screenX, float screenY) = ToScreen(normalized);
                var worldSpaceNormal = Vector3.TransformNormal(vertex.NormalVector, modelMatrix);
                var worldSpaceLocation = new Vector3(worldSpaceCoordinates.X, worldSpaceCoordinates.Y, worldSpaceCoordinates.Z);
                faceInfo.Add(new VertexInfo(screenX, screenY, clipCoordinates.Z, worldSpaceNormal, worldSpaceLocation));
            }

            return faceInfo;
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
            var worldSpaceLocation = new Vector3(worldSpaceCoordinates.X, worldSpaceCoordinates.Y, worldSpaceCoordinates.Z);

            return new VertexInfo(screenX, screenY, normalized.Z, worldSpaceNormal, worldSpaceLocation);
        }

        private (float, float) ToScreen(Vector3 point)
        {
            float x = Zoom * point.X + canvasWidth / 2;
            float y = canvasHeight / 2 - Zoom * point.Y;
            return (x, y);
        }
    }
}
