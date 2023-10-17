using M=System.Math;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynVis.Math.Geometry
{
    public class Atom
    {
        public int Element;
        public IPoint3D Coord;
    }
    
    public class IntrestingPoint
    {
        public IPoint3D Coord;

        public IPoint3D Atom1Coord;

        public IPoint3D Atom2Coord;

        public IntrestingPoint (IPoint3D coord, IPoint3D atom1Coord, IPoint3D atom2Coord)
        {
            Coord = coord;
            Atom1Coord = atom1Coord;
            Atom2Coord = atom2Coord;
        }
    }

    public class SymmetrySystem
    {
        public List<Atom> Atoms;

        public IEnumerable<IPoint3D> Coords
        {
            get
            {
                return from atom in Atoms select atom.Coord;
            }
        }

        public List<IntrestingPoint> IntrestingPoints;

        private Point3D _Ceneter;
        public Point3D Ceneter
        {
            get
            {
                if (_Ceneter == null)
                {
                    _Ceneter = Coords.GeometryCenter();
                }
                return _Ceneter;
            }
        }

        public double Epsilon = 1E-9;

        private bool Equials(double d1, double d2)
        {
            return M.Abs(d1 - d2) <= Epsilon;
        }
        
        private void CalcIntrestingPoint()
        {
            IntrestingPoints = new List<IntrestingPoint>();

            for (var i = 0; i < Atoms.Count; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (CheckPair(Atoms[i], Atoms[j]))
                    {
                        var p = new Point3D(Atoms[j].Coord);
                        p.SumTo(Atoms[i].Coord);
                        p.DerTo(2);

                        IntrestingPoints.Add(new IntrestingPoint(p,Atoms[i].Coord, Atoms[j].Coord));
                    }
                }
            }
        }

        private bool CheckPair(Atom a1, Atom a2)
        {
            if (a1.Element != a2.Element) return false;

            if (Equials(Ceneter.SquearDistanceTo(a1.Coord), Ceneter.SquearDistanceTo(a2.Coord))) return false;

            return true;
        }


    }
  
    

}
