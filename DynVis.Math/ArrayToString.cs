using System;
using System.Text;

namespace DynVis.Math
{
    public static class ArrayToString
    {
        private static string GetFormat(this object obj, string format)
        {
            return String.Format("{0," + format + "}", obj);
        }

        public static string ToMatrixString(this double[,] array)
        {
            var dim1 = array.GetLength(0);
            var dim2 = array.GetLength(1);

            var SB = new StringBuilder();

            for (var i = 0; i < dim1; i++)
            {
                for (var j = 0; j < dim2; j++)
                {
                    SB.Append(array[i, j].GetFormat("20:F9"));
                }
                SB.AppendLine();
            }
            return SB.ToString();
        }

        public static string ToColumnVaectorString(this double[] array)
        {
            var SB = new StringBuilder();
            for (var i = 0; i < array.Length; i++)
            {
                SB.AppendLine(array[i].GetFormat("20:F9"));
            }
            return SB.ToString();
        }

        public static string ToRowVaectorString(this double[] array)
        {
            var SB = new StringBuilder();
            for (var i = 0; i < array.Length; i++)
            {
                SB.Append(array[i].GetFormat("20:F9"));
            }
            return SB.ToString();
        }
    }
}
