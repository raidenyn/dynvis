using System;
using M=System.Math;

namespace DynVis.Math.MathLibrary.Matrix
{
    public static class MatrixCompare
    {
        private static void CheckMatrix(double[,] m1, double[,] m2)
        {
            if (m1.GetLength(0) != m2.GetLength(0) || m1.GetLength(1) != m2.GetLength(1))
            {
                throw new ArgumentException("Matrix dimensions not equial");
            }
        }
        

        /// <summary>
        /// Среднеквадратичное отклонение значения элементов двух матриц
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static double AQD(double[,] m1, double[,] m2)
        {
            CheckMatrix(m1, m2);

            var dim1 = m1.GetLength(0);
            var dim2 = m1.GetLength(1);

            double sum = 0;
            for (var i=0;i <dim1;i++)
            {
                for (var j = 0; j < dim2; j++)
                {
                    var dv = m1[i, j] - m2[i, j];
                    sum += dv * dv;
                }
            }

            return sum / m1.Length;
        }

        /// <summary>
        /// Максимальная разница между элементами двух матриц
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static double MaxDisplacement(double[,] m1, double[,] m2)
        {
            CheckMatrix(m1, m2);
            
            var dim1 = m1.GetLength(0);
            var dim2 = m1.GetLength(1);

            double result = 0;
            for (var i = 0; i < dim1; i++)
            {
                for (var j = 0; j < dim2; j++)
                {
                    var dv = m1[i, j] - m2[i, j];
                    var dv2 = dv*dv;
                    if (dv2 > result) result = dv2;
                }
            }

            return result;
        }

        /// <summary>
        /// Сравнение двух матриц на равенстов с заднымсредним отклонением и максимальным отклонением
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <param name="aqd"></param>
        /// <param name="md"></param>
        /// <returns></returns>
        public static bool IsMatrixEqual(double[,] m1, double[,] m2, double aqd, double md)
        {
            return IsMatrixEqual(m1, m2, aqd, md);
        }

        /// <summary>
        /// Сравнение двух матриц на равенстов с заднымсредним отклонением и максимальным отклонением
        /// На выходе переменны aqd и md содержат реальные значения отклонений для данных атриц.
        /// Эта функция работает многим быстрее чам AQD и MaxDisplacement вместе.
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <param name="aqd"></param>
        /// <param name="md"></param>
        /// <returns></returns>
        public static bool IsMatrixEqual(double[,] m1, double[,] m2, ref double aqd, ref double md)
        {
            CheckMatrix(m1, m2);
            
            var et_aqd = aqd;
            var et_md = md;

            var dim1 = m1.GetLength(0);
            var dim2 = m1.GetLength(1);

            md = 0;
            aqd = 0;
            for (var i = 0; i < dim1; i++)
            {
                for (var j = 0; j < dim2; j++)
                {
                    var dv = m1[i, j] - m2[i, j];
                    var dv2 = dv * dv;
                    
                    if (dv2 > md) md = dv2;

                    aqd += dv2;
                }
            }
            aqd /= dim1*dim2;


            return aqd <= et_aqd && md <= et_md;
        }
    }
}
