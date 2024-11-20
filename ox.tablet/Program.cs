using Akka.Actor;
using OX.Persistence.LevelDB;
using OX.Tablet.Config;
using OX.Wallets;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace OX.Tablet
{
    static class Program
    {
        static bool createNew;
        public static IActorRef BlockHandler;
        public static MainForm MainForm;
        public static BlockIndex BlockIndex;
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    BlockHandler = TabletBlockHandler.Instance;
                    Application.Run(MainForm = MainForm.Instance);
                    MainForm.Instance.FormClosing += MainForm_FormClosing;
                }
                else
                {
                    MessageBox.Show(UIHelper.LocalString("OX娱乐程序已经在运行中...", " OX casino program is already running..."));
                    System.Threading.Thread.Sleep(1000);
                    System.Environment.Exit(1);
                }
            }

        }

        private static void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            using (FileStream fs = new FileStream("error.log", FileMode.Create, FileAccess.Write, FileShare.None))
            using (StreamWriter w = new StreamWriter(fs))
                if (e.ExceptionObject is Exception ex)
                {
                    PrintErrorLogs(w, ex);
                }
                else
                {
                    w.WriteLine(e.ExceptionObject.GetType());
                    w.WriteLine(e.ExceptionObject);
                }
        }
        private static void PrintErrorLogs(StreamWriter writer, Exception ex)
        {
            writer.WriteLine(ex.GetType());
            writer.WriteLine(ex.Message);
            writer.WriteLine(ex.StackTrace);
            if (ex is AggregateException ex2)
            {
                foreach (Exception inner in ex2.InnerExceptions)
                {
                    writer.WriteLine();
                    PrintErrorLogs(writer, inner);
                }
            }
            else if (ex.InnerException != null)
            {
                writer.WriteLine();
                PrintErrorLogs(writer, ex.InnerException);
            }
        }
    }
}
