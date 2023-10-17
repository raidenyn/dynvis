using System;
using System.IO;
using DynVis.Core.IO;
using DynVis.Data.Properties;

namespace DynVis.Data.Geometry
{
    partial class GeometryGrid 
    {
        public void SaveToFile(Stream stream, FileFilter filter)
        {
            using (var ObjectWriter = new ObjectTextWriter(stream))
            {
                ObjectWriter.WriteObjectArray(Q1ArrayName, GridStructure.ArrayQ1, ObjectTextWriter.DecimalFormat, ObjectTextWriter.StandartLineWidth);
                ObjectWriter.WriteObjectArray(Q2ArrayName, GridStructure.ArrayQ2, ObjectTextWriter.DecimalFormat, ObjectTextWriter.StandartLineWidth);

                ObjectWriter.WriteObjectArray(ElementsName, Elements, ObjectTextWriter.IntegerFormat, ObjectTextWriter.StandartLineWidth);

                ObjectWriter.WriteObjectArray2D(GeometryName, GridStructure.Objects, Environment.NewLine, ObjectTextWriter.StandartLineWidth);
            }
        }

        internal const string Q1ArrayName = "Q1 Geometry Values";
        internal const string Q2ArrayName = "Q2 Geometry Values";
        internal const string ElementsName = "Elements";
        internal const string GeometryName = "Structures";

        internal static readonly FileFilter[] Filters = new[]
                                                            {
                                                                new FileFilter(LangResource.ReagentSystemStructure, "*.srs")
                                                            };
        public FileFilter[] SaveFilters
        {
            get { return Filters; }
        }
    }
}
