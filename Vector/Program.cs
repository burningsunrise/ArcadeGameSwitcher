using System;
using System.Windows.Forms;
using LowLevelHooking;

namespace Vector
{
    static class Program
    {
        public static GlobalKeyboardHook GlobalKeyboardHook { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // RunningProcesses startProcesses = new RunningProcesses();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (GlobalKeyboardHook = new GlobalKeyboardHook())
                {
                    Application.Run(new Form1());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
        }
    }
}
