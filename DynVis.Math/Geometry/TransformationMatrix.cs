using System.Collections.Generic;
using System.Linq;
using System.Text;
using M=System.Math;
using System;

namespace DynVis.Math.Geometry
{
    /// <summary>
    /// Матрица преобразований в однородных координатах.
    /// Реализует закрытый подход к образованию матрицы. 
    /// В данный реализации работает только силами математики .NET,
    /// однако может быть преобразована к использованию билеотек на C для ускорения.
    /// </summary>
    [Serializable]
    public sealed class TransformationMatrix: ICloneable
    {
        public TransformationMatrix()
        {
            Array[X, X] = 1;
            Array[Y, Y] = 1;
            Array[Z, Z] = 1;
            Array[W, W] = 1;
        }

        private TransformationMatrix(double[,] array)
        {
            if (array.GetLength(0) != MatrixDimension || array.GetLength(1) != MatrixDimension)
                throw new ArgumentException("Matrix Dimension must be " + MatrixDimension);
            Array = array;
        }

        private const int X = 0;
        private const int Y = 1;
        private const int Z = 2;
        private const int W = 3;

        private const int MatrixDimension = 4;

        private double[,] Array = new double[MatrixDimension,MatrixDimension]; 
    
        public double this[int i, int j]
        {
            get
            {
                return Array[i, j];
            }
            set
            {
                Array[i, j] = value;
            }
        }

        #region Kernel Functions

        /// <summary>
        /// Умножает текущую матрицу на матрицу tm
        /// </summary>
        /// <param name="tm"></param>
        /// <returns>Результат перемножения</returns>
        public void Mult(TransformationMatrix tm)
        {
            Array = MultMatrix(this, tm).Array;
        }

        /// <summary>
        /// Обращает матрицу
        /// </summary>
        /// <returns></returns>
        public void Inverse()
        {
            inv.rmatrixinverse(ref Array);
        }

        #endregion

        #region Static Functions
        /// <summary>
        /// Возращает матрицу переноса вдоль оси X
        /// </summary>
        /// <param name="distance">Расстояние переноса</param>
        /// <returns></returns>
        public static TransformationMatrix GetTranslateX(double distance)
        {
            var TM = new TransformationMatrix();

            TM[W, X] = distance;

            return TM;
        }

        /// <summary>
        /// Возращает матрицу переноса вдоль оси Y
        /// </summary>
        /// <param name="distance">Расстояние переноса</param>
        /// <returns></returns>
        public static TransformationMatrix GetTranslateY(double distance)
        {
            var TM = new TransformationMatrix();

            TM[W, Y] = distance;

            return TM;
        }

        /// <summary>
        /// Возращает матрицу переноса вдоль оси Z
        /// </summary>
        /// <param name="distance">Расстояние переноса</param>
        /// <returns></returns>
        public static TransformationMatrix GetTranslateZ(double distance)
        {
            var TM = new TransformationMatrix();

            TM[W, Z] = distance;

            return TM;
        }

        /// <summary>
        /// Возращает матрицу переноса по трем осям
        /// </summary>
        /// <param name="distanceX">Перенос по оси X</param>
        /// <param name="distanceY">Перенос по оси Y</param>
        /// <param name="distanceZ">Перенос по оси Z</param>
        /// <returns></returns>
        public static TransformationMatrix GetTranslate(double distanceX, double distanceY, double distanceZ)
        {
            var TM = new TransformationMatrix();

            TM[W, X] = distanceX;
            TM[W, Y] = distanceY;
            TM[W, Z] = distanceZ;

            return TM;
        }
        
        /// <summary>
        /// Возращает матрицу поворота вокруг оси X
        /// </summary>
        /// <param name="angle">Угол поворота в радианах</param>
        /// <returns></returns>
        public static TransformationMatrix GetRotateX(double angle)
        {
            return GetRotateX(M.Sin(angle), M.Cos(angle));
        }

        /// <summary>
        /// Возращает матрицу поворота вокруг оси Y
        /// </summary>
        /// <param name="angle">Угол поворота в радианах</param>
        /// <returns></returns>
        public static TransformationMatrix GetRotateY(double angle)
        {
            return GetRotateY(M.Sin(angle), M.Cos(angle));
        }

        /// <summary>
        /// Возращает матрицу поворота вокруг оси Z
        /// </summary>
        /// <param name="angle">Угол поворота в радианах</param>
        /// <returns></returns>
        public static TransformationMatrix GetRotateZ(double angle)
        {
            return GetRotateZ(M.Sin(angle), M.Cos(angle));
        }

        /// <summary>
        /// Возращает матрицу поворота вокруг оси X
        /// </summary>
        /// <param name="s">Синус поворота угла</param>
        /// <param name="c">Косинус поворота угла</param>
        /// <returns></returns>
        public static TransformationMatrix GetRotateX(double s, double c)
        {
            var TM = new TransformationMatrix();

            TM[Y, Y] = c;
            TM[Y, Z] = s;
            TM[Z, Y] = -s;
            TM[Z, Z] = c;

            return TM;
        }

