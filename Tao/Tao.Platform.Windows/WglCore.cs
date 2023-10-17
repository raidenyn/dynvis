#region

using System;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Tao.Platform.Windows
{
    partial class Wgl
    {
        #region Nested type: Imports

        internal static partial class Imports
        {
            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglCreateContext", ExactSpelling = true)]
            internal static extern IntPtr CreateContext(IntPtr hDc);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglDeleteContext", ExactSpelling = true)]
            internal static extern Boolean DeleteContext(IntPtr oldContext);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglGetCurrentContext", ExactSpelling = true)]
            internal static extern IntPtr GetCurrentContext();

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglMakeCurrent", ExactSpelling = true)]
            internal static extern Boolean MakeCurrent(IntPtr hDc, IntPtr newContext);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglCopyContext", ExactSpelling = true)]
            internal static extern Boolean CopyContext(IntPtr hglrcSrc, IntPtr hglrcDst, UInt32 mask);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglChoosePixelFormat", ExactSpelling = true)]
            internal static extern unsafe int ChoosePixelFormat(IntPtr hDc, Gdi.PIXELFORMATDESCRIPTOR* pPfd);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglDescribePixelFormat", ExactSpelling = true)]
            internal static extern unsafe int DescribePixelFormat(IntPtr hdc, int ipfd, UInt32 cjpfd,
                                                                  Gdi.PIXELFORMATDESCRIPTOR* ppfd);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglGetCurrentDC", ExactSpelling = true)]
            internal static extern IntPtr GetCurrentDC();

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglGetDefaultProcAddress", ExactSpelling = true, CharSet = CharSet.Ansi)]
            internal static extern IntPtr GetDefaultProcAddress(String lpszProc);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglGetProcAddress", ExactSpelling = true, CharSet = CharSet.Ansi)]
            internal static extern IntPtr GetProcAddress(String lpszProc);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglGetPixelFormat", ExactSpelling = true)]
            internal static extern int GetPixelFormat(IntPtr hdc);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglSetPixelFormat", ExactSpelling = true)]
            internal static extern unsafe Boolean SetPixelFormat(IntPtr hdc, int ipfd, Gdi.PIXELFORMATDESCRIPTOR* ppfd);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglSwapBuffers", ExactSpelling = true)]
            internal static extern Boolean SwapBuffers(IntPtr hdc);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglShareLists", ExactSpelling = true)]
            internal static extern Boolean ShareLists(IntPtr hrcSrvShare, IntPtr hrcSrvSource);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglCreateLayerContext", ExactSpelling = true)]
            internal static extern IntPtr CreateLayerContext(IntPtr hDc, int level);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglDescribeLayerPlane", ExactSpelling = true)]
            internal static extern unsafe Boolean DescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane,
                                                                     UInt32 nBytes, Gdi.LAYERPLANEDESCRIPTOR* plpd);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglSetLayerPaletteEntries", ExactSpelling = true)]
            internal static extern unsafe int SetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart,
                                                                     int cEntries, Int32* pcr);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglGetLayerPaletteEntries", ExactSpelling = true)]
            internal static extern unsafe int GetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart,
                                                                     int cEntries, Int32* pcr);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglRealizeLayerPalette", ExactSpelling = true)]
            internal static extern Boolean RealizeLayerPalette(IntPtr hdc, int iLayerPlane, Boolean bRealize);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglSwapLayerBuffers", ExactSpelling = true)]
            internal static extern Boolean SwapLayerBuffers(IntPtr hdc, UInt32 fuFlags);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglUseFontBitmapsA", CharSet = CharSet.Auto)]
            internal static extern Boolean UseFontBitmapsA(IntPtr hDC, Int32 first, Int32 count, Int32 listBase);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglUseFontBitmapsW", CharSet = CharSet.Auto)]
            internal static extern Boolean UseFontBitmapsW(IntPtr hDC, Int32 first, Int32 count, Int32 listBase);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglUseFontOutlinesA", CharSet = CharSet.Auto)]
            internal static extern unsafe Boolean UseFontOutlinesA(IntPtr hDC, Int32 first, Int32 count, Int32 listBase,
                                                                   float thickness, float deviation, Int32 fontMode,
                                                                   Gdi.GLYPHMETRICSFLOAT* glyphMetrics);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Library, EntryPoint = "wglUseFontOutlinesW", CharSet = CharSet.Auto)]
            internal static extern unsafe Boolean UseFontOutlinesW(IntPtr hDC, Int32 first, Int32 count, Int32 listBase,
                                                                   float thickness, float deviation, Int32 fontMode,
                                                                   Gdi.GLYPHMETRICSFLOAT* glyphMetrics);
        }

        #endregion
    }
}