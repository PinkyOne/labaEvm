using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice
{
    public partial class FormChart : Form
    {
        private static int launch; // AAA DON'T DELETE THIS HACK!!!

        private int length;

        private double[] x;

        public FormChart(double[] x, PictureBox p)
        {
            InitializeComponent();

            launch = new Random().Next();
            length = x.Length;
            this.x = x;
        }

        private static int[] GetNormalizedVector(double[] vec, int heightOfWindow, out int curZero)
        {
            var normVec = new int[vec.Length];
            var correctVec = new double[vec.Length];
            double zero = 0;

            var correction = Math.Abs(vec.Min());
            for (var i = 0; i < vec.Length; i++)
            {
                correctVec[i] = vec[i] + correction;
            }
            zero += correction;

            var vecMaxValue = correctVec.Max();
            var coeff = heightOfWindow / vecMaxValue;

            for (var i = 0; i < vec.Length; i++) normVec[i] = (int)Math.Round(correctVec[i] * coeff);
            curZero = (int)Math.Round(zero * coeff);

            return normVec;
        }

        private void FormChartLoad(object sender, EventArgs e)
        {
            DrawChart();
        }

        private void DrawChart()
        {
            pictureBox.Controls.Clear();
            int curZero;
            var normX = GetNormalizedVector(this.x, pictureBox.Height - 50, out curZero);
            var lengthOfSegment = pictureBox.Width * 1.0 / length;
            double curLeft = 0;

            var chartY = pictureBox.Height - 25;

            var bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            var g = Graphics.FromImage(bitmap);
            var pen = new Pen(Color.Green, 2);
            var rand = new Random(launch);

            int[] x = new int[length], y = new int[length];

            for (var i = 0; i < length; i++)
            {
                var height = normX[i];
                x[i] = (int)Math.Round(curLeft);
                y[i] = chartY - height;
                curLeft += lengthOfSegment;
            }

            // Gradient implementation
            var colors = new Color[length];
            var mainColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            pen.Color = mainColor;
            for (var i = 1; i < length; i++)
            {
                g.DrawLine(pen, x[i - 1], y[i - 1], x[i], y[i]);
            }

            g.DrawLine(new Pen(Color.Black, 1), 0, chartY - curZero, this.Width, chartY - curZero);
            pictureBox.Image = bitmap;
        }
    }
}