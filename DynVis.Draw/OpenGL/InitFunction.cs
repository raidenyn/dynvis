using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DynVis.Draw.Properties;
using DynVis.EventsLog;
using Tao.Platform.Windows;

namespace DynVis.Draw.OpenGL
{
    partial class OpenGLEngine : IDisposable
    {
        private Form OwnerForm;
        public readonly Control OwnerControl;
        private IntPtr DC;
        private IntPtr HRC;
        private int PixelFormat;
        private bool EngineNotInitialized = true;

        protected OpenGLEngine(Control ownerControl)
        {
            if (ownerControl.Handle == IntPtr.Zero)
            {
                throw new OpenGLEngineException(LangResource.InvalidWindowHandle);
            }

            OwnerControl = ownerControl;

            OwnerControl.ParentChanged += ParentChanged;
        }

        void OwnerForm_Load(object sender, EventArgs e)
        {
            if (EngineNotInitialized) InitializeEngine();
        }

        void ParentChanged(object sender, EventArgs e)
        {
            EngineNotInitialized = true;
        }

        bool SetParentForm()
        {
            if (OwnerForm != null)
            {
                OwnerForm.FormClosing -= FormClosing;
                OwnerForm.Load -= OwnerForm_Load;
            }
            OwnerForm = OwnerControl.FindForm();
            if (OwnerForm != null)
            {
                OwnerForm.Load += OwnerForm_Load;
            }

            return OwnerForm != null;
        }

        private void InitializeEngine()
        {
            if (EngineNotInitialized)
            {
                if (SetParentForm())
                {
                    OwnerForm.FormClosing += FormClosing;

                    InitOpenGL();

                    BuildFont(new Font("Courier New", 12, FontStyle.Bold));

                    SetInitPatams();

                    EngineNotInitialized = false;

                    Refresh();
                }
            }
        }

        private void InitOpenGL()
        {
            Log.LogEvent(String.Format("OpenGL starting for handle: {0}", OwnerControl.Handle));
            
            DC = CreateDC(OwnerControl.Handle);

            var pfd = CreatePFD();

            PixelFormat = GetPixelFormat(ref pfd);

            SetPixelFormat(DC, PixelFormat, ref pfd);

            HRC = CreateContext(DC);

            MakeCurentContext(DC, HRC);

            Wgl.ReloadFunctions();
        }

        ~OpenGLEngine()
        {
            Destructor();
        }

        #region Initialize OpenGL Engine Functions
        private static IntPtr CreateDC(IntPtr handle)
        {
            var dc = User.GetDC(handle);

            Log.LogResult(String.Format("Created Device Context: {0}", dc), dc != IntPtr.Zero);

            if (dc == IntPtr.Zero)
            {
                throw new OpenGLEngineException("Can't get device context!");
            }
            return dc;
        }

        private static Gdi.PIXELFORMATDESCRIPTOR CreatePFD()
        {
            var pfd = new Gdi.PIXELFORMATDESCRIPTOR
                          {
                              dwFlags = (Gdi.PFD_SUPPORT_OPENGL | // Format Must Support Window
                                         Gdi.PFD_DRAW_TO_WINDOW | // Format Must Support OpenGL
                                         Gdi.PFD_DOUBLEBUFFER),
                              // Format Must Support Double Buffering
                              nVersion = 1 // Version Number
                          };

            pfd.nSize = (short) Marshal.SizeOf(pfd); // Size Of This Pixel Format Descriptor
            
            pfd.iPixelType = Gdi.PFD_TYPE_RGBA; // Request An RGBA Format
            pfd.cColorBits = 24; // Select Our Color Depth
            pfd.cRedBits = 0; // Color Bits Ignored
            pfd.cRedShift = 0;
            pfd.cGreenBits = 0;
            pfd.cGreenShift = 0;
            pfd.cBlueBits = 0;
            pfd.cBlueShift = 0;
            pfd.cAlphaBits = 0; // No Alpha Buffer
            pfd.cAlphaShift = 0; // Shift Bit Ignored
            pfd.cAccumBits = 0; // No Accumulation Buffer
            pfd.cAccumRedBits = 0; // Accumulation Bits Ignored
            pfd.cAccumGreenBits = 0;
            pfd.cAccumBlueBits = 0;
            pfd.cAccumAlphaBits = 0;
            pfd.cDepthBits = 32; // 16Bit Z-Buffer (Depth Buffer)
            pfd.cStencilBits = 0; // No Stencil Buffer
            pfd.cAuxBuffers = 0; // No Auxiliary Buffer
            pfd.iLayerType = Gdi.PFD_MAIN_PLANE; // Main Drawing Layer
            pfd.bReserved = 0; // Reserved
            pfd.dwLayerMask = 0; // Layer Masks Ignored
            pfd.dwVisibleMask = 0;
            pfd.dwDamageMask = 0;
            

            Log.LogEvent(String.Format("Created Pixel Format Descriptor"));

            return pfd;
        }

