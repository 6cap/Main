using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Threading;
using System.IO;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;

namespace test
{
    public partial class webcam : Form
    {

        private readonly VideoCapture capture;
        public webcam()
        {
            InitializeComponent();
            capture = new VideoCapture();
            Dlib.TestShapePredictor("//test");
            var landmarks = ShapePredictor.Deserialize;
            
        }


        private void webcam_Load(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            capture.Read(frame);

            capture.Open(0, VideoCaptureAPIs.ANY);
            Mat src = Cv2.ImRead("desert.jpg");
            Mat dst = new Mat(src.Size(), MatType.CV_8UC1);
            Cv2.CvtColor(frame, dst, ColorConversionCodes.BGR2GRAY);
            if (!capture.IsOpened())
            {
                Close();
                return;
            }
            ClientSize = new System.Drawing.Size(capture.FrameWidth, capture.FrameHeight);
            backgroundWorker1.RunWorkerAsync();

        }

        


        private void webcam_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
            capture.Dispose();

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var frameBitmap = (Bitmap)e.UserState;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = frameBitmap;

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;
            while (!bgWorker.CancellationPending)
            {
                using (var frameMat = capture.RetrieveMat())
                {
                    var frameBitmap = BitmapConverter.ToBitmap(frameMat);
                    bgWorker.ReportProgress(0, frameBitmap);
                }
                Thread.Sleep(100);

            }
        }
    }
}
