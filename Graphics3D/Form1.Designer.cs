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
            this.buttonPauseAnimation = new System.Windows.Forms.Button();
            this.buttonAnimationStart = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonSelectShape = new System.Windows.Forms.Button();
            this.textBoxSelectShape = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panel2.Controls.Add(this.buttonSelectShape);
            this.panel2.Controls.Add(this.textBoxSelectShape);
            this.panel2.Location = new System.Drawing.Point(3, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(114, 531);
            this.panel2.TabIndex = 4;
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
    }
}