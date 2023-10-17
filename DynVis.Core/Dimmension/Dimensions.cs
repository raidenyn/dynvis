using DynVis.Core.Properties;
using M=System.Math;

//------------Перечисление классов физически размерностей----------------------------
namespace DynVis.Core
{
    /// <summary>
    /// Безразмерная величина
    /// </summary>
    [Savable]
    public class DimNone : Dimensions
    {
        internal DimNone() {
            Name = LangDimension.Dimensionless; }

        public override Dimension StandartDimension { get { return None; } }
        public override DimensionType Type
        {
            get { return DimensionType.None; }
        }

        private static readonly Dimension[] Dimms = new[]
                                                              {
                                                                  new Dimension(LangDimension.Dimensionless,"Dimensionless", LangDimension.Dimensionless_s, 1, DimensionType.None)
                                                              };

        public static readonly Dimension None = Dimms[(int)Types.None];


        public override Dimension[] DimensionTypes { get { return Dimms; } }

        public enum Types { None = 0 }


    }

    /// <summary>
    /// Длина
    /// </summary>
    [Savable]
    public class DimLength : Dimensions
    {
        internal DimLength() { Name = LangDimension.Length; }

        public override Dimension StandartDimension { get { return Metr; } }
        public override DimensionType Type
        {
            get { return DimensionType.Lenght; }
        }

        private static readonly Dimension[] Dimms = new[]
                                                              {
                                                                  new Dimension(LangDimension.Length_Metr,"Metr", LangDimension.Length_Metr_s, 1, DimensionType.Lenght),
                                                                  new Dimension(LangDimension.Length_Angstrom,"Angstrom", LangDimension.Length_Angstrom_s, 1E-10, DimensionType.Lenght),
                                                                  new Dimension(LangDimension.Length_Bohr,"Bohr", LangDimension.Length_Bohr_s, 1E-10*0.529177249, DimensionType.Lenght)
                                                              };

        public static readonly Dimension Metr = Dimms[(int)Types.Metr];
        public static readonly Dimension Angstrom = Dimms[(int)Types.Angstrom];
        public static readonly Dimension Bohr = Dimms[(int)Types.Bohr];


        public override Dimension[] DimensionTypes { get { return Dimms; } }

        public enum Types { Metr, Angstrom, Bohr }
    }

    /// <summary>
    /// Обратная длина
    /// </summary>
    [Savable]
    public class DimReverseLenth : Dimensions
    {
        internal DimReverseLenth() { Name = LangDimension.ReciprocalLength; }

        public override Dimension StandartDimension { get { return ReverseMetr; } }
        public override DimensionType Type
        {
            get { return DimensionType.ReverseLenth; }
        }

        private static readonly Dimension[] Dimms = new[]
                                                              {
                                                                  new Dimension(LangDimension.ReciprocalLength_Metr,"ReciprocalMetr", LangDimension.ReciprocalLength_Metr_s, 1, DimensionType.ReverseLenth),
                                                                  new Dimension(LangDimension.ReciprocalLength_Angstrom,"ReciprocalAngstrom", LangDimension.ReciprocalLength_Angstrom_s, 1E+10, DimensionType.ReverseLenth),
                                                                  new Dimension(LangDimension.ReciprocalLength_Bohr,"ReciprocalBohr", LangDimension.ReciprocalLength_Bohr_s, 1E+10*1.889725989, DimensionType.ReverseLenth)
                                                              };

        public static readonly Dimension ReverseMetr = Dimms[(int)Types.ReverseMetr];
        public static readonly Dimension ReverseAngstrom = Dimms[(int)Types.ReverseAngstrom];
        public static readonly Dimension ReverseBohr = Dimms[(int)Types.ReverseBohr];

        public override Dimension[] DimensionTypes { get { return Dimms; } }

        public enum Types { ReverseMetr, ReverseAngstrom, ReverseBohr }

        public override double ConvertScale(double v, ScaleCoeff currentScaleCoeff, ScaleCoeff destinationScaleCoeff)
        {
            return v * (DimensionCoeff.Coeff(destinationScaleCoeff).CoeffValue / DimensionCoeff.Coeff(currentScaleCoeff).CoeffValue);
        }
    }

    /// <summary>
    /// Масса
    /// </summary>
    [Savable]
    public class DimMass : Dimensions
    {
        internal DimMass() { Name = LangDimension.Mass; }
        public override Dimension StandartDimension { get { return Gram; } }
        public override DimensionType Type
        {
            get { return DimensionType.Mass; }
        }

