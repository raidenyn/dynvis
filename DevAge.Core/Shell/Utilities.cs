#region

using System.Diagnostics;

#endregion

namespace DevAge.Shell
{
    /// <summary>
    /// Shell utilities
    /// </summary>
    public class Utilities
    {
        public static void OpenFile(string p_File)
        {
            ExecCommand(p_File);
        }

        public static void ExecCommand(string p_Command)
        {
            var p = new ProcessStartInfo(p_Command);
            p.UseShellExecute = true;
            var process = new Process();
            process.StartInfo = p;
            process.Start();
        }
    }
}