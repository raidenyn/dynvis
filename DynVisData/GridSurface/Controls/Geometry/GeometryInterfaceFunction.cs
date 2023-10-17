using System;
using DynVis.Core;
using DynVis.Core.IO;
using DynVis.Data.Geometry;
using DynVis.Data.Properties;
using DynVis.Math;
using SourceGrid;

namespace DynVis.Data.GridSurface
{
    static class GeometryInterfaceFunction
    {
        private const string SpaceString = " ";
        
        public static void Past3LastPositionFromText(Grid grid, string Text, int startRow, int FirstCol)
        {
            var textsLine = Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var elementSeparator = SpaceString;

            var rowNumber = startRow;
            foreach (var line in textsLine)
            {
                if (rowNumber >= grid.RowsCount) break;
                
                var elementsLine = line.Split(elementSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (elementsLine.Length > 2)
                {
                    var ZCoordStr = elementsLine[elementsLine.Length - 1];
                    var YCoordStr = elementsLine[elementsLine.Length - 2];
                    var XCoordStr = elementsLine[elementsLine.Length - 3];

                    double ZCoord, YCoord, XCoord;

                    if (double.TryParse(ZCoordStr, out ZCoord) && double.TryParse(YCoordStr, out YCoord) && double.TryParse(XCoordStr, out XCoord))
                    {
                        grid[rowNumber, FirstCol + 0].Value = XCoord;
                        grid[rowNumber, FirstCol + 1].Value = YCoord;
                        grid[rowNumber, FirstCol + 2].Value = ZCoord;
                    }
                }

                rowNumber++;
            }
        }

        public static void Past1FirstPositionFromText(Grid grid, string Text, int startRow, int FirstCol)
        {
            var textsLine = Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var elementSeparator = SpaceString;

            var rowNumber = startRow;
            foreach (var line in textsLine)
            {
                if (rowNumber >= grid.RowsCount) break;

                var elementsLine = line.Split(elementSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (elementsLine.Length > 0)
                {
                    var elementStr = elementsLine[0];

                    int element;

                    if (int.TryParse(elementStr, out element))
                    {
                        grid[rowNumber, FirstCol + 0].Value = element;
                    }
                }

                rowNumber++;
            }
        }


        public static string OpenGeometryFromText(out int[] elements, int Q1Count, int Q2Count, out Structure[,] structures)
        {
            var str = IOFileDialog.OpenTextFile(GeometryGrid.Filters);

            elements = null;
            structures = null;

            if (!String.IsNullOrEmpty(str))
            {
                var ObjectTextReader = new ObjectTextReader(str);

                var tempElementsArray = ObjectTextReader.ReadArray<int>(GeometryGrid.ElementsName, ObjectTextReader.Separators, ObjectTextReader.EndSectionPattern);
                if (tempElementsArray == null || tempElementsArray.Length < 3)
                {
                    return LangResource.ElementArrayNotFoundOrElementsCountLessThen3;
                }
                elements = tempElementsArray.ConvetToIntArray();
                var elementsCount = elements.Length;

                var GeometryArray = ObjectTextReader.ReadObjectArray(GeometryGrid.GeometryName,
                                       (string st, ref int position, out Structure obj) =>
                                           {
                                               position = st.GetNextLinePosition(position);
                                               obj = Structure.GetFromString(st, ref position, elementsCount);
                                               return obj != null;
                                           });

                if (GeometryArray == null || GeometryArray.Length != Q1Count * Q2Count)
                {
                    return LangResource.GeometryStructuresNotFoundOrItsCountIsWrong;
                }

                structures = GeometryArray.GetArray2D(Q1Count, Q2Count);

                return null;
            }
            return String.Empty;
        }
         
    }
}
