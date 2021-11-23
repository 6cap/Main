using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace test
{
    public partial class Form1 : Form
    {

        public static int BBC = 0;
        public static int RC = 0;
        public static int LC = 0;
        public static int CC = 0;
        public static int Score = 100;
        public static int CF = 30;
        StreamReader sr = new StreamReader("C:/Users/Ryo_ideapad_slim3/Desktop/Output.txt");
        public static string line;
        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter("C:/Users/Ryo_ideapad_slim3/Desktop/Output.txt");


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webcam web = new webcam();
            web.Show();


        }
        private void main()
        {
            string saveFolder = @"C:/Users/Ryo_ideapad_slim3/Desktop/test/image";
            while (true)
            {
                frame = capture.Read(frame);

                gaze.refresh(frame)

            frame = gaze.annotated_frame();
                sw.WriteLine("");
                if (gaze.is_blinking())
                {
                    sw.WriteLine("Blinking");
                    BBC = BBC + 1;
                }
                else if (gaze.is_right())
                {
                    sw.WriteLine("Looking right");
                    RC = RC + 1;
                }
                else if (gaze.is_left())
                {
                    sw.WriteLine("Looking left");
                    LC = LC + 1;
                }
                else if (gaze.is_center())
                {
                    sw.WriteLine("Looking center");
                    CC = CC + 1;
                }
            }
            if (BBC >= CF | RC >= CF | LC >= CF | CC >= CF)
                if (RC >= CF | LC >= CF)
                {
                    Score = Score - 1;
                    sw.WriteLine("점수 : %s\n", (Score));
                    sw.WriteLine("감점 원인: 딴 곳 쳐다봄\n");
                    temp, image = capture.Read(frame); ;
                    pictureBox1.Image.Save(saveFolder + "//image.png", System.Drawing.Imaging.ImageFormat.Png);
                }
                else if (BC >= CF)
                {
                    Score = Score - 1;
                    sw.WriteLine("점수 : %s\n", (Score));
                    sw.WriteLine("감점 원인: 졸음\n");
                    temp, image = capture.Read(frame); ;
                    pictureBox1.Image.Save(saveFolder + "//image.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            BBC = 0;
            RC = 0;
            LC = 0;
            CC = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Write a line of text
            sw.WriteLine("점수 : %s\n", (Score));
            //Close the file
            sw.Close();
        }
    }
}