        private static void SetPixelFormat(IntPtr dc, int pixelFormat, ref Gdi.PIXELFORMATDESCRIPTOR pfd)
        {
            var result = Gdi.SetPixelFormat(dc, pixelFormat, ref pfd);

            Log.LogResult(String.Format("Set Pixel Format: {0}", pixelFormat), result);

            if (!result)
            {
                throw new OpenGLEngineException("Can't set pixel format!");
            }
        }

        private static IntPtr CreateContext(IntPtr dc)
        {
            var hrc = Wgl.wglCreateContext(dc);

            Log.LogResult(String.Format("Context Created: {0}", hrc), hrc != IntPtr.Zero);

            if (hrc == IntPtr.Zero)
            {
                throw new OpenGLEngineException("Can't create rendering context!");
            }
            return hrc;
        }
        #endregion

        #region Desposing Function

        void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel != true)
            {
                Dispose();
            }
        }

        [Browsable(false)]
        public bool Disposed { get; private set; }

        private void Destructor()
        {
            if (!Disposed)
            {
                CloseAll();
            }
            Disposed = true;
        }

        private void CloseAll()
        {
            if (!EngineNotInitialized && MakeCurentContext())
            {
                RemoveFont();

                CloseAll(OwnerControl, ref DC, ref HRC);

                OwnerForm.ParentChanged -= ParentChanged;
            }
        }

        private static void CloseAll(Control control, ref IntPtr dc, ref IntPtr hrc)
        {
            DeleteContext(dc, ref hrc);

            ReleaseDeviceContext(control, ref dc);

            Log.LogEvent(String.Format("OpenGL was stoped for handle: {0}", control.Handle));
        }

        private static void DeleteContext(IntPtr dc, ref IntPtr hrc)
        {
            if (hrc != IntPtr.Zero)
            {
                if (!Wgl.wglMakeCurrent(dc, IntPtr.Zero)) throw new Exception("Cann't unhandle context");
                if (!Wgl.wglDeleteContext(hrc)) throw new Exception("Cann't delete context");

                Log.LogEvent(String.Format("Context Deleted: {0}", hrc));

                hrc = IntPtr.Zero;
            }
        }

        private static void ReleaseDeviceContext(Control control, ref IntPtr dc)
        {
            if (dc != IntPtr.Zero)
            {
                if (control != null && !control.IsDisposed)
                {
                    if (control.Handle != IntPtr.Zero)
                    {
                        if (!User.ReleaseDC(control.Handle, dc)) throw new Exception("Cann't release DC");

                        Log.LogEvent(String.Format("Device Context Released: {0}", dc));
                    }
                }

                dc = IntPtr.Zero;
            }
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Destructor();
        }

        #endregion


        private bool MakeCurentContext()
        {
            InitializeEngine();

            return MakeCurentContext(DC, HRC);
        }

        private static bool MakeCurentContext(IntPtr dc, IntPtr hrc)
        {
            return Wgl.wglMakeCurrent(dc, hrc);
            /*
            if (!Wgl.wglMakeCurrent(dc, hrc))
            {
                throw new OpenGLEngineException("Can't set current rendering context!");
            }
             * */
        }
    }
}
