using System.Drawing;
using DynVis.Core;
using DynVis.Core.Plugin;
using DynVis.Data.Properties;

namespace DynVis.Data.GridSurface
{
    [DynVisSurfacePlugin]
    public class GridSurfacePluginInterface: IDynVisSurfacePlugin
    {
        public string PluginName
        {
            get { return LangResource.CreateSurfaceByApplicateMatrix; }
        }

        public string CaptionText
        {
            get { return LangResource.GridValue; }
        }

        public Image CaptionImage
        {
            get { return null; }
        }

        public ReactionData CreateReactionData()
        {
            return new GridData();
        }
    }
}
