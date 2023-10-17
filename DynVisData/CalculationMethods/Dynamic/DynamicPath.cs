using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DynVis.Core;
using DynVis.Core.IO;
using DynVis.Core.Progress;
using DynVis.Data.Properties;
using DynVis.Math;
using Path=DynVis.Core.ReactionPath.Path;

namespace DynVis.Data.CalculationMethods.Dynamic
{
    [Serializable]
    public class DynamicPath : ICalculationPath, ISaveFile, ICloneable
    {
        [NonSerialized]
        private const double DimensionFactorForce = 9.99997E-5;
        [NonSerialized]
        private const double DimensionFactorSpeed = 1;

        public double Q1 = 1.5; //Первая координаты [ангстрем]
        public double Q2 = 1.5; //Вторая координаты [ангстрем]

        public double v1; //Начальная скорость по первой координате [ангстрем / фемтосекунда]
        public double v2; //Начальная скорость по второй координате [ангстрем / фемтосекунда]

        public double M1 = 1; //Средняя масса по координате Q1
        public double M2 = 1; //Средняя масса по координате Q2

        public double dt = 0.001; //Шаг времени [фемтосек]

        public double OptSquereDelta = 0.025 / 1000; //Минимальное приращение координаты [ангстрем]
        public bool AutoOptimizationDelta = true;
        public bool Optimize = true;

        public int MaxStep = 2000000; //Максимальное число шагов
        
        public IPath CalcPath(ISurface3D PES)
        {
            var v_1 = v1;
            var v_2 = v2;
            
            if (AutoOptimizationDelta)
            {
                OptSquereDelta = ((PES.Axes1.MaxValue - PES.Axes1.MinValue) + (PES.Axes2.MaxValue - PES.Axes2.MinValue)) / 2000;
                OptSquereDelta *= OptSquereDelta;
            }

            var DPath = new Path {EnergyDimension = PES.EnergyDimension};

            if (MaxStep == 0)
            {
                return DPath;
            } //Ноль, то закругляемся

            var dt2 = dt * dt; //Квадрат времени
            //const double dtdM1 = dt/M1; //Отношение времени к массе
            //const double dtdM2 = dt/M2; //Отношение времени к массе
            var dt2dM1 = dt2 / M1 * DimensionFactorForce; //Отношение квадрата времени к массе
            var dt2dM2 = dt2 / M2 * DimensionFactorForce; //Отношение квадрата времени к массе


            //Задаем начальные значения:

            //Начальные координаты заданы
            var R1 = Q1;
            var R2 = Q2;

            //Потенциальная энергия интерполируется из данных в матрице ППЭ
            //Одновременно ищется частная производная в этой точке, т.е. сила от потенциальной энергии по координатам
            if (!PES.IsValidCoord(R1, R2)) return DPath;
            //EV = PES.CurrentE; //[кг*м/с^2]

            var V1 = PES.dEdq1(R1, R2);
            var V2 = PES.dEdq2(R1, R2);

            //Вычесляется вклады в приращение от кинетической и потенциальной энергии по координатам
            //Т.к F = m*a = m*s/(t^2), то s = F*(t^2)/m
            //Или p = m*v = m*s/t, то s = p*t/m
            var ST1 = v_1 * dt * DimensionFactorSpeed;
            var SV1 = -V1 * dt2dM1;
            var ST2 = v_2 * dt * DimensionFactorSpeed;
            var SV2 = -V2 * dt2dM2;

            //Результирующий вклад ищется как сумма вкладов от кинетической и потенциальной энергии
            var S1 = ST1 + SV1;
            var S2 = ST2 + SV2;

            //Скорость ищется отношением пройденого пути ко времени
            v_1 = S1 / dt; //[A/фс]
            v_2 = S2 / dt; //[A/фс]


            //Сохраняем начальные значения
            DPath.Add(R1, R2, 0); //Начальное время равно нулю

            var PrevPoint = new PointD(R1, R2);

            for (var i = 1; i < MaxStep; i++)
            {
                //Начинаем считать


                //Увеличиваем координаты на предсказанную величину
                R1 = R1 + S1;
                R2 = R2 + S2;

                //Находим потенциальную энергию в новой точке
                //А за одно и силы по координатам в ней (как производную)
                if (!PES.IsValidCoord(R1, R2))
                {
                    break;
                }

                //var EV = PES.CurrentE; //[кг*м/с^2]

                //Инвертируем градиент, чтоб скорости были в правельную сторону
                V1 = -PES.dEdq1(R1, R2); //[кг*А/с] ([H*1E-10])
                V2 = -PES.dEdq2(R1, R2); //[кг*А/с] ([H*1E-10])


                //Считаем прирощения
                ST1 = v_1 * dt; //[A]
                SV1 = V1 * dt2dM1; //[A]
                ST2 = v_2 * dt; //[A]
                SV2 = V2 * dt2dM2; //[A]

                S1 = ST1 + SV1; //[A]
                S2 = ST2 + SV2; //[A]

                //Считаем скорости
                v_1 = S1 / dt; //[A/фс]
                v_2 = S2 / dt; //[A/фс]

                //Если включана оптимизация то проверям условия
                if (!Optimize || (Optimize && PrevPoint.SquearDistanceTo(R1, R2) >= OptSquereDelta))
                {
                    PrevPoint.SetValues(R1, R2);

                    DPath.Add(R1, R2, dt * i); //Текущее время от начала процесса
                }

                //Вызываем контрольную функцию, если она задана
                if (Progress != null && Progress(i * 100 / MaxStep)) return null;
            }
            //Устанавливаем текущую позицию (для отрисовки)
            DPath.CurrentPointIndex = 0;

            DPath.ApllyToPES(PES);

            DPath.Name = LangResource.ReactionDynamic;

            return DPath;
        }
        
        [NonSerialized]
        public ProgressChanged Progress;
        
        public void SaveToFile(Stream stream, FileFilter filter)
        {
            var Serialiser = new BinaryFormatter();
            Serialiser.Serialize(stream, this);
        }

        public static DynamicPath OpenFromFile(Stream stream, FileFilter filter)
        {
            var Serialiser = new BinaryFormatter();
            return (DynamicPath)Serialiser.Deserialize(stream);
        }
        
        [NonSerialized]
        private static readonly FileFilter[] _SaveFilters = new[]{new FileFilter(LangResource.DynamicParams, "*.dyn", FileFilter.Type.Comressed)};
        public FileFilter[] SaveFilters
        {
            get { return _SaveFilters; }
        }

        public object Clone()
        {
            return Serialization.Clone(this);
        }
    }
}
