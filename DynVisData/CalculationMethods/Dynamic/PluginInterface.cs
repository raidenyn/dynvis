using System.Drawing;
using DynVis.Core;
using DynVis.Core.Plugin;
using DynVis.Data.Properties;

namespace DynVis.Data.CalculationMethods.Dynamic
{
    [DynVisPathPlugin]
    public class DynamicPathPluginInterface : IDynVisPathPlugin
    {
        public string PluginName
        {
            get { return LangResource.ReactionDynamicCalculation; }
        }

        public string CaptionText
        {
            get { return LangResource.ReactionDynamic; }
        }

        public Image CaptionImage
        {
            get { return null; }
        }


        private ICalculationPath _CalculationPath;
        public ICalculationPath CalculationPath
        {
            get
            {
                if (_CalculationPath == null)
                {
                    _CalculationPath = new CalcDynamicPath();
                }
                return _CalculationPath;
            }
        }
    }
}
