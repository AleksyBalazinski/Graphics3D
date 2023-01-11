using Graphics3D.Model;
using Graphics3D.Rendering;
using Graphics3D.Utility;
using System.Numerics;

namespace Graphics3D
{
    /// <summary>
    /// Animation containing:
    /// * one moving car and two stationary objects (sphere and cube);
    /// * two stationary light sources and one spotlight moving along with the car (headlights);
    /// * transitions between day and night.
    /// Rendering is done on the client's side.
    /// </summary>
    internal class Animation
    {
        bool animateLight = false;

        readonly Painter painter;
        readonly List<Shape> shapes;

        private bool darken = true;
        private bool swingRight = true;
        private float swingAngle = 0f;
        private int r = 255;
        private int g = 255;
        private int b = 255;

        private float prevX = 0;
        private float prevY = 0;

        public float x = 0;
        public float y = 0;

        public float verticalLightAngle = 0;
        public float horizontalLightAngle = 0;

        public CamType CameraType { get; set; }

        public enum CamType
        {
            Fixed, Tracking, TPP
        }

        readonly LightAnimator lightAnimator;
        const float initialRadius = 30;

        public Animation(Painter painter, List<Shape> shapes, LightAnimator lightAnimator)
        {
            this.painter = painter;
            this.shapes = shapes;
            this.lightAnimator = lightAnimator;
        }

        public void InitScene()
        {
            string carObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\car.obj"));
            List<Face> faces = ObjFileReader.Read(carObj);
            shapes.Add(new Shape(faces, shapes.Count, new RGB(Color.LightBlue), -Vector3.UnitX));

            string sphereObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\sphere.obj"));
            faces = ObjFileReader.Read(sphereObj);
            shapes.Add(new Shape(faces, shapes.Count, new RGB(Color.PaleGreen), Vector3.UnitX));
            shapes[1].Scale(2);
            shapes[1].Translate(-10, 0, 0);

            string cubeObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\cube.obj"));
            faces = ObjFileReader.Read(cubeObj);
            shapes.Add(new Shape(faces, shapes.Count, new RGB(Color.IndianRed), Vector3.UnitX));
            shapes[2].Scale(2);
            shapes[2].Translate(-10, -7, 0);

            painter.rasterizer.colorPicker.lightSources.Add(
                new LightSource(LightSource.Type.Spotlight, new Vector3(0, 0, 1), new RGB(Color.White), 2, 0.7f, new Vector3(0, 0, 2)));

            painter.rasterizer.colorPicker.lightSources.Add(
                new LightSource(LightSource.Type.Point, new Vector3(0, 0, 1), new RGB(Color.Gray)));

            painter.rasterizer.colorPicker.lightSources.Add(
                new LightSource(LightSource.Type.Point, new Vector3(1, 0, 0), new RGB(Color.Yellow)));
        }

        public void HandleInput(KeyboardState keyboard)
        {
            float step = 0.05f;

            if (keyboard == KeyboardState.None)
                return;

            if ((keyboard & KeyboardState.pressedW) != 0)
            {
                y += step;
            }
            if ((keyboard & KeyboardState.pressedS) != 0)
            {
                y -= step;
            }
            if ((keyboard & KeyboardState.pressedA) != 0)
            {
                x -= step;
            }
            if ((keyboard & KeyboardState.pressedD) != 0)
            {
                x += step;
            }
        }

        public void UpdateScene(ulong ticks)
        {
            Thread.Sleep(1);
            float a = 5;
            var (s, c) = MathF.SinCos(ticks / 200f);
            x = (a * c) / (1 + MathF.Pow(s, 2));
            y = (a * s * c) / (1 + MathF.Pow(s, 2));

            UpdateScene();
        }

