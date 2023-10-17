using System.Collections.Generic;
using System.Linq;
using DynVis.Math.Geometry;

namespace DynVis.Math
{
    public static class GeometryExtension
    {
        public const int CoordCount = 3;
        public const int X = 0;
        public const int Y = 1;
        public const int Z = 2;

        public static  IEnumerable<IPoint3D> ToIPointCollection(this IEnumerable<Point3D> points)
        {
            return from point in points select (IPoint3D) point;
        }

        public static double[,] ToMatrix(this IEnumerable<IPoint3D> coords)
        {
            var result = new double[coords.Count(),CoordCount];
            var counter = 0;
            foreach (var coord in coords)
            {
                result[counter, X] = coord.X;
                result[counter, Y] = coord.Y;
                result[counter, Z] = coord.Z;
                counter++;
            }
            return result;
        }

        public static double[,] GeometryMatrixSquare(this IEnumerable<IPoint3D> coords)
        {
            var coordMatrix = coords.ToMatrix();
            return mulmatrix.multiplymatrixes(CoordCount, coordMatrix.GetLength(0), CoordCount, (i, j) => coordMatrix[j, i], (i, j) => coordMatrix[i, j]);
        }

        public static bool GeometryEigensystem(this IEnumerable<IPoint3D> coords, out double[] evals, out double[,] evecs)
        {
            var coordSquareMatrix = coords.GeometryMatrixSquare();
            return sevd.smatrixevd(coordSquareMatrix, 1, true, out evals, out evecs);
        }

        /// <summary>
        /// Определяет точку геометрического центра структуры
        /// </summary>
        /// <param name="structure">Атомная структура</param>
        /// <returns>Центр</returns>
        public static Point3D GeometryCenter(this IEnumerable<IPoint3D> structure)
        {
            var Center = new Point3D();
            var counter = 0;
            foreach (Point3D coord in structure)
            {
                Center += coord;
                counter++;
            }
            return counter > 0 ? Center/counter : null;
        }

        /// <summary>
        /// Функция устанавливает стандартную ориентацию системы по трём заданным точкам.
        /// </summary>
        /// <param name="structure">Перечисление точек</param>
        /// <param name="ceneter">Центер системы</param>
        /// <param name="zDirection">Направление оси Z</param>
        /// <param name="xzOrientation">Определяет плоскость ZX</param>
        /// <returns>Перечисление преобразованных точек</returns>
        public static IEnumerable<Point3D> StandartOrientation(IEnumerable<IPoint3D> structure, IPoint3D ceneter, IPoint3D zDirection, IPoint3D xzOrientation)
        {
            var tm = TransformationMatrixExtension.StandartOrientation(ceneter, zDirection, xzOrientation);

            return tm.ApplyTo(structure);
        }

        /// <summary>
        /// Функция устанавливает стандартную ориентацию системы по трём заданным точкам.
        /// </summary>
        /// <param name="structure">Перечисление точек</param>
        /// <param name="ceneter">Центер системы</param>
        /// <param name="zDirection">Направление оси Z</param>
        /// <param name="xzOrientation">Определяет плоскость ZX</param>
        /// <returns>Перечисление преобразованных точек</returns>
        public static Point3D[] GetStandartOrientation(IEnumerable<IPoint3D> structure, IPoint3D ceneter, IPoint3D zDirection, IPoint3D xzOrientation)
        {
            return StandartOrientation(structure, ceneter, zDirection, xzOrientation).ToArray();
        }
    }
}
