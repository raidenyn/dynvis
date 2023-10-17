using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DynVis.Core;
using DynVis.Core.Geometry;
using DynVis.Math;

namespace DynVis.Data.Geometry
{
    internal sealed class Structure : IStructure
    {
        internal Structure(int count)
        {
            PointList = new List<Point3D>(count);
            for (var i = 0; i < count; i++) PointList.Add(new Point3D());

            if (CountItemChanged != null) CountItemChanged(this, new EventArgs());
        }

        internal Structure(IStaticCollection<Point3D> baseStructure, int count)
        {
            PointList = new List<Point3D>(count);
            int i;
            for (i = 0; i < count && i < baseStructure.Count; i++)
            {
                PointList.Add(new Point3D(baseStructure[i]));
            }
            for (i = baseStructure.Count; i < count; i++) PointList[i] = new Point3D();
        }
        
        private readonly List<Point3D> PointList ;

        public Point3D this[int index]
        {
            get
            {
                return PointList[index];
            }
        }

        public event EventHandler CountItemChanged;

        public object Clone()
        {
            return new Structure(this, Count);
        }

        public IEnumerator GetEnumerator()
        {
            return PointList.GetEnumerator();
        }

        public int Count
        {
            get { return PointList.Count; }
        }

        IEnumerator<Point3D> IEnumerable<Point3D>.GetEnumerator()
        {
            return PointList.GetEnumerator();
        }

        public override string ToString()
        {
            var SB = new StringBuilder();
            foreach (var atom in PointList)
            {
                SB.Append(atom + Environment.NewLine);
            }
            return SB.ToString();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var SB = new StringBuilder();
            foreach (var atom in PointList)
            {
                SB.Append(atom.ToString(format, formatProvider) + Environment.NewLine);
            }
            return SB.ToString();
        }

        public static Structure GetFromString(string str, ref int position, int elementsCount)
        {
            var Result = new Structure(elementsCount);

            for (var i = 0; i < elementsCount; i++)
            {
                var Line = str.FromPositionToEndLine(position);
                position += Line.Length + Environment.NewLine.Length;

                var doubleArray = Line.SplitToDoubleArray(" ");
                if (doubleArray.Length > 2)
                {
                    Result.PointList[i].SetValues(doubleArray[doubleArray.Length - 3],
                                                  doubleArray[doubleArray.Length - 2],
                                                  doubleArray[doubleArray.Length - 1]);
                }
                else
                {
                    return null;
                }
            }
            return Result;
        }

        public void SetPoints(IEnumerable<Point3D> points)
        {
            var counter = 0;
            foreach (var point in points)
            {
                PointList[counter] = point;
                counter++;
                if (counter >= PointList.Count) break;
            }
        }
    }

    internal sealed class CalcAtom : IAtom
    {
        internal CalcAtom(int number, GetIntValue getElement, GetIPoint3DValue getCoord)
        {
            Number = number;
            GetCoord = getCoord;
            GetElement = getElement;
        }

        public int Number
        {
            get; private set;
        }

        internal GetIPoint3DValue GetCoord
        {
            get;
            private set;
        }
        internal GetIntValue GetElement
        {
            get;
            private set;
        }

        public int Element { get { return GetElement(Number); } }

        /// <summary>
        /// Необходим для того, чтобы на объект можно было ссылаться без потери ссылки при пересчете
        /// </summary>
        private readonly Point3D RealCoord = new Point3D();

        public IPoint3D Coordinate
        {
            get
            {
                RealCoord.SetValues(GetCoord(Number));
                return RealCoord;
            }
        }

        public override string ToString()
        {
            return String.Format("{0,-3}   {1,16:F9}  {2,16:F9}  {3,16:F9}", Element, Coordinate.X, Coordinate.Y, Coordinate.Z);
        }
    }

    internal sealed class CalcAtomStructure : IAtomStructure
    {
        internal CalcAtomStructure(int atomCount, GetIntValue getElement, GetIPoint3DValue getCoord, BondListHandler bondListHandler)
        {
            Atoms = new CalcAtom[atomCount];

            for (var i=0;i<atomCount;i++)
            {
                Atoms[i] = new CalcAtom(i, getElement, getCoord);
            }

            BondListHandler = bondListHandler;

            //Чтобы не ругались на неиспользрования события
            if (CountItemChanged != null) CountItemChanged(this, new EventArgs());
        }

        private readonly BondListHandler BondListHandler;

        public IBondList BondList
        {
            get { return BondListHandler(); }
        }

        private readonly CalcAtom[] Atoms;

        public IEnumerator GetEnumerator()
        {
            return Atoms.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            Atoms.CopyTo(array, index);
        }

        public int Count
        {
            get { return Atoms.Length; }
        }

        public object SyncRoot
        {
            get { return Atoms.SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return Atoms.IsSynchronized; }
        }

        public IAtom this[int index]
        {
            get { return Atoms[index]; }
        }

        public event EventHandler CountItemChanged;

        IEnumerator<IAtom> IEnumerable<IAtom>.GetEnumerator()
        {
            var enumerator = from atom in Atoms select atom as IAtom;
            return enumerator.GetEnumerator();
        }

        public override string ToString()
        {
            var SB = new StringBuilder();
            foreach (var atom in Atoms)
            {
                SB.Append(atom + Environment.NewLine);
            }
            return SB.ToString();
        }

        public event EventHandler GeometryChanged;

        internal void RiseGeomentryChangedEvent()
        {
            if (GeometryChanged != null) GeometryChanged(this, new EventArgs());
        }
    }

    internal delegate double GetDoubleValue(int number);
    internal delegate int GetIntValue(int number);
    internal delegate IPoint3D GetIPoint3DValue(int number);
    internal delegate BondList BondListHandler();
}
