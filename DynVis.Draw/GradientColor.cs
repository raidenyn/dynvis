using System.Drawing;

namespace DynVis.Draw
{

    /// <summary>
    /// Класс рисующий градиент, перепиши как тебе будет удобней обращаться с ним.
    /// После последней переделки не тестировал, так что могут быть глюки.
    /// Возможно удобней будет minE и maxE вынести в члены класса, так как они меняются очень редко,
    /// тогда будет иметь смысл оптимизировать формулу для ColorNumber и вычислять значения ColorCount / (maxE - minE) заранее.
    /// </summary>
    public static class GradientColor
    {

        /// <summary>
        /// Массив опорных цветов из которых складывается градиент
        /// </summary>
        private static readonly RGB[] ReperColor = new[]
                                                       {
                                                           //new RGB(0, 0, 0), //Черный
                                                           //new RGB(255, 0, 255), //Фиолетовый
                                                           new RGB{R = 0, G = 0, B = 255}, //Синий
                                                           new RGB{R = 0, G = 255, B = 0}, //Зеленый
                                                           new RGB{R = 255, G = 255, B = 0}, //Желтый
                                                           new RGB{R = 255, G = 0, B = 0}, //Красный
                                                       };
        
        private static int ReperColorCount
        {
            get { return ReperColor.Length; }
        }

        private static int ColorCount
        {
            get { return (ReperColorCount - 1) * 255; }
        }

        public static Color GetGradientColor(double e, double minE, double maxE)
        {
            if (e >= maxE) return ReperColor[ReperColor.Length - 1].ToColor();
            if (e <= minE) return ReperColor[0].ToColor();

            var ColorNumber = (int)((e - minE) * ColorCount / (maxE - minE));

            var ReperIndex = ColorNumber / 255;

            var Color1 = ReperColor[ReperIndex];
            var Color2 = ReperColor[ReperIndex + 1];
          
            var Result = Color1 + (Color2 - Color1) * (ColorNumber / (double)255 - ReperIndex);

            return Result.ToColor();
             
        }
    }

    public struct RGB
    {
      
        public int B { get; set; }
        public int G { get; set; }
        public int R { get; set; }

        public void SetRGB(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public void SetRGB(RGB c)
        {
            R = c.R;
            G = c.G;
            B = c.B;
        }

        public static RGB operator +(RGB c1, RGB c2)
        {
            var c = new RGB {R = (c1.R + c2.R), G = (c1.G + c2.G), B = (c1.B + c2.B)};
            return c;
        }
        public static RGB operator *(RGB c1, RGB c2)
        {
            var c = new RGB {R = (c1.R*c2.R), G = (c1.G*c2.G), B = (c1.B*c2.B)};
            return c;
        }
        public static RGB operator -(RGB c1, RGB c2)
        {
            var c = new RGB {R = (c1.R - c2.R), G = (c1.G - c2.G), B = (c1.B - c2.B)};
            return c;
        }
        public static RGB operator /(RGB c1, RGB c2)
        {
            var c = new RGB {R = (c1.R/c2.R), G = (c1.G/c2.G), B = (c1.B/c2.B)};
            return c;
        }
        public static RGB operator +(RGB c1, double i)
        {
            var c = new RGB { R = (int)(c1.R + i), G = (int)(c1.G + i), B = (int)(c1.B + i) };
            return c;
        }
        public static RGB operator *(RGB c1, double i)
        {
            var c = new RGB { R = (int)(c1.R * i), G = (int)(c1.G * i), B = (int)(c1.B * i) };
            return c;
        }
        public static RGB operator -(RGB c1, double i)
        {
            var c = new RGB { R = (int)(c1.R - i), G = (int)(c1.G - i), B = (int)(c1.B - i) };
            return c;
        }
        public static RGB operator /(RGB c1, double i)
        {
            var c = new RGB { R = (int)(c1.R / i), G = (int)(c1.G / i), B = (int)(c1.B / i) };
            return c;
        }
        public static RGB operator +(double i, RGB c1)
        {
            var c = new RGB { R = (int)(c1.R + i), G = (int)(c1.G + i), B = (int)(c1.B + i) };
            return c;
        }
        public static RGB operator *(double i, RGB c1)
        {
            var c = new RGB { R = (int)(c1.R * i), G = (int)(c1.G * i), B = (int)(c1.B * i) };
            return c;
        }
        public static RGB operator -(double i, RGB c1)
        {
            var c = new RGB { R = (int)(i - c1.R), G = (int)(i - c1.G), B = (int)(i - c1.B) };
            return c;
        }
        public static RGB operator /(double i, RGB c1)
        {
            var c = new RGB { R = (int)(i / c1.R), G = (int)(i / c1.G), B = (int)(i / c1.B) };
            return c;
        }
        public void Add(RGB c)
        {
            R += c.R;
            G += c.G;
            B += c.B;
        }
        public void Subtract(RGB c)
        {
            R -= c.R;
            G -= c.G;
            B -= c.B;
        }
        public void Mult(RGB c)
        {
            R *= c.R;
            G *= c.G;
            B *= c.B;
        }
        public void Divide(RGB c)
        {
            R /= c.R;
            G /= c.G;
            B /= c.B;
        }
        public void Add(double d)
        {
            R = (int)(R * d);
            G = (int)(G * d);
            B = (int)(B * d);
        }
        public void Subtract(double d)
        {
            R = (int)(R - d);
            G = (int)(G - d);
            B = (int)(B - d);
        }
        public void Mult(double d)
        {
            R = (int)(R * d);
            G = (int)(G * d);
            B = (int)(B * d);
        }
        public void Divide(double d)
        {
            R = (int)(R / d);
            G = (int)(G / d);
            B = (int)(B / d);
        }

        public Color ToColor()
        {
            return Color.FromArgb(R, G, B);
        }
    }

}
