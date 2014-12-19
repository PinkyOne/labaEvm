using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal static class RandomExtension
    {
        private static bool haveNextNextGaussian;

        private static double nextNextGaussian;

        public static double NextGaussian(this Random rand)
        {
            if (haveNextNextGaussian)
            {
                haveNextNextGaussian = false;
                return nextNextGaussian;
            }
            else
            {
                double v1, v2, s;
                do
                {
                    v1 = 2 * rand.NextDouble() - 1; // between -1.0 and 1.0
                    v2 = 2 * rand.NextDouble() - 1; // between -1.0 and 1.0
                    s = v1 * v1 + v2 * v2;
                }
                while (s >= 1 || s == 0);
                var multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
                nextNextGaussian = v2 * multiplier;
                haveNextNextGaussian = true;
                return v1 * multiplier;
            }
        }
    }
}
