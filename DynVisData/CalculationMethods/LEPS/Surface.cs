using DynVis.Core;
using DynVis.Core.Surface;
using M=System.Math;


namespace DynVis.Data.CalculationMethods.LEPS
{
    internal class SurfaceLEPS : BaseSurface3D
    {
        public SurfaceLEPS()
            : this(new ParamsLEPS())
        {
            
        }

        public SurfaceLEPS(ParamsLEPS p)
        {
            AxesQ1 = (AxesLEPS)p.Axes1.Clone();
            AxesQ2 = (AxesLEPS)p.Axes2.Clone();
            Params = p;
            SetExtremumE();
        }
        
        protected override double GetE(double q1, double q2)
        {
            return CalcU(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2));
        }

        protected override double GetdEdq1(double q1, double q2)
        {
            return CalcdUdrAB(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2));
        }

        protected override double GetdEdq2(double q1, double q2)
        {
            return Q2IsDistance ? CalcdUdrAC(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2)) : CalcdUdaABC(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2), q2);
        }

        protected override double Getd2Edq1dq1(double q1, double q2)
        {
            return Calcd2UdrAB2(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2));
        }

        protected override double Getd2Edq2dq2(double q1, double q2)
        {
            return Q2IsDistance ? Calcd2UdrAC2(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2)) : Calcd2UdaABC2(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2), q2);
        }

        protected override double Getd2Edq1dq2(double q1, double q2)
        {
            return Q2IsDistance ? Calcd2UdrABdrAC(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2)) : Calcd2UrABdaABC(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2), q2);
        }

        protected override double Getd2Edq2dq1(double q1, double q2)
        {
            return Q2IsDistance ? Calcd2UdrABdrAC(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2)) : Calcd2UrABdaABC(GetR_AB(q1), GetR_AC(q2), GetR_BC(q1, q2), q2);
        }



        #region Calculating function
        private static double Ex(double r, AtomAtomProperty IJ)
        {
            return M.Exp(-IJ.B * (r - IJ.r0));
        }

        private static double Up(double r, AtomAtomProperty IJ)
        {
            var E = 1 - Ex(r, IJ);
            return IJ.De * (E * E - 1);
        }
        private static double dUp(double r, AtomAtomProperty IJ)//Не проверено!!!
        {
            var E = Ex(r, IJ);
            return 2 * IJ.De * IJ.B * (E - E * E);
        }
        private static double d2Up(double r, AtomAtomProperty IJ)//Не проверено!!!
        {
            var E = Ex(r, IJ);
            return IJ.De * IJ.B * IJ.B * 2 * (2 * E * E - E);
        }
        private static double Um(double r, AtomAtomProperty IJ)
        {
            var E = 1 + Ex(r, IJ);
            return IJ.De * (E * E - 1) * 0.5;
        }
        private static double dUm(double r, AtomAtomProperty IJ)//Не проверено!!!
        {
            var E = Ex(r, IJ);
            return - IJ.De * IJ.B * (E + E * E);
        }
        private static double d2Um(double r, AtomAtomProperty IJ)//Не проверено!!!
        {
            var E = Ex(r, IJ);
            return IJ.De * IJ.B * IJ.B * (2 * E * E + E);
        }
        private static double Q(double r, AtomAtomProperty IJ)
        {
            var up = Up(r, IJ);
            var um = Um(r, IJ);
            return ((up + um) + IJ.k * (up - um)) * 0.5;
        }
        private static double dQ(double r, AtomAtomProperty IJ)//Не проверено!!!
        {
            var dup = dUp(r, IJ);
            var dum = dUm(r, IJ);
            return ((dup + dum) + IJ.k * (dup - dum)) * 0.5;
        }
        private static double d2Q(double r, AtomAtomProperty IJ)//Не проверено!!!
        {
            var d2up = d2Up(r, IJ);
            var d2um = d2Um(r, IJ);
            return ((d2up + d2um) + IJ.k * (d2up - d2um)) * 0.5;
        }
        private static double J(double r, AtomAtomProperty IJ)
        {
            var up = Up(r, IJ);
            var um = Um(r, IJ);
            return ((up - um) + IJ.k * (up + um)) * 0.5;
        }
        private static double dJ(double r, AtomAtomProperty IJ)//Не проверено!!!
        {
            var dup = dUp(r, IJ);
            var dum = dUm(r, IJ);
            return ((dup - dum) + IJ.k * (dup + dum)) * 0.5;
        }
        private static double d2J(double r, AtomAtomProperty IJ)//Не проверено!!!
        {
            var d2up = d2Up(r, IJ);
            var d2um = d2Um(r, IJ);
            return ((d2up - d2um) + IJ.k * (d2up + d2um)) * 0.5;
        }

        private double CalcU(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var SQ = Q(rAB, AB) / kAB + Q(rAC, AC) / kAC + Q(rBC, BC) / kBC;

            var JAB = J(rAB, AB)/kAB;
            var JAC = J(rAC, AC)/kAC;
            var JBC = J(rBC, BC)/kBC;

            var J1 = JAB - JAC;
            var J2 = JAC - JBC;
            var J3 = JAB - JBC;
            var SJ = M.Sqrt((J1 * J1 + J2 * J2 + J3 * J3) * 0.5);

            return SQ - SJ;
        }
        private double CalcdUdrAB(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var drBCdrAB = ((rAB * rAB - rAC * rAC + rBC * rBC) / (rBC * rAB* 2));

            var dSQ = dQ(rAB, AB) / kAB + dQ(rBC, BC) * drBCdrAB / kBC;

            var JAB = J(rAB, AB) / kAB;
            var JAC = J(rAC, AC) / kAC;
            var JBC = J(rBC, BC) / kBC;

            var dJAB = dJ(rAB, AB) / kAB;
            var dJBC = dJ(rBC, BC) * drBCdrAB / kBC;

            var J1 = JAB - JAC;
            var J2 = JAC - JBC;
            var J3 = JAB - JBC;

            var dJ1 = dJAB;
            var dJ2 = -dJBC;
            var dJ3 = dJAB - dJBC;
            var dSJ = (J1 * dJ1 + J2 * dJ2 + J3 * dJ3) / M.Sqrt((J1 * J1 + J2 * J2 + J3 * J3) * 2);

            return dSQ - dSJ;
        }
        private double CalcdUdrAC(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var drBCdrAC = ((rAC * rAC - rAB * rAB + rBC * rBC) / (rBC * rAC * 2));

            var dSQ = dQ(rAC, AC) / kAC + dQ(rBC, BC) * drBCdrAC / kBC;


            var JAB = J(rAB, AB) / kAB;
            var JAC = J(rAC, AC) / kAC;
            var JBC = J(rBC, BC) / kBC;

            var dJAC = dJ(rAC, AC) / kAC;
            var dJBC = dJ(rBC, BC) * drBCdrAC / kBC;

            var J1 = JAB - JAC;
            var J2 = JAC - JBC;
            var J3 = JAB - JBC;

            var dJ1 = -dJAC;
            var dJ2 = dJAC - dJBC;
            var dJ3 = -dJBC;
            var dSJ = (J1 * dJ1 + J2 * dJ2 + J3 * dJ3) / M.Sqrt((J1 * J1 + J2 * J2 + J3 * J3) * 2);

            return dSQ - dSJ;
        }
        
        private double CalcdUdrBC(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var dSQ = dQ(rBC, BC) / kBC;

            var JAB = J(rAB, AB) / kAB;
            var JAC = J(rAC, AC) / kAC;
            var JBC = J(rBC, BC) / kBC;

            var dJBC = dJ(rBC, BC) / kBC;

            var J1 = JAB - JAC;
            var J2 = JAC - JBC;
            var J3 = JAB - JBC;

            var dJ2 = -dJBC;
            var dJ3 = -dJBC;
            var dSJ = (J2 * dJ2 + J3 * dJ3) / M.Sqrt((J1 * J1 + J2 * J2 + J3 * J3) * 2);

            return dSQ - dSJ;
        }
        private double CalcdUdaABC(double rAB, double rAC, double rBC, double alpha)
        {
            return CalcdUdrBC(rAB, rAC, rBC) * rAB * rAC / rBC * M.Sin(alpha);
        }

        private double Calcd2UdrAB2(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var cos = (rAB * rAB + rAC * rAC - rBC * rBC) / (2 * rAB * rAC);

            var drBCdrAB = (rAB - rAC * cos) / rBC;
            var drBCdrAB_2 = drBCdrAB * drBCdrAB;
            var d2rBCdrAB2 = (1 - drBCdrAB_2) / rBC;

            var d2SQ = d2Q(rAB, AB) / kAC + (d2Q(rBC, BC) * drBCdrAB_2 + dQ(rBC, BC) * d2rBCdrAB2) / kBC;


            var JAB = J(rAB, AB) / kAB;
            var dJAB = dJ(rAB, AB) / kAB;
            var d2JAB = d2J(rAB, AB) / kAB;

            var JAC = J(rAC, AC) / kAC;

            var JBC = J(rBC, BC) / kBC;
            var dJBC = dJ(rBC, BC) * drBCdrAB / kBC;
            var d2JBC = (d2J(rBC, BC) * drBCdrAB_2 + dJ(rBC, BC) * d2rBCdrAB2) / kBC;


            var J1 = JAB - JAC;
            var dJ1 = -dJAB;
            var d2J1 = -d2JAB;
            var J2 = JAC - JBC;
            var dJ2 = - dJBC;
            var d2J2 = - d2JBC;
            var J3 = JAB - JBC;
            var dJ3 = dJAB - dJBC;
            var d2J3 = d2JAB - d2JBC;


            var NabJ = J1 * J1 + J2 * J2 + J3 * J3;
            var NabdJ = dJ1 * dJ1 + dJ2 * dJ2 + dJ3 * dJ3;
            var J_dJ = J1 * dJ1 + J2 * dJ2 + J3 * dJ3;
            var J_d2J = J1 * d2J1 + J2 * d2J2 + J3 * d2J3;

            var d2SJ = (-J_dJ * J_dJ + NabJ * (NabdJ + J_d2J)) / (M.Sqrt(NabJ * 2) * NabJ);

            return d2SQ - d2SJ;
        }
        private double Calcd2UdrAC2(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var cos = (rAB*rAB + rAC*rAC - rBC*rBC)/(2*rAB*rAC);
            
            var drBCdrAC = (rAC - rAB*cos)/rBC;
            var drBCdrAC_2 = drBCdrAC * drBCdrAC;
            var d2rBCdrAC2 = (1 - drBCdrAC_2) / rBC;

            var d2SQ = d2Q(rAC, AC) / kAC + (d2Q(rBC, BC) * drBCdrAC_2 + dQ(rBC, BC) * d2rBCdrAC2) / kBC;


            var JAB   = J(rAB, AB) / kAB;

            var JAC   = J(rAC, AC)/kAC;
            var dJAC  = dJ(rAC, AC)/kAC;
            var d2JAC = d2J(rAC, AC) / kAC;

            var JBC   = J(rBC, BC) / kBC;
            var dJBC  = dJ(rBC, BC) * drBCdrAC / kBC;
            var d2JBC = (d2J(rBC, BC) * drBCdrAC_2 + dJ(rBC, BC) * d2rBCdrAC2) / kBC;

            var J1    =  JAB - JAC;
            var dJ1   = -dJAC;
            var d2J1  = -d2JAC;
            var J2    =  JAC - JBC;
            var dJ2   =  dJAC - dJBC;
            var d2J2  =  d2JAC - d2JBC;
            var J3    =  JAB - JBC;
            var dJ3   = -dJBC;
            var d2J3  = -d2JBC;

            var NabJ = J1 * J1 + J2 * J2 + J3 * J3;
            var NabdJ = dJ1 * dJ1 + dJ2 * dJ2 + dJ3 * dJ3;
            var J_dJ = J1 * dJ1 + J2 * dJ2 + J3 * dJ3;
            var J_d2J = J1 * d2J1 + J2 * d2J2 + J3 * d2J3;

            var d2SJ = (-J_dJ * J_dJ + NabJ * (NabdJ + J_d2J)) / (M.Sqrt(NabJ * 2) * NabJ);

            return d2SQ - d2SJ;
        }
        private double Calcd2UdrBC2(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var d2SQ = d2Q(rBC, BC) / kBC;


            var JAB = J(rAB, AB) / kAB;

            var JAC = J(rAC, AC) / kAC;

            var JBC = J(rBC, BC) / kBC;
            var dJBC = dJ(rBC, BC);
            var d2JBC = d2J(rBC, BC) / kBC;

            var J1 = JAB - JAC;
            var J2 = JAC - JBC;
            var dJ2 = - dJBC;
            var d2J2 = - d2JBC;
            var J3 = JAB - JBC;
            var dJ3 = -dJBC;
            var d2J3 = -d2JBC;

            var NabJ = J1 * J1 + J2 * J2 + J3 * J3;
            var NabdJ = dJ2 * dJ2 + dJ3 * dJ3;
            var J_dJ = J2 * dJ2 + J3 * dJ3;
            var J_d2J = J2 * d2J2 + J3 * d2J3;

            var d2SJ = (-J_dJ * J_dJ + NabJ * (NabdJ + J_d2J)) / (M.Sqrt(NabJ * 2) * NabJ);

            return d2SQ - d2SJ;
        }
        
        private double Calcd2UdaABC2(double rAB, double rAC, double rBC, double alpha)
        {
            var K = rAB*rAC/rBC;
            var dBCdA = K * M.Sin(alpha);
            var d2BCdA2 = K * M.Cos(alpha);

            return Calcd2UdrBC2(rAB, rAC, rBC) * dBCdA * dBCdA + CalcdUdrBC(rAB, rAC, rBC) * d2BCdA2;
        }

        private double Calcd2UdrABdrAC(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var rAB2 = rAB*rAB;
            var rAC2 = rAC*rAC;
            var rBC2 = rBC*rBC;

            var rABrBC = rBC * rAB;

            var drBCdrAB = ((rAB2 - rAC2 + rBC2) / (rABrBC * 2));
            var drBCdrAC = ((rAC2 - rAB2 + rBC2) / (rBC * rAC * 2));
            var drBCdrAB_drBCdrAC = drBCdrAB*drBCdrAC;
            var d2rBCdrABdrAC = - rAC / rABrBC;

            var d2SQ = (d2Q(rBC, BC) * drBCdrAB_drBCdrAC + dQ(rBC, BC) * d2rBCdrABdrAC) / kBC;

            

            var JAB = J(rAB, AB) / kAB;
            var JAC = J(rAC, AC) / kAC;
            var JBC = J(rBC, BC) / kBC;

            var dJAC = dJ(rAC, AC) / kAC;
            var dJAB = dJ(rAB, AB) / kAB;
            var dJBCdAB = dJ(rBC, BC) * drBCdrAB / kBC;
            var dJBCdAC = dJ(rBC, BC) * drBCdrAC / kBC;

            var d2JBCdABdAC = (d2J(rBC, BC) * drBCdrAB_drBCdrAC + dJ(rBC, BC) * d2rBCdrABdrAC) / kBC;

            var J1 = JAB - JAC;
            var J2 = JAC - JBC;
            var J3 = JAB - JBC;

            var dJ1dAB = dJAB;
            var dJ2dAB = -dJBCdAB;
            var dJ3dAB = dJAB - dJBCdAB;

            var dJ1dAC = -dJAC;
            var dJ2dAC = dJAC - dJBCdAC;
            var dJ3dAC = -dJBCdAC;

            var d2J2dABdAC = -d2JBCdABdAC;
            var d2J3dABdAC = -d2JBCdABdAC;

            var J1_dJ1 = dJ1dAC * dJ1dAB;
            var J2_dJ2 = dJ2dAC * dJ2dAB + J2 * d2J2dABdAC;
            var J3_dJ3 = dJ3dAC * dJ3dAB + J3 * d2J3dABdAC;

            var NabJ = J1*J1 + J2*J2 + J3*J3;
            var J_dJ = J1_dJ1 + J2_dJ2 + J3_dJ3;
            var J_dJAC = J1 * dJ1dAC + J2 * dJ2dAC + J3 * dJ3dAC;
            var J_dJAB = J1 * dJ1dAB + J2 * dJ2dAB + J3 * dJ3dAB;

            var d2SJ = (J_dJ * NabJ - J_dJAC * J_dJAB) / (NabJ * M.Sqrt(NabJ * 2));

            return d2SQ - d2SJ;
        }

        private double Calcd2UdrABdrBC(double rAB, double rAC, double rBC)
        {
            var kAB = 1 + AB.k;
            var kAC = 1 + AC.k;
            var kBC = 1 + BC.k;

            var rAB2 = rAB * rAB;
            var rAC2 = rAC * rAC;
            var rBC2 = rBC * rBC;

            var rABrBC = rBC * rAB;

            var drBCdrAB = ((rAB2 - rAC2 + rBC2) / (rABrBC * 2));
            var d2rBCdrABdrBC = (rBC - drBCdrAB * rAB) / (rBC * rAB);


            var d2SQ = (d2Q(rBC, BC) * drBCdrAB + dQ(rBC, BC) * d2rBCdrABdrBC) / kBC;

            var JAB = J(rAB, AB) / kAB;
            var JAC = J(rAC, AC) / kAC;
            var JBC = J(rBC, BC) / kBC;

            var dJABdAB = dJ(rAB, AB) / kAB;
            var dJBCdBC = dJ(rBC, BC) / kBC;
            var dJBCdAB = dJBCdBC * drBCdrAB;


            var d2JBCdABdBC = (d2J(rBC, BC) * drBCdrAB + dJ(rBC, BC) * d2rBCdrABdrBC) / kBC;

            var J1 = JAB - JAC;
            var J2 = JAC - JBC;
            var J3 = JAB - JBC;

            var dJ1dAB = dJABdAB;
            var dJ2dAB = -dJBCdAB;
            var dJ3dAB = dJABdAB - dJBCdAB;

            var d2J2dABdBC = -d2JBCdABdBC;
            var d2J3dABdBC = -d2JBCdABdBC;

            var dJ2dBC = -dJBCdBC;
            var dJ3dBC = -dJBCdBC;

            var J2_dJ2 = dJ2dBC * dJ2dAB + J2 * d2J2dABdBC;
            var J3_dJ3 = dJ3dBC * dJ3dAB + J3 * d2J3dABdBC;

            var NabJ = J1 * J1 + J2 * J2 + J3 * J3;
            var J_dJ = J2_dJ2 + J3_dJ3;
            var J_dJAC = J2 * dJ2dBC + J3 * dJ3dBC;
            var J_dJAB = J1 * dJ1dAB + J2 * dJ2dAB + J3 * dJ3dAB;

            var d2SJ = (J_dJ * NabJ - J_dJAC * J_dJAB) / (NabJ * M.Sqrt(NabJ * 2));


            return d2SJ - d2SQ;
        }

        private double Calcd2UrABdaABC(double rAB, double rAC, double rBC, double alpha)
        {
            return Calcd2UdrABdrBC(rAB, rAC, rBC) * rAB * rAC / rBC * M.Sin(alpha);
        }

        #endregion

        internal AxesLEPS AxesQ1 { get; private set; }
        internal AxesLEPS AxesQ2 { get; private set; }

        public override IAxes Axes1
        {
            get { return AxesQ1; }
        }

        public override IAxes Axes2
        {
            get { return AxesQ2; }
        }

        private readonly ScaleDimension _EnergyDimension = new ScaleDimension(DimEnergy.Joule, ScaleCoeff.kilo);
        public override ScaleDimension EnergyDimension
        {
            get { return _EnergyDimension; }
        }

        private bool Q2IsDistance { get { return AxesQ2.AxesType == LEPSAxesType.Distance; } }

        private double GetR_AB(double q1)
        {
            return q1;
        }
        private double GetR_AC(double q2)
        {
            return Q2IsDistance ? q2 : ConstQValue;
        }
        private double GetR_BC(double q1, double q2)
        {
            var rAB = GetR_AB(q1);
            var rAC = GetR_AC(q2);
            var Angle = Q2IsDistance ? ConstQValue : q2;
            return M.Sqrt(rAB * rAB + rAC * rAC - 2 * rAB * rAC * M.Cos(Angle));
        }

        private double ConstQValue
        {
            get
            {
                return Params.ConstQValue;
            }
        }

        internal readonly ParamsLEPS Params;
        private AtomAtomProperty AB
        {
            get { return Params.AB; }
        }
        private AtomAtomProperty AC
        {
            get { return Params.AC; }
        }
        private AtomAtomProperty BC
        {
            get { return Params.BC; }
        }

        private void SetExtremumE()
        {
            _MinE.Point.SetValues(AB.De > BC.De ? AB.r0 : BC.r0, AxesQ2.MaxValue);
            _MinE.Value = -(AB.De > BC.De ? AB.De : BC.De);
            _MaxE.Point.SetValues(AxesQ1.MaxValue, AxesQ2.MaxValue);
            _MaxE.Value = 0;
        }
    }
}
