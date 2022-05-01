using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stars
{
    public partial class Form1 : Form
    {
        public class Star
        {
            private static Random rnd = new Random();
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
            public Brush color { get; set; }

            public Star(int width, int height)
            {
                X = rnd.Next(-width, width);
                Y = rnd.Next(-height, height);
                Z = rnd.Next(1, width);
                color = GetRandomBrush();
            }

            private Brush GetRandomBrush()
            {
                Brush result = Brushes.Transparent;

                Type brushesType = typeof(Brushes);

                PropertyInfo[] properties = brushesType.GetProperties();

                int random = rnd.Next(properties.Length);
                result = (Brush)properties[random].GetValue(null, null);

                return result;
            }

            public void ResetStar(int width, int height)
            {
                X = rnd.Next(-width, width);
                Y = rnd.Next(-height, height);
                Z = rnd.Next(1, width);
                color = GetRandomBrush();
            }
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
                MoveStar(star);
            }

            pictureBox1.Refresh();
        }

        private void MoveStar(Star star)
        {
            star.Z -= 10;
            if (star.Z < 1)
            {
                star.ResetStar(pictureBox1.Width, pictureBox1.Height);
            }
        }

        private void DrawStar(Star star)
        {
            float starSize = Remap(star.Z, 0, pictureBox1.Width, 7, 0);

            float x = Remap(star.X / star.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width / 2;
            float y = Remap(star.Y / star.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height / 2;

            graphics.FillEllipse(star.color, x, y, starSize, starSize);
        }

        private float Remap(float n, float start1, float end1, float start2, float end2)
        {
            return ((n - start1) / (end1 - start2)) * (end2 - start2) + start2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cursor.Hide();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            graphics = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star(pictureBox1.Width, pictureBox1.Height);
            }

            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
