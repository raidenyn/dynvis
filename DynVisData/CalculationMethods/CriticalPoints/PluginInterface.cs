using System.Drawing;
using DynVis.Core;
using DynVis.Core.Plugin;
using DynVis.Data.Properties;

namespace DynVis.Data.CalculationMethods.CriticalPoints
{
    [DynVisPointsPlugin]
    public class CriticalPointsPluginInterface : IDynVisPointsPlugin
    {
        public string PluginName
        {
            get { return LangResource.FindStationaryPoint; }
        }

        public string CaptionText
        {
            get { return LangResource.StationaryPoints; }
        }

        public Image CaptionImage
        {
            get { return null; }
        }

        private readonly CalcCriticalPoints CalcCriticalPoints = new CalcCriticalPoints();

        public ICalcSurfacePoints CalculationPoints
        {
            get { return CalcCriticalPoints; }
        }
    }
}
