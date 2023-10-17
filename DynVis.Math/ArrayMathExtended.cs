
using System;
using System.Collections.Generic;


namespace DynVis.Math
{
    public static class ArrayMathExtended
    {
        public static void FindMinMax2D(this double[,] array, Point3D min, Point3D max)
        {
            max.Value = array[0, 0];
            min.Value = array[0, 0];
            for (var q1 = 0; q1 < array.GetLength(0); q1++)
            {
                for (var q2 = 0; q2 < array.GetLength(1); q2++)
                {
                    var e = array[q1, q2];
                    if (e > max.Value)
                    {
                        max.Value = e;
                        max.Point.X = q1;
                        max.Point.Y = q2;
                    }
                    if (e < min.Value)
                    {
                        min.Value = e;
                        min.Point.X = q1;
                        min.Point.Y = q2;
                    }
                }
            }
        }

        /// <summary>
        /// Функция осуществляет бинарный поиск по сортированному по возрастанию массиву array заданного значения v.
        /// Возращает индекс элемента, меньшего либо равного искомому значению. При v равном последнему элементу возращает индекс предпоследнего элемента. 
        /// Функцимя не осуществляет проверку на выход искомого значения за диапазон массива.
        /// При несортированном массиве функция зацикливается!
        /// </summary>
        /// <param name="array">Сортированный по возрастанию числовой массив</param>
        /// <param name="v">Искомое значение</param>
        /// <returns>Индекс элемента, меньшего либо равного искомому значению.</returns>
        public static int BinaryFind(this double[] array, double v)
        {
            var MaxIndex = array.Length - 1;
            if (v == array[MaxIndex]) return MaxIndex - 1;

            var CurrIndex = (int)(MaxIndex * 0.5);
            var TempIndex = MaxIndex;
            while (true)
            {
                var LastIndex = TempIndex;
                TempIndex = CurrIndex;

                var LeftCondition = v >= array[CurrIndex];
                var RightCondition = v < array[CurrIndex + 1];


                if (!LeftCondition && RightCondition)
                {
                    MaxIndex = CurrIndex;
                    CurrIndex = (int)(CurrIndex * 0.5);
                    continue;
                }
                if (!RightCondition && LeftCondition)
                {
                    if (LastIndex < CurrIndex) LastIndex = MaxIndex;

                    CurrIndex = (int)((LastIndex + CurrIndex) * 0.5);
                    continue;
                }
                break;
            }
            return CurrIndex;
        }

        /// <summary>
        /// Процедура для сортировки массива методом фон Неймана (слияний)
        /// </summary>
        /// <param name="arr">Cортируемый массив.</param>
        public static void NeumannSort<T>(this T[] arr) where T: IComparable<T>
        {
            var n = arr.Length;

            int i;

            var barr = new T[n];
            int mergelen = 1;
            var c = true;
            while (mergelen < n)
            {
                if (c)
                {
                    i = 0;
                    while (i + mergelen <= n)
                    {
                        var i1 = i + 1;
                        var n1 = i + mergelen;
                        var i2 = n1 + 1;
                        var n2 = i + 2 * mergelen;
                        if (n2 > n)
                        {
                            n2 = n;
                        }
                        while (i1 <= n1 | i2 <= n2)
                        {
                            if (i1 > n1)
                            {
                                while (i2 <= n2)
                                {
                                    i = i + 1;
                                    barr[i - 1] = arr[i2 - 1];
                                    i2 = i2 + 1;
                                }
                            }
                            else
                            {
                                if (i2 > n2)
                                {
                                    while (i1 <= n1)
                                    {
                                        i = i + 1;
                                        barr[i - 1] = arr[i1 - 1];
                                        i1 = i1 + 1;
                                    }
                                }
                                else
                                {
                                    if (arr[i1 - 1].CompareTo(arr[i2 - 1]) > 0)
                                    {
                                        i = i + 1;
                                        barr[i - 1] = arr[i2 - 1];
                                        i2 = i2 + 1;
                                    }
                                    else
                                    {
                                        i = i + 1;
                                        barr[i - 1] = arr[i1 - 1];
                                        i1 = i1 + 1;
                                    }
                                }
                            }
                        }
                    }
                    i = i + 1;
                    while (i <= n)
                    {
                        barr[i - 1] = arr[i - 1];
                        i = i + 1;
                    }
                }
                else
                {
                    i = 0;
                    while (i + mergelen <= n)
                    {
                        var i1 = i + 1;
                        var n1 = i + mergelen;
                        var i2 = n1 + 1;
                        var n2 = i + 2 * mergelen;
                        if (n2 > n)
                        {
                            n2 = n;
                        }
                        while (i1 <= n1 | i2 <= n2)
                        {
                            if (i1 > n1)
                            {
                                while (i2 <= n2)
                                {
                                    i = i + 1;
                                    arr[i - 1] = barr[i2 - 1];
                                    i2 = i2 + 1;
                                }
                            }
                            else
                            {
                                if (i2 > n2)
                                {
                                    while (i1 <= n1)
                                    {
                                        i = i + 1;
                                        arr[i - 1] = barr[i1 - 1];
                                        i1 = i1 + 1;
                                    }
                                }
                                else
                                {
                                    if (barr[i1 - 1].CompareTo(barr[i2 - 1]) > 0)
                                    {
                                        i = i + 1;
                                        arr[i - 1] = barr[i2 - 1];
                                        i2 = i2 + 1;
                                    }
                                    else
                                    {
                                        i = i + 1;
                                        arr[i - 1] = barr[i1 - 1];
                                        i1 = i1 + 1;
                                    }
                                }
                            }
                        }
                    }
                    i = i + 1;
                    while (i <= n)
                    {
                        arr[i - 1] = barr[i - 1];
                        i = i + 1;
                    }
                }
                mergelen = 2 * mergelen;
                c = !c;
            }
            if (!c)
            {
                i = 1;
                do
                {
                    arr[i - 1] = barr[i - 1];
                    i = i + 1;
                }
                while (i <= n);
            }
        }

