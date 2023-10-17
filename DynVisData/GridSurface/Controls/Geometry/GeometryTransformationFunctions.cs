using DynVis.Math;
using DynVis.Data.Geometry;

namespace DynVis.Data.GridSurface
{
    internal static class GeometryTransformationFunctions
    {
        /// <summary>
        /// Устанавливает стандартную ориентацию по заданым атомам для всех структур в массиве.
        /// </summary>
        /// <param name="Structures"></param>
        /// <param name="aCenter"></param>
        /// <param name="aZ"></param>
        /// <param name="aZX"></param>
        public static void SetStandartOrientation(Structure[,] Structures, int aCenter, int aZ, int aZX)
        {
            for (var i = 0; i < Structures.GetLength(0);i++)
            {
                for (var j = 0; j < Structures.GetLength(1); j++)
                {
                    var Structure = Structures[i, j];

                    var result = GeometryExtension.GetStandartOrientation(Structure.ToIPointCollection(), Structure[aCenter], Structure[aZ],
                          Structure[aZX]);

                    Structure.SetPoints(result);
                }
            }
        }
    }
}
