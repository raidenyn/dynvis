using DynVis.Math;

namespace DynVis.Core.ReactionPath
{
    public static class PathExtended
    {
        static public void OptimizeByDistance(this IPath path, double minDistance)
        {
            var SqueareMinDistance = minDistance*minDistance;
            for (var i = 1; i < path.Count; )
            {
                if (path[i - 1].Point.SquearDistanceTo(path[i].Point) < SqueareMinDistance)
                {
                    path.Delete(i);
                } 
                else
                {
                    i++;
                }
            }
        }
    }
}
