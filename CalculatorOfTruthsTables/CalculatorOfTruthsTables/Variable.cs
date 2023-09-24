using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfTruthsTables
{
    // MODEL
    public class RowInTableOfTruth
    {
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int x3 { get; set; }
        public int x4 { get; set; }
        public int x5 { get; set; }
        public int x6 { get; set; }
        public int x7 { get; set; }
        public int x8 { get; set; }
        public int Function { get; set; }

        public RowInTableOfTruth(bool[,] bools, int i)
        {
            if (bools.GetLength(1) >= 1)
                x1 = Convert.ToInt32(bools[i, 0]);
            if (bools.GetLength(1) >= 2)
                x2 = Convert.ToInt32(bools[i, 1]);
            if (bools.GetLength(1) >= 3)
                x3 = Convert.ToInt32(bools[i, 2]);
            if (bools.GetLength(1) >= 4)
                x4 = Convert.ToInt32(bools[i, 3]);
            if (bools.GetLength(1) >= 5)
                x5 = Convert.ToInt32(bools[i, 4]);
            if (bools.GetLength(1) >= 6)
                x6 = Convert.ToInt32(bools[i, 5]);
            if (bools.GetLength(1) >= 7)
                x7 = Convert.ToInt32(bools[i, 6]);
            if (bools.GetLength(1) >= 8)
                x8 = Convert.ToInt32(bools[i, 7]);
        }

        public static bool[,] InitBools(int size)
        {
            bool[,] matrix = new bool[(int)Math.Pow(2, size), size];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // 0110101 -> 2^5 + 2^4 + 2^2 + 2^0 = 51 - суть алгоритма перебора 
                    if (Math.Pow(2, size - j - 1) + sum <= i)
                    {
                        matrix[i, j] = true;
                        sum += (int)Math.Pow(2, size - j - 1);
                    }
                    else
                        matrix[i, j] = false;
                }
            }
            return matrix;
        }

        public static List<RowInTableOfTruth> GetVariables(int size)
        {
            bool[,] bools = InitBools(size);

            List<RowInTableOfTruth> variables = new List<RowInTableOfTruth>();
            for (int i = 0; i < bools.GetLength(0); i++)
            {
                variables.Add(new RowInTableOfTruth(bools, i));
            }

            return variables;
        }

        // Исключительно для отладки
        public static void Print(bool[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{Convert.ToInt16(matrix[i, j]),2}");
                }
                Console.WriteLine();
            }
        }
    }
}
