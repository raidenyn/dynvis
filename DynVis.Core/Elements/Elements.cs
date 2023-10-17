using System.Drawing;
using DynVis.Math;

//---------------Химические элементы и их основные характеристики----------------------------------
namespace DynVis.Core.Elements
{
    public static class Elements
    {
        /// <summary>
        /// Возращает символ элемента по его порядковому номеру
        /// </summary>
        /// <param name="index">Порядковый номер элемента</param>
        /// <returns>Символ</returns>
        public static string GetElementSymbol(int index)
        {
            return Symbols.GetSafeElement(index);
        }

        /// <summary>
        /// Возращает символ данного атома
        /// </summary>
        /// <param name="atom"></param>
        /// <returns></returns>
        public static string Symbol(this IAtom atom)
        {
            return GetElementSymbol(atom.Element);
        }

        /// <summary>
        /// Массив символов элементов
        /// </summary>
        private static readonly string[] Symbols = new[]
                                                       {
                                                           "X",
                                                           "H",
                                                           "He",
                                                           "Li",
                                                           "Be",
                                                           "B",
                                                           "C",
                                                           "N",
                                                           "O",
                                                           "F",
                                                           "Ne",
                                                           "Na",
                                                           "Mg",
                                                           "Al",
                                                           "Si",
                                                           "P",
                                                           "S",
                                                           "Cl",
                                                           "Ar",
                                                           "K",
                                                           "Ca",
                                                           "Sc",
                                                           "Ti",
                                                           "V",
                                                           "Cr",
                                                           "Mn",
                                                           "Fe",
                                                           "Co",
                                                           "Ni",
                                                           "Cu",
                                                           "Zn",
                                                           "Ga",
                                                           "Ge",
                                                           "As",
                                                           "Se",
                                                           "Br",
                                                           "Kr",
                                                           "Rb",
                                                           "Sr",
                                                           "Y",
                                                           "Zr",
                                                           "Nb",
                                                           "Mo",
                                                           "Tc",
                                                           "Ru",
                                                           "Rh",
                                                           "Pd",
                                                           "Ag",
                                                           "Cd",
                                                           "In",
                                                           "Sn",
                                                           "Sb",
                                                           "Te",
                                                           "I",
                                                           "Xe",
                                                           "Cs",
                                                           "Ba",
                                                           "La",
                                                           "Ce",
                                                           "Pr",
                                                           "Nd",
                                                           "Pm",
                                                           "Sm",
                                                           "Eu",
                                                           "Gd",
                                                           "Tb",
                                                           "Dy",
                                                           "Ho",
                                                           "Er",
                                                           "Tm",
                                                           "Yb",
                                                           "Lu",
                                                           "Hf",
                                                           "Ta",
                                                           "W",
                                                           "Re",
                                                           "Os",
                                                           "Ir",
                                                           "Pt",
                                                           "Au",
                                                           "Hg",
                                                           "Tl",
                                                           "Pb",
                                                           "Bi",
                                                           "Po",
                                                           "At",
                                                           "Rn",
                                                           "Fr",
                                                           "Ra",
                                                           "Ac",
                                                           "Th",
                                                           "Pa",
                                                           "U",
                                                           "Np",
                                                           "Pu",
                                                           "Am",
                                                           "Cm",
                                                           "Bk",
                                                           "Cf",
                                                           "Es",
                                                           "Fm",
                                                           "Md",
                                                           "No",
                                                           "Lr",
                                                           "Rf",
                                                           "Db",
                                                           "Sg",
                                                           "Bh",
                                                           "Hs",
                                                           "Mt",
                                                           "Uun",
                                                           "Uuu",
            
                                                       };

        /// <summary>
        /// Возращает латинское название элемента по его порядковому номеру 
        /// </summary>
        /// <param name="index">Порядковый номер элемента</param>
        /// <returns>Латинское название</returns>
        public static string GetElementName(int index)
        {
            return Names.GetSafeElement(index);
        }

        /// <summary>
        /// Возращает латинское название элемента
        /// </summary>
        /// <param name="atom"></param>
        /// <returns></returns>
        public static string Name(this IAtom atom)
        {
            return GetElementName(atom.Element);
        }

