using Graphics3D.Model;
using Graphics3D.Rendering;
using Graphics3D.Utility;
using System.Numerics;

namespace Graphics3D
{
    /// <summary>
    /// Animation containing:
    /// * one moving car and three stationary objects (sphere, cube, and torus);
    /// * two stationary light sources
    ///     1. yellow coming from positive x direction,
    ///     2. white coming from posisitve z direction,
    /// and one white spotlight moving along with the car (headlights);
    /// * transitions between day and night.
    /// Rendering is done on the client's side.
    /// </summary>
    internal class Animation
    {
        private readonly Painter painter;

        private readonly Shape car;
        private readonly Shape cube;
        private readonly Shape sphere;
        private readonly Shape torus;
        private readonly List<Shape> shapes;
        public List<Shape> Shapes { get => shapes; }

        // environment
        private bool darken = true;
        private RGB backgroundColor = new(1, 1, 1);

        // car's position
        private float prevX = 0;
        private float prevY = 0;
        private float x = 0;
        private float y = 0;

        // headlights
        private float verticalLightAngle = 0;
        private float horizontalLightAngle = 0;

        // car's movement
        private float speed = 0.1f;
        private float angle = 0;
        private bool swingRight = true;
        private float swingAngle = 0f;

        public CamType CameraType { get; set; }

        public enum CamType
        {
            Fixed, Tracking, TPP
        }

        public Animation(Painter painter)
        {
            this.painter = painter;

            string carObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\car.obj"));
            List<Face> faces = ObjFileReader.Read(carObj);
            car = new Shape(faces, new RGB(Color.LightBlue), -Vector3.UnitX);

            string sphereObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\sphere.obj"));
            faces = ObjFileReader.Read(sphereObj);
            sphere = new Shape(faces, new RGB(Color.PaleGreen), Vector3.UnitX);

            string cubeObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\cube.obj"));
            faces = ObjFileReader.Read(cubeObj);
            cube = new Shape(faces, new RGB(Color.IndianRed), Vector3.UnitX);

            string torusObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\torus.obj"));
            faces = ObjFileReader.Read(torusObj);
            torus = new Shape(faces, new RGB(Color.DarkTurquoise), Vector3.UnitX);

            shapes = new List<Shape> { car, sphere, cube, torus };
        }

        public void InitScene()
        {
            sphere.Scale(2);
            sphere.Translate(-10, 0, 0);

            cube.Scale(2);
            cube.Translate(-10, -7, 0);

            torus.Scale(3);
            torus.Translate(10, -10, 0);

            painter.Rasterizer.ColorPicker.LightSources.Add(
                new LightSource(LightSource.LightSourceType.Spotlight, new Vector3(0, 0, 1), new RGB(Color.White), e: 2, cutoff: 0.7f, location: new Vector3(0, 0, 2)));

            painter.Rasterizer.ColorPicker.LightSources.Add(
                new LightSource(LightSource.LightSourceType.Point, new Vector3(0, 0, 1), new RGB(Color.White)));

            painter.Rasterizer.ColorPicker.LightSources.Add(
                new LightSource(LightSource.LightSourceType.Point, new Vector3(1, 0, 0), new RGB(Color.Yellow)));
        }

        public void HandleInput(KeyboardState keyboard)
        {
            float stepSpeed = 0.05f;
            float stepAngle = 0.1f;

            if (keyboard == KeyboardState.None)
                return;

            if ((keyboard & KeyboardState.pressedW) != 0)
            {
                speed += stepSpeed;
            }
            if ((keyboard & KeyboardState.pressedS) != 0)
            {
                speed -= stepSpeed;
                if (speed < 0) speed = 0;
            }
            if ((keyboard & KeyboardState.pressedA) != 0)
            {
                angle += stepAngle;
            }
            if ((keyboard & KeyboardState.pressedD) != 0)
            {
                angle -= stepAngle;
            }
            if ((keyboard & KeyboardState.pressedI) != 0)
            {
                verticalLightAngle += 0.1f;
            }
            if ((keyboard & KeyboardState.pressedK) != 0)
            {
                verticalLightAngle -= 0.1f;
            }
            if ((keyboard & KeyboardState.pressedJ) != 0)
            {
                horizontalLightAngle += 0.1f;
            }
            if ((keyboard & KeyboardState.pressedL) != 0)
            {
                horizontalLightAngle -= 0.1f;
            }
        }

        public void UpdateScene(ulong ticks, bool swinging)
        {
            float a = 5;
            var (s, c) = MathF.SinCos(ticks / 20f);
            x = (a * c) / (1 + MathF.Pow(s, 2));
            y = (a * s * c) / (1 + MathF.Pow(s, 2));

            UpdateScene(swinging);
        }