        public void UpdateScene()
        {
            UpdateDayNightTransition(step: 1);

            painter.rasterizer.ClearCanvas(Color.FromArgb(r, g, b));

            if (x != prevX || y != prevY)
            {
                shapes[0].ResetPosition();

                shapes[0].direction.X = x - prevX;
                shapes[0].direction.Y = y - prevY;
                shapes[0].direction.Z = 0;

                UpdateSwing(swingAngleStep: 0.05f, maxSwing: 0.3f);

                shapes[0].Rotate(swingAngle);
                shapes[0].ApplyGeneralRotation(MathUtils.RotateOnto(shapes[0].InitialDirection, shapes[0].direction));
                shapes[0].Translate(x, y, 0f);
                prevX = x; prevY = y;

                painter.rasterizer.colorPicker.lightSources[0].location = Vector3.Normalize(shapes[0].direction);
                if (horizontalLightAngle == 0 && verticalLightAngle == 0)
                {
                    painter.rasterizer.colorPicker.lightSources[0].lightDirection = -Vector3.Normalize(shapes[0].direction);
                }
                else
                {
                    var horizontalRotation = Matrix4x4.CreateRotationZ(horizontalLightAngle);
                    var verticalRotationAxis = Vector3.Normalize(Vector3.Cross(shapes[0].direction, Vector3.UnitZ));
                    var verticalRotation = Quaternion.CreateFromAxisAngle(verticalRotationAxis, verticalLightAngle);

                    Vector3 u = new Vector3(verticalRotation.X, verticalRotation.Y, verticalRotation.Z);
                    float s = verticalRotation.W;
                    var lightDirection = 2 * Vector3.Dot(u, shapes[0].direction) * u
                        + (s * s - Vector3.Dot(u, u)) * shapes[0].direction
                        + 2 * s * Vector3.Cross(u, shapes[0].direction);

                    lightDirection = Vector3.Transform(lightDirection, horizontalRotation);
                    lightDirection = -Vector3.Normalize(lightDirection);

                    painter.rasterizer.colorPicker.lightSources[0].lightDirection = lightDirection;
                }

            }

            if (CameraType == CamType.Tracking)
            {
                painter.vertexProcessor.CameraTarget = shapes[0].Position;
            }
            if (CameraType == CamType.TPP)
            {
                float camDist = 3;
                float targetDist = 10;
                Vector3 camElevation = new Vector3(0, 0, 4f);
                Vector3 camPosition = shapes[0].Position - (Vector3.Normalize(shapes[0].direction) * camDist) + camElevation;
                Vector3 camTarget = shapes[0].Position + Vector3.Normalize(shapes[0].direction) * targetDist;
                painter.vertexProcessor.CameraPosition = camPosition;
                painter.vertexProcessor.CameraTarget = camTarget;
            }

            if (animateLight)
            {
                painter.rasterizer.colorPicker.lightSources[0].lightDirection =
                    Vector3.Normalize(lightAnimator.MoveLightSource());
            }
        }

        private void UpdateSwing(float swingAngleStep, float maxSwing)
        {
            if (swingRight)
            {
                swingAngle += swingAngleStep;
                if (swingAngle > maxSwing)
                    swingRight = false;
            }
            else
            {
                swingAngle -= swingAngleStep;
                if (swingAngle < -maxSwing)
                    swingRight = true;
            }
        }

        private void UpdateDayNightTransition(int step)
        {
            if (darken)
            {
                r -= step; g -= step; b -= step;
                painter.rasterizer.colorPicker.fogColor.r -= step / 255.0f;
                painter.rasterizer.colorPicker.fogColor.g -= step / 255.0f;
                painter.rasterizer.colorPicker.fogColor.b -= step / 255.0f;

                for (int i = 1; i < painter.rasterizer.colorPicker.lightSources.Count; i++)
                {
                    var lightSource = painter.rasterizer.colorPicker.lightSources[i];

                    lightSource.lightColor.r -= step / 255.0f;
                    lightSource.lightColor.g -= step / 255.0f;
                    lightSource.lightColor.b -= step / 255.0f;
                }

                if (r == 0)
                    darken = false;
            }
            else
            {
                r += step; g += step; b += step;
                painter.rasterizer.colorPicker.fogColor.r += step / 255.0f;
                painter.rasterizer.colorPicker.fogColor.g += step / 255.0f;
                painter.rasterizer.colorPicker.fogColor.b += step / 255.0f;

                for (int i = 1; i < painter.rasterizer.colorPicker.lightSources.Count; i++)
                {
                    var lightSource = painter.rasterizer.colorPicker.lightSources[i];

                    lightSource.lightColor.r += step / 255.0f;
                    lightSource.lightColor.g += step / 255.0f;
                    lightSource.lightColor.b += step / 255.0f;
                }

                if (r == 255)
                    darken = true;
            }
        }
    }
}
