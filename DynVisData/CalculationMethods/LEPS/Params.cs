using System;
using DynVis.Core;


namespace DynVis.Data.CalculationMethods.LEPS
{
    [Savable]
    internal partial class ParamsLEPS : ICloneable
    {
        public ParamsLEPS()
        {
            
        }

        public ParamsLEPS(ParamsLEPS other)
        {
            SetValues(other);
        }

        public int ElementA = 1;
        public int ElementB = 1;
        public int ElementC = 1;

        public AtomAtomProperty AB = new AtomAtomProperty();
        public AtomAtomProperty AC = new AtomAtomProperty();
        public AtomAtomProperty BC = new AtomAtomProperty();


        public double ConstQValue;

        public AxesLEPS Axes1 = new AxesLEPS { MinValue = 0.3, MaxValue = 5 };
        public AxesLEPS Axes2 = new AxesLEPS { MinValue = 0.3, MaxValue = 5 };
        
        public object Clone()
        {
            return new ParamsLEPS(this);
        }

        public void SetValues(ParamsLEPS obj)
        {
            ConstQValue = obj.ConstQValue;
            AB = (AtomAtomProperty)obj.AB.Clone();
            AC = (AtomAtomProperty)obj.AC.Clone();
            BC = (AtomAtomProperty)obj.BC.Clone();
        }
    }

    [Savable]
    public class AtomAtomProperty : ICloneable
    {
        public AtomAtomProperty()
        {
            
        }

        public AtomAtomProperty(AtomAtomProperty other)
        {
            SetValues(other);
        }
        
        public double B = 1.943; //Морзевская конcтанта

        public double De = 432.2; //Энергия диссоциации

        public double k; //Квадрат интеграла перекрывания

        public double r0 = 0.741; //Равновесное растояние
        
        public object Clone()
        {
            return new AtomAtomProperty(this);
        }

        public void SetValues(AtomAtomProperty obj)
        {
            B = obj.B;
            De = obj.De;
            k = obj.k;
            r0 = obj.r0;
        }
    }
}
