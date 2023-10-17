using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DynVis.Core.IO;
using DynVis.Core.Properties;
using DynVis.Math;

namespace DynVis.Core.Points
{
    public sealed class SurfacePoints : CollectionWithCurrentItems<ISurfacePoint>, ISurfacePoints
    {
        public void AddPoints(ICollection<ISurfacePoint> surfacePoints)
        {
            foreach (var point in surfacePoints)
            {
                Add(point);
            }
        }

        public void SaveToFile(Stream stream, FileFilter filter)
        {
            using(var streamWriter = new StreamWriter(stream))
            {
                foreach (var point in this)
                {
                    streamWriter.WriteLine(SurfacePoint.GetAsString(point));
                }
            }
        }

        public void OpenFromFile(Stream stream, FileFilter filter)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var line = streamReader.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    var point = SurfacePoint.GetFromString(line);
                    if (point != null)
                    {
                        Add(point);
                    }
                    line = streamReader.ReadLine();
                }
            }
        }

        private static readonly FileFilter[] _SaveFilters = new[] {new FileFilter(LangGeneral.SurfacePoints, "*.points")};
        public FileFilter[] SaveFilters
        {
            get { return _SaveFilters; }
        }

        public FileFilter[] OpenFilters
        {
            get { return _SaveFilters; }
        }
    }

    public class SurfacePoint : ISurfacePoint
    {
        private string _Name;
        public string Name
        {
            get
            {
                return _Name ?? SurfacePointTypeName.GetName(SurfacePointType);
            }
            set
            {
                _Name = value;
            }
        }

        public IPointD Point
        {
            get { return PointD; }
        }

        public PointD PointD
        {
            get;
            set;
        }

        public string AdditionalInformation
        {
            get;
            set;
        }

        public SurfacePointType SurfacePointType
        {
            get;
            set;
        }

        public static string GetAsString(ISurfacePoint point)
        {
            return String.Format("'{0}' ( {1}; {2}) '{3}'", point.Name, point.Point.X, point.Point.Y, point.AdditionalInformation);
        }

        public static SurfacePoint GetFromString(string strPoint)
        {
            var RegEx = new Regex(@"'(?<Name>.*)' +\( *(?<X>(\d|\.|-|E)+) *; *(?<Y>(\d|\.|-|E)+) *\) *('(?<AdditionalInformation>.*)')?");

            var match = RegEx.Match(strPoint);

            

            if (match.Success)
            {
                var result = new SurfacePoint();

                result.Name = match.Groups["Name"].Value;

                result.PointD = new PointD();

                double d;

                if (double.TryParse(match.Groups["X"].Value, out d))
                result.PointD.X = d;

                if (double.TryParse(match.Groups["Y"].Value, out d))
                result.PointD.Y = d;

                result.AdditionalInformation = match.Groups["AdditionalInformation"].Value;

                return result;
            }

            return null;
        }
    }
}