        private static readonly Dimension[] Dimms = new[]
                                                              {
                                                                  new Dimension(LangDimension.Mass_Gram,"Gram", LangDimension.Mass_Gram_s, 1, DimensionType.Mass),
                                                                  new Dimension(LangDimension.Mass_Dalton,"Dalton", LangDimension.Mass_Dalton_s, 1.6605402E-24, DimensionType.Mass)
                                                              };

        public static readonly Dimension Gram = Dimms[(int)Types.Gram];
        public static readonly Dimension Dalton = Dimms[(int)Types.Dalton];


        public override Dimension[] DimensionTypes { get { return Dimms; } }

        public enum Types { Gram, Dalton }
    }

    /// <summary>
    /// Энергия
    /// </summary>
    [Savable]
    public class DimEnergy : Dimensions
    {
        internal DimEnergy() { Name = LangDimension.Energy; }

        public override Dimension StandartDimension { get { return Joule; } }
        public override DimensionType Type
        {
            get { return DimensionType.Energy; }
        }

        private static readonly Dimension[] Dimms = new[]
                                                              {
                                                                  new Dimension(LangDimension.Energy_Joule,"Joule", LangDimension.Energy_Joule_s, 1, DimensionType.Energy),
                                                                  new Dimension(LangDimension.Energy_Hartree,"Hartree", LangDimension.Energy_Haretree_s, 627.511*1000*4.186, DimensionType.Energy),
                                                                  new Dimension(LangDimension.Energy_Calorie,"Calorie", LangDimension.Energy_Calorie_s, 4.186, DimensionType.Energy)
                                                              };

        public static readonly Dimension Joule = Dimms[(int)Types.Joule];
        public static readonly Dimension Hartree = Dimms[(int)Types.Hartree];
        public static readonly Dimension Сalorie = Dimms[(int)Types.Calorie];

        public override Dimension[] DimensionTypes { get { return Dimms; } }

        public enum Types { Joule, Hartree, Calorie }
    }

    /// <summary>
    /// Время
    /// </summary>
    [Savable]
    public class DimTime : Dimensions
    {
        internal DimTime() { Name = LangDimension.Time; }

        public override Dimension StandartDimension { get { return Second; } }
        public override DimensionType Type
        {
            get { return DimensionType.Time; }
        }

        private static readonly Dimension[] Dimms = new[]
                                                              {
                                                                  new Dimension(LangDimension.Time_Second,"Second", LangDimension.Time_Second_s, 1, DimensionType.Time)
                                                              };

        public static readonly Dimension Second = Dimms[(int)Types.Second];

        public override Dimension[] DimensionTypes { get { return Dimms; } }

        public enum Types { Second }
    }

    /// <summary>
    /// Сила
    /// </summary>
    [Savable]
    public class DimForce : Dimensions
    {
        internal DimForce() { Name = LangDimension.Force; }

        public override Dimension StandartDimension { get { return Newton; } }
        public override DimensionType Type
        {
            get { return DimensionType.Force; }
        }

        private static readonly Dimension[] Dimms = new[]
                                                              {
                                                                  new Dimension(LangDimension.Force_Newton,"Newton", LangDimension.Force_Newton_s, 1, DimensionType.Force),
                                                                  new Dimension(LangDimension.Force_Dinah,"Dinah", LangDimension.Force_Dinah_s, 10E-5, DimensionType.Force)
                                                              };

        public static readonly Dimension Newton = Dimms[(int)Types.Newton];
        public static readonly Dimension Dinah = Dimms[(int)Types.Dinah];

        public override Dimension[] DimensionTypes { get { return Dimms; } }

        public enum Types { Newton, Dinah }
    }

    /// <summary>
    /// Угол
    /// </summary>
    [Savable]
    public class DimAngle : Dimensions
    {
        internal DimAngle() { Name = LangDimension.Angle; }

        public override Dimension StandartDimension { get { return Degree; } }
        public override DimensionType Type
        {
            get { return DimensionType.Angle; }
        }

        private static readonly Dimension[] Dimms = new[]
                                                              {
                                                                  new Dimension(LangDimension.Angle_Degree,"Degree", LangDimension.Angle_Degree_s, 1, DimensionType.Angle),
                                                                  new Dimension(LangDimension.Angle_Radian,"Radian", LangDimension.Angle_Radian_s, 180/M.PI, DimensionType.Angle)
                                                              };

        public static readonly Dimension Degree = Dimms[(int)Types.Degree];
        public static readonly Dimension Radian = Dimms[(int)Types.Radian];

        public override Dimension[] DimensionTypes { get { return Dimms; } }

        public enum Types { Degree, Radian }
    }
}
