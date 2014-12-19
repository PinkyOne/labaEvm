namespace Practice
{
    using System.Windows.Forms;

    public static class ChartUtilities
    {
        public static void DrawHistogram(double[] vector, double[] vector2, int countOfColumns, PictureBox p, PictureBox p2)
        {
            // var hist = new FormHistogram(vector, countOfColumns);
            //   hist.Show();
            var hist = new Histogram(vector,vector2, countOfColumns, p,p2);
            hist.DrawChart();
            hist.DrawChart2();
        }

        public static void DrawChart(double[] vector, double[] vector2, PictureBox p, PictureBox p2)
        {
            var ch = new Chart(vector,vector2, p,p2);
            ch.DrawChart();
            ch.DrawChart2();
        }
    }
}