        /// <summary>
        /// Возращает матрицу поворота вокруг оси Y
        /// </summary>
        /// <param name="s">Синус поворота угла</param>
        /// <param name="c">Косинус поворота угла</param>
        /// <returns></returns>
        public static TransformationMatrix GetRotateY(double s, double c)
        {
            var TM = new TransformationMatrix();

            TM[X, X] = c;
            TM[X, Z] = -s;
            TM[Z, X] = s;
            TM[Z, Z] = c;

            return TM;
        }

        /// <summary>
        /// Возращает матрицу поворота вокруг оси Z
        /// </summary>
        /// <param name="s">Синус поворота угла</param>
        /// <param name="c">Косинус поворота угла</param>
        /// <returns></returns>
        public static TransformationMatrix GetRotateZ(double s, double c)
        {
            var TM = new TransformationMatrix();

            TM[X, X] = c;
            TM[X, Y] = s;
            TM[Y, X] = -s;
            TM[Y, Y] = c;

            return TM;
        }

        /// <summary>
        /// Перемножает две матрицы
        /// </summary>
        /// <param name="tm1"></param>
        /// <param name="tm2"></param>
        /// <returns></returns>
        public static TransformationMatrix MultMatrix(TransformationMatrix tm1, TransformationMatrix tm2)
        {
            return new TransformationMatrix(mulmatrix.multiplymatrixes(tm1.Array, tm2.Array));
        }

        /// <summary>
        /// Возрашает матрицу обратной данной
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public static TransformationMatrix InverseMatrix(TransformationMatrix tm)
        {
            var result = (TransformationMatrix)tm.Clone();
            result.Inverse();
            return result;
        }

        #endregion

        #region Miscellaneous Functions
        public override string ToString()
        {
            return Array.ToMatrixString();
        }

        public object Clone()
        {
            return new TransformationMatrix((double[,])Array.Clone());
        }
        #endregion

        #region Result Functions
        /// <summary>
        /// Применяет текущую матрицу преобразований к точке
        /// </summary>
        /// <param name="p">Исходная точка</param>
        /// <returns>Преобразованная точка</returns>
        public Point3D ApplyTo(IPoint3D p)
        {
            return new Point3D
                       {
                           X = (p.X*Array[X, X] + p.Y*Array[Y, X] + p.Z*Array[Z, X] + Array[W, X]),
                           Y = (p.X*Array[X, Y] + p.Y*Array[Y, Y] + p.Z*Array[Z, Y] + Array[W, Y]),
                           Z = (p.X*Array[X, Z] + p.Y*Array[Y, Z] + p.Z*Array[Z, Z] + Array[W, Z])
                       };
        }

        /// <summary>
        /// Применяет матрицу преобразований к перечислению точек.
        /// В силу отложенной природы применения (преобразования происходят в момент вызова перечисления)
        /// полученные изменения могут изменятся при изменении матрицы трансформаций уже после выполнения данной функции.
        /// Т.к. вычесления происходят напрямую, данные не кэшируются, не стоит обрабатывать полученные перечисления дважды.
        /// В этом случае точки будут вычесленны также дважды. 
        /// </summary>
        /// <param name="points">Перечисление исходных точек</param>
        /// <returns></returns>
        public IEnumerable<Point3D> ApplyTo(IEnumerable<IPoint3D> points)
        {
            var result = from point in points select ApplyTo(point);

            return result;
        }

        /// <summary>
        /// Функция подобна ApplyTo, за исключением того что возращает результат в виде массива,
        /// который уже является вычисленным и не изменится при изменении матрицы.
        /// </summary>
        /// <param name="points">Перечисление исходных точек</param>
        /// <returns></returns>
        public Point3D[] GetResultArray(IEnumerable<IPoint3D> points)
        {
            return ApplyTo(points).ToArray();
        }
        #endregion

        #region Base Override
        public static TransformationMatrix operator *(TransformationMatrix tm1, TransformationMatrix tm2)
        {
            return MultMatrix(tm1, tm2);
        }

        #endregion
    }

    /// <summary>
    /// Класс работы со списком матриц преобразований с поддержкой функций комплексной работы с матрицами
    /// </summary>
    [Serializable]
    public sealed class TransformationMatrixList : List<TransformationMatrix>
    {
        /// <summary>
        /// Возращает результат перемножения списка матриц
        /// </summary>
        /// <returns></returns>
        public TransformationMatrix GetResult()
        {
            var result = new TransformationMatrix();
            foreach (var matrix in this)
            {
                result.Mult(matrix);
            }
            return result;
        }

        #region Miscellaneous Functions
        public override string ToString()
        {
            var SB = new StringBuilder();
            foreach (var matrix in this)
            {
                SB.AppendLine(matrix.ToString());
                SB.AppendLine();
            }
            return SB.ToString();
        }

        /// <summary>
        /// Глубокое копирование списка матриц.
        /// (Матрицы также копируются глубоко)
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new TransformationMatrixList();
            foreach (var matrix in this)
            {
                result.Add((TransformationMatrix)matrix.Clone());
            }
            return result;
        }
        #endregion
    }
}
