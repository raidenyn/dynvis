using DynVis.Core;
using DynVis.Math;
using System.Drawing;

namespace DynVis.Draw.Geometry
{
    public static class AtomGraphProperty
    {
        public static Color Color(this IAtom atom)
        {
            return AtomColor.GetSafeElement(atom.Element);
        }


        private static readonly Color[] AtomColor = new[]
                                                        {
                                                            System.Drawing.Color.FromArgb(255, 0, 255), //X
                                                            System.Drawing.Color.FromArgb(199, 199, 199), //H
                                                            System.Drawing.Color.FromArgb(209, 209, 209), //He
                                                            System.Drawing.Color.FromArgb(232, 79, 200), //Li
                                                            System.Drawing.Color.FromArgb(255, 255, 0), //Be
                                                            System.Drawing.Color.FromArgb(252, 197, 67), //B
                                                            System.Drawing.Color.FromArgb(114, 114, 114), //C
                                                            System.Drawing.Color.FromArgb(0, 0, 255), //N
                                                            System.Drawing.Color.FromArgb(255, 64, 64), //O
                                                            System.Drawing.Color.FromArgb(128, 255, 255), //F
                                                            System.Drawing.Color.FromArgb(12, 255, 255), //Ne
                                                            System.Drawing.Color.FromArgb(138, 5, 171), //Na
                                                            System.Drawing.Color.FromArgb(196, 196, 0), //Mg
                                                            System.Drawing.Color.FromArgb(248, 192, 234), //Al
                                                            System.Drawing.Color.FromArgb(143, 160, 160), //Si
                                                            System.Drawing.Color.FromArgb(255, 128, 0), //P
                                                            System.Drawing.Color.FromArgb(250, 214, 5), //S
                                                            System.Drawing.Color.FromArgb(128, 255, 0), //Cl
                                                            System.Drawing.Color.FromArgb(0, 210, 210), //Ar
                                                            System.Drawing.Color.FromArgb(179, 1, 254), //K
                                                            System.Drawing.Color.FromArgb(255, 0, 255), //Ca
                                                            System.Drawing.Color.FromArgb(199, 199, 199), //Sc
                                                            System.Drawing.Color.FromArgb(209, 209, 209), //Ti
                                                            System.Drawing.Color.FromArgb(232, 79, 200), //V
                                                            System.Drawing.Color.FromArgb(255, 255, 0), //Cr
                                                            System.Drawing.Color.FromArgb(252, 197, 67), //Mn
                                                            System.Drawing.Color.FromArgb(114, 114, 114), //Fe
                                                            System.Drawing.Color.FromArgb(0, 0, 255), //Co
                                                            System.Drawing.Color.FromArgb(255, 64, 64), //Ni
                                                            System.Drawing.Color.FromArgb(128, 255, 255), //Cu
                                                            System.Drawing.Color.FromArgb(12, 255, 255), //Zn
                                                            System.Drawing.Color.FromArgb(138, 5, 171), //Ga
                                                            System.Drawing.Color.FromArgb(196, 196, 0), //Ge
                                                            System.Drawing.Color.FromArgb(248, 192, 234), //As
                                                            System.Drawing.Color.FromArgb(143, 160, 160), //Se
                                                            System.Drawing.Color.FromArgb(255, 128, 0), //Br
                                                            System.Drawing.Color.FromArgb(250, 214, 5), //Kr
                                                            System.Drawing.Color.FromArgb(128, 255, 0), //Rb
                                                            System.Drawing.Color.FromArgb(0, 210, 210), //Sr
                                                            System.Drawing.Color.FromArgb(179, 1, 254), //Y
                                                            System.Drawing.Color.FromArgb(255, 0, 255), //Zr
                                                            System.Drawing.Color.FromArgb(199, 199, 199), //Nb
                                                            System.Drawing.Color.FromArgb(209, 209, 209), //Mo
                                                            System.Drawing.Color.FromArgb(232, 79, 200), //Tc
                                                            System.Drawing.Color.FromArgb(255, 255, 0), //Ru
                                                            System.Drawing.Color.FromArgb(252, 197, 67), //Rh
                                                            System.Drawing.Color.FromArgb(114, 114, 114), //Pd
                                                            System.Drawing.Color.FromArgb(0, 0, 255), //Ag
                                                            System.Drawing.Color.FromArgb(255, 64, 64), //Cd
                                                            System.Drawing.Color.FromArgb(128, 255, 255), //In
                                                            System.Drawing.Color.FromArgb(12, 255, 255), //Sn
                                                            System.Drawing.Color.FromArgb(138, 5, 171), //Sb
                                                            System.Drawing.Color.FromArgb(196, 196, 0), //Te
                                                            System.Drawing.Color.FromArgb(248, 192, 234), //I
                                                            System.Drawing.Color.FromArgb(143, 160, 160), //Xe
                                                            System.Drawing.Color.FromArgb(255, 128, 0), //Cs
                                                            System.Drawing.Color.FromArgb(250, 214, 5), //Ba
                                                            System.Drawing.Color.FromArgb(128, 255, 0), //La
                                                            System.Drawing.Color.FromArgb(0, 210, 210), //Ce
                                                            System.Drawing.Color.FromArgb(179, 1, 254), //Pr
                                                            System.Drawing.Color.FromArgb(255, 0, 255), //Nd
                                                            System.Drawing.Color.FromArgb(199, 199, 199), //Pm
                                                            System.Drawing.Color.FromArgb(209, 209, 209), //Sm
                                                            System.Drawing.Color.FromArgb(232, 79, 200), //Eu
                                                            System.Drawing.Color.FromArgb(255, 255, 0), //Gd
                                                            System.Drawing.Color.FromArgb(252, 197, 67), //Tb
                                                            System.Drawing.Color.FromArgb(114, 114, 114), //Dy
                                                            System.Drawing.Color.FromArgb(0, 0, 255), //Ho
                                                            System.Drawing.Color.FromArgb(255, 64, 64), //Er
                                                            System.Drawing.Color.FromArgb(128, 255, 255), //Tm
                                                            System.Drawing.Color.FromArgb(12, 255, 255), //Yb
                                                            System.Drawing.Color.FromArgb(138, 5, 171), //Lu
                                                            System.Drawing.Color.FromArgb(196, 196, 0), //Hf
                                                            System.Drawing.Color.FromArgb(248, 192, 234), //Ta
                                                            System.Drawing.Color.FromArgb(143, 160, 160), //W
                                                            System.Drawing.Color.FromArgb(255, 128, 0), //Re
                                                            System.Drawing.Color.FromArgb(250, 214, 5), //Os
                                                            System.Drawing.Color.FromArgb(128, 255, 0), //Ir
                                                            System.Drawing.Color.FromArgb(0, 210, 210), //Pt
                                                            System.Drawing.Color.FromArgb(179, 1, 254), //Au
                                                            System.Drawing.Color.FromArgb(255, 0, 255), //Hg
                                                            System.Drawing.Color.FromArgb(199, 199, 199), //Tl
                                                            System.Drawing.Color.FromArgb(209, 209, 209), //Pb
                                                            System.Drawing.Color.FromArgb(232, 79, 200), //Bi
                                                            System.Drawing.Color.FromArgb(255, 255, 0), //Po
                                                            System.Drawing.Color.FromArgb(252, 197, 67), //At
                                                            System.Drawing.Color.FromArgb(114, 114, 114), //Rn
                                                            System.Drawing.Color.FromArgb(0, 0, 255), //Fr
                                                            System.Drawing.Color.FromArgb(255, 64, 64), //Ra
                                                            System.Drawing.Color.FromArgb(128, 255, 255), //Ac
                                                            System.Drawing.Color.FromArgb(12, 255, 255), //Th
                                                            System.Drawing.Color.FromArgb(138, 5, 171), //Pa
                                                            System.Drawing.Color.FromArgb(196, 196, 0), //U
                                                            System.Drawing.Color.FromArgb(248, 192, 234), //Np
                                                            System.Drawing.Color.FromArgb(143, 160, 160), //Pu
                                                            System.Drawing.Color.FromArgb(255, 128, 0), //Am
                                                            System.Drawing.Color.FromArgb(250, 214, 5), //Cm
                                                            System.Drawing.Color.FromArgb(128, 255, 0), //Bk
                                                            System.Drawing.Color.FromArgb(0, 210, 210), //Cf
                                                            System.Drawing.Color.FromArgb(179, 1, 254), //Es
                                                            System.Drawing.Color.FromArgb(255, 0, 255), //Fm
                                                            System.Drawing.Color.FromArgb(199, 199, 199), //Md
                                                            System.Drawing.Color.FromArgb(209, 209, 209), //No
                                                            System.Drawing.Color.FromArgb(232, 79, 200), //Lr
                                                            System.Drawing.Color.FromArgb(255, 255, 0), //Rf
                                                            System.Drawing.Color.FromArgb(252, 197, 67), //Db
                                                            System.Drawing.Color.FromArgb(114, 114, 114), //Sg
                                                            System.Drawing.Color.FromArgb(0, 0, 255), //Bh
                                                            System.Drawing.Color.FromArgb(255, 64, 64), //Hs
                                                            System.Drawing.Color.FromArgb(128, 255, 255), //Mt
                                                            System.Drawing.Color.FromArgb(12, 255, 255), //Uun
                                                            System.Drawing.Color.FromArgb(138, 5, 171), //Uuu

                                                        };


