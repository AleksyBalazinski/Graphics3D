using System.Numerics;

namespace Graphics3D
{
    public partial class Form1 : Form
    {
        static readonly System.Windows.Forms.Timer timer = new();
        uint ticks = 0;

        readonly Painter painter;
        readonly DirectBitmap canvasBitmap;
        readonly List<Shape> shapes;
        Shape? selectedShape;

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
            painter = new Painter(canvasBitmap.Width, canvasBitmap.Height, 100);

            painter.DrawCoordinateSystem(canvasBitmap);
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
                shapes.Add(new Shape(faces, shapes.Count));

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
            if (selectedShape == null)
                return;

            ticks++;
            ClearCanvas();
            selectedShape.Rotate(0.1f * ticks);

            DrawScene();
        }

        private void DrawScene()
        {
            foreach (var shape in shapes)
            {
                shape.DrawMesh(painter, canvasBitmap);
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

            using var g = Graphics.FromImage(canvasBitmap.Bitmap);
            g.Clear(Color.White);

            DrawScene();
        }

        private void ClearCanvas()
        {
            using var g = Graphics.FromImage(canvasBitmap.Bitmap);
            g.Clear(Color.White);

            painter.DrawCoordinateSystem(canvasBitmap);
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

            if (e.KeyCode == Keys.D)
                MoveShape(selectedShape, MoveDirection.Right);
            if (e.KeyCode == Keys.A)
                MoveShape(selectedShape, MoveDirection.Left);
            if (e.KeyCode == Keys.S)
                MoveShape(selectedShape, MoveDirection.Down);
            if (e.KeyCode == Keys.W)
                MoveShape(selectedShape, MoveDirection.Up);

            using Graphics g = Graphics.FromImage(canvasBitmap.Bitmap);
            g.Clear(Color.White);
            DrawScene();
        }

        private void MoveShape(Shape shape, MoveDirection direction)
        {
            foreach (var f in shape.Faces)
            {
                for (int i = 0; i < f.Vertices.Count; i++)
                {
                    Vector3 locationDiff;
                    if (direction == MoveDirection.Right)
                    {
                        locationDiff = new Vector3(0.1f, 0, 0);
                    }
                    else if (direction == MoveDirection.Left)
                    {
                        locationDiff = new Vector3(-0.1f, 0, 0);
                    }
                    else if (direction == MoveDirection.Up)
                    {
                        locationDiff = new Vector3(0, 0.1f, 0);
                    }
                    else // direction == MoveDirection.Down
                    {
                        locationDiff = new Vector3(0, -0.1f, 0);
                    }

                    f.Vertices[i].Location += locationDiff;
                }
            }
        }

        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            painter.Zoom = trackBarScale.Value * 5;

            ClearCanvas();
            DrawScene();
        }

        private void trackBarFov_Scroll(object sender, EventArgs e)
        {
            painter.FieldOfView = trackBarFov.Value * MathF.PI / 180.0f;

            ClearCanvas();
            DrawScene();
        }

        private void numericUpDownCamX_ValueChanged(object sender, EventArgs e)
        {
            painter.CameraPosition.X = (float)numericUpDownCamX.Value;

            ClearCanvas();
            DrawScene();
        }

        private void numericUpDownCamY_ValueChanged(object sender, EventArgs e)
        {
            painter.CameraPosition.Y = (float)numericUpDownCamY.Value;

            ClearCanvas();
            DrawScene();
        }

        private void numericUpDownCamZ_ValueChanged(object sender, EventArgs e)
        {
            painter.CameraPosition.Z = (float)numericUpDownCamZ.Value;

            ClearCanvas();
            DrawScene();
        }

        private void checkBoxBackFaces_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBackFaces.Checked)
            {
                painter.CullBackFaces = true;
            }
            else
            {
                painter.CullBackFaces = false;
            }
        }
    }
}