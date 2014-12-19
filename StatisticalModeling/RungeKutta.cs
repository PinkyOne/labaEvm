namespace Practice
{
    using System;

    public static class RungeKutta
    {
        private static double r;

        public static double L { get; set; }

        public static double[,] Runge_Kutta(
            double y0,
            double y1,
            double y2,
            int steps,
            double maxt,
            double t0,
            double R)
        {
            var res = new double[steps, 2];
            double k1, k2, k4, k3, h;
            double q1, q2, q4, q3;
            h = maxt / steps;
            var Y0 = y0;
            var Z0 = y1;
            var X0 = y2;
            var T0 = t0;
            double Y1, Z1;
            r = R;
            L = R;
            for (var i = 0; i < steps; i++)
            {
                k1 = h * f(T0, X0, Y0, Z0);
                q1 = h * g(T0, X0, Y0, Z0);

                k2 = h * f(T0 + h / 2.0, X0, Y0 + q1 / 2.0, Z0 + k1 / 2.0);
                q2 = h * g(T0 + h / 2.0, X0, Y0 + q1 / 2.0, Z0 + k1 / 2.0);

                k3 = h * f(T0 + h / 2.0, X0, Y0 + q2 / 2.0, Z0 + k2 / 2.0);
                q3 = h * g(T0 + h / 2.0, X0, Y0 + q2 / 2.0, Z0 + k2 / 2.0);

                k4 = h * f(T0 + h, X0, Y0 + q3, Z0 + k3);
                q4 = h * g(T0 + h, X0, Y0 + q3, Z0 + k3);

                Z1 = Z0 + (k1 + 2.0 * k2 + 2.0 * k3 + k4) / 6.0;
                Y1 = Y0 + (q1 + 2.0 * q2 + 2.0 * q3 + q4) / 6.0;

                res[i, 0] = Y1;
                res[i, 1] = Z1;

                Y0 = Y1;
                Z0 = Z1;
                T0 += h;
            }
            return res;
        }

        public static double f(double t, double x, double y, double z)
        {
            return z * z * Math.Sin(x) * Math.Cos(x) + 10 * Math.Sin(x) / L;
        }

        public static double g(double t, double x, double y, double z)
        {
            return -2 * z * y / Math.Tan(x);
        }
    }
}