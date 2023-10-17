using System;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.EventsLog;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace DynVis.Draw.OpenGL
{
    partial class OpenGLEngine
    {
        private const string WGL_ARB_multisample = "WGL_ARB_multisample";

        public static readonly int RequiredMultisampling = GlobalParams.GetInt("RequiredMultisampling", 2);
        public static int Multisampling;

        public static bool WGLisExtensionSupported(IntPtr DC, string extension)
        {
            bool Result = false;
            try
            {
                var supported = Wgl.wglGetExtensionsStringARB(DC);
                Result = String.IsNullOrEmpty(supported) ? false : supported.IndexOf(extension) >= 0;
            }
            catch (NullReferenceException){}

            Log.LogResult(String.Format("Support OpenGL extension {0}", extension), Result);

            return Result;
        }

        private static int GetMultisamplePixelFormat(IntPtr DC)
        {
            // See If The String Exists In WGL!
            if (!UseArbMultisample || !WGLisExtensionSupported(DC, WGL_ARB_multisample))
            {
                //arbMultisampleSupported=false;
                return 0;
            }


            var pixelFormat = new int[1];
            var numFormats = new int[1];
            float[] fAttributes = { 0, 0 };

            // These Attributes Are The Bits We Want To Test For In Our Sample
            // Everything Is Pretty Standard, The Only One We Want To 
            // Really Focus On Is The SAMPLE BUFFERS ARB And WGL SAMPLES
            // These Two Are Going To Do The Main Testing For Whether Or Not
            // We Support Multisampling On This Hardware
            var iAttributes = new[]
                                  {
                                      Wgl.WGL_DRAW_TO_WINDOW_ARB, Gl.GL_TRUE,
                                      Wgl.WGL_SUPPORT_OPENGL_ARB, Gl.GL_TRUE,
                                      Wgl.WGL_ACCELERATION_ARB, Wgl.WGL_FULL_ACCELERATION_ARB,
                                      Wgl.WGL_COLOR_BITS_ARB, 24,
                                      Wgl.WGL_ALPHA_BITS_ARB, 8,
                                      Wgl.WGL_DEPTH_BITS_ARB, 16,
                                      Wgl.WGL_STENCIL_BITS_ARB, 0,
                                      Wgl.WGL_DOUBLE_BUFFER_ARB, Gl.GL_TRUE,
                                      Wgl.WGL_SAMPLE_BUFFERS_ARB, Gl.GL_TRUE,
                                      Wgl.WGL_SAMPLES_ARB, RequiredMultisampling, // Check For 4x Multisampling
                                      0, 0
                                  };

            for (var MultisamplingPower = RequiredMultisampling; MultisamplingPower > 1; MultisamplingPower /= 2)
            {
                iAttributes[19] = MultisamplingPower;
                
                // First We Check To See If We Can Get A Pixel Format For 4 Samples
                bool valid = Wgl.wglChoosePixelFormatARB(DC, iAttributes, fAttributes, 1, pixelFormat, numFormats);

                // if We Returned True, And Our Format Count Is Greater Than 1
                if (valid && (numFormats[0] >= 1))
                {
                    //arbMultisampleSupported	= true;
                    Multisampling = MultisamplingPower;
                    Log.LogEvent(String.Format("Use Multisampling Power: {0}", MultisamplingPower));
                    return pixelFormat[0];
                }
            }
            Multisampling = 0;
            // Return The Valid Format
            return pixelFormat[0];
        }

        public static int ChoosePixelFormat(IntPtr DC, ref Gdi.PIXELFORMATDESCRIPTOR pfd)
        {
            var pixelFormat = Gdi.ChoosePixelFormat(DC, ref pfd);

            Log.LogResult(String.Format("GDI Choosen Pixel Format: {0}", pixelFormat), pixelFormat != 0);

            if (pixelFormat == 0)
            {
                pixelFormat = Wgl.wglChoosePixelFormat(DC, ref pfd);

                Log.LogResult(String.Format("WGL Choosen Pixel Format: {0}", pixelFormat), pixelFormat != 0);

                if (pixelFormat == 0)
                {
                    throw new OpenGLEngineException("Can't choose pixel format!");
                }
            }

            return pixelFormat;
        }

        private static int? CashedPixelFormat;

        private static int GetPixelFormat(ref Gdi.PIXELFORMATDESCRIPTOR pfd)
        {
            if (CashedPixelFormat == null)
            {
                var form = new Form();

                Log.LogEvent(String.Format("OpenGL test starting for handle: {0}", form.Handle));

                var dc = CreateDC(form.Handle);

                CashedPixelFormat = ChoosePixelFormat(dc, ref pfd);

                SetPixelFormat(dc, CashedPixelFormat.Value, ref pfd);

                var hrc = CreateContext(dc);

                MakeCurentContext(dc, hrc);


                Wgl.ReloadFunctions();

                var pixelFormatTemp = GetMultisamplePixelFormat(dc);

                if (pixelFormatTemp != 0)
                {
                    CashedPixelFormat = pixelFormatTemp;
                }

                CloseAll(form, ref dc, ref hrc);
            }
            return CashedPixelFormat.Value;
        }


    }
}
