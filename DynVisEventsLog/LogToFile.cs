using System;
using System.Windows.Forms;
using System.IO;

namespace DynVis.EventsLog
{
    internal static class LogToFile
    {
        public const string LogFileName = "log.txt";

        private static StreamWriter LogFile;

        public static void Init()
        {
            
        }

        static LogToFile()
        {
            if (CreateFile())
            {
                Log.OnNewEvent += Log_OnNewEvent;

                Application.ApplicationExit += Application_ApplicationExit;
            }
        }

        private static bool CreateFile()
        {
            try
            {
                LogFile = new StreamWriter(LogFileName);
            } 
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (SystemException) { }

            return LogFile != null;
        }

        private static void Log_OnNewEvent(EventEventArgs e)
        {
            if (LogFile != null)
            {
                try
                {
                    LogFile.WriteLine(e.Event.ToString());
                    LogFile.Flush();
                }
                catch (IOException) { }
                catch (ObjectDisposedException) { }
            }
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (LogFile != null) LogFile.Close();
        }
    }
}
