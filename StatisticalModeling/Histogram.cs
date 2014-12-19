using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Practice
{
    using System.Globalization;

    public partial class Histogram
    {
        private double[] x;
        private double[] y;

        private int length;

        private int countOfColumns;

        private PictureBox p;
        private PictureBox p2;

        private static int launch; // AAA DON'T DELETE THIS HACK!!!!!

        public Histogram(double[] x, double[] y, int countOfColumns, PictureBox p, PictureBox p2)
        {
            this.p2 = p2;
            this.p = p;
            this.y = y;
            this.x = x;
            this.length = x.Length;
            this.countOfColumns = countOfColumns;
            launch = new Random().Next();
        }

        public void DrawChart()
        {
            p.Controls.Clear();

            Array.Sort(x);
            double minX = x[0], maxX = x[length - 1];
            var segmentLength = (maxX - minX) / countOfColumns;
            var histogram = new int[countOfColumns];
            var curColumn = 0;

            var ranges = new double[countOfColumns];
            ranges[0] = (minX + segmentLength) / 2;

            for (var i = 1; i < countOfColumns; i++) ranges[i] = ranges[i - 1] + segmentLength;

            for (var i = 0; i < length; i++)
            {
                var curRight = minX + segmentLength * (curColumn + 1);
                while (x[i] > curRight && curColumn - 1 < countOfColumns)
                {
                    curColumn++;
                    curRight = minX + segmentLength * (curColumn + 1);
                }
                try
                {
                    histogram[curColumn]++;
                }
                catch
                {
                }
            }

            var bitmap = new Bitmap(p.Width, p.Height);
            var g = Graphics.FromImage(bitmap);
            var pen = new Pen(Color.Black, 1);
            var rand = new Random(launch);

            var left = 0;
            var right = bitmap.Width;
            var chartLength = right - left;
            var columnWidth = chartLength / countOfColumns;
            var chartY = bitmap.Height - 50;

            var maxValue = histogram.Max();
            var coeff = (bitmap.Height - 100) * 1d / maxValue;

            var curLeft = left;
            var showTitles = columnWidth > 35 && Math.Round(ranges[1], 3) > Math.Round(ranges[0], 3);

            // Gradient implementation
            var color = new Color[countOfColumns];
            var mainColumn = countOfColumns / 2;
            var mainColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            color[mainColumn] = mainColor;

            var k = 255d / (mainColumn * (mainColumn));
            for (int i = 0, j = 0; i < mainColumn; i++, j++)
            {
                color[i] = Color.FromArgb(Math.Max(20, (int)Math.Round(j * j * k)), mainColor);
            }

            for (int i = countOfColumns - 1, j = 0; i > mainColumn; i--, j++)
            {
                color[i] = Color.FromArgb(Math.Max(20, (int)Math.Round(j * j * k)), mainColor);
            }

            for (var i = 0; i < countOfColumns; i++)
            {
                var height = (int)Math.Round(histogram[i] * coeff);
                g.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(curLeft, chartY - height, columnWidth, height));
                g.DrawRectangle(pen, new Rectangle(curLeft, chartY - height, columnWidth, height));

                if (showTitles)
                {
                    var label = new Label();
                    label.AutoSize = true;
                    label.Text = Math.Round(ranges[i], 3).ToString(CultureInfo.InvariantCulture);
                    label.Top = chartY + 20;
                    label.Left = curLeft;
                    label.Parent = p;
                }

                curLeft += columnWidth;
            }

            p.Image = bitmap;
        }
        public void DrawChart2()
        {
            p.Controls.Clear();

            Array.Sort(y);
            double minX = y[0], maxX = y[length - 1];
            var segmentLength = (maxX - minX) / countOfColumns;
            var histogram = new int[countOfColumns];
            var curColumn = 0;

            var ranges = new double[countOfColumns];
            ranges[0] = (minX + segmentLength) / 2;

            for (var i = 1; i < countOfColumns; i++) ranges[i] = ranges[i - 1] + segmentLength;

            for (var i = 0; i < length; i++)
            {
                var curRight = minX + segmentLength * (curColumn + 1);
                while (y[i] > curRight && curColumn - 1 < countOfColumns)
                {
                    curColumn++;
                    curRight = minX + segmentLength * (curColumn + 1);
                }
                try
                {
                    histogram[curColumn]++;
                }
                catch
                {
                }
            }

            var bitmap = new Bitmap(p2.Width, p2.Height);
            var g = Graphics.FromImage(bitmap);
            var pen = new Pen(Color.Red, 1);
            var rand = new Random(launch);

            var left = 0;
            var right = bitmap.Width;
            var chartLength = right - left;
            var columnWidth = chartLength / countOfColumns;
            var chartY = bitmap.Height - 50;

            var maxValue = histogram.Max();
            var coeff = (bitmap.Height - 100) * 1d / maxValue;

            var curLeft = left;
            var showTitles = columnWidth > 35 && Math.Round(ranges[1], 3) > Math.Round(ranges[0], 3);

            // Gradient implementation
            var color = new Color[countOfColumns];
            var mainColumn = countOfColumns / 2;
            var mainColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            color[mainColumn] = mainColor;

            var k = 255d / (mainColumn * (mainColumn));
            for (int i = 0, j = 0; i < mainColumn; i++, j++)
            {
                color[i] = Color.FromArgb(Math.Max(20, (int)Math.Round(j * j * k)), mainColor);
            }

            for (int i = countOfColumns - 1, j = 0; i > mainColumn; i--, j++)
            {
                color[i] = Color.FromArgb(Math.Max(20, (int)Math.Round(j * j * k)), mainColor);
            }

            for (var i = 0; i < countOfColumns; i++)
            {
                var height = (int)Math.Round(histogram[i] * coeff);
                g.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(curLeft, chartY - height, columnWidth, height));
                g.DrawRectangle(pen, new Rectangle(curLeft, chartY - height, columnWidth, height));

                if (showTitles)
                {
                    var label = new Label();
                    label.AutoSize = true;
                    label.Text = Math.Round(ranges[i], 3).ToString(CultureInfo.InvariantCulture);
                    label.Top = chartY + 20;
                    label.Left = curLeft;
                    label.Parent = p2;
                }

                curLeft += columnWidth;
            }

            p2.Image = bitmap;
        }
    }
}