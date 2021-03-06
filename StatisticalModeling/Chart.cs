﻿using System;
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
    public partial class Chart
    {
        private static int launch; // AAA DON'T DELETE THIS HACK!!!

        private int length;

        private double[] x;

        private double[] y;

        private PictureBox p;

        private PictureBox p2;

        public Chart(double[] x, double[] y, PictureBox p, PictureBox p2)
        {
            this.p = p;
            this.p2 = p2;
            this.y = y;
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

        public void DrawChart()
        {
            p.Controls.Clear();
            int curZero;
            var normX = GetNormalizedVector(this.x, p.Height - 50, out curZero);
            var lengthOfSegment = p.Width * 1.0 / length;
            double curLeft = 0;

            var chartY = p.Height - 25;

            var bitmap = new Bitmap(p.Width, p.Height);
            var g = Graphics.FromImage(bitmap);
            var pen = new Pen(Color.Blue, 2);
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
            var mainColor = Color.Blue;
            pen.Color = mainColor;
            for (var i = 1; i < length; i++)
            {
                try
                {
                    g.DrawLine(pen, x[i - 1], y[i - 1], x[i], y[i]);
                }
                catch
                {
                }
            }

            try
            {
                g.DrawLine(new Pen(Color.Black, 1), 0, chartY - curZero, p.Width, chartY - curZero);
            }
            catch
            {
            }
            p.Image = bitmap;
        }

        public void DrawChart2()
        {
            p2.Controls.Clear();
            int curZero;
            var normX = GetNormalizedVector(this.y, p2.Height - 50, out curZero);
            var lengthOfSegment = p2.Width * 1.0 / length;
            double curLeft = 0;

            var chartY = p.Height - 25;

            var bitmap = new Bitmap(p2.Width, p2.Height);
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
            var mainColor = Color.Red;
            pen.Color = mainColor;
            for (var i = 1; i < length; i++)
            {
                try
                {
                    g.DrawLine(pen, x[i - 1], y[i - 1], x[i], y[i]);
                }
                catch
                {
                }
            }

            try
            {
                g.DrawLine(new Pen(Color.Black, 1), 0, chartY - curZero, p2.Width, chartY - curZero);
            }
            catch
            {
            }
            p2.Image = bitmap;
        }
    }
}