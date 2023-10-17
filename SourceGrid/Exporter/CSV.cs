#region

using System;
using System.Globalization;
using System.IO;
using SourceGrid.Cells;

#endregion

namespace SourceGrid.Exporter
{
    /// <summary>
    /// An utility class to export a grid to a csv delimited format file.
    /// </summary>
    public class CSV
    {
        private string mFieldSeparator;

        private string mLineSeparator;

        public CSV()
        {
            mFieldSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            mLineSeparator = Environment.NewLine;
        }

        public string FieldSeparator
        {
            get { return mFieldSeparator; }
            set { mFieldSeparator = value; }
        }

        public string LineSeparator
        {
            get { return mLineSeparator; }
            set { mLineSeparator = value; }
        }

        public virtual void Export(GridVirtual grid, TextWriter stream)
        {
            for (int r = 0; r < grid.Rows.Count; r++)
            {
                for (int c = 0; c < grid.Columns.Count; c++)
                {
                    if (c > 0)
                        stream.Write(mFieldSeparator);

                    ICellVirtual cell = grid.GetCell(r, c);
                    var pos = new Position(r, c);
                    var context = new CellContext(grid, pos, cell);
                    ExportCSVCell(context, stream);
                }
                stream.Write(mLineSeparator);
            }
        }

        protected virtual void ExportCSVCell(CellContext context, TextWriter stream)
        {
            if (context.Cell != null)
            {
                string text = context.DisplayText;
                if (text == null)
                    text = string.Empty;
                text = text.Replace("\r\n", " ");
                text = text.Replace("\n", " ");
                text = text.Replace("\r", " ");
                stream.Write(text);
            }
            else
            {
                stream.Write("");
            }
        }
    }
}