using System;
using System.IO;
using DynVis.Core.IO;
using DynVis.Core.Properties;

namespace DynVis.Core.ReactionPath
{
    public interface IPathList : ICollectionWithCurrentItems<IPath>
    {
        Func<bool> OpenPathDialog { get; set; }
        Func<bool> SavePathDialog { get; set; }
    }

    internal class PathList : CollectionWithCurrentItems<IPath>, IPathList, ISaveFile
    {
        public Func<bool> OpenPathDialog { get; set; }
        public Func<bool> SavePathDialog { get; set; }

        public void SaveToFile(Stream stream, FileFilter filter)
        {
            
        }

        public static readonly FileFilter[] _SaveFilters = new[] {new FileFilter(LangGeneral.ReactionPathList, "*.spl")};
        public FileFilter[] SaveFilters
        {
            get { return _SaveFilters; }
        }
    }
}
