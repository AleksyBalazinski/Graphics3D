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
        bool animationRunning = false;

        readonly Painter painter;
        readonly DirectBitmap canvasBitmap;
        readonly List<Shape> shapes;
        readonly Random random = new();

        readonly Animation animation;

        KeyboardState pressedKeys;
        ulong framesCount = 0;

        DateTime fpsCountStart;

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
            painter.VertexProcessor.Zoom = 100;
            painter.CameraPosition = new Vector3((float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);

            animation = new Animation(painter);
            shapes.AddRange(animation.Shapes);

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
                shapes.Add(new Shape(faces, new RGB(random.NextSingle(), random.NextSingle(), random.NextSingle()), new Vector3(-1, 0, 0)));

                DrawScene();
            }
        }

        private void buttonAnimationStart_Click(object sender, EventArgs e)
        {
            animationRunning = true;
            timer.Start();
            buttonPauseInteractive.Enabled = false;
            buttonStartInteractive.Enabled = false;
            fpsCountStart = DateTime.UtcNow;
        }

        private void buttonPauseAnimation_Click(object sender, EventArgs e)
        {
            animationRunning = false;
            timer.Stop();
            buttonPauseInteractive.Enabled = true;
            buttonStartInteractive.Enabled = true;
        }

        private void TimerEventProcessor(object? sender, EventArgs e)
        {
            ticks++;
            framesCount++;

            if (animationRunning)
            {
                animation.UpdateScene(ticks, checkBoxSwinging.Checked);
            }
            else // interactive
            {
                animation.UpdateSceneInteractive(checkBoxSwinging.Checked);
            }

            DrawScene();

            if (framesCount == 10)
            {
                textBoxFps.Text = string.Format("{0:0.0}", (framesCount / (DateTime.UtcNow - fpsCountStart).TotalSeconds));
                framesCount = 0;
                fpsCountStart = DateTime.UtcNow;
            }
        }

        private void DrawScene()
        {
            painter.Rasterizer.ClearDepthBuffer();
            if (checkBoxCoordSystem.Checked)
                painter.DrawCoordinateSystem();

            foreach (var shape in shapes)
            {
                shape.PaintShape(painter);
            }

            canvas.Refresh();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearScene();
        }

        private void ClearScene()
        {
            shapes.Clear();
            painter.Rasterizer.ClearCanvas(Color.White);

            DrawScene();
        }

        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            painter.VertexProcessor.Zoom = trackBarScale.Value * 5;
            painter.Rasterizer.ClearCanvas(painter.Rasterizer.ColorPicker.FogColor);

            DrawScene();
        }

        private void trackBarFov_Scroll(object sender, EventArgs e)
        {
            painter.VertexProcessor.FieldOfView = trackBarFov.Value / 100f;
            painter.Rasterizer.ClearCanvas(painter.Rasterizer.ColorPicker.FogColor);

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
            painter.CameraPosition = new Vector3((float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);
            painter.Rasterizer.ClearCanvas(painter.Rasterizer.ColorPicker.FogColor);
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
                painter.Rasterizer.ColorPicker.Interpolant = InterpolantType.NormalVector;
            }
            else if (radioButtonColors.Checked)
            {
                painter.Rasterizer.ColorPicker.Interpolant = InterpolantType.Color;
            }
            else if (radioButtonConst.Checked)
            {
                painter.Rasterizer.ColorPicker.Interpolant = InterpolantType.Constant;
            }
        }

        private void radioButtonColors_CheckedChanged(object sender, EventArgs e)
        {
            InvalidateInterpolationMethod();
            DrawScene();
        }

        private void radioButtonConst_CheckedChanged(object sender, EventArgs e)
        {
            InvalidateInterpolationMethod();
            DrawScene();
        }

        private void radioButtonCamFixed_CheckedChanged(object sender, EventArgs e)
        {
            painter.CameraPosition = new Vector3(
                (float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);
            painter.CameraTarget = new Vector3(0);

            animation.CameraType = Animation.CamType.Fixed;
        }

        private void radioButtonCamTracking_CheckedChanged(object sender, EventArgs e)
        {
            painter.CameraPosition = new Vector3(
                (float)numericUpDownCamX.Value, (float)numericUpDownCamY.Value, (float)numericUpDownCamZ.Value);

            animation.CameraType = Animation.CamType.Tracking;
        }

        private void radioButtonCamTpp_CheckedChanged(object sender, EventArgs e)
        {
            animation.CameraType = Animation.CamType.TPP;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.I)
            {
                pressedKeys |= KeyboardState.pressedI;
            }
            if (e.KeyCode == Keys.K)
            {
                pressedKeys |= KeyboardState.pressedK;
            }
            if (e.KeyCode == Keys.J)
            {
                pressedKeys |= KeyboardState.pressedJ;
            }
            if (e.KeyCode == Keys.L)
            {
                pressedKeys |= KeyboardState.pressedL;
            }
            if (!animationRunning)
            {
                if (e.KeyCode == Keys.W)
                {
                    pressedKeys |= KeyboardState.pressedW;
                }
                if (e.KeyCode == Keys.S)
                {
                    pressedKeys |= KeyboardState.pressedS;
                }
                if (e.KeyCode == Keys.A)
                {
                    pressedKeys |= KeyboardState.pressedA;
                }
                if (e.KeyCode == Keys.D)
                {
                    pressedKeys |= KeyboardState.pressedD;
                }
            }

            animation.HandleInput(pressedKeys);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.I)
            {
                pressedKeys &= ~KeyboardState.pressedI;
            }
            if (e.KeyCode == Keys.K)
            {
                pressedKeys &= ~KeyboardState.pressedK;
            }
            if (e.KeyCode == Keys.J)
            {
                pressedKeys &= ~KeyboardState.pressedJ;
            }
            if (e.KeyCode == Keys.L)
            {
                pressedKeys &= ~KeyboardState.pressedL;
            }

            if (animationRunning)
                return;
            if (e.KeyCode == Keys.W)
            {
                pressedKeys &= ~KeyboardState.pressedW;
            }
            if (e.KeyCode == Keys.S)
            {
                pressedKeys &= ~KeyboardState.pressedS;
            }
            if (e.KeyCode == Keys.A)
            {
                pressedKeys &= ~KeyboardState.pressedA;
            }
            if (e.KeyCode == Keys.D)
            {
                pressedKeys &= ~KeyboardState.pressedD;
            }
        }

        private void buttonStartInteractive_Click(object sender, EventArgs e)
        {
            timer.Start();
            animationRunning = false;
            buttonAnimationStart.Enabled = false;
            buttonPauseAnimation.Enabled = false;
        }

        private void buttonPauseInteractive_Click(object sender, EventArgs e)
        {
            timer.Stop();
            buttonAnimationStart.Enabled = true;
            buttonPauseAnimation.Enabled = true;
        }
    }
}