        public static T[,] GetArray2D<T>(this T[] array, int firstDim, int secondDim)
        {
            if (array.Length != firstDim * secondDim)
            {
                return null;
            }
            var Result = new T[firstDim,secondDim];
            for (var i = 0; i < array.Length; i++)
            {
                var q1 = i / secondDim;
                var q2 = i - q1 * secondDim;
                Result[q1, q2] = array[i];
            }
            return Result;
        }

        public static T[] GetArray<T>(this T[,] array)
        {
            var length1 = array.GetLength(0);
            var length2 = array.GetLength(1);

            var Result = new T[length1 * length2];
            for (var i = 0; i < length1; i++)
            {
                for (var j = 0; j < length2; j++)
                {
                    Result[i * length1 + j] = array[i,j];
                }
            }
            return Result;
        }

        public static T[] MergeTo<T>(this T[] array1, T[] array2)
        {
            var Result = new T[array1.Length + array2.Length];
            array1.CopyTo(Result, 0);
            array2.CopyTo(Result, array1.Length);
            return Result;
        }

        public static T GetSafeElement<T>(this T[] array, int index)
        {
            return index > array.Length
                       ? array[array.Length - 1]
                       : index < 0 ? array[0] : array[index];
        }

        public static ResultT[] ConvetToArray<InputT, ResultT>(this InputT[] array, Func<InputT, ResultT> CovertFunction)
        {
            var Result = new ResultT[array.Length];
            for (var i = 0; i < array.Length; i++)
            {
                Result[i] = CovertFunction(array[i]);
            }
            return Result;
        }

        public static int[] ConvetToIntArray<InputT>(this InputT[] array)
        {
            return array.ConvetToArray(element => Convert.ToInt32(element));
        }

        public static int GetIndexOfMin<T>(this ICollection<T> array, Func<T,double> f)
        {
            var Result = 0;
            var MinValue = double.MaxValue;
            var Counter = 0;
            foreach (var item in array)
            {
                var currValue = f(item);
                if (currValue < MinValue)
                {
                    MinValue = currValue;
                    Result = Counter;
                }
                Counter++;
            }
            return Result;
        }

        public static int GetIndexOfMax<T>(this ICollection<T> array, Func<T, double> f)
        {
            var Result = 0;
            var MaxValue = double.MinValue;
            var Counter = 0;
            foreach (var item in array)
            {
                var currValue = f(item);
                if (currValue > MaxValue)
                {
                    MaxValue = currValue;
                    Result = Counter;
                }
                Counter++;
            }
            return Result;
        }

        public static void SetAllItemsTo<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }

        public static void SetAllItemsTo<T>(this T[] array, Func<int, T> func)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = func(i);
            }
        }

        public static int IndexOfEquialPoint<T>(this IEnumerable<T> array, double q1, double q2, Func<T, IPointD> point, double epsilon)
        {
            var counter = 0;
            foreach (var item in array)
            {
                if (point(item).DistanceTo(q1, q2) < epsilon)
                {
                    return counter;
                }
                counter++;
            }
            return -1;
        }

        /// <summary>
        /// Нормирует вектор заданный масивом
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        static public void Normalize(this double[] vec)
        {
            var Norm = vec.SumOfSquear();
            for (var i = 0; i < vec.Length; i++ )
            {
                vec[i] /= Norm;
            }
        }

        /// <summary>
        /// Вычисляет сумму квадратов всех элементов вектора
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        static public double SumOfSquear(this double[] vec)
        {
            var sum = 0.0;
            foreach (var item in vec)
            {
                sum += item*item;
            }
            return sum;
        }
    }
}
