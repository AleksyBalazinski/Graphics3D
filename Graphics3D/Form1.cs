using Graphics3D.Model;
using Graphics3D.Rendering;
using Graphics3D.Utility;
using System.Numerics;

namespace Graphics3D
{
    public partial class Form1 : Form
    {
        static readonly System.Windows.Forms.Timer timer = new();
        ulong ticks = 0;
        bool animateLight = false;

        readonly Painter painter;
        readonly DirectBitmap canvasBitmap;
        readonly List<Shape> shapes;
        readonly Random random = new();
        readonly LightAnimator lightAnimator;
        const float initialRadius = 30;

        readonly Animation animation;

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

            animation = new Animation(painter, shapes, lightAnimator);

            painter.DrawCoordinateSystem();
            animation.InitScene();
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

            animation.UpdateScene(ticks);
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

        private void radioButtonCamFixed_CheckedChanged(object sender, EventArgs e)
        {
            painter.vertexProcessor.CameraPosition = new Vector3(
                (float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);
            painter.vertexProcessor.CameraTarget = new Vector3(0);

            animation.CameraType = Animation.CamType.Fixed;
        }

        private void radioButtonCamTracking_CheckedChanged(object sender, EventArgs e)
        {
            painter.vertexProcessor.CameraPosition = new Vector3(
                (float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);

            animation.CameraType = Animation.CamType.Tracking;
        }

        private void radioButtonCamTpp_CheckedChanged(object sender, EventArgs e)
        {
            animation.CameraType = Animation.CamType.TPP;
        }
    }
}