// Author:   Manfredas Zabarauskas, 2012 
// E-mail:   manfredas@zabarauskas.com
// Website:  http://zabarauskas.com
// Tutorial: http://blog.zabarauskas.com/expectation-maximization-tutorial
// Note:     Most of the UI/util/file parsing code is a write-once-read-never hack; only the 
//           ExpectationMaximization.cs code should be used for reference.
//
//           Handwritten digits taken from MNIST database: http://yann.lecun.com/exdb/mnist/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BernoulliMixtureModels
{
    public partial class MainForm : Form
    {
        private const string DATA_FOLDER_LOCATION = @"../../data/";
        private const string LABELS_FILE_NAME = "train-labels.idx1-ubyte";
        private const string IMAGES_FILE_NAME = "train-images.idx3-ubyte";

        private int _currentDigit = -1;
        private List<DigitData> _digitData = null;

        private int _currentCluster = -1;
        private int CurrentCluster
        {
            get
            {
                return _currentCluster;
            }
            set
            {
                _currentCluster = value;

                if (value != -1)
                {
                    buttonNext_Click(buttonNext, null);
                }
            }
        }

        private ExpectationMaximization expectationMaximization;

        private BackgroundWorker _backgroundWorker = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();

            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += (object workerSender, DoWorkEventArgs workerE) =>
            {
                BackgroundWorker worker = workerSender as BackgroundWorker;

                while (!worker.CancellationPending)
                {
                    expectationMaximization.PerformEMStep();
                    this.Invoke(new UpdateControlsCallback(UpdateControls));
                }
            };

            CurrentCluster = -1;

            _handwriting = new Bitmap(panelHandwriting.Width, panelHandwriting.Height, panelHandwriting.CreateGraphics());
            Graphics.FromImage(_handwriting).Clear(Color.White);
        }

        delegate void UpdateControlsCallback();
        private void UpdateControls()
        {
                List<Bitmap> clusterImages = expectationMaximization.VisualizeClusterPixelDistributions();

                clusterImage1.BackgroundImage = clusterImages[0];
                clusterImage2.BackgroundImage = clusterImages[1];
                clusterImage3.BackgroundImage = clusterImages[2];
                clusterImage4.BackgroundImage = clusterImages[3];
        }

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            _digitData = DataParser.ParseDigitData(DATA_FOLDER_LOCATION + IMAGES_FILE_NAME, DATA_FOLDER_LOCATION + LABELS_FILE_NAME);

            int[] digitCounts = new int[4];
            List<DigitData> subsampledDigitData = new List<DigitData>();
            foreach (DigitData digitData in _digitData)
            {
                if (digitCounts[digitData.Label] < 500)
                {
                    subsampledDigitData.Add(digitData);
                    digitCounts[digitData.Label]++;
                }
            }
            _digitData = subsampledDigitData;

            this.OnDataLoad();

            _currentDigit = 0;
            this.DisplayTrainingDigit();

            this.expectationMaximization = new ExpectationMaximization(_digitData[_currentDigit].Width * _digitData[_currentDigit].Height, _digitData);
        }


        private void DisplayTrainingDigit()
        {
            DigitData digitData = _digitData[_currentDigit];
            
            pictureBoxDigit.BackgroundImage = checkBoxBinarization.Checked ?
                Utils.BinaryBoolArrayToBitmap(digitData.BinaryImage, digitData.Width, digitData.Height) :
                Utils.MonochromeByteArrayToBitmap(digitData.Image, digitData.Width, digitData.Height);

            labelDigit.Text = digitData.Label.ToString();
        }

        
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            do
            {
                _currentDigit = (_currentDigit - 1) % _digitData.Count;

                if (_currentDigit == -1)
                {
                    _currentDigit = _digitData.Count - 1;
                }
            }
            while ((_currentCluster != -1) && (expectationMaximization.GetCluster(_digitData[_currentDigit].BinaryImage) != _currentCluster));

            this.DisplayTrainingDigit();
        }

        
        private void buttonNext_Click(object sender, EventArgs e)
        {
            do
            {
                _currentDigit = (_currentDigit + 1) % _digitData.Count;
            }
            while ((_currentCluster != -1) && (expectationMaximization.GetCluster(_digitData[_currentDigit].BinaryImage) != _currentCluster));

            this.DisplayTrainingDigit();
        }


        private void OnDataLoad()
        {
            buttonLoadData.Enabled = false;

            buttonNext.Enabled = true;
            buttonPrev.Enabled = true;
            buttonEMStep.Enabled = true;
            buttonReset.Enabled = true;
            buttonClear.Enabled = true;
            buttonRecognizeDigit.Enabled = true;

            pictureBoxDigit.Enabled = true;
            
            labelDigit.Enabled = true;
            
            checkBoxStretch.Enabled = true;
            checkBoxBinarization.Enabled = true;

            clusterImage1.Enabled = true;
            clusterImage2.Enabled = true;
            clusterImage3.Enabled = true;
            clusterImage4.Enabled = true;
            labelStatus.Text = String.Format("{0} training images loaded successfully.", _digitData.Count);
        }


        private void checkBoxStretch_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox senderCheckBox = (CheckBox)sender;

            if (senderCheckBox.Checked)
            {
                pictureBoxDigit.BackgroundImageLayout = ImageLayout.Zoom;
                clusterImage1.BackgroundImageLayout = ImageLayout.Zoom;
                clusterImage2.BackgroundImageLayout = ImageLayout.Zoom;
                clusterImage3.BackgroundImageLayout = ImageLayout.Zoom;
                clusterImage4.BackgroundImageLayout = ImageLayout.Zoom;
            }
            else
            {
                pictureBoxDigit.BackgroundImageLayout = ImageLayout.Center;
                clusterImage1.BackgroundImageLayout = ImageLayout.Center;
                clusterImage2.BackgroundImageLayout = ImageLayout.Center;
                clusterImage3.BackgroundImageLayout = ImageLayout.Center;
                clusterImage4.BackgroundImageLayout = ImageLayout.Center;
            }
        }


        private void checkBoxBinarization_CheckedChanged(object sender, EventArgs e)
        {
            this.DisplayTrainingDigit();
        }


        private void buttonEMStep_Click(object sender, EventArgs e)
        {
            if (_backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
                buttonEMStep.Text = "Start EM clustering";
            }
            else
            {
                _backgroundWorker.RunWorkerAsync();
                buttonEMStep.Text = "Stop EM clustering";
            }
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
            }
        }


        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.expectationMaximization = new ExpectationMaximization(_digitData[_currentDigit].Width * _digitData[_currentDigit].Height, _digitData);

            clusterImage1.BackgroundImage = null;
            clusterImage2.BackgroundImage = null;
            clusterImage3.BackgroundImage = null;
            clusterImage4.BackgroundImage = null;
        }


        Bitmap _handwriting;
        Pen _handwritingPen = new Pen(Color.Black, 40)
        {
            EndCap = System.Drawing.Drawing2D.LineCap.Round,
            StartCap = System.Drawing.Drawing2D.LineCap.Round
        };


        private void panelHandwriting_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Graphics.FromImage(_handwriting).DrawLine(_handwritingPen, _pInit, e.Location);
                _pInit = e.Location;

                panelHandwriting.CreateGraphics().DrawImageUnscaled(_handwriting, new Point(0, 0));
            }
        }


        private void panelHandwriting_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_handwriting, new Point(0, 0));
        }


        Point _pInit;
        private void panelHandwriting_MouseDown(object sender, MouseEventArgs e)
        {
            _pInit = e.Location;
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            labelRecognisedDigit.Text = "-";
            
            Graphics.FromImage(_handwriting).Clear(Color.White);
            panelHandwriting.CreateGraphics().DrawImageUnscaled(_handwriting, new Point(0, 0));
        }


        private void buttonRecognizeDigit_Click(object sender, EventArgs e)
        {
            Bitmap handwritingRescaled = new Bitmap(28, 28);

            Graphics g = Graphics.FromImage(handwritingRescaled);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawImage(_handwriting, new Rectangle(0, 0, handwritingRescaled.Width, handwritingRescaled.Height));

            bool[] convertedHandwriting = Utils.BitmapToBinaryBoolArray(handwritingRescaled);

            int clusterNumber = expectationMaximization.GetCluster(convertedHandwriting);

            int[] digitsInTheSameCluster = new int[4];
            foreach (DigitData digitData in _digitData)
            {
                if (clusterNumber == expectationMaximization.GetCluster(digitData.BinaryImage))
                {
                    digitsInTheSameCluster[digitData.Label]++;
                }
            }

            int maxDigits = -1;
            int maxLabel = -1;
            for (int i = 0; i < 4; i++)
            {
                if (digitsInTheSameCluster[i] > maxDigits)
                {
                    maxDigits = digitsInTheSameCluster[i];
                    maxLabel = i;
                }
            }

            labelRecognisedDigit.Text = maxLabel.ToString();
        }


        #region Cluster image click handling
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (CurrentCluster != 0)
            {
                CurrentCluster = 0;

                clusterImage1.BackColor = Color.SkyBlue;
                clusterImage1.BorderStyle = BorderStyle.FixedSingle;
                clusterImage2.BackColor = SystemColors.Control;
                clusterImage2.BorderStyle = BorderStyle.Fixed3D;
                clusterImage3.BackColor = SystemColors.Control;
                clusterImage3.BorderStyle = BorderStyle.Fixed3D;
                clusterImage4.BackColor = SystemColors.Control;
                clusterImage4.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                CurrentCluster = -1;

                clusterImage1.BackColor = SystemColors.Control;
                clusterImage1.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (CurrentCluster != 1)
            {
                CurrentCluster = 1;

                clusterImage2.BackColor = Color.SkyBlue;
                clusterImage2.BorderStyle = BorderStyle.FixedSingle;
                clusterImage1.BackColor = SystemColors.Control;
                clusterImage1.BorderStyle = BorderStyle.Fixed3D;
                clusterImage3.BackColor = SystemColors.Control;
                clusterImage3.BorderStyle = BorderStyle.Fixed3D;
                clusterImage4.BackColor = SystemColors.Control;
                clusterImage4.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                CurrentCluster = -1;

                clusterImage2.BackColor = SystemColors.Control;
                clusterImage2.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (CurrentCluster != 2)
            {
                CurrentCluster = 2;

                clusterImage3.BackColor = Color.SkyBlue;
                clusterImage3.BorderStyle = BorderStyle.FixedSingle;
                clusterImage2.BackColor = SystemColors.Control;
                clusterImage2.BorderStyle = BorderStyle.Fixed3D;
                clusterImage1.BackColor = SystemColors.Control;
                clusterImage1.BorderStyle = BorderStyle.Fixed3D;
                clusterImage4.BackColor = SystemColors.Control;
                clusterImage4.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                CurrentCluster = -1;

                clusterImage3.BackColor = SystemColors.Control;
                clusterImage3.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (CurrentCluster != 3)
            {
                CurrentCluster = 3;

                clusterImage4.BackColor = Color.SkyBlue;
                clusterImage4.BorderStyle = BorderStyle.FixedSingle;
                clusterImage2.BackColor = SystemColors.Control;
                clusterImage2.BorderStyle = BorderStyle.Fixed3D;
                clusterImage3.BackColor = SystemColors.Control;
                clusterImage3.BorderStyle = BorderStyle.Fixed3D;
                clusterImage1.BackColor = SystemColors.Control;
                clusterImage1.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                CurrentCluster = -1;

                clusterImage4.BackColor = SystemColors.Control;
                clusterImage4.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        #endregion
    }
}
