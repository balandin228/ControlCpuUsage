using Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public class App
{
    private static Mutex mut = new Mutex();

    private static IEnumerable<int> Fibonacci
    {
        get
        {
            var a = 1;
            var b = 1;
            yield return 1;
            yield return 1;
            while (true)
            {
                var c = a + b;
                a = b;
                b = c;
                yield return c;
            }
        }
    }
	public static void Main()
    {
        var suspensionToken = new SuspensionToken();
        var fileName = Process.GetCurrentProcess().ProcessName + DateTime.Now.GetHashCode();

        var checkThread = new Thread(() => CheckCpuUsage(suspensionToken));
        checkThread.Start();
        for (var i = 0; i < 5; i++)
        {
            var workThread = new Thread(() => DoProcess(fileName+i, suspensionToken));
            workThread.Name = i + fileName;
            workThread.Start();
        }
        //mut.WaitOne();
        //while (true)
        //{
        //    if (suspensionToken.Suspended && suspensionToken.WorkThreadTaken)
        //    {
        //        mut.WaitOne();
        //        suspensionToken.WorkThreadTaken = false;
        //    }

        //    if (!suspensionToken.Suspended && suspensionToken.WorkThreadTaken)
        //    {
        //        mut.ReleaseMutex();

        //    }
        //}
    }

    public  static void CheckCpuUsage(SuspensionToken token)
    {
        var currentProcessCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
        var totalCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        var taken = true;
        while (true)
        {
            var processCpu = currentProcessCounter.NextValue() / Environment.ProcessorCount;
            Console.Write("\r");
            Console.Write("                                                                               ");
            Console.Write("\r");
            Console.Write($"ConsoleApp10={processCpu}, Total={totalCounter.NextValue()}");

            Thread.Sleep(200);
            //if (cpu > 10)
            //{
            //    token.Suspend();
            //}
            //else
            //{
            //    token.Release();
            //}
        }
    }
    public static async Task DoProcessAsync(string fileName, SuspensionToken token)
    {
        var pathToDirectory = Directory.GetCurrentDirectory();
        var fullPath = $"{pathToDirectory}\\{fileName}.txt";
        if (!File.Exists(fullPath))
        {
            var fs = File.Create(fullPath);
            fs.Close();
        }

        await using var sw = new StreamWriter(fullPath);
        foreach (var i in Fibonacci)
        {
            if(token.Suspended)
                mut.ReleaseMutex();
            if (mut.WaitOne())
            {
                await sw.WriteAsync(i.ToString());

                //Console.WriteLine(i);
            }
            else
            {
                Console.WriteLine("not get Mutex");
            }
        }
    }

    public static void DoProcess(string fileName, SuspensionToken token)
    {
        var pathToDirectory = Directory.GetCurrentDirectory();
        //var fullPath = $"{pathToDirectory}\\{fileName}.txt";
        //if (!File.Exists(fullPath))
        //{
        //    var fs = File.Create(fullPath);
        //    fs.Close();
        //}

        //using var sw = new StreamWriter(fullPath);
        foreach (var i in Fibonacci)
        {
            //mut.WaitOne();
            //if (token.Suspended)
            //{
            //    mut.ReleaseMutex();
            //    token.WorkThreadTaken = true;
            //    Thread.Sleep(1000);
            //    Console.WriteLine("mutex was released by workThread");
            //}
            //else
            //sw.Write(i.ToString());

                //Console.WriteLine(i);
        }
    }
}