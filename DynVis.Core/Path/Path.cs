using System;
using System.Collections;
using System.Collections.Generic;
using DynVis.Math;

namespace DynVis.Core.ReactionPath
{
    public sealed partial class Path : IPath
    {
        private ISurface3D PES;
        
        public Path()
        {
            
        }

        public Path(ISurface3D pes)
        {
            ApllyToPES(pes);
        }

        private readonly List<PathPoint> CalculatingValuePoint = new List<PathPoint>();

        public IPathPoint Point(int index)
        {
            return CalculatingValuePoint[index];
        }

        public IPathPoint this[int index]
        {
            get { return CalculatingValuePoint[index];}
        }

        public int Count
        {
            get { return CalculatingValuePoint.Count; }
        }

        private Surface3DPointD PointProvider;

        private double GetValue(PointD p)
        {
            return (PointProvider != null) ? PointProvider(p): 0;
        }

        public void Add(IPointD p, double time)
        {
            Add(p.X, p.Y, time);
        }

        public void Add(double q1, double q2, double time)
        {
            var point = new PointD(q1, q2);
            var PathPoint = new PathPoint(new CalculatingValuePoint(point, GetValue), time);
            CalculatingValuePoint.Add(PathPoint);
            CurrentPointIndex = Count - 1;
        }

        public void Delete(int index)
        {
            CalculatingValuePoint.RemoveAt(index);
        }

        private int _CurrentPointIndex;
        public int CurrentPointIndex
        {
            get
            {
                return _CurrentPointIndex;
            }
            set
            {
                _CurrentPointIndex = value;
                RiseCurrentPointChanged();
            }
        }

        public IPathPoint CurrentPoint
        {
            get { return this[CurrentPointIndex]; }
        }

        private int? _MinEnergyPointIndex;
        private int? _MaxEnergyPointIndex;

        public int MinEnergyPointIndex
        {
            get
            {
                if (_MinEnergyPointIndex == null && Count > 0)
                {
                    _MinEnergyPointIndex = CalculatingValuePoint.GetIndexOfMin(p => p.Value);
                }
                return _MinEnergyPointIndex ?? 0;
            }
        }

        public int MaxEnergyPointIndex
        {
            get
            {
                if (_MaxEnergyPointIndex == null)
                {
                    _MaxEnergyPointIndex = CalculatingValuePoint.GetIndexOfMax(p => p.Value);
                }
                return _MaxEnergyPointIndex.Value;
            }
        }

        public IPathPoint MinEnergyPoint
        {
            get { return this[MinEnergyPointIndex]; }
        }

        public IPathPoint MaxEnergyPoint
        {
            get { return this[MaxEnergyPointIndex]; }
        }

        public event EventHandler CurrentPointChanged;

        private void RiseCurrentPointChanged()
        {
            if (CurrentPointChanged != null) CurrentPointChanged(this, new EventArgs());
        }

        public void ApllyToPES(ISurface3D pes)
        {
            PES = pes;
            PointProvider = pes.E;
        }

        public IEnumerator GetEnumerator()
        {
            return CalculatingValuePoint.GetEnumerator();
        }

        private ScaleDimension _EnergyDimension;
        public ScaleDimension EnergyDimension
        {
            get
            {
                if (PES != null)
                {
                    return PES.EnergyDimension;
                }
                return _EnergyDimension;
            }
            set
            {
                _EnergyDimension = value;
            }
        }

        public string Name
        {
            get;
            set;
        }
    }

    internal class PathPoint: IPathPoint
    {
        internal PathPoint()
        {
            CalculatingValuePoint = new CalculatingValuePoint();
        }

        internal PathPoint(CalculatingValuePoint valuePoint, double time)
        {
            CalculatingValuePoint = (CalculatingValuePoint)valuePoint.Clone();
            Time = time;
        }


        private readonly CalculatingValuePoint CalculatingValuePoint;

        public PointD Point
        {
            get { return CalculatingValuePoint.Point; }
        }

        public double Value
        {
            get { return CalculatingValuePoint.Value; }
        }

        public Surface3DPointD GetPointValue
        {
            get { return CalculatingValuePoint.GetPointValue; }
            set { CalculatingValuePoint.GetPointValue = value; }
        }

        public double Time
        { get;set;
        }
    }
}
