namespace Graphics3D
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonPauseAnimation = new System.Windows.Forms.Button();
            this.buttonAnimationStart = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numericUpDownCamZ = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCamY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCamX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarFov = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarScale = new System.Windows.Forms.TrackBar();
            this.buttonSelectShape = new System.Windows.Forms.Button();
            this.textBoxSelectShape = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBoxBackFaces = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCamZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCamY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCamX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFov)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScale)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.canvas, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(866, 617);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // canvas
            // 
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(123, 83);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(740, 531);
            this.canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonPauseAnimation);
            this.panel1.Controls.Add(this.buttonAnimationStart);
            this.panel1.Controls.Add(this.buttonLoad);
            this.panel1.Location = new System.Drawing.Point(123, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 74);
            this.panel1.TabIndex = 3;
            // 
            // buttonPauseAnimation
            // 
            this.buttonPauseAnimation.Location = new System.Drawing.Point(177, 17);
            this.buttonPauseAnimation.Name = "buttonPauseAnimation";
            this.buttonPauseAnimation.Size = new System.Drawing.Size(84, 48);
            this.buttonPauseAnimation.TabIndex = 4;
            this.buttonPauseAnimation.Text = "Pause animation";
            this.buttonPauseAnimation.UseVisualStyleBackColor = true;
            this.buttonPauseAnimation.Click += new System.EventHandler(this.buttonPauseAnimation_Click);
            // 
            // buttonAnimationStart
            // 
            this.buttonAnimationStart.Location = new System.Drawing.Point(86, 17);
            this.buttonAnimationStart.Name = "buttonAnimationStart";
            this.buttonAnimationStart.Size = new System.Drawing.Size(85, 48);
            this.buttonAnimationStart.TabIndex = 3;
            this.buttonAnimationStart.Text = "Start animation";
            this.buttonAnimationStart.UseVisualStyleBackColor = true;
            this.buttonAnimationStart.Click += new System.EventHandler(this.buttonAnimationStart_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLoad.Location = new System.Drawing.Point(3, 17);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(77, 48);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load from file";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBoxBackFaces);
            this.panel2.Controls.Add(this.numericUpDownCamZ);
            this.panel2.Controls.Add(this.numericUpDownCamY);
            this.panel2.Controls.Add(this.numericUpDownCamX);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.trackBarFov);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.trackBarScale);
            this.panel2.Controls.Add(this.buttonSelectShape);
            this.panel2.Controls.Add(this.textBoxSelectShape);
            this.panel2.Location = new System.Drawing.Point(3, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(114, 531);
            this.panel2.TabIndex = 4;
            // 
            // numericUpDownCamZ
            // 
            this.numericUpDownCamZ.DecimalPlaces = 2;
            this.numericUpDownCamZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownCamZ.Location = new System.Drawing.Point(23, 307);
            this.numericUpDownCamZ.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDownCamZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownCamZ.Name = "numericUpDownCamZ";
            this.numericUpDownCamZ.Size = new System.Drawing.Size(78, 23);
            this.numericUpDownCamZ.TabIndex = 28;
            this.numericUpDownCamZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCamZ.ValueChanged += new System.EventHandler(this.numericUpDownCamZ_ValueChanged);
            // 
            // numericUpDownCamY
            // 
            this.numericUpDownCamY.DecimalPlaces = 2;
            this.numericUpDownCamY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownCamY.Location = new System.Drawing.Point(23, 267);
            this.numericUpDownCamY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDownCamY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownCamY.Name = "numericUpDownCamY";
            this.numericUpDownCamY.Size = new System.Drawing.Size(78, 23);
            this.numericUpDownCamY.TabIndex = 27;
            this.numericUpDownCamY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCamY.ValueChanged += new System.EventHandler(this.numericUpDownCamY_ValueChanged);
            // 
            // numericUpDownCamX
            // 
            this.numericUpDownCamX.DecimalPlaces = 2;
            this.numericUpDownCamX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownCamX.Location = new System.Drawing.Point(23, 232);
            this.numericUpDownCamX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDownCamX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownCamX.Name = "numericUpDownCamX";
            this.numericUpDownCamX.Size = new System.Drawing.Size(78, 23);
            this.numericUpDownCamX.TabIndex = 26;
            this.numericUpDownCamX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCamX.ValueChanged += new System.EventHandler(this.numericUpDownCamX_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "z";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 267);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 15);
            this.label5.TabIndex = 24;
            this.label5.Text = "y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Field of view";
            // 
            // trackBarFov
            // 
            this.trackBarFov.Location = new System.Drawing.Point(4, 183);
            this.trackBarFov.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBarFov.Maximum = 120;
            this.trackBarFov.Minimum = 30;
            this.trackBarFov.Name = "trackBarFov";
            this.trackBarFov.Size = new System.Drawing.Size(97, 45);
            this.trackBarFov.TabIndex = 18;
            this.trackBarFov.Value = 100;
            this.trackBarFov.Scroll += new System.EventHandler(this.trackBarFov_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Zoom";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Shape selection";
            // 
            // trackBarScale
            // 
            this.trackBarScale.Location = new System.Drawing.Point(3, 108);
            this.trackBarScale.Maximum = 100;
            this.trackBarScale.Name = "trackBarScale";
            this.trackBarScale.Size = new System.Drawing.Size(104, 45);
            this.trackBarScale.TabIndex = 15;
            this.trackBarScale.Value = 20;
            this.trackBarScale.Scroll += new System.EventHandler(this.trackBarScale_Scroll);
            // 
            // buttonSelectShape
            // 
            this.buttonSelectShape.Location = new System.Drawing.Point(55, 46);
            this.buttonSelectShape.Name = "buttonSelectShape";
            this.buttonSelectShape.Size = new System.Drawing.Size(55, 23);
            this.buttonSelectShape.TabIndex = 14;
            this.buttonSelectShape.Text = "Select shape";
            this.buttonSelectShape.UseVisualStyleBackColor = true;
            this.buttonSelectShape.Click += new System.EventHandler(this.buttonSelectShape_Click);
            // 
            // textBoxSelectShape
            // 
            this.textBoxSelectShape.Location = new System.Drawing.Point(3, 46);
            this.textBoxSelectShape.Name = "textBoxSelectShape";
            this.textBoxSelectShape.Size = new System.Drawing.Size(46, 23);
            this.textBoxSelectShape.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonClear);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(114, 74);
            this.panel3.TabIndex = 5;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(11, 17);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(72, 48);
            this.buttonClear.TabIndex = 0;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // checkBoxBackFaces
            // 
            this.checkBoxBackFaces.AutoSize = true;
            this.checkBoxBackFaces.Location = new System.Drawing.Point(4, 351);
            this.checkBoxBackFaces.Name = "checkBoxBackFaces";
            this.checkBoxBackFaces.Size = new System.Drawing.Size(105, 19);
            this.checkBoxBackFaces.TabIndex = 29;
            this.checkBoxBackFaces.Text = "Cull back faces";
            this.checkBoxBackFaces.UseVisualStyleBackColor = true;
            this.checkBoxBackFaces.CheckedChanged += new System.EventHandler(this.checkBoxBackFaces_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 617);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Filler";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCamZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCamY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCamX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFov)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScale)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox canvas;
        private Button buttonLoad;
        private Panel panel1;
        private Panel panel2;
        private Button buttonPauseAnimation;
        private Button buttonAnimationStart;
        private Panel panel3;
        private Button buttonClear;
        private Button buttonSelectShape;
        private TextBox textBoxSelectShape;
        private TrackBar trackBarScale;
        private Label label2;
        private Label label1;
        private Label label3;
        private TrackBar trackBarFov;
        private NumericUpDown numericUpDownCamZ;
        private NumericUpDown numericUpDownCamY;
        private NumericUpDown numericUpDownCamX;
        private Label label6;
        private Label label5;
        private Label label4;
        private CheckBox checkBoxBackFaces;
    }
}