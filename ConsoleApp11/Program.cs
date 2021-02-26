using System;
using System.Diagnostics;
using System.Threading;
using Library;

namespace ConsoleApp11
{
    public class Program
    {
        private const string processName = "ConsoleApp10";
        private static  ProcessesSystemMonitor _monitor;
        static void Main(string[] args)
        {
            _monitor = new ProcessesSystemMonitor();
            var processFileName  = "C:\\Users\\79090\\Desktop\\Processes\\netcoreapp3.1\\ConsoleApp10.exe";
            _monitor.AddProcess(processName, 4);
            //using var process = new Process();
            //process.StartInfo.FileName = processFileName;
            //process.StartInfo.UseShellExecute = false;
            //process.StartInfo.RedirectStandardOutput = true;
            //process.StartInfo.RedirectStandardInput = true;
            //process.StartInfo.RedirectStandardError = true;
            //process.StartInfo.CreateNoWindow = true;
            //process.Start();
            var i = 0;
            while (i != 100)
            {
                i++;
                var cpu = _monitor.GetProcessCpuUsage(processName);
                Console.Write($"Cpu={cpu}");
                Thread.Sleep(100);
                Console.Write("\r");
                Console.Write("                 ");
                Console.Write("\r");
                if (cpu > 30)
                    _monitor.SuspendProcess("ConsoleApp10");
                else
                    _monitor.ReleaseProcess(processName);
            }

            _monitor.Dispose();

        }
    }
}
