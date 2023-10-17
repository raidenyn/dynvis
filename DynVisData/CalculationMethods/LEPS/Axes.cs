using DynVis.Math;
using M=System.Math;
using DynVis.Core;

namespace DynVis.Data.CalculationMethods.LEPS
{
    [Savable]
    internal sealed class AxesLEPS: Axes 
    {
        private LEPSAxesType _AxesType = LEPSAxesType.Distance;
        
        public LEPSAxesType AxesType
        {
            get
            {
                return _AxesType;
            }
            set
            {
                _AxesType = value;
                switch (AxesType)
                {
                    case LEPSAxesType.Distance:
                        ScaleDimension = new ScaleDimension(DimLength.Angstrom);
                	break;
                    case LEPSAxesType.Angle:
                        ScaleDimension = new ScaleDimension(DimAngle.Radian);
                        if (MathExtended.EquialAngle(MinValue, 0)) TypeMinValue = Core.Surface.CoordinateType.Mirrored;
                        if (MathExtended.EquialAngle(MaxValue, M.PI)) TypeMaxValue = Core.Surface.CoordinateType.Mirrored;
                    break;
                }
            }
        }

        public new object Clone()
        {
            var result = new AxesLEPS();
            result.SetAs(this, GetMinValue, GetMaxValue);
            result.AxesType = AxesType;
            return result;
        }
    }

    internal enum LEPSAxesType {Distance, Angle}
}