        /// <summary>
        /// Массив латинских названий элментов
        /// </summary>
        private static readonly string[] Names = new[]
                                                      {
                                                          "Imaginary",
                                                          "Hydrogen",
                                                          "Helium",
                                                          "Lithium",
                                                          "Beryllium",
                                                          "Boron",
                                                          "Carbon",
                                                          "Nitrogen",
                                                          "Oxygen",
                                                          "Fluorine",
                                                          "Neon",
                                                          "Sodium",
                                                          "Magnesium",
                                                          "Aluminum",
                                                          "Silicon",
                                                          "Phosphorus",
                                                          "Sulfur",
                                                          "Chlorine",
                                                          "Argon",
                                                          "Potassium",
                                                          "Calcium",
                                                          "Scandium",
                                                          "Titanium",
                                                          "Vanadium",
                                                          "Chromium",
                                                          "Manganese",
                                                          "Iron",
                                                          "Cobalt",
                                                          "Nickel",
                                                          "Copper",
                                                          "Zinc",
                                                          "Gallium",
                                                          "Germanium",
                                                          "Arsenic",
                                                          "Selenium",
                                                          "Bromine",
                                                          "Krypton",
                                                          "Rubidium",
                                                          "Strontium",
                                                          "Yttrium",
                                                          "Zirconium",
                                                          "Niobium",
                                                          "Molybdenum",
                                                          "Technetium",
                                                          "Ruthenium",
                                                          "Rhodium",
                                                          "Palladium",
                                                          "Silver",
                                                          "Cadmium",
                                                          "Indium",
                                                          "Tin",
                                                          "Antimony",
                                                          "Tellerium",
                                                          "Iodine",
                                                          "Xenon",
                                                          "Cesium",
                                                          "Barium",
                                                          "Lanthanum",
                                                          "Cerium",
                                                          "Praseodynium",
                                                          "Neodymium",
                                                          "Prometheum",
                                                          "Samarium",
                                                          "Europium",
                                                          "Gadolinium",
                                                          "Terbium",
                                                          "Dysprosium",
                                                          "Holmium",
                                                          "Erbium",
                                                          "Thulium",
                                                          "Ytterbium",
                                                          "Lutetium",
                                                          "Hafnium",
                                                          "Tantalum",
                                                          "Wolffram",
                                                          "Rhenium",
                                                          "Osmium",
                                                          "Iridium",
                                                          "Platinum",
                                                          "Gold",
                                                          "Mercury",
                                                          "Thallium",
                                                          "Lead",
                                                          "Bismuth",
                                                          "Polonium",
                                                          "Astatine",
                                                          "Radon",
                                                          "Francium",
                                                          "Radium",
                                                          "Actinium",
                                                          "Thorium",
                                                          "Protactinium",
                                                          "Uranium",
                                                          "Neptunium",
                                                          "Plutonium",
                                                          "Americium",
                                                          "Curium",
                                                          "Berkelium",
                                                          "Californium",
                                                          "Enstainium",
                                                          "Fermium",
                                                          "Mendelevium",
                                                          "Nobelium",
                                                          "Lowrencium",
                                                          "Reserfordium",
                                                          "Dubnium",
                                                          "Siborgovium",
                                                          "Boronium",
                                                          "Hassium",
                                                          "Meithnerium",
                                                          "Ununnilium",
                                                          "Unununium",
                                                      };
        
        /// <summary>
        /// Возращает атомную массу элемента по его порядковому номеру
        /// </summary>
        /// <param name="index">Порядковый номер</param>
        /// <returns>Масса элмента</returns>
        public static double GetElementMass(int index)
        {
            return Masses.GetSafeElement(index);
        }

        /// <summary>
        /// Возращает атомную массу элемента
        /// </summary>
        /// <param name="atom"></param>
        /// <returns></returns>
        public static double Mass(this IAtom atom)
        {
            return GetElementMass(atom.Element);
        }

