using System;
using System.Diagnostics;
using System.Threading;
using Fs.Processes;
using Fs.Processes.JobObjects;
using Library;
using Library.JobObject;

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
            var job = JobObjectFactory.CreateJob(20, RateControlInterval.Short);
            var counter = new PerformanceCounter("Process", "% Processor Time", processName);
            var createProcessInfo = new CreateProcessInfo()
            {
                FileName = "C:\\Users\\79090\\Desktop\\Processes\\netcoreapp3.1\\ConsoleApp10.exe",
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
            };
            var process = job.CreateProcess(createProcessInfo);
            while (!job.Idle.IsCompleted)
            {
                Thread.Sleep(100);
                Console.Write("\r");
                Console.Write("                 ");
                Console.Write("\r");
                Console.WriteLine( counter.NextValue() / Environment.ProcessorCount);
            }
        }
    }
}
