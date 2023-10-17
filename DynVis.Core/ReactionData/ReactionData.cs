using System;
using System.Windows.Forms;
using DynVis.Core.ReactionPath;
using DynVis.Core.Points;
using DynVis.Core.Properties;
using DynVis.EventsLog;
using DynVis.Math;

namespace DynVis.Core
{
    /// <summary>
    /// Базовый класс хранения данных реакционной системы
    /// </summary>
    [Serializable]
    public abstract partial class ReactionData
    {
        protected ReactionData()
        {
            Culture.SetPointAsDecimalSeparator();
            SetPathParams();
            SetPointsParams();
        }

        private void SetPathParams()
        {
            PathList.CurrentItemChanged += PathList_CurrentPathChanged;
            PathList.OpenPathDialog = OpenPathDialog;
            PathList.SavePathDialog = SavePathDialog;
        }

        private void SetPointsParams()
        {
            Points.CurrentItemChanged += Points_CurrentPointChanged;
        }

        /// <summary>
        /// ППЭ
        /// </summary>
        public abstract ISurface3D Surface { get; }
        /// <summary>
        /// Структура реагирующей системы
        /// </summary>
        public abstract IReactionSystemGeometry Geometry { get; }
        /// <summary>
        /// Текущий путь реакции
        /// </summary>
        public IPath CurrentReactionPath
        { get; private set;
        }

        /// <summary>
        /// Список точек на поверхности
        /// </summary>
        public readonly SurfacePoints Points = new SurfacePoints();

        /// <summary>
        /// Список всех путей реакции
        /// </summary>
        private readonly PathList PathList = new PathList();
        /// <summary>
        /// Список всех путей реакции
        /// </summary>
        public IPathList ReactionPathList
        {
            get { return PathList; } 
        }

        /// <summary>
        /// Создание поверхности
        /// </summary>
        /// <param name="ownerForms"></param>
        /// <returns></returns>
        public bool CreateSurface(IWin32Window ownerForms)
        {
            UnsetSurfaceEvents(Surface);
            UnsetGeometryEvents(Geometry);
            var Result = GetNewSurface(ownerForms);
            
            if (Result)
            {
                SetSurfaceEvents(Surface);
                SetGeometryEvents(Geometry);

                Surface.SetAveragePosition();

                Log.LogEvent(LangGeneral.PesCreated);
                RiseSurfaceChanged();
            }
            return Result;
        }

        /// <summary>
        /// Создание геометрии
        /// </summary>
        /// <param name="ownerForms"></param>
        /// <returns></returns>
        public bool CreateGeometry(IWin32Window ownerForms)
        {
            if (Surface == null)
            {
                Log.ShowError(ownerForms, LangGeneral.CreatingGeometryWithoutPESUnpossible);
            }

            UnsetGeometryEvents(Geometry);

            var Result = false;

            if (AllowCreateGeometry)
            {
                Result = GetNewGeometry(ownerForms);
            }

            if (Result)
            {
                Geometry.SetPoint(Surface.CurrentPoint);

                SetGeometryEvents(Geometry);
                Log.LogEvent(LangGeneral.GeometryCreated);
                RiseGeometryChanged();
            }
            return Result;
        }

        /// <summary>
        /// Создает поверхность
        /// </summary>
        /// <param name="ownerForms"></param>
        /// <returns>истина - в случае успеха</returns>
        protected abstract bool GetNewSurface(IWin32Window ownerForms);
        /// <summary>
        /// Создает геометрию
        /// </summary>
        /// <param name="ownerForms"></param>
        /// <returns>истина - в случае успеха</returns>
        protected abstract bool GetNewGeometry(IWin32Window ownerForms);

        /// <summary>
        /// Определяет возможность создания поверхности в данный момент
        /// </summary>
        public bool AllowCreateSurface { get { return AllowNewSurface; } }
        /// <summary>
        /// Определяет возможность создания геометрии в данный момент
        /// </summary>
        public bool AllowCreateGeometry { get { return (Surface != null) && AllowNewGeometry; } }
        /// <summary>
        /// Определяет возможность создания пути в данный момент
        /// </summary>
        public bool AllowCalcPath { get { return (Surface != null) && AllowCalcNewPath; } }
        /// <summary>
        /// Определяет возможность создания точек в данный момент
        /// </summary>
        public bool AllowCalcPoints { get { return (Surface != null) && AllowCalcNewPoints; } }

        protected abstract bool AllowNewSurface { get; }
        protected abstract bool AllowNewGeometry { get; }
        protected virtual bool AllowCalcNewPath { get { return true; } }
        protected virtual bool AllowCalcNewPoints { get { return true; } }

