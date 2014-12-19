using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public static class Gauss
    {
        // Ax = B
        public static double[] GetAnswerVector(int size, double[,] matrix, double[] vector)
        {
            var swaps = new List<Swap>();

            var a = new double[size, size];
            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    a[i, j] = matrix[i, j];

            for (var curRow = 0; curRow < size - 1; curRow++)
            {
                for (var row = curRow + 1; row < size; row++)
                {
                    if (a[curRow, curRow] == 0)
                    {
                        for (var i = curRow + 1; i < size; i++)
                        {
                            if (a[i, curRow] != 0)
                            {
                                for (var j = 0; j < size; j++)
                                {
                                    var t = a[curRow, j];
                                    a[curRow, j] = a[i, j];
                                    a[i, j] = t;
                                }
                                swaps.Add(new Swap(curRow, i));
                                break;
                            }
                        }
                    }

                    var toAdd = new double[size];
                    var coefficient = -a[row, curRow] / a[curRow, curRow];

                    for (var column = 0; column < size; column++)
                        toAdd[column] = a[curRow, column] * coefficient;

                    var add = vector[curRow] * coefficient;

                    for (var column = 0; column < size; column++)
                        if (column <= curRow) a[row, column] = 0;
                        else a[row, column] += toAdd[column];
                    vector[row] += add;
                }
            }

            double det = 1;
            for (var i = 0; i < size; i++)
            {
                det *= a[i, i];
            }

            if (det == 0) throw new Exception("Determinant is zero");

            var xx = new double[size];
            RecursiveAnswer(size, 0, ref xx, a, vector);

            swaps.Reverse();
            foreach (var swap in swaps)
            {
                var x = swap.x;
                var y = swap.y;

                var t = xx[x];
                xx[x] = xx[y];
                xx[y] = t;
            }

            return xx;
        }

        private static void RecursiveAnswer(int size, int curRow, ref double[] x, double[,] a, double[] b) 
        {
            if (curRow == size - 1)
            {
                x[curRow] = b[curRow] / a[curRow, curRow];
                return;
            }
            RecursiveAnswer(size, curRow + 1, ref x, a, b);
            var value = b[curRow];
            for (var column = curRow + 1; column < size; column++)
                value -= x[column] * a[curRow, column];
            x[curRow] = value / a[curRow, curRow];
        }
    }
}
