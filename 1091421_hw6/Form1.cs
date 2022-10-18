using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace _1091421_hw6
{
    public partial class Form1 : Form
    {
        Bitmap img,img1;
        Bitmap[] picture;
        Bitmap[] image;
        int time;
        int i;
        ImageAttributes ia;
        ColorMatrix cm;
        Rectangle rect;
        Point pos = new Point();
        int n;
        Random rd;
        Point[] point;
        int seconds;
        int num;
        float[][] cmArray1 = {
                  new float[] {1, 0, 0, 0, 0},
                  new float[] {0, 1, 0, 0, 0},
                  new float[] {0, 0, 1, 0, 0},
                  new float[] {0, 0, 0, 0.5f, 0},
                  new float[] {0, 0, 0, 0, 1} };
        public Form1()
        {
            InitializeComponent();
            picture = new Bitmap[3];
            picture[0] = Properties.Resources.Tulips;//圖
            picture[1] = Properties.Resources.Hydrangeas;//圖
            picture[2] = Properties.Resources.Penguins;//圖
            img1 = Properties.Resources.Bowl;//碗
            img = picture[0];
            image = new Bitmap[3];
            image[0] = Properties.Resources.StawBerry;
            image[1] = Properties.Resources.Tomato;
            image[2] = Properties.Resources.Banana;
            i = 1;//圖變換
            pos.Y = 175;
            point = new Point[3];
        }   
        private void Form1_Load(object sender, EventArgs e)
        {
            time = 0;
            timer1.Start();
            timer2.Start();
            ia = new ImageAttributes();
            cm = new ColorMatrix(cmArray1);
            ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            rect = new Rectangle(0, 0, 450, 350);
            rd = new Random();
            for (int i = 0; i < 3; i++)
            {
                n = rd.Next(450 - image[i].Width);
                point[i] = new Point(n, 20);
            }
            seconds = 120;
            label3.Text = "120";
            label4.Text = "0";
            num = 0;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            for (int i = 0; i < 3; i++)
                e.Graphics.DrawImage(image[i], point[i].X, point[i].Y, image[i].Width, image[i].Height);      
            e.Graphics.TranslateTransform(pos.X, pos.Y);
            e.Graphics.DrawImage(img1, pos.X, pos.Y,img1.Width, img1.Height);
        }   
        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            time++;
            if (time % 10 == 0)
            {
                img = picture[i];
                g.Clear(Color.White);
                g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                i++;
                if (i == 3)  
                    i = 0;
            }
            label3.Text = seconds.ToString();
            if (seconds == 0)
            {
                timer1.Stop();
                timer2.Stop();
            }
            else
                seconds--;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < 3; i++)
            {
                g.DrawImage(image[i], point[i].X, point[i].Y, image[i].Width, image[i].Height);
                point[i].Y += 15;
            }
            if (point[0].Y > 320)
            {
                 if (point[0].X + picture[i].Width >= pos.X || point[0].X <= pos.X + img1.Width)
                 {
                     num++;
                     label4.Text = num.ToString();
                 }
            }
            if (point[1].Y > 320)
            {
                if (point[1].X + picture[i].Width >= pos.X || point[1].X <= pos.X + img1.Width)
                {
                    label4.Text = num.ToString();
                }
            }
            if (point[2].Y > 320)
            {
                if (point[2].X + picture[2].Width >= pos.X || point[2].X <= pos.X + img1.Width)
                {
                    label4.Text = num.ToString();
                }
                for (int i = 0; i < 3; i++)
                {
                    n = rd.Next(450 - image[i].Width);
                    point[i] = new Point(n, 20);
                }
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            if (e.Location.X >= 1 && e.Location.X <= 270 - img1.Width)
                pos = new Point(e.Location.X, pos.Y);
            Invalidate();
        }
    }
}
