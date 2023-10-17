using System;
using System.Windows.Forms;

namespace DynVis.Core.ReactionPath
{
    /// <summary>
    /// Класс реализующий последовательное изменение текущей точки на пути реакции
    /// </summary>
    public class PathTimer
    {
        private readonly Timer Timer = new Timer();

        public readonly IPath Path;

        /// <summary>
        /// Временной интервал
        /// </summary>
        public int TimeInterval
        {
            get { return Timer.Interval; }
            set { Timer.Interval = value; }
        }

        /// <summary>
        /// Количество шагов от первой до последней точке
        /// </summary>
        public static int StepCount = 200;

        /// <summary>
        /// Шаг приращения точек
        /// </summary>
        public int Step
        {
            get
            {
                return Path.Count/StepCount;
            }
        }


        public PathTimer(IPath path)
        {
            if (path == null) throw new ArgumentNullException("path");
            
            Path = path;
            Timer.Enabled = false;
            Timer.Tick += Timer_Tick;

            TimeInterval = 41;
        }

        /// <summary>
        /// Начинает движение
        /// </summary>
        public void StartMove()
        {
            if (Path.CurrentPointIndex >= Path.Count - 1)
            {
                Path.CurrentPointIndex = 0;
            }

            Tick();
        }

        /// <summary>
        /// Останавливает движение
        /// </summary>
        public void StopMove()
        {
            Timer.Stop();
        }

        /// <summary>
        /// Текущее состояние движения
        /// </summary>
        public bool IsMoving
        {
            get { return Timer.Enabled; }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            Tick();
        }

        private void Tick()
        {
            if (Path.CurrentPointIndex < Path.Count - 1)
            {
                if (Path.CurrentPointIndex + Step < Path.Count - 1)
                {
                    Path.CurrentPointIndex += Step;
                }
                else
                {
                    Path.CurrentPointIndex = Path.Count - 1;
                }
                /*
                    Timer.Interval =
                        (int) ((Path[Path.CurrentPointIndex + 1].Time - Path[Path.CurrentPointIndex].Time)*TimeScale);
                    */

                Timer.Enabled = true;
            }
            else
            {
                Timer.Enabled = false;
            }
        }
    }
    
}
