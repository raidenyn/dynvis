using System;
using System.Drawing;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace DynVis.Draw.OpenGL
{
    partial class OpenGLEngine
    {
        private int fontbase = NullFontBase;                                            // Base Display List For The Font

        private const int FontSymbolsCount = 256;
        private const int NullFontBase = -1;

        private readonly Gdi.GLYPHMETRICSFLOAT[] gmf = new Gdi.GLYPHMETRICSFLOAT[FontSymbolsCount];

        private const int RUSSIAN_CHARSET = 204;

        protected void BuildFont(Font font)
        {
            if (fontbase != NullFontBase)
            {
                RemoveFont();
            }

            fontbase = Gl.glGenLists(FontSymbolsCount); // Storage For 256 Characters

            IntPtr gdiFont = Gdi.CreateFont( // Create The Font
                -(int)font.Size, // Height Of Font
                0, // Width Of Font
                0, // Angle Of Escapement
                0, // Orientation Angle
                font.Style == FontStyle.Bold ? Gdi.FW_BOLD : 0, // Font Weight
                font.Style == FontStyle.Italic, // Italic
                font.Style == FontStyle.Underline, // Underline
                font.Style == FontStyle.Strikeout, // Strikeout
                RUSSIAN_CHARSET, // Character Set Identifier
                Gdi.OUT_TT_PRECIS, // Output Precision
                Gdi.CLIP_DEFAULT_PRECIS, // Clipping Precision
                Gdi.ANTIALIASED_QUALITY, // Output Quality
                Gdi.FF_DONTCARE | Gdi.DEFAULT_PITCH, // Family And Pitch
                font.Name);

            Gdi.SelectObject(DC, gdiFont); // Selects The Font We Created
            
            
            if (!
                Wgl.wglUseFontOutlinesA(
                    DC, // Select The Current DC
                    0, // Starting Character
                    FontSymbolsCount, // Number Of Display Lists To Build
                    fontbase, // Starting Display Lists
                    0, // Deviation From The True Outlines
                    0.015f, // Font Thickness In The Z Direction
                    Wgl.WGL_FONT_POLYGONS, // Use Polygons, Not Lines
                    gmf // Address Of Buffer To Recieve Data
                    )
                )
            {
                throw new OpenGLEngineException("Cann't create font");
            }
            
            /*
            if (!
                Wgl.wglUseFontBitmapsA(
                    DC, // Select The Current DC
                    0, // Starting Character
                    FontSymbolsCount, // Number Of Display Lists To Build
                    fontbase // Starting Display Lists
                    ))
            {
                throw new OpenGLEngineException("Cann't create font");
            }
            */
            
        }

        private void RemoveFont()
        {
            Gl.glDeleteLists(fontbase, FontSymbolsCount);
            fontbase = NullFontBase;
        }

        public void PrintString(string text, float x, float y, float z)
        {
            Gl.glRasterPos3f(x, y, z);
            PrintString(text);
        }

        public void PrintString(string text, float x, float y)
        {
            Gl.glRasterPos2f(x, y);
            PrintString(text);
        }

        public void PrintString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {                              // If There's No Text
                return;                                                         // Do Nothing
            }

            var textbytes = Encoding.Default.GetBytes(text);

            var widthSift = - textbytes.Sum(b => gmf[b].gmfCellIncX) * 0.5f;
            var heightShift = - textbytes.Max(b => gmf[b].gmfBlackBoxY) * 0.5f;

            Gl.glPushAttrib(Gl.GL_LIST_BIT);                                    // Pushes The Display List Bits
            Gl.glListBase(fontbase);                                        // Sets The Base Character to 0
            Gl.glTranslatef(widthSift, heightShift, 0.0f);
            Gl.glCallLists(text.Length, Gl.GL_UNSIGNED_BYTE, textbytes);         // Draws The Display List Text
            Gl.glTranslatef(-widthSift, -heightShift, 0.0f);
            Gl.glPopAttrib();                                                   // Pops The Display List Bits
        }
    }
}
