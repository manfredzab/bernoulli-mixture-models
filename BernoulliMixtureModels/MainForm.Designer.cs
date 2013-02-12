namespace BernoulliMixtureModels
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLoadData = new System.Windows.Forms.Button();
            this.pictureBoxDigit = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxBinarization = new System.Windows.Forms.CheckBox();
            this.checkBoxStretch = new System.Windows.Forms.CheckBox();
            this.labelDigit = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.buttonEMStep = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clusterImage3 = new System.Windows.Forms.PictureBox();
            this.clusterImage4 = new System.Windows.Forms.PictureBox();
            this.clusterImage2 = new System.Windows.Forms.PictureBox();
            this.clusterImage1 = new System.Windows.Forms.PictureBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelRecognisedDigit = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonRecognizeDigit = new System.Windows.Forms.Button();
            this.panelHandwriting = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDigit)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clusterImage3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clusterImage4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clusterImage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clusterImage1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoadData
            // 
            this.buttonLoadData.Location = new System.Drawing.Point(6, 19);
            this.buttonLoadData.Name = "buttonLoadData";
            this.buttonLoadData.Size = new System.Drawing.Size(140, 23);
            this.buttonLoadData.TabIndex = 0;
            this.buttonLoadData.Text = "Load MNIST training data";
            this.buttonLoadData.UseVisualStyleBackColor = true;
            this.buttonLoadData.Click += new System.EventHandler(this.buttonLoadData_Click);
            // 
            // pictureBoxDigit
            // 
            this.pictureBoxDigit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxDigit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxDigit.Enabled = false;
            this.pictureBoxDigit.Location = new System.Drawing.Point(6, 57);
            this.pictureBoxDigit.Name = "pictureBoxDigit";
            this.pictureBoxDigit.Size = new System.Drawing.Size(140, 140);
            this.pictureBoxDigit.TabIndex = 1;
            this.pictureBoxDigit.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(750, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(94, 17);
            this.labelStatus.Text = "Data not loaded.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxBinarization);
            this.groupBox1.Controls.Add(this.checkBoxStretch);
            this.groupBox1.Controls.Add(this.labelDigit);
            this.groupBox1.Controls.Add(this.buttonNext);
            this.groupBox1.Controls.Add(this.buttonPrev);
            this.groupBox1.Controls.Add(this.buttonLoadData);
            this.groupBox1.Controls.Add(this.pictureBoxDigit);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 282);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training data:";
            // 
            // checkBoxBinarization
            // 
            this.checkBoxBinarization.AutoSize = true;
            this.checkBoxBinarization.Enabled = false;
            this.checkBoxBinarization.Location = new System.Drawing.Point(6, 255);
            this.checkBoxBinarization.Name = "checkBoxBinarization";
            this.checkBoxBinarization.Size = new System.Drawing.Size(129, 17);
            this.checkBoxBinarization.TabIndex = 8;
            this.checkBoxBinarization.Text = "Show binarized image";
            this.checkBoxBinarization.UseVisualStyleBackColor = true;
            this.checkBoxBinarization.CheckedChanged += new System.EventHandler(this.checkBoxBinarization_CheckedChanged);
            // 
            // checkBoxStretch
            // 
            this.checkBoxStretch.AutoSize = true;
            this.checkBoxStretch.Enabled = false;
            this.checkBoxStretch.Location = new System.Drawing.Point(6, 235);
            this.checkBoxStretch.Name = "checkBoxStretch";
            this.checkBoxStretch.Size = new System.Drawing.Size(122, 17);
            this.checkBoxStretch.TabIndex = 4;
            this.checkBoxStretch.Text = "Enable image strech";
            this.checkBoxStretch.UseVisualStyleBackColor = true;
            this.checkBoxStretch.CheckedChanged += new System.EventHandler(this.checkBoxStretch_CheckedChanged);
            // 
            // labelDigit
            // 
            this.labelDigit.Enabled = false;
            this.labelDigit.Location = new System.Drawing.Point(60, 208);
            this.labelDigit.Name = "labelDigit";
            this.labelDigit.Size = new System.Drawing.Size(30, 18);
            this.labelDigit.TabIndex = 4;
            this.labelDigit.Text = "-";
            this.labelDigit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonNext
            // 
            this.buttonNext.Enabled = false;
            this.buttonNext.Location = new System.Drawing.Point(96, 203);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(50, 23);
            this.buttonNext.TabIndex = 5;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Enabled = false;
            this.buttonPrev.Location = new System.Drawing.Point(4, 203);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(50, 23);
            this.buttonPrev.TabIndex = 4;
            this.buttonPrev.Text = "<";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // buttonEMStep
            // 
            this.buttonEMStep.Enabled = false;
            this.buttonEMStep.Location = new System.Drawing.Point(12, 300);
            this.buttonEMStep.Name = "buttonEMStep";
            this.buttonEMStep.Size = new System.Drawing.Size(104, 23);
            this.buttonEMStep.TabIndex = 5;
            this.buttonEMStep.Text = "Start EM clustering";
            this.buttonEMStep.UseVisualStyleBackColor = true;
            this.buttonEMStep.Click += new System.EventHandler(this.buttonEMStep_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clusterImage3);
            this.groupBox2.Controls.Add(this.clusterImage4);
            this.groupBox2.Controls.Add(this.clusterImage2);
            this.groupBox2.Controls.Add(this.clusterImage1);
            this.groupBox2.Location = new System.Drawing.Point(172, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 311);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Clusters:";
            // 
            // clusterImage3
            // 
            this.clusterImage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.clusterImage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.clusterImage3.Enabled = false;
            this.clusterImage3.Location = new System.Drawing.Point(6, 165);
            this.clusterImage3.Name = "clusterImage3";
            this.clusterImage3.Size = new System.Drawing.Size(140, 140);
            this.clusterImage3.TabIndex = 10;
            this.clusterImage3.TabStop = false;
            this.clusterImage3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // clusterImage4
            // 
            this.clusterImage4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.clusterImage4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.clusterImage4.Enabled = false;
            this.clusterImage4.Location = new System.Drawing.Point(152, 165);
            this.clusterImage4.Name = "clusterImage4";
            this.clusterImage4.Size = new System.Drawing.Size(140, 140);
            this.clusterImage4.TabIndex = 9;
            this.clusterImage4.TabStop = false;
            this.clusterImage4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // clusterImage2
            // 
            this.clusterImage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.clusterImage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.clusterImage2.Enabled = false;
            this.clusterImage2.Location = new System.Drawing.Point(152, 19);
            this.clusterImage2.Name = "clusterImage2";
            this.clusterImage2.Size = new System.Drawing.Size(140, 140);
            this.clusterImage2.TabIndex = 2;
            this.clusterImage2.TabStop = false;
            this.clusterImage2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // clusterImage1
            // 
            this.clusterImage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.clusterImage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.clusterImage1.Enabled = false;
            this.clusterImage1.Location = new System.Drawing.Point(6, 19);
            this.clusterImage1.Name = "clusterImage1";
            this.clusterImage1.Size = new System.Drawing.Size(140, 140);
            this.clusterImage1.TabIndex = 1;
            this.clusterImage1.TabStop = false;
            this.clusterImage1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonReset.Enabled = false;
            this.buttonReset.Location = new System.Drawing.Point(122, 300);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(44, 23);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelRecognisedDigit);
            this.groupBox3.Controls.Add(this.buttonClear);
            this.groupBox3.Controls.Add(this.buttonRecognizeDigit);
            this.groupBox3.Controls.Add(this.panelHandwriting);
            this.groupBox3.Location = new System.Drawing.Point(477, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 311);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Handwritten character recognition:";
            // 
            // labelRecognisedDigit
            // 
            this.labelRecognisedDigit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecognisedDigit.Location = new System.Drawing.Point(188, 287);
            this.labelRecognisedDigit.Name = "labelRecognisedDigit";
            this.labelRecognisedDigit.Size = new System.Drawing.Size(18, 18);
            this.labelRecognisedDigit.TabIndex = 9;
            this.labelRecognisedDigit.Text = "-";
            this.labelRecognisedDigit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonClear
            // 
            this.buttonClear.Enabled = false;
            this.buttonClear.Location = new System.Drawing.Point(212, 282);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(50, 23);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonRecognizeDigit
            // 
            this.buttonRecognizeDigit.Enabled = false;
            this.buttonRecognizeDigit.Location = new System.Drawing.Point(6, 282);
            this.buttonRecognizeDigit.Name = "buttonRecognizeDigit";
            this.buttonRecognizeDigit.Size = new System.Drawing.Size(176, 23);
            this.buttonRecognizeDigit.TabIndex = 6;
            this.buttonRecognizeDigit.Text = "Recognize handwritten digit";
            this.buttonRecognizeDigit.UseVisualStyleBackColor = true;
            this.buttonRecognizeDigit.Click += new System.EventHandler(this.buttonRecognizeDigit_Click);
            // 
            // panelHandwriting
            // 
            this.panelHandwriting.BackColor = System.Drawing.Color.White;
            this.panelHandwriting.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelHandwriting.Location = new System.Drawing.Point(6, 19);
            this.panelHandwriting.Name = "panelHandwriting";
            this.panelHandwriting.Size = new System.Drawing.Size(256, 256);
            this.panelHandwriting.TabIndex = 0;
            this.panelHandwriting.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHandwriting_Paint);
            this.panelHandwriting.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHandwriting_MouseDown);
            this.panelHandwriting.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHandwriting_MouseMove);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonEMStep;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonReset;
            this.ClientSize = new System.Drawing.Size(750, 359);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonEMStep);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Bernoulli Mixture Models with Expectation-Maximization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDigit)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clusterImage3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clusterImage4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clusterImage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clusterImage1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadData;
        private System.Windows.Forms.PictureBox pictureBoxDigit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelDigit;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.CheckBox checkBoxStretch;
        private System.Windows.Forms.CheckBox checkBoxBinarization;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox clusterImage3;
        private System.Windows.Forms.PictureBox clusterImage4;
        private System.Windows.Forms.PictureBox clusterImage2;
        private System.Windows.Forms.PictureBox clusterImage1;
        private System.Windows.Forms.Button buttonEMStep;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonRecognizeDigit;
        private System.Windows.Forms.Panel panelHandwriting;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelRecognisedDigit;
    }
}

