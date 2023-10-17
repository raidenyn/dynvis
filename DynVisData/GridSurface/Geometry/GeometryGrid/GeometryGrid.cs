using DynVis.Core;
using DynVis.Core.Geometry;
using DynVis.Math;

namespace DynVis.Data.Geometry
{
    internal partial class GeometryGrid : IReactionSystemGeometry
    {
        public GeometryGrid(int[] elements, double[] arrayQ1, double[] arrayQ2, IStructure[,] geometries)
        {
            GridStructure = new ApplicateGrid<IStructure>(arrayQ1, arrayQ2, geometries);

            Elements = (int[])elements.Clone();

            _AtomStructure = new CalcAtomStructure(Elements.Length, GetElement, GetCoord, GetBondList);
        }


        private readonly ApplicateGrid<IStructure> GridStructure;
        private readonly BondList _BondList = new BondList();

        private readonly int[] Elements;

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

        private BondList GetBondList()
        {
            return _BondList;
        }

        public bool SetPoint(double q1, double q2)
        {
            Q1 = q1;
            Q2 = q2;

            GridStructure.CalcState(q1, q2);

            BondList.UpdateBondsByCovalentRadius(AtomStructure, _BondList);

            _AtomStructure.RiseGeomentryChangedEvent();

            return true;
        }

        public bool SetPoint(IPointD P)
        {
            return SetPoint(P.X, P.Y);
        }

        private readonly CalcAtomStructure _AtomStructure;
        public IAtomStructure AtomStructure
        {
            get
            {
                return _AtomStructure;
            }
        }

        private int GetElement(int number)
        {
            return Elements[number];
        }

        private IPoint3D GetCoord(int number)
        {
            var p1 = GridStructure.objectQ1MinQ2Min[number];
            var p2 = GridStructure.objectQ1MinQ2Max[number];
            var p3 = GridStructure.objectQ1MaxQ2Max[number];
            var p4 = GridStructure.objectQ1MaxQ2Min[number];

            var q1Coeff = GridStructure.q1Coeff;
            var q2Coeff = GridStructure.q2Coeff;

            var p12 = (p1*(1-q2Coeff) + p2*(q2Coeff));
            var p23 = (p2*(1-q1Coeff) + p3*(q1Coeff));
            var p34 = (p4*(1-q2Coeff) + p3*(q2Coeff));
            var p41 = (p1*(1-q1Coeff) + p4*(q1Coeff));

            return ((p12 * (1-q1Coeff) + p34 * (q1Coeff)) + (p41 * (1-q2Coeff) + p23 * (q2Coeff))) * 0.5;
        }
    }
}