        private bool AddPath(IPath path)
        {
            if (path != null)
            {
                PathList.Add(path);
                PathList.CurrentItem = path;
                Log.LogEvent(LangGeneral.ReactionPathCreated);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Вычисление пути реакции по заданному алгоритму
        /// </summary>
        /// <param name="cp">Алгоритм расчета</param>
        /// <returns>истина - в случае успеха</returns>
        public bool CalcPath(ICalculationPath cp)
        {
            if (AllowCalcPath)
            {
                return AddPath(cp.CalcPath(Surface));
            }
            return false;
        }

        /// <summary>
        /// Вычисление точек по заданному алгоритму
        /// </summary>
        /// <param name="cp">Алгоритм расчета</param>
        /// <returns>истина - в случае успеха</returns>
        public bool CalcPoints(ICalcSurfacePoints cp)
        {
            if (AllowCalcPath)
            {
                var points = cp.CalcPoints(Surface);
                if (points != null)
                {
                    Points.AddPoints(points);
                    Log.LogEvent(LangGeneral.PESPointsFounded);
                    RisePointsChanged();
                    return true;
                }
            }
            return false;
        }


        //События изменения объектов
        public event EventHandler SurfaceChanged;
        public event EventHandler GeometryChanged;
        public event EventHandler ReactionPathChanged;
        public event EventHandler PointsChanged;

        //Функции возбуждения событий
        protected void RiseSurfaceChanged()
        {
            if (SurfaceChanged != null)
            {
                SurfaceChanged(this, new EventArgs());
            }
        }
        protected void RiseGeometryChanged()
        {
            if (GeometryChanged != null) GeometryChanged(this, new EventArgs());
            Surface.SetPoint(Surface.CurrentPoint);
        }
        protected void RiseReactionPathChanged()
        {
            if (ReactionPathChanged != null) ReactionPathChanged(this, new EventArgs());
            Surface.SetPoint(Surface.CurrentPoint);
        }
        protected void RisePointsChanged()
        {
            if (PointsChanged != null) PointsChanged(this, new EventArgs());
        }

        void PathList_CurrentPathChanged(object sender, EventArgs e)
        {
            UnsetPathEvents(CurrentReactionPath);
            CurrentReactionPath = PathList.CurrentItem;
            SetPathEvents(CurrentReactionPath);
            RiseReactionPathChanged();
        }

        protected void SetSurfaceEvents(ISurface3D surface)
        {
            if (surface != null)
            {
                surface.CurrentPointChanged += Surface_CurrentPointChanged;
            }
        }
        protected void UnsetSurfaceEvents(ISurface3D surface)
        {
            if (surface != null)
            {
                surface.CurrentPointChanged -= Surface_CurrentPointChanged;
            }
            
        }

        protected void SetGeometryEvents(IReactionSystemGeometry geometry)
        {
            if (geometry != null)
            {
                geometry.AtomStructure.GeometryChanged += AtomStructure_GeomentryChanged;
            }
        }
        protected void UnsetGeometryEvents(IReactionSystemGeometry geometry)
        {
            if (geometry != null)
            {
                geometry.AtomStructure.GeometryChanged -= AtomStructure_GeomentryChanged;
            }

        }

        protected void SetPathEvents(IPath path)
        {
            if (path != null)
            {
                path.CurrentPointChanged += Path_CurrentPointChanged;
            }
        }
        protected void UnsetPathEvents(IPath path)
        {
            if (path != null)
            {
                path.CurrentPointChanged -= Path_CurrentPointChanged;
            }

        }

        protected void SetPointsEvents(ISurfacePoints points)
        {
            if (points != null)
            {
                points.CurrentItemChanged += Points_CurrentPointChanged;
            }
        }
        protected void UnsetPointsEvents(ISurfacePoints points)
        {
            if (points != null)
            {
                points.CurrentItemChanged -= Points_CurrentPointChanged;
            }

        }

        private void Surface_CurrentPointChanged(object sender, EventArgs e)
        {
            if (Geometry != null)
            {
                if (!PointsFunc.Equals(Surface.CurrentPoint, Geometry.Q1, Geometry.Q2))
                {
                    Geometry.SetPoint(Surface.CurrentPoint);
                }
            }
        }
        private void AtomStructure_GeomentryChanged(object sender, EventArgs e)
        {
            if (!PointsFunc.Equals(Surface.CurrentPoint, Geometry.Q1, Geometry.Q2))
            {
                Surface.SetPoint(Geometry.Q1, Geometry.Q2);
            }
        }
        private void Path_CurrentPointChanged(object sender, EventArgs e)
        {
            if (CurrentReactionPath.CurrentPoint.Point != Surface.CurrentPoint)
            {
                Surface.SetPoint(CurrentReactionPath.CurrentPoint.Point);
            }
        }
        private void Points_CurrentPointChanged(object sender, EventArgs e)
        {
            if (Surface != null && Points != null && Points.CurrentItem != null)
            {
                if (!Equals(Surface.CurrentPoint, Points.CurrentItem.Point))
                {
                    Surface.SetPoint(Points.CurrentItem.Point);
                }
            }
        }
    }
}
