using DynVis.Core.IO;
using DynVis.Core.ReactionPath;

namespace DynVis.Core
{
    partial class ReactionData
    {
        public bool SavePathDialog()
        {
            if (CurrentReactionPath != null)
            {
                return IOFileDialog.SaveToFile(CurrentReactionPath);
            }
            return false;
        }

        public bool OpenPathDialog()
        {
            if (AllowCalcPath)
            {
                var path = IOFileDialog.OpenObjectFromFile<Path>(Path.OpenFilters, Path.OpenFromFile);
                if (path != null)
                {
                    path.ApllyToPES(Surface);
                    return AddPath(path);
                }
            }
            return false;
        }
    }
}
