
namespace DynVis.Math
{
    public sealed class ApplicateGrid<ApplicateType>
    {
        public ApplicateGrid(double[] q1, double[] q2, ApplicateType[,] objects)
        {
            if (q1.Length != objects.GetLength(0) || q2.Length != objects.GetLength(1))
            {
                throw new ArraySizeException();
            }

            ArrayQ1 = (double[])q1.Clone();
            ArrayQ2 = (double[])q2.Clone();
            Objects = (ApplicateType[,])objects.Clone();

            CalcState(ArrayQ1[0], ArrayQ2[0]);
        }

        public readonly double[] ArrayQ1;
        public readonly double[] ArrayQ2;
        public readonly ApplicateType[,] Objects;

        public double Q1MaxValue
        {
            get { return ArrayQ1[ArrayQ1.Length - 1]; }
        }
        public double Q2MaxValue
        {
            get { return ArrayQ2[ArrayQ2.Length - 1]; }
        }
        public double Q1MinValue
        {
            get { return ArrayQ1[0]; }
        }
        public double Q2MinValue
        {
            get { return ArrayQ2[0]; }
        }


        private bool CheckOverflow(double q1, double q2)
        {
            return !(q1 > Q1MaxValue || q1 < Q1MinValue || q2 > Q2MaxValue || q2 < Q2MinValue);
        }

        public bool CalcState(double q1, double q2)
        {
            if (CheckOverflow(q1, q2))
            {
                int Q1Index, Q2Index, NextQ1Index, NextQ2Index; 
                
                if (ArrayQ1.Length > 1)
                {
                    Q1Index = ArrayQ1.BinaryFind(q1);
                    q1Coeff = (q1 - ArrayQ1[Q1Index]) / (ArrayQ1[Q1Index + 1] - ArrayQ1[Q1Index]);
                    NextQ1Index = Q1Index + 1;
                } 
                else
                {
                    q1Coeff = 1.0;
                    Q1Index = 0;
                    NextQ1Index = 0;
                }
                if (ArrayQ2.Length > 1)
                {
                    Q2Index = ArrayQ2.BinaryFind(q2);
                    q2Coeff = (q2 - ArrayQ2[Q2Index]) / (ArrayQ2[Q2Index + 1] - ArrayQ2[Q2Index]);
                    NextQ2Index = Q2Index + 1;
                }
                else
                {
                    q2Coeff = 1.0;
                    Q2Index = 0;
                    NextQ2Index = 0;
                }
                

                objectQ1MinQ2Min = Objects[Q1Index, Q2Index];
                objectQ1MaxQ2Min = Objects[NextQ1Index, Q2Index];
                objectQ1MinQ2Max = Objects[Q1Index, NextQ2Index];
                objectQ1MaxQ2Max = Objects[NextQ1Index, NextQ2Index];

                return true;
            }
            return false;
        }


        public ApplicateType objectQ1MinQ2Min { get; private set; }
        public ApplicateType objectQ1MaxQ2Min { get; private set; }
        public ApplicateType objectQ1MinQ2Max { get; private set; }
        public ApplicateType objectQ1MaxQ2Max { get; private set; }

        public double q1Coeff { get; private set; }
        public double q2Coeff { get; private set; }
    }
}
