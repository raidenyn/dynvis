using System.IO;
using DynVis.Core.IO;
using DynVis.Core.Progress;
using DynVis.Core.Properties;
using DynVis.Core.Surface.IO;

namespace DynVis.Core.Surface
{
    partial class BaseSurface3D
    {
        private void SaveApplicateMatrixText(Stream stream)
        {
            var form = new FormFileProperty();
            form.ShowDialog();

            var FirstDerivative = form.FirstDerivative;
            var SecondDerivative = form.SecondDerivative;

            var Q1PointCount = form.Q1Count;
            var Q2PointCount = form.Q2Count;

            var Q1Min = Axes1.MinValue;
            var Q2Min = Axes2.MinValue;
            var Q1Max = Axes1.MaxValue;
            var Q2Max = Axes2.MaxValue;

            var ObjectWriter = new ObjectTextWriter(stream);

            var ProgressNotifier = new ProgressNotifier(LangGeneral.FileCreating);
            ProgressNotifier.BeginProgress();

            ObjectWriter.WriteObject(Axes1ValuesName, (IStringable)Axes1);
            ObjectWriter.WriteObject(Axes2ValuesName, (IStringable)Axes2);

            ObjectWriter.AppendLine();

            ObjectWriter.WriteDoubleSeries(Q1ValuesName, Q1Min, Q1Max, Q1PointCount, ObjectTextWriter.StandartSeparator, ObjectTextWriter.StandartLineWidth, ObjectTextWriter.DecimalFormat);

            ProgressNotifier.ProgressChanged(10);

            ObjectWriter.WriteDoubleSeries(Q2ValuesName, Q2Min, Q2Max, Q2PointCount, ObjectTextWriter.StandartSeparator, ObjectTextWriter.StandartLineWidth, ObjectTextWriter.DecimalFormat);

            ProgressNotifier.ProgressChanged(20);

            ObjectWriter.WriteObjectSeries2D(EnergyValuesName, Q1Min, Q1Max, Q1PointCount, Q2Min, Q2Max, Q2PointCount, E, ObjectTextWriter.StandartSeparator, ObjectTextWriter.StandartLineWidth, ObjectTextWriter.DecimalFormat);

            ProgressNotifier.ProgressChanged(30);

            if (FirstDerivative)
            {
                ObjectWriter.WriteObjectSeries2D(FirstDerivativeQ1ValuesName, Q1Min, Q1Max, Q1PointCount, Q2Min, Q2Max, Q2PointCount, dEdq1, ObjectTextWriter.StandartSeparator, ObjectTextWriter.StandartLineWidth, ObjectTextWriter.DecimalFormat);

                ProgressNotifier.ProgressChanged(40);


                ObjectWriter.WriteObjectSeries2D(FirstDerivativeQ2ValuesName, Q1Min, Q1Max, Q1PointCount, Q2Min, Q2Max, Q2PointCount, dEdq2, ObjectTextWriter.StandartSeparator, ObjectTextWriter.StandartLineWidth, ObjectTextWriter.DecimalFormat);

                ProgressNotifier.ProgressChanged(50);
            }

            if (SecondDerivative)
            {
                ObjectWriter.WriteObjectSeries2D(SecondDerivativeQ1ValuesName, Q1Min, Q1Max, Q1PointCount, Q2Min, Q2Max, Q2PointCount, d2Edq1dq1, ObjectTextWriter.StandartSeparator, ObjectTextWriter.StandartLineWidth, ObjectTextWriter.DecimalFormat);

                ProgressNotifier.ProgressChanged(60);

                ObjectWriter.WriteObjectSeries2D(SecondDerivativeQ2ValuesName, Q1Min, Q1Max, Q1PointCount, Q2Min, Q2Max, Q2PointCount, d2Edq2dq2, ObjectTextWriter.StandartSeparator, ObjectTextWriter.StandartLineWidth, ObjectTextWriter.DecimalFormat);

                ProgressNotifier.ProgressChanged(70);

                ObjectWriter.WriteObjectSeries2D(MixedSecondDerivativeValuesName, Q1Min, Q1Max, Q1PointCount, Q2Min, Q2Max, Q2PointCount, d2Edq1dq2, ObjectTextWriter.StandartSeparator, ObjectTextWriter.StandartLineWidth, ObjectTextWriter.DecimalFormat);

                ProgressNotifier.ProgressChanged(80);
            }

            ObjectWriter.Close();
            ProgressNotifier.CompliteProgress();

            
        }

