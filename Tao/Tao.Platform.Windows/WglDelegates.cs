#region

using System;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Tao.Platform.Windows
{
#pragma warning disable 0649

    partial class Wgl
    {
        #region Nested type: Delegates

        internal static class Delegates
        {
            internal static AllocateMemoryNV wglAllocateMemoryNV;
            internal static AssociateImageBufferEventsI3D wglAssociateImageBufferEventsI3D;
            internal static BeginFrameTrackingI3D wglBeginFrameTrackingI3D;
            internal static BindDisplayColorTableEXT wglBindDisplayColorTableEXT;
            internal static BindTexImageARB wglBindTexImageARB;
            internal static ChoosePixelFormat wglChoosePixelFormat;
            internal static ChoosePixelFormatARB wglChoosePixelFormatARB;
            internal static ChoosePixelFormatEXT wglChoosePixelFormatEXT;
            internal static CopyContext wglCopyContext;
            internal static CreateBufferRegionARB wglCreateBufferRegionARB;
            internal static CreateContext wglCreateContext;
            internal static CreateDisplayColorTableEXT wglCreateDisplayColorTableEXT;
            internal static CreateImageBufferI3D wglCreateImageBufferI3D;
            internal static CreateLayerContext wglCreateLayerContext;
            internal static CreatePbufferARB wglCreatePbufferARB;
            internal static CreatePbufferEXT wglCreatePbufferEXT;
            internal static DeleteBufferRegionARB wglDeleteBufferRegionARB;

            internal static DeleteContext wglDeleteContext;
            internal static DescribeLayerPlane wglDescribeLayerPlane;

            internal static DescribePixelFormat wglDescribePixelFormat;
            internal static DestroyDisplayColorTableEXT wglDestroyDisplayColorTableEXT;
            internal static DestroyImageBufferI3D wglDestroyImageBufferI3D;
            internal static DestroyPbufferARB wglDestroyPbufferARB;
            internal static DestroyPbufferEXT wglDestroyPbufferEXT;
            internal static DisableFrameLockI3D wglDisableFrameLockI3D;
            internal static DisableGenlockI3D wglDisableGenlockI3D;
            internal static EnableFrameLockI3D wglEnableFrameLockI3D;
            internal static EnableGenlockI3D wglEnableGenlockI3D;
            internal static EndFrameTrackingI3D wglEndFrameTrackingI3D;
            internal static FreeMemoryNV wglFreeMemoryNV;
            internal static GenlockSampleRateI3D wglGenlockSampleRateI3D;
            internal static GenlockSourceDelayI3D wglGenlockSourceDelayI3D;
            internal static GenlockSourceEdgeI3D wglGenlockSourceEdgeI3D;
            internal static GenlockSourceI3D wglGenlockSourceI3D;
            internal static GetCurrentContext wglGetCurrentContext;

            internal static GetCurrentDC wglGetCurrentDC;
            internal static GetCurrentReadDCARB wglGetCurrentReadDCARB;
            internal static GetCurrentReadDCEXT wglGetCurrentReadDCEXT;

            internal static GetDefaultProcAddress wglGetDefaultProcAddress;
            internal static GetDigitalVideoParametersI3D wglGetDigitalVideoParametersI3D;
            internal static GetExtensionsStringARB wglGetExtensionsStringARB;
            internal static GetExtensionsStringEXT wglGetExtensionsStringEXT;
            internal static GetFrameUsageI3D wglGetFrameUsageI3D;
            internal static GetGammaTableI3D wglGetGammaTableI3D;
            internal static GetGammaTableParametersI3D wglGetGammaTableParametersI3D;
            internal static GetGenlockSampleRateI3D wglGetGenlockSampleRateI3D;
            internal static GetGenlockSourceDelayI3D wglGetGenlockSourceDelayI3D;
            internal static GetGenlockSourceEdgeI3D wglGetGenlockSourceEdgeI3D;
            internal static GetGenlockSourceI3D wglGetGenlockSourceI3D;
            internal static GetLayerPaletteEntries wglGetLayerPaletteEntries;
            internal static GetMscRateOML wglGetMscRateOML;
            internal static GetPbufferDCARB wglGetPbufferDCARB;
            internal static GetPbufferDCEXT wglGetPbufferDCEXT;

            internal static GetPixelFormat wglGetPixelFormat;
            internal static GetPixelFormatAttribfvARB wglGetPixelFormatAttribfvARB;
            internal static GetPixelFormatAttribfvEXT wglGetPixelFormatAttribfvEXT;
            internal static GetPixelFormatAttribivARB wglGetPixelFormatAttribivARB;
            internal static GetPixelFormatAttribivEXT wglGetPixelFormatAttribivEXT;
            internal static GetProcAddress wglGetProcAddress;
            internal static GetSwapIntervalEXT wglGetSwapIntervalEXT;
            internal static GetSyncValuesOML wglGetSyncValuesOML;
            internal static IsEnabledFrameLockI3D wglIsEnabledFrameLockI3D;
            internal static IsEnabledGenlockI3D wglIsEnabledGenlockI3D;
            internal static LoadDisplayColorTableEXT wglLoadDisplayColorTableEXT;
            internal static MakeContextCurrentARB wglMakeContextCurrentARB;
            internal static MakeContextCurrentEXT wglMakeContextCurrentEXT;
            internal static MakeCurrent wglMakeCurrent;
            internal static QueryFrameLockMasterI3D wglQueryFrameLockMasterI3D;
            internal static QueryFrameTrackingI3D wglQueryFrameTrackingI3D;
            internal static QueryGenlockMaxSourceDelayI3D wglQueryGenlockMaxSourceDelayI3D;
            internal static QueryPbufferARB wglQueryPbufferARB;
            internal static QueryPbufferEXT wglQueryPbufferEXT;
            internal static RealizeLayerPalette wglRealizeLayerPalette;
            internal static ReleaseImageBufferEventsI3D wglReleaseImageBufferEventsI3D;
            internal static ReleasePbufferDCARB wglReleasePbufferDCARB;
            internal static ReleasePbufferDCEXT wglReleasePbufferDCEXT;
            internal static ReleaseTexImageARB wglReleaseTexImageARB;
            internal static RestoreBufferRegionARB wglRestoreBufferRegionARB;
            internal static SaveBufferRegionARB wglSaveBufferRegionARB;
            internal static SetDigitalVideoParametersI3D wglSetDigitalVideoParametersI3D;
            internal static SetGammaTableI3D wglSetGammaTableI3D;
            internal static SetGammaTableParametersI3D wglSetGammaTableParametersI3D;
            internal static SetLayerPaletteEntries wglSetLayerPaletteEntries;
            internal static SetPbufferAttribARB wglSetPbufferAttribARB;

            internal static SetPixelFormat wglSetPixelFormat;

            internal static ShareLists wglShareLists;
            internal static SwapBuffers wglSwapBuffers;
            internal static SwapBuffersMscOML wglSwapBuffersMscOML;
            internal static SwapIntervalEXT wglSwapIntervalEXT;

            internal static SwapLayerBuffers wglSwapLayerBuffers;
            internal static SwapLayerBuffersMscOML wglSwapLayerBuffersMscOML;

            internal static UseFontBitmapsA wglUseFontBitmapsA;

            internal static UseFontBitmapsW wglUseFontBitmapsW;

            internal static UseFontOutlinesA wglUseFontOutlinesA;

            internal static UseFontOutlinesW wglUseFontOutlinesW;
            internal static WaitForMscOML wglWaitForMscOML;
            internal static WaitForSbcOML wglWaitForSbcOML;

            #region Nested type: AllocateMemoryNV

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr AllocateMemoryNV(Int32 size, Single readfreq, Single writefreq, Single priority);

            #endregion

            #region Nested type: AssociateImageBufferEventsI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean AssociateImageBufferEventsI3D(
                IntPtr hDC, IntPtr* pEvent, IntPtr pAddress, Int32* pSize, UInt32 count);

            #endregion

            #region Nested type: BeginFrameTrackingI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean BeginFrameTrackingI3D();

            #endregion

            #region Nested type: BindDisplayColorTableEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate bool BindDisplayColorTableEXT(UInt16 id);

            #endregion

            #region Nested type: BindTexImageARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean BindTexImageARB(IntPtr hPbuffer, int iBuffer);

            #endregion

            #region Nested type: ChoosePixelFormat

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate int ChoosePixelFormat(IntPtr hDc, Gdi.PIXELFORMATDESCRIPTOR* pPfd);

            #endregion

            #region Nested type: ChoosePixelFormatARB

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean ChoosePixelFormatARB(
                IntPtr hdc, int* piAttribIList, Single* pfAttribFList, UInt32 nMaxFormats, [Out] int* piFormats,
                [Out] UInt32* nNumFormats);

            #endregion

            #region Nested type: ChoosePixelFormatEXT

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean ChoosePixelFormatEXT(
                IntPtr hdc, int* piAttribIList, Single* pfAttribFList, UInt32 nMaxFormats, [Out] int* piFormats,
                [Out] UInt32* nNumFormats);

            #endregion

            #region Nested type: CopyContext

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean CopyContext(IntPtr hglrcSrc, IntPtr hglrcDst, UInt32 mask);

            #endregion

            #region Nested type: CreateBufferRegionARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr CreateBufferRegionARB(IntPtr hDC, int iLayerPlane, UInt32 uType);

            #endregion

            #region Nested type: CreateContext

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr CreateContext(IntPtr hDc);

            #endregion

            #region Nested type: CreateDisplayColorTableEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate bool CreateDisplayColorTableEXT(UInt16 id);

            #endregion

            #region Nested type: CreateImageBufferI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr CreateImageBufferI3D(IntPtr hDC, Int32 dwSize, UInt32 uFlags);

            #endregion

            #region Nested type: CreateLayerContext

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr CreateLayerContext(IntPtr hDc, int level);

            #endregion

            #region Nested type: CreatePbufferARB

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate IntPtr CreatePbufferARB(
                IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, int* piAttribList);

            #endregion

            #region Nested type: CreatePbufferEXT

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate IntPtr CreatePbufferEXT(
                IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, int* piAttribList);

            #endregion

            #region Nested type: DeleteBufferRegionARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate void DeleteBufferRegionARB(IntPtr hRegion);

            #endregion

            #region Nested type: DeleteContext

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean DeleteContext(IntPtr oldContext);

            #endregion

            #region Nested type: DescribeLayerPlane

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean DescribeLayerPlane(
                IntPtr hDc, int pixelFormat, int layerPlane, UInt32 nBytes, Gdi.LAYERPLANEDESCRIPTOR* plpd);

            #endregion

            #region Nested type: DescribePixelFormat

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate int DescribePixelFormat(
                IntPtr hdc, int ipfd, UInt32 cjpfd, Gdi.PIXELFORMATDESCRIPTOR* ppfd);

            #endregion

            #region Nested type: DestroyDisplayColorTableEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate void DestroyDisplayColorTableEXT(UInt16 id);

            #endregion

            #region Nested type: DestroyImageBufferI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean DestroyImageBufferI3D(IntPtr hDC, IntPtr pAddress);

            #endregion

            #region Nested type: DestroyPbufferARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean DestroyPbufferARB(IntPtr hPbuffer);

            #endregion

            #region Nested type: DestroyPbufferEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean DestroyPbufferEXT(IntPtr hPbuffer);

            #endregion

            #region Nested type: DisableFrameLockI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean DisableFrameLockI3D();

            #endregion

            #region Nested type: DisableGenlockI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean DisableGenlockI3D(IntPtr hDC);

            #endregion

            #region Nested type: EnableFrameLockI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean EnableFrameLockI3D();

            #endregion

            #region Nested type: EnableGenlockI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean EnableGenlockI3D(IntPtr hDC);

            #endregion

            #region Nested type: EndFrameTrackingI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean EndFrameTrackingI3D();

            #endregion

            #region Nested type: FreeMemoryNV

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate void FreeMemoryNV([Out] IntPtr* pointer);

            #endregion

            #region Nested type: GenlockSampleRateI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean GenlockSampleRateI3D(IntPtr hDC, UInt32 uRate);

            #endregion

            #region Nested type: GenlockSourceDelayI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean GenlockSourceDelayI3D(IntPtr hDC, UInt32 uDelay);

            #endregion

            #region Nested type: GenlockSourceEdgeI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean GenlockSourceEdgeI3D(IntPtr hDC, UInt32 uEdge);

            #endregion

            #region Nested type: GenlockSourceI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean GenlockSourceI3D(IntPtr hDC, UInt32 uSource);

            #endregion

            #region Nested type: GetCurrentContext

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetCurrentContext();

            #endregion

            #region Nested type: GetCurrentDC

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetCurrentDC();

            #endregion

            #region Nested type: GetCurrentReadDCARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetCurrentReadDCARB();

            #endregion

            #region Nested type: GetCurrentReadDCEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetCurrentReadDCEXT();

            #endregion

            #region Nested type: GetDefaultProcAddress

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetDefaultProcAddress(String lpszProc);

            #endregion

            #region Nested type: GetDigitalVideoParametersI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, [Out] int* piValue
                );

            #endregion

            #region Nested type: GetExtensionsStringARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetExtensionsStringARB(IntPtr hdc);

            #endregion

            #region Nested type: GetExtensionsStringEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetExtensionsStringEXT();

            #endregion

            #region Nested type: GetFrameUsageI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetFrameUsageI3D([Out] float* pUsage);

            #endregion

            #region Nested type: GetGammaTableI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetGammaTableI3D(
                IntPtr hDC, int iEntries, [Out] UInt16* puRed, [Out] UInt16* puGreen, [Out] UInt16* puBlue);

            #endregion

            #region Nested type: GetGammaTableParametersI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetGammaTableParametersI3D(IntPtr hDC, int iAttribute, [Out] int* piValue);

            #endregion

            #region Nested type: GetGenlockSampleRateI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetGenlockSampleRateI3D(IntPtr hDC, [Out] UInt32* uRate);

            #endregion

            #region Nested type: GetGenlockSourceDelayI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetGenlockSourceDelayI3D(IntPtr hDC, [Out] UInt32* uDelay);

            #endregion

            #region Nested type: GetGenlockSourceEdgeI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetGenlockSourceEdgeI3D(IntPtr hDC, [Out] UInt32* uEdge);

            #endregion

            #region Nested type: GetGenlockSourceI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetGenlockSourceI3D(IntPtr hDC, [Out] UInt32* uSource);

            #endregion

            #region Nested type: GetLayerPaletteEntries

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate int GetLayerPaletteEntries(
                IntPtr hdc, int iLayerPlane, int iStart, int cEntries, Int32* pcr);

            #endregion

            #region Nested type: GetMscRateOML

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetMscRateOML(IntPtr hdc, [Out] Int32* numerator, [Out] Int32* denominator);

            #endregion

            #region Nested type: GetPbufferDCARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetPbufferDCARB(IntPtr hPbuffer);

            #endregion

            #region Nested type: GetPbufferDCEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetPbufferDCEXT(IntPtr hPbuffer);

            #endregion

            #region Nested type: GetPixelFormat

            [SuppressUnmanagedCodeSecurity]
            internal delegate int GetPixelFormat(IntPtr hdc);

            #endregion

            #region Nested type: GetPixelFormatAttribfvARB

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetPixelFormatAttribfvARB(
                IntPtr hdc, int iPixelFormat, int iLayerPlane, UInt32 nAttributes, int* piAttributes,
                [Out] Single* pfValues);

            #endregion

            #region Nested type: GetPixelFormatAttribfvEXT

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetPixelFormatAttribfvEXT(
                IntPtr hdc, int iPixelFormat, int iLayerPlane, UInt32 nAttributes, [Out] int* piAttributes,
                [Out] Single* pfValues);

            #endregion

            #region Nested type: GetPixelFormatAttribivARB

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetPixelFormatAttribivARB(
                IntPtr hdc, int iPixelFormat, int iLayerPlane, UInt32 nAttributes, int* piAttributes,
                [Out] int* piValues);

            #endregion

            #region Nested type: GetPixelFormatAttribivEXT

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetPixelFormatAttribivEXT(
                IntPtr hdc, int iPixelFormat, int iLayerPlane, UInt32 nAttributes, [Out] int* piAttributes,
                [Out] int* piValues);

            #endregion

            #region Nested type: GetProcAddress

            [SuppressUnmanagedCodeSecurity]
            internal delegate IntPtr GetProcAddress(String lpszProc);

            #endregion

            #region Nested type: GetSwapIntervalEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate int GetSwapIntervalEXT();

            #endregion

            #region Nested type: GetSyncValuesOML

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean GetSyncValuesOML(
                IntPtr hdc, [Out] Int64* ust, [Out] Int64* msc, [Out] Int64* sbc);

            #endregion

            #region Nested type: IsEnabledFrameLockI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean IsEnabledFrameLockI3D([Out] Boolean* pFlag);

            #endregion

            #region Nested type: IsEnabledGenlockI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean IsEnabledGenlockI3D(IntPtr hDC, [Out] Boolean* pFlag);

            #endregion

            #region Nested type: LoadDisplayColorTableEXT

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate bool LoadDisplayColorTableEXT(UInt16* table, UInt32 length);

            #endregion

            #region Nested type: MakeContextCurrentARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean MakeContextCurrentARB(IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);

            #endregion

            #region Nested type: MakeContextCurrentEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean MakeContextCurrentEXT(IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);

            #endregion

            #region Nested type: MakeCurrent

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean MakeCurrent(IntPtr hDc, IntPtr newContext);

            #endregion

            #region Nested type: QueryFrameLockMasterI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean QueryFrameLockMasterI3D([Out] Boolean* pFlag);

            #endregion

            #region Nested type: QueryFrameTrackingI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean QueryFrameTrackingI3D(
                [Out] Int32* pFrameCount, [Out] Int32* pMissedFrames, [Out] float* pLastMissedUsage);

            #endregion

            #region Nested type: QueryGenlockMaxSourceDelayI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean QueryGenlockMaxSourceDelayI3D(
                IntPtr hDC, [Out] UInt32* uMaxLineDelay, [Out] UInt32* uMaxPixelDelay);

            #endregion

            #region Nested type: QueryPbufferARB

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean QueryPbufferARB(IntPtr hPbuffer, int iAttribute, [Out] int* piValue);

            #endregion

            #region Nested type: QueryPbufferEXT

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean QueryPbufferEXT(IntPtr hPbuffer, int iAttribute, [Out] int* piValue);

            #endregion

            #region Nested type: RealizeLayerPalette

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean RealizeLayerPalette(IntPtr hdc, int iLayerPlane, Boolean bRealize);

            #endregion

            #region Nested type: ReleaseImageBufferEventsI3D

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean ReleaseImageBufferEventsI3D(IntPtr hDC, IntPtr pAddress, UInt32 count);

            #endregion

            #region Nested type: ReleasePbufferDCARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate int ReleasePbufferDCARB(IntPtr hPbuffer, IntPtr hDC);

            #endregion

            #region Nested type: ReleasePbufferDCEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate int ReleasePbufferDCEXT(IntPtr hPbuffer, IntPtr hDC);

            #endregion

            #region Nested type: ReleaseTexImageARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean ReleaseTexImageARB(IntPtr hPbuffer, int iBuffer);

            #endregion

            #region Nested type: RestoreBufferRegionARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean RestoreBufferRegionARB(
                IntPtr hRegion, int x, int y, int width, int height, int xSrc, int ySrc);

            #endregion

            #region Nested type: SaveBufferRegionARB

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean SaveBufferRegionARB(IntPtr hRegion, int x, int y, int width, int height);

            #endregion

            #region Nested type: SetDigitalVideoParametersI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean SetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, int* piValue);

            #endregion

            #region Nested type: SetGammaTableI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean SetGammaTableI3D(
                IntPtr hDC, int iEntries, UInt16* puRed, UInt16* puGreen, UInt16* puBlue);

            #endregion

            #region Nested type: SetGammaTableParametersI3D

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean SetGammaTableParametersI3D(IntPtr hDC, int iAttribute, int* piValue);

            #endregion

            #region Nested type: SetLayerPaletteEntries

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate int SetLayerPaletteEntries(
                IntPtr hdc, int iLayerPlane, int iStart, int cEntries, Int32* pcr);

            #endregion

            #region Nested type: SetPbufferAttribARB

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean SetPbufferAttribARB(IntPtr hPbuffer, int* piAttribList);

            #endregion

            #region Nested type: SetPixelFormat

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean SetPixelFormat(IntPtr hdc, int ipfd, Gdi.PIXELFORMATDESCRIPTOR* ppfd);

            #endregion

            #region Nested type: ShareLists

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean ShareLists(IntPtr hrcSrvShare, IntPtr hrcSrvSource);

            #endregion

            #region Nested type: SwapBuffers

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean SwapBuffers(IntPtr hdc);

            #endregion

            #region Nested type: SwapBuffersMscOML

            [SuppressUnmanagedCodeSecurity]
            internal delegate Int64 SwapBuffersMscOML(IntPtr hdc, Int64 target_msc, Int64 divisor, Int64 remainder);

            #endregion

            #region Nested type: SwapIntervalEXT

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean SwapIntervalEXT(int interval);

            #endregion

            #region Nested type: SwapLayerBuffers

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean SwapLayerBuffers(IntPtr hdc, UInt32 fuFlags);

            #endregion

            #region Nested type: SwapLayerBuffersMscOML

            [SuppressUnmanagedCodeSecurity]
            internal delegate Int64 SwapLayerBuffersMscOML(
                IntPtr hdc, int fuPlanes, Int64 target_msc, Int64 divisor, Int64 remainder);

            #endregion

            #region Nested type: UseFontBitmapsA

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean UseFontBitmapsA(IntPtr hDC, Int32 first, Int32 count, Int32 listBase);

            #endregion

            #region Nested type: UseFontBitmapsW

            [SuppressUnmanagedCodeSecurity]
            internal delegate Boolean UseFontBitmapsW(IntPtr hDC, Int32 first, Int32 count, Int32 listBase);

            #endregion

            #region Nested type: UseFontOutlinesA

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean UseFontOutlinesA(
                IntPtr hDC, Int32 first, Int32 count, Int32 listBase, float thickness, float deviation, Int32 fontMode,
                Gdi.GLYPHMETRICSFLOAT* glyphMetrics);

            #endregion

            #region Nested type: UseFontOutlinesW

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean UseFontOutlinesW(
                IntPtr hDC, Int32 first, Int32 count, Int32 listBase, float thickness, float deviation, Int32 fontMode,
                Gdi.GLYPHMETRICSFLOAT* glyphMetrics);

            #endregion

            #region Nested type: WaitForMscOML

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean WaitForMscOML(
                IntPtr hdc, Int64 target_msc, Int64 divisor, Int64 remainder, [Out] Int64* ust, [Out] Int64* msc,
                [Out] Int64* sbc);

            #endregion

            #region Nested type: WaitForSbcOML

            [SuppressUnmanagedCodeSecurity]
            internal unsafe delegate Boolean WaitForSbcOML(
                IntPtr hdc, Int64 target_sbc, [Out] Int64* ust, [Out] Int64* msc, [Out] Int64* sbc);

            #endregion
        }

        #endregion
    }
#pragma warning restore 0649
}