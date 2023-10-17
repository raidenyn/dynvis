using System.IO;
using DynVis.Core.IO;
using DynVis.Data.Properties;
using DynVis.Math;

namespace DynVis.Data.GridSurface
{
    partial class GridSurface
    {
        private void SaveToFileApplicateMatrixGrid(Stream stream)
        {
            using (var ObjectWriter = new ObjectTextWriter(stream))
            {
                ObjectWriter.WriteObjectArray("Q1 Values:", Q1, ObjectTextWriter.DecimalFormat, ObjectTextWriter.StandartLineWidth);

                ObjectWriter.WriteObjectArray("Q2 Values:", Q2, ObjectTextWriter.DecimalFormat, ObjectTextWriter.StandartLineWidth);
                ObjectWriter.WriteObjectArray2D("Energy Values:", Energy, ObjectTextWriter.DecimalFormat, ObjectTextWriter.StandartLineWidth);
            }
        }

        private void SaveToFileTableGrid(Stream stream)
        {
            var ColumnLength = 20;

            using (var TableWriter = new TextTableWriter(stream))
            {
                TableWriter.AppendArrayColumn(Q1ValuesName, ColumnLength, Q1, TextTableWriter.DecimalFormat);
                TableWriter.AppendArrayColumn(Q2ValuesName, ColumnLength, Q2, TextTableWriter.DecimalFormat);
                TableWriter.AppendArray2DColumn(Q1ValuesName, ColumnLength, Energy, TextTableWriter.DecimalFormat);
            }
        }

        public override void SaveToFile(Stream stream, FileFilter filter)
        {
            if (filter == _SaveFilters[0])
            {
                SaveToFileApplicateMatrixGrid(stream);
                return;
            }
            if (filter == _SaveFilters[1])
            {
                SaveToFileTableGrid(stream);
                return;
            }
            base.SaveToFile(stream, filter);
        }

        private readonly FileFilter[] _SaveFilters = new[] { new FileFilter(LangResource.GridValue, "*.sam"), new FileFilter("Таблица значений", "*.st") };
        /*
        public override FileFilter[] SaveFilters
        {
            get
            {
                return _SaveFilters.MergeTo(base.SaveFilters);
            }
        }
         * */
    }
}
