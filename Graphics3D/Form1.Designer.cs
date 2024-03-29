﻿namespace Graphics3D
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFps = new System.Windows.Forms.TextBox();
            this.buttonPauseInteractive = new System.Windows.Forms.Button();
            this.buttonStartInteractive = new System.Windows.Forms.Button();
            this.buttonPauseAnimation = new System.Windows.Forms.Button();
            this.buttonAnimationStart = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxCoordSystem = new System.Windows.Forms.CheckBox();
            this.checkBoxSwinging = new System.Windows.Forms.CheckBox();
            this.groupBoxInterpolationMethod = new System.Windows.Forms.GroupBox();
            this.radioButtonConst = new System.Windows.Forms.RadioButton();
            this.radioButtonColors = new System.Windows.Forms.RadioButton();
            this.radioButtonNormals = new System.Windows.Forms.RadioButton();
            this.groupBoxCamMode = new System.Windows.Forms.GroupBox();
            this.radioButtonCamTpp = new System.Windows.Forms.RadioButton();
            this.radioButtonCamFixed = new System.Windows.Forms.RadioButton();
            this.radioButtonCamTracking = new System.Windows.Forms.RadioButton();
            this.numericUpDownCamZ = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCamY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCamX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarFov = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarScale = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBoxInterpolationMethod.SuspendLayout();
            this.groupBoxCamMode.SuspendLayout();
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
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxFps);
            this.panel1.Controls.Add(this.buttonPauseInteractive);
            this.panel1.Controls.Add(this.buttonStartInteractive);
            this.panel1.Controls.Add(this.buttonPauseAnimation);
            this.panel1.Controls.Add(this.buttonAnimationStart);
            this.panel1.Controls.Add(this.buttonLoad);
            this.panel1.Location = new System.Drawing.Point(123, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 74);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(522, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "FPS";
            // 
            // textBoxFps
            // 
            this.textBoxFps.Location = new System.Drawing.Point(522, 42);
            this.textBoxFps.Name = "textBoxFps";
            this.textBoxFps.ReadOnly = true;
            this.textBoxFps.Size = new System.Drawing.Size(63, 23);
            this.textBoxFps.TabIndex = 7;
            // 
            // buttonPauseInteractive
            // 
            this.buttonPauseInteractive.Location = new System.Drawing.Point(369, 17);
            this.buttonPauseInteractive.Name = "buttonPauseInteractive";
            this.buttonPauseInteractive.Size = new System.Drawing.Size(76, 48);
            this.buttonPauseInteractive.TabIndex = 6;
            this.buttonPauseInteractive.Text = "Pause interactive";
            this.buttonPauseInteractive.UseVisualStyleBackColor = true;
            this.buttonPauseInteractive.Click += new System.EventHandler(this.buttonPauseInteractive_Click);
            // 
            // buttonStartInteractive
            // 
            this.buttonStartInteractive.Location = new System.Drawing.Point(288, 17);
            this.buttonStartInteractive.Name = "buttonStartInteractive";
            this.buttonStartInteractive.Size = new System.Drawing.Size(75, 48);
            this.buttonStartInteractive.TabIndex = 5;
            this.buttonStartInteractive.Text = "Start interactive";
            this.buttonStartInteractive.UseVisualStyleBackColor = true;
            this.buttonStartInteractive.Click += new System.EventHandler(this.buttonStartInteractive_Click);
            // 
            // buttonPauseAnimation
            // 
            this.buttonPauseAnimation.Location = new System.Drawing.Point(198, 17);
            this.buttonPauseAnimation.Name = "buttonPauseAnimation";
            this.buttonPauseAnimation.Size = new System.Drawing.Size(84, 48);
            this.buttonPauseAnimation.TabIndex = 4;
            this.buttonPauseAnimation.Text = "Pause animation";
            this.buttonPauseAnimation.UseVisualStyleBackColor = true;
            this.buttonPauseAnimation.Click += new System.EventHandler(this.buttonPauseAnimation_Click);
            // 
            // buttonAnimationStart
            // 
            this.buttonAnimationStart.Location = new System.Drawing.Point(107, 17);
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
            this.buttonLoad.Location = new System.Drawing.Point(24, 17);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(77, 48);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load from file";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBoxCoordSystem);
            this.panel2.Controls.Add(this.checkBoxSwinging);
            this.panel2.Controls.Add(this.groupBoxInterpolationMethod);
            this.panel2.Controls.Add(this.groupBoxCamMode);
            this.panel2.Controls.Add(this.numericUpDownCamZ);
            this.panel2.Controls.Add(this.numericUpDownCamY);
            this.panel2.Controls.Add(this.numericUpDownCamX);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.trackBarFov);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.trackBarScale);
            this.panel2.Location = new System.Drawing.Point(3, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(114, 531);
            this.panel2.TabIndex = 4;
            // 
            // checkBoxCoordSystem
            // 
            this.checkBoxCoordSystem.AutoSize = true;
            this.checkBoxCoordSystem.Checked = true;
            this.checkBoxCoordSystem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCoordSystem.Location = new System.Drawing.Point(11, 488);
            this.checkBoxCoordSystem.Name = "checkBoxCoordSystem";
            this.checkBoxCoordSystem.Size = new System.Drawing.Size(100, 19);
            this.checkBoxCoordSystem.TabIndex = 39;
            this.checkBoxCoordSystem.Text = "coord. system";
            this.checkBoxCoordSystem.UseVisualStyleBackColor = true;
            // 
            // checkBoxSwinging
            // 
            this.checkBoxSwinging.AutoSize = true;
            this.checkBoxSwinging.Location = new System.Drawing.Point(11, 463);
            this.checkBoxSwinging.Name = "checkBoxSwinging";
            this.checkBoxSwinging.Size = new System.Drawing.Size(74, 19);
            this.checkBoxSwinging.TabIndex = 38;
            this.checkBoxSwinging.Text = "swinging";
            this.checkBoxSwinging.UseVisualStyleBackColor = true;
            // 
            // groupBoxInterpolationMethod
            // 
            this.groupBoxInterpolationMethod.Controls.Add(this.radioButtonConst);
            this.groupBoxInterpolationMethod.Controls.Add(this.radioButtonColors);
            this.groupBoxInterpolationMethod.Controls.Add(this.radioButtonNormals);
            this.groupBoxInterpolationMethod.Location = new System.Drawing.Point(11, 328);
            this.groupBoxInterpolationMethod.Name = "groupBoxInterpolationMethod";
            this.groupBoxInterpolationMethod.Size = new System.Drawing.Size(90, 120);
            this.groupBoxInterpolationMethod.TabIndex = 37;
            this.groupBoxInterpolationMethod.TabStop = false;
            this.groupBoxInterpolationMethod.Text = "Interpolation method";
            // 
            // radioButtonConst
            // 
            this.radioButtonConst.AutoSize = true;
            this.radioButtonConst.Location = new System.Drawing.Point(6, 86);
            this.radioButtonConst.Name = "radioButtonConst";
            this.radioButtonConst.Size = new System.Drawing.Size(71, 19);
            this.radioButtonConst.TabIndex = 33;
            this.radioButtonConst.Text = "constant";
            this.radioButtonConst.UseVisualStyleBackColor = true;
            this.radioButtonConst.CheckedChanged += new System.EventHandler(this.radioButtonConst_CheckedChanged);
            // 
            // radioButtonColors
            // 
            this.radioButtonColors.AutoSize = true;
            this.radioButtonColors.Location = new System.Drawing.Point(6, 61);
            this.radioButtonColors.Name = "radioButtonColors";
            this.radioButtonColors.Size = new System.Drawing.Size(57, 19);
            this.radioButtonColors.TabIndex = 32;
            this.radioButtonColors.Text = "colors";
            this.radioButtonColors.UseVisualStyleBackColor = true;
            this.radioButtonColors.CheckedChanged += new System.EventHandler(this.radioButtonColors_CheckedChanged);
            // 
            // radioButtonNormals
            // 
            this.radioButtonNormals.AutoSize = true;
            this.radioButtonNormals.Checked = true;
            this.radioButtonNormals.Location = new System.Drawing.Point(6, 36);
            this.radioButtonNormals.Name = "radioButtonNormals";
            this.radioButtonNormals.Size = new System.Drawing.Size(68, 19);
            this.radioButtonNormals.TabIndex = 31;
            this.radioButtonNormals.TabStop = true;
            this.radioButtonNormals.Text = "normals";
            this.radioButtonNormals.UseVisualStyleBackColor = true;
            this.radioButtonNormals.CheckedChanged += new System.EventHandler(this.radioButtonNormals_CheckedChanged);
            // 
            // groupBoxCamMode
            // 
            this.groupBoxCamMode.Controls.Add(this.radioButtonCamTpp);
            this.groupBoxCamMode.Controls.Add(this.radioButtonCamFixed);
            this.groupBoxCamMode.Controls.Add(this.radioButtonCamTracking);
            this.groupBoxCamMode.Location = new System.Drawing.Point(9, 3);
            this.groupBoxCamMode.Name = "groupBoxCamMode";
            this.groupBoxCamMode.Size = new System.Drawing.Size(99, 103);
            this.groupBoxCamMode.TabIndex = 36;
            this.groupBoxCamMode.TabStop = false;
            this.groupBoxCamMode.Text = "Camera mode";
            // 
            // radioButtonCamTpp
            // 
            this.radioButtonCamTpp.AutoSize = true;
            this.radioButtonCamTpp.Location = new System.Drawing.Point(8, 72);
            this.radioButtonCamTpp.Name = "radioButtonCamTpp";
            this.radioButtonCamTpp.Size = new System.Drawing.Size(45, 19);
            this.radioButtonCamTpp.TabIndex = 39;
            this.radioButtonCamTpp.Text = "TPP";
            this.radioButtonCamTpp.UseVisualStyleBackColor = true;
            this.radioButtonCamTpp.CheckedChanged += new System.EventHandler(this.radioButtonCamTpp_CheckedChanged);
            // 
            // radioButtonCamFixed
            // 
            this.radioButtonCamFixed.AutoSize = true;
            this.radioButtonCamFixed.Checked = true;
            this.radioButtonCamFixed.Location = new System.Drawing.Point(8, 22);
            this.radioButtonCamFixed.Name = "radioButtonCamFixed";
            this.radioButtonCamFixed.Size = new System.Drawing.Size(51, 19);
            this.radioButtonCamFixed.TabIndex = 37;
            this.radioButtonCamFixed.TabStop = true;
            this.radioButtonCamFixed.Text = "fixed";
            this.radioButtonCamFixed.UseVisualStyleBackColor = true;
            this.radioButtonCamFixed.CheckedChanged += new System.EventHandler(this.radioButtonCamFixed_CheckedChanged);
            // 
            // radioButtonCamTracking
            // 
            this.radioButtonCamTracking.AutoSize = true;
            this.radioButtonCamTracking.Location = new System.Drawing.Point(8, 47);
            this.radioButtonCamTracking.Name = "radioButtonCamTracking";
            this.radioButtonCamTracking.Size = new System.Drawing.Size(68, 19);
            this.radioButtonCamTracking.TabIndex = 38;
            this.radioButtonCamTracking.Text = "tracking";
            this.radioButtonCamTracking.UseVisualStyleBackColor = true;
            this.radioButtonCamTracking.CheckedChanged += new System.EventHandler(this.radioButtonCamTracking_CheckedChanged);
            // 
            // numericUpDownCamZ
            // 
            this.numericUpDownCamZ.DecimalPlaces = 2;
            this.numericUpDownCamZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownCamZ.Location = new System.Drawing.Point(30, 300);
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
            4,
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
            this.numericUpDownCamY.Location = new System.Drawing.Point(30, 273);
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
            4,
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
            this.numericUpDownCamX.Location = new System.Drawing.Point(28, 241);
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
            4,
            0,
            0,
            0});
            this.numericUpDownCamX.ValueChanged += new System.EventHandler(this.numericUpDownCamX_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "z";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 15);
            this.label5.TabIndex = 24;
            this.label5.Text = "y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Field of view";
            // 
            // trackBarFov
            // 
            this.trackBarFov.Location = new System.Drawing.Point(9, 192);
            this.trackBarFov.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBarFov.Maximum = 120;
            this.trackBarFov.Minimum = 30;
            this.trackBarFov.Name = "trackBarFov";
            this.trackBarFov.Size = new System.Drawing.Size(97, 45);
            this.trackBarFov.TabIndex = 18;
            this.trackBarFov.Value = 80;
            this.trackBarFov.Scroll += new System.EventHandler(this.trackBarFov_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Zoom";
            // 
            // trackBarScale
            // 
            this.trackBarScale.Location = new System.Drawing.Point(9, 127);
            this.trackBarScale.Maximum = 100;
            this.trackBarScale.Name = "trackBarScale";
            this.trackBarScale.Size = new System.Drawing.Size(104, 45);
            this.trackBarScale.TabIndex = 15;
            this.trackBarScale.Value = 20;
            this.trackBarScale.Scroll += new System.EventHandler(this.trackBarScale_Scroll);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 617);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Graphics 3D";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBoxInterpolationMethod.ResumeLayout(false);
            this.groupBoxInterpolationMethod.PerformLayout();
            this.groupBoxCamMode.ResumeLayout(false);
            this.groupBoxCamMode.PerformLayout();
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
        private TrackBar trackBarScale;
        private Label label2;
        private Label label3;
        private TrackBar trackBarFov;
        private NumericUpDown numericUpDownCamZ;
        private NumericUpDown numericUpDownCamY;
        private NumericUpDown numericUpDownCamX;
        private Label label6;
        private Label label5;
        private Label label4;
        private RadioButton radioButtonColors;
        private RadioButton radioButtonNormals;
        private RadioButton radioButtonCamTpp;
        private RadioButton radioButtonCamTracking;
        private RadioButton radioButtonCamFixed;
        private GroupBox groupBoxInterpolationMethod;
        private GroupBox groupBoxCamMode;
        private RadioButton radioButtonConst;
        private CheckBox checkBoxSwinging;
        private Button buttonPauseInteractive;
        private Button buttonStartInteractive;
        private Label label1;
        private TextBox textBoxFps;
        private CheckBox checkBoxCoordSystem;
    }
}