        public const string Q1ValuesName = "Q1 Values";
        public const string Q2ValuesName = "Q2 Values";
        public const string Axes1ValuesName = "Axes1";
        public const string Axes2ValuesName = "Axes2";
        public const string EnergyValuesName = "Energy Values";
        public const string FirstDerivativeQ1ValuesName = "dE / dq1";
        public const string FirstDerivativeQ2ValuesName = "dE / dq2";
        public const string SecondDerivativeQ1ValuesName = "dE2 / dq1 dq1";
        public const string SecondDerivativeQ2ValuesName = "dE2 / dq2 dq2";
        public const string MixedSecondDerivativeValuesName = "dE2 / dq1 dq2";

        private void SaveTableText(Stream stream)
        {
            var form = new FormFileProperty();
            form.ShowDialog();

            var q1PointCount = form.Q1Count;
            var q2PointCount = form.Q2Count;

            var FirstDerivative = form.FirstDerivative;
            var SecondDerivative = form.SecondDerivative;

            var q1Min = Axes1.MinValue;
            var q2Min = Axes2.MinValue;
            var q1Max = Axes1.MaxValue;
            var q2Max = Axes2.MaxValue;


            var ProgressNotifier = new ProgressNotifier(LangGeneral.FileCreating);
            ProgressNotifier.BeginProgress();

            var ColumnLength = 20;

            using (var TableWriter = new TextTableWriter(stream))
            {
                TableWriter.AppendDoubleSeries2DColumn(Q1ValuesName, ColumnLength, q1Min, q1Max, q1PointCount, q2Min, q2Max, q2PointCount, (q1, q2) => q1);
                TableWriter.AppendDoubleSeries2DColumn(Q2ValuesName, ColumnLength, q1Min, q1Max, q1PointCount, q2Min, q2Max, q2PointCount, (q1, q2) => q2);
                TableWriter.AppendDoubleSeries2DColumn(EnergyValuesName, ColumnLength, q1Min, q1Max, q1PointCount, q2Min, q2Max, q2PointCount, E);
                if (FirstDerivative)
                {
                    TableWriter.AppendDoubleSeries2DColumn(FirstDerivativeQ1ValuesName, ColumnLength, q1Min, q1Max, q1PointCount, q2Min, q2Max, q2PointCount, dEdq1);
                    TableWriter.AppendDoubleSeries2DColumn(FirstDerivativeQ2ValuesName, ColumnLength, q1Min, q1Max, q1PointCount, q2Min, q2Max, q2PointCount, dEdq2);
                }
                if (SecondDerivative)
                {
                    TableWriter.AppendDoubleSeries2DColumn(SecondDerivativeQ1ValuesName, ColumnLength, q1Min, q1Max, q1PointCount, q2Min, q2Max, q2PointCount, d2Edq1dq1);
                    TableWriter.AppendDoubleSeries2DColumn(SecondDerivativeQ2ValuesName, ColumnLength, q1Min, q1Max, q1PointCount, q2Min, q2Max, q2PointCount, d2Edq2dq2);
                    TableWriter.AppendDoubleSeries2DColumn(MixedSecondDerivativeValuesName, ColumnLength, q1Min, q1Max, q1PointCount, q2Min, q2Max, q2PointCount, d2Edq1dq2);
                }
            }

            ProgressNotifier.CompliteProgress();
        }

        public virtual void SaveToFile(Stream stream, FileFilter filter)
        {
            if (filter == _SaveFilters[0])
            {
                SaveApplicateMatrixText(stream);
            }
            if (filter == _SaveFilters[1])
            {
                SaveTableText(stream);
            }
        }


        private static readonly FileFilter[] _SaveFilters = new[] { new FileFilter(LangGeneral.ApplicateMatrix, "*.sam"), new FileFilter(LangGeneral.Table, "*.st") };
        public virtual FileFilter[] SaveFilters
        {
            get { return _SaveFilters; }
        }

        private static readonly FileFilter[] _OpenFilters = new FileFilter[0];
        public virtual FileFilter[] OpenFilters
        {
            get { return _OpenFilters; }
        }
    }


}
