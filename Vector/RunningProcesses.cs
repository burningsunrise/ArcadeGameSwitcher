using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Vector
{
    class RunningProcesses
    {
        public RunningProcesses()
        {
            // First run check if process running, if not run it
            Process[] pname = Process.GetProcessesByName("notepad");
            if (pname.Length == 0)
            {
                Process.Start(@"C:\Windows\System32\Notepad.exe");
            }

        }
    }
}