        public void UpdateScene(bool swinging)
        {
            UpdateDayNightTransition(stepBrightnessChange: new RGB(1 / 255.0f, 1 / 255.0f, 1 / 255.0f));

            painter.Rasterizer.ClearCanvas(backgroundColor);

            if (x != prevX || y != prevY)
            {
                car.ResetPosition();

                car.direction.X = x - prevX;
                car.direction.Y = y - prevY;
                car.direction.Z = 0;

                if (swinging)
                {
                    UpdateSwing(swingAngleStep: 0.05f, maxSwing: 0.2f);
                    car.RotateX(swingAngle);
                }

                car.ApplyGeneralRotation(MathUtils.RotateOnto(car.InitialDirection, car.direction));
                car.Translate(x, y, 0f);
                prevX = x; prevY = y;

                UpdateLighting();
            }

            UpdateCamera();
        }

        public void UpdateSceneInteractive(bool swinging)
        {
            UpdateDayNightTransition(stepBrightnessChange: new RGB(1 / 255.0f, 1 / 255.0f, 1 / 255.0f));

            painter.Rasterizer.ClearCanvas(backgroundColor);

            Vector3 initialDir = car.InitialDirection;
            var newDir = Vector3.Transform(initialDir, Matrix4x4.CreateRotationZ(angle));
            Vector3 newPosition = new Vector3(prevX, prevY, 0) + Vector3.Normalize(newDir) * speed;
            x = newPosition.X;
            y = newPosition.Y;

            if (x != prevX || y != prevY)
            {
                car.ResetPosition();

                car.direction.X = x - prevX;
                car.direction.Y = y - prevY;
                car.direction.Z = 0;

                if (swinging)
                {
                    UpdateSwing(swingAngleStep: 0.05f, maxSwing: 0.2f);
                    car.RotateX(swingAngle);
                }

                car.ApplyGeneralRotation(Matrix4x4.CreateRotationZ(angle));
                car.Translate(newPosition.X, newPosition.Y, newPosition.Z);
                prevX = x; prevY = y;
            }

            UpdateLighting();

            UpdateCamera();
        }

        private void UpdateLighting()
        {
            painter.Rasterizer.ColorPicker.LightSources[0].Location = car.Position + Vector3.Normalize(car.direction);
            if (horizontalLightAngle == 0 && verticalLightAngle == 0)
            {
                painter.Rasterizer.ColorPicker.LightSources[0].LightDirection = -Vector3.Normalize(car.direction);
            }
            else
            {
                var horizontalRotation = Matrix4x4.CreateRotationZ(horizontalLightAngle);
                var verticalRotationAxis = Vector3.Normalize(Vector3.Cross(car.direction, Vector3.UnitZ));

                // details at https://gamedev.stackexchange.com/a/50545
                var verticalRotation = Quaternion.CreateFromAxisAngle(verticalRotationAxis, verticalLightAngle);

                Vector3 u = new(verticalRotation.X, verticalRotation.Y, verticalRotation.Z);
                float s = verticalRotation.W;
                var lightDirection = 2 * Vector3.Dot(u, car.direction) * u
                    + (s * s - Vector3.Dot(u, u)) * car.direction
                    + 2 * s * Vector3.Cross(u, car.direction);

                lightDirection = Vector3.Transform(lightDirection, horizontalRotation);
                lightDirection = -Vector3.Normalize(lightDirection);

                painter.Rasterizer.ColorPicker.LightSources[0].LightDirection = lightDirection;
            }
        }

        private void UpdateCamera()
        {
            if (CameraType == CamType.Tracking)
            {
                painter.CameraTarget = car.Position;
            }
            if (CameraType == CamType.TPP)
            {
                float camDist = 3;
                float targetDist = 10;
                Vector3 camElevation = new(0, 0, 4f);
                Vector3 camPosition = car.Position - (Vector3.Normalize(car.direction) * camDist) + camElevation;
                Vector3 camTarget = car.Position + Vector3.Normalize(car.direction) * targetDist;
                painter.CameraPosition = camPosition;
                painter.CameraTarget = camTarget;
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

        private void UpdateDayNightTransition(RGB stepBrightnessChange)
        {
            if (darken)
            {
                if ((backgroundColor - stepBrightnessChange).R <= 0)
                {
                    darken = false;
                }

                painter.Rasterizer.ColorPicker.FogColor -= stepBrightnessChange;
                backgroundColor -= stepBrightnessChange;

                for (int i = 1; i < painter.Rasterizer.ColorPicker.LightSources.Count; i++)
                {
                    var lightSource = painter.Rasterizer.ColorPicker.LightSources[i];

                    lightSource.LightColor -= stepBrightnessChange;
                }
            }
            else
            {
                if ((backgroundColor - stepBrightnessChange).R >= 1)
                {
                    darken = true;
                }

                painter.Rasterizer.ColorPicker.FogColor += stepBrightnessChange;
                backgroundColor += stepBrightnessChange;

                for (int i = 1; i < painter.Rasterizer.ColorPicker.LightSources.Count; i++)
                {
                    var lightSource = painter.Rasterizer.ColorPicker.LightSources[i];

                    lightSource.LightColor += stepBrightnessChange;
                }
            }
        }
    }
}
