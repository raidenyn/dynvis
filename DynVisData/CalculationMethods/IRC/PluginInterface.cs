using System.Drawing;
using DynVis.Core;
using DynVis.Core.Plugin;

namespace DynVis.Data.CalculationMethods.IRC
{
    [DynVisPathPlugin]
    public class IRCPluginInterface : IDynVisPathPlugin
    {
        public string PluginName
        {
            get { return "Расчет пути реакции минимальной энергии"; }
        }

        public string CaptionText
        {
            get { return "Путь реакции минимальной энергии"; }
        }

        public Image CaptionImage
        {
            get { return null; }
        }

        private readonly CalcIRC CalcIRC = new CalcIRC();
        public ICalculationPath CalculationPath
        {
            get { return CalcIRC; }
        }
    }
}
