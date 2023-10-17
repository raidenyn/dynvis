using System.Drawing;

namespace DynVis.Draw
{
    public static class ColorFunction
    {
        public static Color ReverseColor(this Color c)
        {
            return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
        }

        public static Color AvgColor(Color c1, Color c2)
        {
            return Color.FromArgb((c1.R + c2.R) / 2, (c1.G + c2.G) / 2, (c1.B + c2.B) / 2);
        }
    }
}
