using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowTimer {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            FileSystem.Init();
            FileSystem.UnpackAllFileExtensions("wav", FlowTimer.Beeps);
            Win32.SetDllDirectory(FlowTimer.Folder);

            Application.ThreadException += (s, e) => LogCrash(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => LogCrash(e.ExceptionObject as Exception);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void LogCrash(Exception ex) {
            File.WriteAllText(
                Path.Combine(FlowTimer.Folder, "crash.log"),
                DateTime.Now + "\n" + ex?.ToString()
            );
        }
    }
}