        /// <summary>
        /// Массив атомных масс элементов
        /// </summary>
        private static readonly double[] Masses = new[]
                                                      {
                                                          0,
                                                          1.007825017,
                                                          4.002600193,
                                                          7.015999794,
                                                          9.012180328,
                                                          11.00930977,
                                                          12,
                                                          14.00306988,
                                                          15.99491024,
                                                          18.99839973,
                                                          19.99243927,
                                                          22.9897995,
                                                          23.98504066,
                                                          26.98152924,
                                                          27.97693062,
                                                          30.9737606,
                                                          31.97207069,
                                                          34.96884918,
                                                          39.94800186,
                                                          38.96371078,
                                                          39.96258926,
                                                          44.95592117,
                                                          47.90000153,
                                                          50.94400024,
                                                          51.94049835,
                                                          54.93809891,
                                                          55.93489838,
                                                          58.93320084,
                                                          57.93529892,
                                                          62.92979813,
                                                          63.92910004,
                                                          68.92569733,
                                                          73.92189789,
                                                          74.92160034,
                                                          79.91649628,
                                                          78.91829681,
                                                          83.91149902,
                                                          84.91169739,
                                                          87.9056015,
                                                          89.90540314,
                                                          89.90429688,
                                                          92.90599823,
                                                          97.90550232,
                                                          97,
                                                          101.9037018,
                                                          102.9048004,
                                                          105.9031982,
                                                          106.9040985,
                                                          113.9036026,
                                                          114.9040985,
                                                          119.9021988,
                                                          120.903801,
                                                          129.9066925,
                                                          126.9044037,
                                                          131.9042053,
                                                          132.9053955,
                                                          137.9051971,
                                                          138.9062958,
                                                          139.9053955,
                                                          140.9075928,
                                                          141.9076996,
                                                          144.9127045,
                                                          151.919693,
                                                          152.9212036,
                                                          157.9241028,
                                                          158.925293,
                                                          163.9291992,
                                                          164.9302979,
                                                          165.9302979,
                                                          168.9342041,
                                                          173.9389038,
                                                          174.9407959,
                                                          179.9465027,
                                                          180.947998,
                                                          183.9508972,
                                                          186.9557037,
                                                          191.9615021,
                                                          192.9629059,
                                                          194.964798,
                                                          196.966507,
                                                          201.9705963,
                                                          204.9743958,
                                                          207.976593,
                                                          208.9803925,
                                                          208.9824066,
                                                          209.9871063,
                                                          222.0175934,
                                                          223.0196991,
                                                          226.0254059,
                                                          227.0278015,
                                                          232.0381012,
                                                          231.0359039,
                                                          238.0507965,
                                                          237.0482025,
                                                          244.0641937,
                                                          243.0614014,
                                                          247.0702972,
                                                          247.0702972,
                                                          251.0796051,
                                                          252.082901,
                                                          257.0751038,
                                                          258.0986023,
                                                          259.1008911,
                                                          260.1052856,
                                                          261.1087036,
                                                          262,
                                                          263,
                                                          262,
                                                          265,
                                                          266,
                                                          271,
                                                          273,

                                                      };


        /// <summary>
        /// Возращает тип связи между атомами на основе их ковалентных радиусов
        /// </summary>
        /// <param name="a">Первый Атом</param>
        /// <param name="b">Второй Атом</param>
        /// <returns>Тип связи</returns>
        public static BondType IsCovalentRadiusBond(this IAtom a, IAtom b)
        {
            if (a.Coordinate.DistanceTo(b.Coordinate) <= CovalentRadius(a) + CovalentRadius(b))
            {
                return BondType.Single;
            }
            return BondType.None;
        }

        /// <summary>
        /// Возращает ковалентный радиус элемента
        /// </summary>
        /// <param name="atom"></param>
        /// <returns></returns>
        public static double CovalentRadius(this IAtom atom)
        {
            return CovalentRadiuses.GetSafeElement(atom.Element);
        }

        /// <summary>
        /// Массив ковалентных радиусов (подправлено вручную)
        /// </summary>
        private static readonly double[] CovalentRadiuses = new[]
                                                                {
                                                                    0,
                                                                    0.459999993,
                                                                    0.930000007,
                                                                    1.230000019,
                                                                    0.899999976,
                                                                    0.919999993,
                                                                    0.769999981,
                                                                    0.75,
                                                                    0.730000019,
                                                                    0.720000029,
                                                                    0.709999979,
                                                                    1.539999962,
                                                                    1.360000014,
                                                                    1.179999948,
                                                                    1.110000014,
                                                                    1.059999943,
                                                                    1.019999981,
                                                                    0.99000001,
                                                                    0.980000019,
                                                                    2.029999971,
                                                                    1.74000001,
                                                                    1.440000057,
                                                                    1.320000052,
                                                                    1.220000029,
                                                                    1.179999948,
                                                                    1.169999957,
                                                                    1.169999957,
                                                                    1.159999967,
                                                                    1.149999976,
                                                                    1.169999957,
                                                                    1.25,
                                                                    1.25999999,
                                                                    1.220000029,
                                                                    1.200000048,
                                                                    1.159999967,
                                                                    1.139999986,
                                                                    1.120000005,
                                                                    2.160000086,
                                                                    1.909999967,
                                                                    1.620000005,
                                                                    1.450000048,
                                                                    1.340000033,
                                                                    1.299999952,
                                                                    1.269999981,
                                                                    1.25,
                                                                    1.25,
                                                                    1.59999971,
                                                                    1.340000033,
                                                                    1.480000019,
                                                                    1.440000057,
                                                                    1.409999967,
                                                                    1.399999976,
                                                                    1.360000014,
                                                                    1.330000043,
                                                                    1.309999943,
                                                                    2.349999905,
                                                                    1.980000019,
                                                                    1.690000057,
                                                                    1.649999976,
                                                                    1.649999976,
                                                                    1.639999986,
                                                                    1.629999995,
                                                                    1.620000005,
                                                                    1.850000024,
                                                                    1.610000014,
                                                                    1.590000033,
                                                                    1.590000033,
                                                                    1.580000043,
                                                                    1.570000052,
                                                                    1.559999943,
                                                                    1.74000001,
                                                                    1.559999943,
                                                                    1.440000057,
                                                                    1.340000033,
                                                                    1.299999952,
                                                                    1.279999971,
                                                                    1.25999999,
                                                                    1.269999981,
                                                                    1.299999952,
                                                                    1.340000033,
                                                                    1.49000001,
                                                                    1.480000019,
                                                                    1.470000029,
                                                                    1.460000038,
                                                                    1.460000038,
                                                                    1.450000048,
                                                                    1.440000057,
                                                                    2.630000114,
                                                                    2.119999886,
                                                                    1.799999952,
                                                                    1.649999976,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                    2,
                                                                };
    }

