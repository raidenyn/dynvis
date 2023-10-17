using System;
using System.Windows.Forms;
using DynVis.Core.UserCallBack.BugReports;
using DynVis.EventsLog;

namespace DynVis
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Log.ApplicationStartEvent();
            BugReports.Init();
#if DEBUG
            Application.Run(new FormMain());
#else
            try
            {
                Application.Run(new FormMain());
            }
            catch (Exception e)
            {
            /*
                MessageBox.Show(e.Message + 
                    Environment.NewLine + 
                    e.Source + 
                    Environment.NewLine + Environment.NewLine +
                    e.StackTrace);
            */
                BugReports.SendBugReportDialog(e);
            }
#endif

        }
    }
}
