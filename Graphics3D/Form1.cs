using Graphics3D.Model;
using Graphics3D.Rendering;
using System.Numerics;

namespace Graphics3D
{
    public partial class Form1 : Form
    {
        static readonly System.Windows.Forms.Timer timer = new();
        uint ticks = 0;
        bool animateLight = false;

        readonly Painter painter;
        readonly DirectBitmap canvasBitmap;
        readonly List<Shape> shapes;
        readonly Random random = new();
        readonly LightAnimator lightAnimator;
        const float initialRadius = 30;

        public Form1()
        {
            InitializeComponent();

            canvasBitmap = new(canvas.Size.Width, canvas.Size.Height);
            using Graphics g = Graphics.FromImage(canvasBitmap.Bitmap);
            g.Clear(Color.White);
            canvas.Image = canvasBitmap.Bitmap;

            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Interval = 100;

            shapes = new List<Shape>();

            painter = new Painter(canvasBitmap);
            painter.vertexProcessor.Zoom = 100;
            painter.vertexProcessor.CameraPosition = new Vector3((float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);

            lightAnimator = new LightAnimator(new Vector3(initialRadius, 0, 20), 5);

            painter.DrawCoordinateSystem();
            InitScene();
        }

        private void InitScene()
        {
            string carObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\car.obj"));
            List<Face> faces = ObjFileReader.Read(carObj);
            shapes.Add(new Shape(faces, shapes.Count, new RGB(random.NextSingle(), random.NextSingle(), random.NextSingle()), new Vector3(-1, 0, 0)));

            string sphereObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\cube.obj"));
            faces = ObjFileReader.Read(sphereObj);
            shapes.Add(new Shape(faces, shapes.Count, new RGB(random.NextSingle(), random.NextSingle(), random.NextSingle()), new Vector3(-1, 0, 0)));
            shapes[1].Translate(-10, 0, 0);

            DrawScene();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new()
            {
                Filter = "obj files (*.obj)|*.obj",
                InitialDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets")),
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = fileDialog.FileName;
                List<Face> faces = ObjFileReader.Read(path);
                shapes.Add(new Shape(faces, shapes.Count, new RGB(random.NextSingle(), random.NextSingle(), random.NextSingle()), new Vector3(-1, 0, 0)));

                DrawScene();
            }
        }

        private void buttonAnimationStart_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void buttonPauseAnimation_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void TimerEventProcessor(object? sender, EventArgs e)
        {
            ticks++;
            ClearCanvas();
            shapes[0].ResetPosition();

            float a = 5;
            var (s, c) = MathF.SinCos(ticks / 20f);
            float x = (a * c) / (1 + MathF.Pow(s, 2));
            float y = (a * s * c) / (1 + MathF.Pow(s, 2));


            (s, c) = MathF.SinCos((ticks + 1) / 20f);
            float nextX = (a * c) / (1 + MathF.Pow(s, 2));
            float nextY = (a * s * c) / (1 + MathF.Pow(s, 2));
            shapes[0].direction.X = nextX - x;
            shapes[0].direction.Y = nextY - y;
            shapes[0].direction.Z = 0;

            shapes[0].ApplyGeneralRotation(Utils.RotateOnto(shapes[0].InitialDirection, shapes[0].direction));
            shapes[0].Translate(x, y, 0f);

            if (radioButtonCamFixed.Checked)
            {
                painter.vertexProcessor.CameraTarget = new Vector3(0);
                painter.vertexProcessor.CameraPosition = new Vector3((float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);
            }
            if (radioButtonCamTracking.Checked)
            {
                painter.vertexProcessor.CameraTarget = shapes[0].Position;
            }
            if (radioButtonCamTpp.Checked)
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
                painter.rasterizer.colorPicker.lightDirection =
                    Vector3.Normalize(lightAnimator.MoveLightSource());
            }

            DrawScene();
        }

        private void DrawScene()
        {
            painter.rasterizer.ClearDepthBuffer();
            painter.DrawCoordinateSystem();

            foreach (var shape in shapes)
            {
                shape.PaintShape(painter);
            }

            canvas.Invalidate();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearScene();
        }

        private void ClearScene()
        {
            shapes.Clear();
            painter.rasterizer.ClearCanvas();

            DrawScene();
        }

        private void ClearCanvas()
        {
            painter.rasterizer.ClearCanvas();
        }

        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            painter.vertexProcessor.Zoom = trackBarScale.Value * 5;

            ClearCanvas();
            DrawScene();
        }

        private void trackBarFov_Scroll(object sender, EventArgs e)
        {
            painter.vertexProcessor.FieldOfView = trackBarFov.Value * MathF.PI / 180.0f;

            ClearCanvas();
            DrawScene();
        }

        private void numericUpDownCamX_ValueChanged(object sender, EventArgs e)
        {
            InvalidateCameraPosition();
        }

        private void numericUpDownCamY_ValueChanged(object sender, EventArgs e)
        {
            InvalidateCameraPosition();
        }

        private void numericUpDownCamZ_ValueChanged(object sender, EventArgs e)
        {
            InvalidateCameraPosition();
        }

        private void InvalidateCameraPosition()
        {
            painter.vertexProcessor.CameraPosition = new Vector3((float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);
            ClearCanvas();
            DrawScene();
        }

        private void radioButtonNormals_CheckedChanged(object sender, EventArgs e)
        {
            InvalidateInterpolationMethod();
            DrawScene();
        }

        private void InvalidateInterpolationMethod()
        {
            if (radioButtonNormals.Checked)
            {
                painter.rasterizer.colorPicker.interpolantType = InterpolantType.NormalVector;
            }
            else if (radioButtonColors.Checked)
            {
                painter.rasterizer.colorPicker.interpolantType = InterpolantType.Color;
            }
        }

        private void radioButtonColors_CheckedChanged(object sender, EventArgs e)
        {
            InvalidateInterpolationMethod();
        }

        private void checkBoxAnimateLight_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAnimateLight.Checked)
            {
                animateLight = true;
            }
            else
            {
                animateLight = false;
            }
        }

        private void trackBarLightZ_Scroll(object sender, EventArgs e)
        {
            lightAnimator.Z = trackBarLightZ.Value;
            painter.rasterizer.colorPicker.lightDirection
                = Vector3.Normalize(lightAnimator.MoveLightSource());

            DrawScene();
        }
    }
}