using System;
using System.Windows.Forms;
using DynVis.Core.VersionFraemwork;

namespace DynVis.EventsLog
{
    static public partial class Log
    {
        public static void ApplicationStartEvent()
        {
            LogEvent(String.Format("{0} ({1}) started.", Application.ProductName, Application.ProductVersion));

            var InstaledFW = FrameworkVersionDetection.GetInstalledFraemvorkList();

            foreach (var fw in InstaledFW)
            {
                LogEvent(fw);
            }
        }
    }
}
