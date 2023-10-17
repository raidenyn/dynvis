using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace DynVis.Draw
{
    
    
    
    public static class GraphicsExtension
    {
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("gdi32.dll", EntryPoint = "SetPixel", ExactSpelling = true)]
        private extern static void SetPixel(IntPtr DC, int X, int Y, int Color);

        public static void DrawPixel(IntPtr DC, Pen pen, int x, int y)
        {
            SetPixel(DC, x, y, pen.Color.ToArgb());
        }
        
        public static void DrawPixel(this Graphics G, Pen pen, int x, int y)
        {
            G.DrawLine(pen, x, y, x+1, y);
        }

        public static void DrawPixel(this Graphics G, Pen pen, Point p)
        {
            G.DrawPixel(pen, p.X, p.Y);
        }
    }
}
