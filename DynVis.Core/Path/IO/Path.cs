using System;
using System.IO;
using DynVis.Core.IO;
using DynVis.Core.Properties;

namespace DynVis.Core.ReactionPath
{
    partial class Path
    {
        private const string Q1Name = "Q1";
        private const string Q2Name = "Q2";
        private const string EnergyName = "Energy";
        private const string TimeName = "Time";
        private const string PathName = "Name";
        
        public void SaveToFile(Stream stream, FileFilter filter)
        {
            var ColumnLength = 20;

            using (var ObjectWriter = new ObjectTextWriter(stream))
            {
                ObjectWriter.WriteObject(PathName, Name);
            }

            using (var TableWriter = new TextTableWriter(stream))
            {
                TableWriter.AppendArrayColumn(Q1Name, ColumnLength, CalculatingValuePoint, item => item.Point.X, TextTableWriter.DecimalFormat);
                TableWriter.AppendArrayColumn(Q2Name, ColumnLength, CalculatingValuePoint, item => item.Point.Y, TextTableWriter.DecimalFormat);
                TableWriter.AppendArrayColumn(EnergyName, ColumnLength, CalculatingValuePoint, item => item.Value, TextTableWriter.DecimalFormat);
                TableWriter.AppendArrayColumn(TimeName, ColumnLength, CalculatingValuePoint, item => item.Time, TextTableWriter.DecimalFormat);
            }
        }

        internal static readonly FileFilter[] OpenFilters = new[] { new FileFilter(LangGeneral.PathNameColumnCaption, "*.rp") };
        public FileFilter[] SaveFilters
        {
            get { return OpenFilters; }
        }

        public static Path OpenFromFile(Stream stream, FileFilter filter)
        {
            string source;
            using (var streamReader = new StreamReader(stream))
            {
                source = streamReader.ReadToEnd();
            }
            
            var Reader = new TextTableReader(source);

            var position = source.GetNextLinePosition(source.GetNextLinePosition(0));

            var ResultPath = new Path();

            Reader.ReadTable(position, i =>
                                                {
                                                    var p = new PathPoint();
                                                    ResultPath.CalculatingValuePoint.Add(p);
                                                },
                             new SetValue<int, double>[]
                                 {
                                     (i, d) =>
                                         {
                                             ResultPath.CalculatingValuePoint[i].Point.X = d;
                                         },
                                     (i, d) =>
                                         {
                                             ResultPath.CalculatingValuePoint[i].Point.Y = d;
                                         },
                                     (i, d) =>
                                         {
                                             ResultPath.CalculatingValuePoint[i].GetPointValue = ResultPath.GetValue;
                                         },
                                     (i, d) =>
                                         {
                                             ResultPath.CalculatingValuePoint[i].Time = d;
                                         }
                                 }
                );

            var ObjectReader = new ObjectTextReader(source);

            ResultPath.Name = ObjectReader.ReadObject(PathName, (string st, ref int pos, out string name) =>
                                                                  {
                                                                      name = st.FromPositionToEndLine(pos+1);
                                                                      return true;
                                                                  }, String.Empty);

            //Если в пути меньше 2 точек, мы так не играем
            return ResultPath.Count > 1 ? ResultPath : null;  
        }
    }
}
