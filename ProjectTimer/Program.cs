using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjectTimer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			// prevent multiple instances
            if (System.Diagnostics.Process.GetProcessesByName("ProjectTimer").Length == 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ProjectTimer());
            }

        }
    }
}
