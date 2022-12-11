using System.Diagnostics;
using System.Numerics;

namespace Graphics3D
{
    public partial class Form1 : Form
    {
        static readonly System.Windows.Forms.Timer timer = new();
        const float scale = 100;

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
            painter = new Painter(canvasBitmap.Width, canvasBitmap.Height, scale);

            painter.DrawXAxis(canvasBitmap);
            painter.DrawYAxis(canvasBitmap);
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
                shapes.Add(new Shape(faces));

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

            ClearCanvas();
            //Rotate(selectedShape, 0.1f);
            Rotate(selectedShape, new Vector3(0, 1, 0), 0.1f);

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
            painter.DrawXAxis(canvasBitmap);
            painter.DrawYAxis(canvasBitmap);
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
            foreach(var f in shape.Faces)
            {
                for(int i = 0; i < f.Vertices.Count; i++)
                {
                    Vector3 locationDiff;
                    if (direction == MoveDirection.Right)
                    {
                        locationDiff = new Vector3(0.1f, 0, 0);
                    }
                    else if(direction == MoveDirection.Left)
                    {
                        locationDiff = new Vector3(-0.1f, 0, 0);
                    }
                    else if(direction == MoveDirection.Up)
                    {
                        locationDiff = new Vector3(0, 0.1f, 0);
                    }
                    else // direction == MoveDirection.Down
                    {
                        locationDiff = new Vector3(0, -0.1f, 0);;
                    }

                    f.Vertices[i].Location += locationDiff;
                }
            }
        }

        private void Rotate(Shape shape, float radians)
        {
            Matrix4x4 Rx = Matrix4x4.CreateRotationX(radians);

            foreach (var f in shape.Faces)
            {
                for(int i = 0; i < f.Vertices.Count; i++)
                {
                    f.Vertices[i].Location = Vector3.Transform(f.Vertices[i].Location, Rx);
                }
            }
        }

        private void Rotate(Shape shape, Vector3 point, float radians)
        {
            Matrix4x4 Rx = Matrix4x4.CreateRotationX(radians);
            Matrix4x4 T = Matrix4x4.CreateTranslation(point);
            Matrix4x4 TRev = Matrix4x4.CreateTranslation(-point);
            Matrix4x4 M = TRev * Rx * T;

            foreach (var f in shape.Faces)
            {
                for (int i = 0; i < f.Vertices.Count; i++)
                {
                    f.Vertices[i].Location = Vector3.Transform(f.Vertices[i].Location, M);
                }
            }
        }
    }
}