        public static Color Color(this IBond bond)
        {
            return ColorFunction.AvgColor(bond.Atom1.Color(), bond.Atom2.Color());
        }

        public static double Radius(this IAtom atom) 
        {
            return AtomRadius.GetSafeElement(atom.Element);
        }

        private static readonly double[] AtomRadius = new[]
                                                          {
                                                              0.150000006,
                                                              0.201000005,
                                                              0.202000007,
                                                              0.202999994,
                                                              0.203999996,
                                                              0.204999998,
                                                              0.206,
                                                              0.207000002,
                                                              0.208000004,
                                                              0.209000006,
                                                              0.209999993,
                                                              0.210999995,
                                                              0.211999997,
                                                              0.213,
                                                              0.214000002,
                                                              0.215000004,
                                                              0.216000006,
                                                              0.216999993,
                                                              0.217999995,
                                                              0.218999997,
                                                              0.219999999,
                                                              0.221000001,
                                                              0.222000003,
                                                              0.223000005,
                                                              0.224000007,
                                                              0.224999994,
                                                              0.225999996,
                                                              0.226999998,
                                                              0.228,
                                                              0.229000002,
                                                              0.230000004,
                                                              0.231000006,
                                                              0.231999993,
                                                              0.232999995,
                                                              0.233999997,
                                                              0.234999999,
                                                              0.236000001,
                                                              0.237000003,
                                                              0.238000005,
                                                              0.238999993,
                                                              0.239999995,
                                                              0.240999997,
                                                              0.241999999,
                                                              0.243000001,
                                                              0.244000003,
                                                              0.245000005,
                                                              0.246000007,
                                                              0.246999994,
                                                              0.247999996,
                                                              0.248999998,
                                                              0.25,
                                                              0.250999987,
                                                              0.252000004,
                                                              0.252999991,
                                                              0.254000008,
                                                              0.254999995,
                                                              0.256000012,
                                                              0.256999999,
                                                              0.257999986,
                                                              0.259000003,
                                                              0.25999999,
                                                              0.261000007,
                                                              0.261999995,
                                                              0.263000011,
                                                              0.263999999,
                                                              0.264999986,
                                                              0.266000003,
                                                              0.26699999,
                                                              0.268000007,
                                                              0.268999994,
                                                              0.270000011,
                                                              0.270999998,
                                                              0.272000015,
                                                              0.273000002,
                                                              0.273999989,
                                                              0.275000006,
                                                              0.275999993,
                                                              0.27700001,
                                                              0.277999997,
                                                              0.279000014,
                                                              0.280000001,
                                                              0.280999988,
                                                              0.282000005,
                                                              0.282999992,
                                                              0.284000009,
                                                              0.284999996,
                                                              0.286000013,
                                                              0.287,
                                                              0.287999988,
                                                              0.289000005,
                                                              0.289999992,
                                                              0.291000009,
                                                              0.291999996,
                                                              0.293000013,
                                                              0.294,
                                                              0.294999987,
                                                              0.296000004,
                                                              0.296999991,
                                                              0.298000008,
                                                              0.298999995,
                                                              0.300000012,
                                                              0.300999999,
                                                              0.301999986,
                                                              0.303000003,
                                                              0.30399999,
                                                              0.305000007,
                                                              0.305999994,
                                                              0.307000011,
                                                              0.307999998,
                                                              0.308999985,
                                                              0.310000002,
                                                              0.31099999,

                                                          };
    }
}
