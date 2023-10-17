using System.Collections.Generic;
using DynVis.Core.IO;
using DynVis.Core.Properties;
using DynVis.Math;

namespace DynVis.Core
{
    public interface ISurfacePoints : ICollectionWithCurrentItems<ISurfacePoint>, ISaveFile, IOpenFile
    {

    }


    public interface ISurfacePoint
    {
        string Name { get; }
        IPointD Point { get; }

        string AdditionalInformation {get;}

        SurfacePointType SurfacePointType {get; }
    }

    public interface ICalcSurfacePoints
    {
        ICollection<ISurfacePoint> CalcPoints(ISurface3D surface);
    }

    public enum SurfacePointType { Simple, Stationary, Maximum, Minimum, Minimax }

    public static class SurfacePointTypeName
    {
        public static string GetName(SurfacePointType type)
        {
            switch (type)
            {
                case SurfacePointType.Simple:
                    return LangGeneral.Point;
                case SurfacePointType.Stationary:
                    return LangGeneral.StationaryPoint;
                case SurfacePointType.Maximum:
                    return LangGeneral.MaximumPoint;
                case SurfacePointType.Minimum:
                    return LangGeneral.MinimumPoint;
                case SurfacePointType.Minimax:
                    return LangGeneral.SaddlePoint;
                default:
                    return LangGeneral.UnknowTypePoint;
            }
        }
    }
}