    /// <summary>
    /// Возможные типы связей
    /// </summary>
    public enum BondType {None, Single, Double, Triple, Hydrogen}

    /// <summary>
    /// Типы элементов
    /// </summary>
    public enum ElementType {S, P, D, F}

    /// <summary>
    /// Класс работающий с элементами в переодической таблице элементов
    /// Не очень красиво, зато эффективно
    /// </summary>
    class ElementInTable
    {
        public readonly Point P;
        public readonly ElementType ElementType;

        private ElementInTable(int x, int y, ElementType elementType)
        {
            P = new Point(x,y);
            ElementType = elementType;
        }

        internal static readonly ElementInTable[] ElementPosition = new[]
                                                                        {
                                                                            new ElementInTable(1, 1, ElementType.S),
                                                                            new ElementInTable(1, 10, ElementType.S),
                                                                            new ElementInTable(2, 1, ElementType.S),
                                                                            new ElementInTable(2, 2, ElementType.S),
                                                                            new ElementInTable(2, 3, ElementType.P),
                                                                            new ElementInTable(2, 4, ElementType.P),
                                                                            new ElementInTable(2, 5, ElementType.P),
                                                                            new ElementInTable(2, 6, ElementType.P),
                                                                            new ElementInTable(2, 7, ElementType.P),
                                                                            new ElementInTable(2, 10, ElementType.P),
                                                                            new ElementInTable(3, 1, ElementType.S),
                                                                            new ElementInTable(3, 2, ElementType.S),
                                                                            new ElementInTable(3, 3, ElementType.P),
                                                                            new ElementInTable(3, 4, ElementType.P),
                                                                            new ElementInTable(3, 5, ElementType.P),
                                                                            new ElementInTable(3, 6, ElementType.P),
                                                                            new ElementInTable(3, 7, ElementType.P),
                                                                            new ElementInTable(3, 10, ElementType.P),
                                                                            new ElementInTable(4, 1, ElementType.S),
                                                                            new ElementInTable(4, 2, ElementType.S),
                                                                            new ElementInTable(4, 3, ElementType.D),
                                                                            new ElementInTable(4, 4, ElementType.D),
                                                                            new ElementInTable(4, 5, ElementType.D),
                                                                            new ElementInTable(4, 6, ElementType.D),
                                                                            new ElementInTable(4, 7, ElementType.D),
                                                                            new ElementInTable(4, 8, ElementType.D),
                                                                            new ElementInTable(4, 9, ElementType.D),
                                                                            new ElementInTable(4, 10, ElementType.D),
                                                                            new ElementInTable(5, 1, ElementType.S),
                                                                            new ElementInTable(5, 2, ElementType.S),
                                                                            new ElementInTable(5, 3, ElementType.P),
                                                                            new ElementInTable(5, 4, ElementType.P),
                                                                            new ElementInTable(5, 5, ElementType.P),
                                                                            new ElementInTable(5, 6, ElementType.P),
                                                                            new ElementInTable(5, 7, ElementType.P),
                                                                            new ElementInTable(5, 10, ElementType.P),
                                                                            new ElementInTable(6, 1, ElementType.S),
                                                                            new ElementInTable(6, 2, ElementType.S),
                                                                            new ElementInTable(6, 3, ElementType.D),
                                                                            new ElementInTable(6, 4, ElementType.D),
                                                                            new ElementInTable(6, 5, ElementType.D),
                                                                            new ElementInTable(6, 6, ElementType.D),
                                                                            new ElementInTable(6, 7, ElementType.D),
                                                                            new ElementInTable(6, 8, ElementType.D),
                                                                            new ElementInTable(6, 9, ElementType.D),
                                                                            new ElementInTable(6, 10, ElementType.D),
                                                                            new ElementInTable(7, 1, ElementType.S),
                                                                            new ElementInTable(7, 2, ElementType.S),
                                                                            new ElementInTable(7, 3, ElementType.P),
                                                                            new ElementInTable(7, 4, ElementType.P),
                                                                            new ElementInTable(7, 5, ElementType.P),
                                                                            new ElementInTable(7, 6, ElementType.P),
                                                                            new ElementInTable(7, 7, ElementType.P),
                                                                            new ElementInTable(7, 10, ElementType.P),
                                                                            new ElementInTable(8, 1, ElementType.S),
                                                                            new ElementInTable(8, 2, ElementType.S),
                                                                            new ElementInTable(8, 3, ElementType.D),
                                                                            new ElementInTable(13, 1, ElementType.F),
                                                                            new ElementInTable(13, 2, ElementType.F),
                                                                            new ElementInTable(13, 3, ElementType.F),
                                                                            new ElementInTable(13, 4, ElementType.F),
                                                                            new ElementInTable(13, 5, ElementType.F),
                                                                            new ElementInTable(13, 6, ElementType.F),
                                                                            new ElementInTable(13, 7, ElementType.F),
                                                                            new ElementInTable(13, 8, ElementType.F),
                                                                            new ElementInTable(13, 9, ElementType.F),
                                                                            new ElementInTable(13, 10, ElementType.F),
                                                                            new ElementInTable(13, 11, ElementType.F),
                                                                            new ElementInTable(13, 12, ElementType.F),
                                                                            new ElementInTable(13, 13, ElementType.F),
                                                                            new ElementInTable(13, 14, ElementType.F),
                                                                            new ElementInTable(8, 4, ElementType.D),
                                                                            new ElementInTable(8, 5, ElementType.D),
                                                                            new ElementInTable(8, 6, ElementType.D),
                                                                            new ElementInTable(8, 7, ElementType.D),
                                                                            new ElementInTable(8, 8, ElementType.D),
                                                                            new ElementInTable(8, 9, ElementType.D),
                                                                            new ElementInTable(8, 10, ElementType.D),
                                                                            new ElementInTable(9, 1, ElementType.S),
                                                                            new ElementInTable(9, 2, ElementType.S),
                                                                            new ElementInTable(9, 3, ElementType.P),
                                                                            new ElementInTable(9, 4, ElementType.P),
                                                                            new ElementInTable(9, 5, ElementType.P),
                                                                            new ElementInTable(9, 6, ElementType.P),
                                                                            new ElementInTable(9, 7, ElementType.P),
                                                                            new ElementInTable(9, 10, ElementType.P),
                                                                            new ElementInTable(10, 1, ElementType.S),
                                                                            new ElementInTable(10, 2, ElementType.S),
                                                                            new ElementInTable(10, 3, ElementType.D),
                                                                            new ElementInTable(14, 1, ElementType.F),
                                                                            new ElementInTable(14, 2, ElementType.F),
                                                                            new ElementInTable(14, 3, ElementType.F),
                                                                            new ElementInTable(14, 4, ElementType.F),
                                                                            new ElementInTable(14, 5, ElementType.F),
                                                                            new ElementInTable(14, 6, ElementType.F),
                                                                            new ElementInTable(14, 7, ElementType.F),
                                                                            new ElementInTable(14, 8, ElementType.F),
                                                                            new ElementInTable(14, 9, ElementType.F),
                                                                            new ElementInTable(14, 10, ElementType.F),
                                                                            new ElementInTable(14, 11, ElementType.F),
                                                                            new ElementInTable(14, 12, ElementType.F),
                                                                            new ElementInTable(14, 13, ElementType.F),
                                                                            new ElementInTable(14, 14, ElementType.F),
                                                                            new ElementInTable(10, 4, ElementType.D),
                                                                            new ElementInTable(10, 5, ElementType.D),
                                                                            new ElementInTable(10, 6, ElementType.D),
                                                                            new ElementInTable(10, 7, ElementType.D),
                                                                            new ElementInTable(10, 8, ElementType.D),
                                                                            new ElementInTable(10, 9, ElementType.D),
                                                                            new ElementInTable(10, 10, ElementType.D),
                                                                            new ElementInTable(11, 1, ElementType.S),

                                                                        };
    }


}
