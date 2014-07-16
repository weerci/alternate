using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CoreCommon;

namespace Alternative
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Можно запустить только один экзепляр приложения
                int c = Process.GetProcesses().Where(n => n.ProcessName == Application.ProductName).ToArray().Count();
                if (c == 0 || c == 1)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.ThreadException += new ThreadExceptionEventHandler(OnThreadException);
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    EAlternate.HandleDb();

                    if (new FormConnect().ShowDialog() == DialogResult.OK)
                        Application.Run(new FormMain());
                }
                else
                {
                    Log.ToLog(ErrorMsg.EAppDoubleApplication);
                    Application.Exit();
                }
            }
            catch (Exception err)
            {
                try
                {
                    Log.ToLog(err.Message);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs t)
        {
            EAlternate.HandleError((Exception)t.Exception);
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            EAlternate.HandleError((Exception)e.ExceptionObject);
        }

    }
}
