using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.Progress;
using DynVis.Data.Properties;

namespace DynVis.Data.CalculationMethods.Dynamic
{
    public class CalcDynamicPath : ICalculationPath
    {
        private DynamicPath DynamicPath;


        public IPath CalcPath(ISurface3D PES)
        {
            if (PES == null)
            {
                MessageBox.Show(ApplicationExtension.GlobalOwner(), LangResource.PESNotSpecified, Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }

            if (DynamicPath == null) DynamicPath = new DynamicPath();
            if ((new FormDynamicParamsEditor()).ShowDialog(ApplicationExtension.GlobalOwner(), PES, ref DynamicPath) == DialogResult.OK)
            {
                var ProgressNotifier = new ProgressNotifier(LangResource.ReactionDynamicCalculation);
                
                DynamicPath.Progress = ProgressNotifier.ProgressChanged;

                ProgressNotifier.BeginProgress();

                var Result =  DynamicPath.CalcPath(PES);

                ProgressNotifier.CompliteProgress();

                return Result;
            }
            return null;
        }
    }
}
