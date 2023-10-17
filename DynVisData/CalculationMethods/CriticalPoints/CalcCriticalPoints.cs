using System.Collections.Generic;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.Progress;
using DynVis.Data.Properties;

namespace DynVis.Data.CalculationMethods.CriticalPoints
{
    internal class CalcCriticalPoints : ICalcSurfacePoints
    {
        private CriticalPoints CriticalPoints;

        public ICollection<ISurfacePoint> CalcPoints(ISurface3D PES)
        {
            if (PES == null)
            {
                MessageBox.Show(ApplicationExtension.GlobalOwner(), LangResource.PESNotSpecified, Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }

            if (CriticalPoints == null) CriticalPoints = new CriticalPoints();

            var FormCriticalPoint = new FormCriticalPoint {Surface = PES, CriticalPoints = CriticalPoints };

            if (FormCriticalPoint.ShowDialog(ApplicationExtension.GlobalOwner()) == DialogResult.OK)
            {
                var ProgressNotifier = new ProgressNotifier(LangResource.ScanSurface);

                CriticalPoints.Progress = ProgressNotifier.ProgressChanged;

                ProgressNotifier.BeginProgress();

                var result = CriticalPoints.CalcPoints(PES);

                ProgressNotifier.CompliteProgress();

                return result;
            }
            return null;
        }
    }
}
