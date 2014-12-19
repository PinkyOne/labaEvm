namespace Practice
{
    using System.Windows.Forms;

    public static class ChartUtilities
    {
        public static void DrawHistogram(double[] vector, int countOfColumns, PictureBox p)
        {
            // var hist = new FormHistogram(vector, countOfColumns);
            //   hist.Show();
            var hist = new Histogram(vector, countOfColumns, p);
            hist.DrawChart();
        }

        public static void DrawChart(double[] vector, PictureBox p)
        {
            var ch = new Chart(vector, p);
            ch.DrawChart();
        }
    }
}
