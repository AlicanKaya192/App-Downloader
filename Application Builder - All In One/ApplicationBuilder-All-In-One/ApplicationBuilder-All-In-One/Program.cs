using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationBuilder_All_In_One
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (SplashScreen splash = new SplashScreen())
            {
                splash.Show();
                Application.DoEvents();
                Thread.Sleep(2000);
            }

            using (Applications mainForm = new Applications())
            {
                Application.Run(mainForm);
            }

            Environment.Exit(0);
        }
    }
}
