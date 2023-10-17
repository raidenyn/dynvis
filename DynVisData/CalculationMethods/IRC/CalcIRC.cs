using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.Progress;
using DynVis.Data.Properties;

namespace DynVis.Data.CalculationMethods.IRC
{
    class CalcIRC : ICalculationPath
    {
        private IRC IRCPath;
        
        public IPath CalcPath(ISurface3D PES)
        {
            if (PES == null)
            {
                MessageBox.Show(ApplicationExtension.GlobalOwner(), LangResource.PESNotSpecified, Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }

            if (IRCPath == null) IRCPath = new IRC();

            var ProgressNotifier = new ProgressNotifier(LangResource.MinimalEnergyPathCalculation);

            IRCPath.Progress = ProgressNotifier.ProgressChanged;

            ProgressNotifier.BeginProgress();

            var result = IRCPath.CalcIRC(PES);

            ProgressNotifier.CompliteProgress();

           

            return result;
        }
    }
}
