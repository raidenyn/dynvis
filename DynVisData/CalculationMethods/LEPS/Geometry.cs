using DynVis.Core.Geometry;
using M=System.Math;
using System.IO;
using DynVis.Core;
using DynVis.Core.IO;
using DynVis.Data.Geometry;
using DynVis.Math;

namespace DynVis.Data.CalculationMethods.LEPS
{
    internal class GeometryLEPS : IReactionSystemGeometry
    {
        private readonly SurfaceLEPS Surface;

        internal GeometryLEPS(SurfaceLEPS surface)
        {
            CalcAtomStructure = new CalcAtomStructure(AtomCount, GetElement, GetCoord, GetBondList);
            Surface = surface;
        }
        public bool SetPoint(double q1, double q2)
        {
            Q1 = q1;
            Q2 = q2;

            BondList.UpdateBondsByCovalentRadius(AtomStructure, _BondList);

            CalcAtomStructure.RiseGeomentryChangedEvent();

            return true;
        }

        public bool SetPoint(IPointD P)
        {
            return SetPoint(P.X, P.Y);
        }

        private const int AtomCount = 3;

        public IAtomStructure AtomStructure
        {
            get { return CalcAtomStructure; }
        }

        public double Q1
        {
            get;
            private set;
        }

        public double Q2
        {
            get;
            private set;
        }

        private readonly BondList _BondList = new BondList();
        
        private BondList GetBondList()
        {
            return _BondList;
        }

        private readonly CalcAtomStructure CalcAtomStructure;

        private int GetElement(int number)
        {
            switch (number)
            {
                case 0: return Surface.Params.ElementA;
                case 1: return Surface.Params.ElementB;
                case 2: return Surface.Params.ElementC;
            }
            return 0;
        }

        private readonly Point3D Point = new Point3D();

        private IPoint3D GetCoord(int number)
        {
            switch (number)
            {
                case 0:
                    return GetCoord1();
                case 1:
                    return GetCoord2();
                case 2:
                    return GetCoord3();
            }
            return null;
        }

        private IPoint3D GetCoord1()
        {
            Point.SetValues(Q1, 0, 0);
            return Point;
        }

        private IPoint3D GetCoord2()
        {
            Point.SetValues(0, 0, 0);
            return Point;
        }

        private IPoint3D GetCoord3()
        {
            if (Surface.AxesQ2.AxesType == LEPSAxesType.Distance)
            {
                Point.SetValues(M.Cos(Surface.Params.ConstQValue) * Q2, M.Sin(Surface.Params.ConstQValue) * Q2, 0);
            }
            else
            {
                Point.SetValues(M.Cos(Q2) * Surface.Params.ConstQValue, M.Sin(Q2) * Surface.Params.ConstQValue, 0);
            }
            return Point;
        }

        public void SaveToFile(Stream stream, FileFilter filter)
        {
            
        }

        public FileFilter[] SaveFilters
        {
            get { return null; }
        }
    }
}
