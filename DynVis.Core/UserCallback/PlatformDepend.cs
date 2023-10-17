using System;
using System.Runtime.InteropServices;


namespace DynVis.Core.UserCallback
{
    internal static class PlatformDepend
    {
        [Flags]
        enum InternetConnectionStateFlags
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        private static InternetConnectionStateFlags InternetConnectionState;

        [DllImport("WININET", CharSet = CharSet.Auto)]
        private static extern bool InternetGetConnectedState(ref InternetConnectionStateFlags lpdwFlag,
                                                             int dwReserved);

        public static bool InternetConnectedState
        {
            get
            {
                return InternetGetConnectedState(ref InternetConnectionState, 0);
            }
        }
    }
}
