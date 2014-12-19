using System;
using System.Windows.Forms;

namespace Practice
{
    using Practice.Properties;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint n;
            try
            {
                n = 10/uint.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show(Resources.MainForm_button1_Click_Некорректный_ввод_, Resources.MainForm_button1_Click_Ошибка);
                return;
            }
            
            var random = new Random();

            var x0 = new double[2];
            var y0 = new double[2];

            x0[0] = random.NextDouble() * 2 * Math.PI / n;
            y0[0] = random.NextDouble() * 2 * Math.PI / n;
          
            y0[1] = random.NextGaussian() * 0.01 * n;
            x0[1] = random.NextGaussian() * 0.01 * n;
          
            var sol1 = RungeKutta.Runge_Kutta(x0[1], y0[1], x0[0], 555, 15, 0, 10);
          
            var vector = new double[100];

            for (var i = 0; i < 100; i++)
            {
                vector[i] = sol1[i, 0];
            }
            ChartUtilities.DrawChart(vector, pictureBox2);
            ChartUtilities.DrawHistogram(vector, 10,pictureBox1);
        }
    }
}
