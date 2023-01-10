using Graphics3D.Model;
using Graphics3D.Rendering;
using System.Diagnostics;
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
                new LightSource(LightSource.Type.Point, new Vector3(0, 0, 1), new RGB(Color.White)));

            painter.rasterizer.colorPicker.lightSources.Add(
                new LightSource(LightSource.Type.Point, new Vector3(1, 0, 0), new RGB(Color.Yellow)));

            /*painter.rasterizer.colorPicker.lightSources.Add(
                new LightSource(LightSource.Type.Spotlight, new Vector3(0, 0, -1), new RGB(Color.White), 1, 0.1f));*/

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
            animationStart = DateTime.UtcNow;
            timer.Start();
        }

        private void buttonPauseAnimation_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private bool darken = true;
        private int r = 255;
        private int g = 255;
        private int b = 255;

        float prevX = 0;
        float prevY = 0;
        DateTime animationStart;

        private void TimerEventProcessor(object? sender, EventArgs e)
        {
            ticks++;

            /*if (darken)
            {
                r++; g++; b++;
                if (r == 255)
                    darken = false;
            }
            else
            {
                r--; g--; b--;
                if (r == 0)
                    darken = true;
            }*/

            painter.rasterizer.ClearCanvas(Color.FromArgb(r, g, b));
            shapes[0].ResetPosition();

            float a = 5;
            var (s, c) = MathF.SinCos(ticks / 20f);
            float x = (a * c) / (1 + MathF.Pow(s, 2));
            float y = (a * s * c) / (1 + MathF.Pow(s, 2));

            shapes[0].direction.X = x - prevX;
            shapes[0].direction.Y = y - prevY;
            shapes[0].direction.Z = 0;

            shapes[0].ApplyGeneralRotation(Utils.RotateOnto(shapes[0].InitialDirection, shapes[0].direction));
            shapes[0].Translate(x, y, 0f);
            prevX = x; prevY= y;

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
                painter.rasterizer.colorPicker.lightSources[0].lightDirection =
                    Vector3.Normalize(lightAnimator.MoveLightSource());
            }

            Stopwatch sw = Stopwatch.StartNew();
            DrawScene();
            sw.Stop();
            if(sw.ElapsedMilliseconds > 300)
            Debug.WriteLine("Elapsed = {0}", sw.ElapsedMilliseconds);
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
            painter.rasterizer.ClearCanvas(Color.White);

            DrawScene();
        }

        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            painter.vertexProcessor.Zoom = trackBarScale.Value * 5;
            painter.rasterizer.ClearCanvas(Color.White);

            DrawScene();
        }

        private void trackBarFov_Scroll(object sender, EventArgs e)
        {
            painter.vertexProcessor.FieldOfView = trackBarFov.Value / 100f;
            painter.rasterizer.ClearCanvas(Color.White);

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
            painter.rasterizer.ClearCanvas(Color.White);
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
            else if (radioButtonConst.Checked)
            {
                painter.rasterizer.colorPicker.interpolantType = InterpolantType.Constant;
            }
        }

        private void radioButtonColors_CheckedChanged(object sender, EventArgs e)
        {
            InvalidateInterpolationMethod();
            DrawScene();
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
            painter.rasterizer.colorPicker.lightSources[0].lightDirection
                = Vector3.Normalize(lightAnimator.MoveLightSource());

            DrawScene();
        }

        private void radioButtonConst_CheckedChanged(object sender, EventArgs e)
        {
            InvalidateInterpolationMethod();
            DrawScene();
        }
    }
}