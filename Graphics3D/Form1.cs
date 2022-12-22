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
        Shape? selectedShape;
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

            lightAnimator = new LightAnimator(new Vector3(initialRadius, 0, 20), 5);

            painter.DrawCoordinateSystem();
            InitScene();
        }

        private void InitScene()
        {
            string pathToTorusObj = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\assets\torus-n.obj"));
            List<Face> faces = ObjFileReader.Read(pathToTorusObj);
            shapes.Add(new Shape(faces, shapes.Count, new RGB(random.NextSingle(), random.NextSingle(), random.NextSingle())));

            faces = ObjFileReader.Read(pathToTorusObj);
            shapes.Add(new Shape(faces, shapes.Count, new RGB(random.NextSingle(), random.NextSingle(), random.NextSingle())));
            MoveShape(shapes[0], MoveDirection.Left, 0.8f);

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
                shapes.Add(new Shape(faces, shapes.Count, new RGB(random.NextSingle(), random.NextSingle(), random.NextSingle())));

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

            float deg = 0.02f;
            for (int i = 0; i < shapes.Count; i++)
            {
                shapes[i].Rotate(deg * (i + 1) * ticks);
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
            painter.DrawCoordinateSystem();
        }

        private void buttonSelectShape_Click(object sender, EventArgs e)
        {
            string input = textBoxSelectShape.Text;
            bool isNumeric = int.TryParse(input, out int shapeId);
            if (!isNumeric || shapeId >= shapes.Count)
            {
                selectedShape = null;
                return;
            }

            selectedShape = shapes[shapeId];
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedShape == null)
                return;

            float amount = 0.1f;

            if (e.KeyCode == Keys.D)
                MoveShape(selectedShape, MoveDirection.Right, amount);
            if (e.KeyCode == Keys.A)
                MoveShape(selectedShape, MoveDirection.Left, amount);
            if (e.KeyCode == Keys.S)
                MoveShape(selectedShape, MoveDirection.Down, amount);
            if (e.KeyCode == Keys.W)
                MoveShape(selectedShape, MoveDirection.Up, amount);

            using Graphics g = Graphics.FromImage(canvasBitmap.Bitmap);
            g.Clear(Color.White);
            DrawScene();
        }

        private void MoveShape(Shape shape, MoveDirection direction, float amount)
        {
            foreach (var f in shape.Faces)
            {
                for (int i = 0; i < f.Vertices.Count; i++)
                {
                    Vector3 locationDiff;
                    if (direction == MoveDirection.Right)
                    {
                        locationDiff = new Vector3(amount, 0, 0);
                    }
                    else if (direction == MoveDirection.Left)
                    {
                        locationDiff = new Vector3(-amount, 0, 0);
                    }
                    else if (direction == MoveDirection.Up)
                    {
                        locationDiff = new Vector3(0, amount, 0);
                    }
                    else // direction == MoveDirection.Down
                    {
                        locationDiff = new Vector3(0, -amount, 0);
                    }

                    f.Vertices[i].Location += locationDiff;
                }
            }
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

        private void checkBoxBackFaces_CheckedChanged(object sender, EventArgs e)
        {
            /*if (checkBoxBackFaces.Checked)
            {
                rasterizer.CullBackFaces = true;
            }
            else
            {
                rasterizer.CullBackFaces = false;
            }*/
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