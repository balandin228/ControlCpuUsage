using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            var processFileName  = "C:\\Users\\79090\\Desktop\\Processes\\netcoreapp3.1\\ConsoleApp10.exe";
            using (var process = new Process(){})
            {
                process.StartInfo.FileName = processFileName;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                var k = process.Threads;
                foreach (ProcessThread processThread in k)
                {
                    Console.WriteLine(processThread.Id);
                }

                process.Close();
            }
            var counter = new PerformanceCounter("Process", "% Processor Time", "ConsoleApp10");
            var counter2 = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
            var counter3 = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            while (true)
            {
                Console.Write("\r");
                Console.Write("                                                                               ");
                Console.Write("\r");
                Console.Write($"ConsoleApp10= {counter.NextValue()/ Environment.ProcessorCount}, ConsoleApp11={counter2.NextValue()/ Environment.ProcessorCount}, Total={counter3.NextValue()}");

                Thread.Sleep(200);
            }
        }
    }
}
