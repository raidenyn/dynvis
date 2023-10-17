using System.Drawing;
using DynVis.Core;
using DynVis.Core.Plugin;
using DynVis.Data.Properties;

namespace DynVis.Data.CalculationMethods
{
    [DynVisSurfacePlugin]
    public class LEPSPluginInterface : IDynVisSurfacePlugin
    {
        public string PluginName
        {
            get { return LangResource.CalcLEPS; }
        }

        public string CaptionText
        {
            get { return LangResource.LEPS; }
        }

        public Image CaptionImage
        {
            get { return null; }
        }

        public ReactionData CreateReactionData()
        {
            return new CalculationLEPS();
        }
    }
}
