using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stars
{
    public partial class Form1 : Form
    {
        public class Star
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        private Star[] stars = new Star[15000];

        private Random random = new Random();

        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);

            foreach (var star in stars)
            {
                DrawStar(star);
            }

            pictureBox1.Refresh();
        }

        private void DrawStar(Star star)
        {
            float starSize = 7;

            float x = Remap(star.X / star.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width / 2;
            float y = Remap(star.Y / star.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height / 2;

            graphics.FillEllipse(Brushes.YellowGreen, x, y, starSize, starSize);
        }

        private float Remap(float n, float start1, float end1, float start2, float end2)
        {
            return ((n - start1) / (end1 - start2)) * (end2 - start2) + start2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            graphics = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star()
                {
                    X = random.Next(-pictureBox1.Width, pictureBox1.Width),
                    Y = random.Next(-pictureBox1.Height, pictureBox1.Height),
                    Z = random.Next(1, pictureBox1.Width)
                };
            }

            timer1.Start();
        }
    }
}
