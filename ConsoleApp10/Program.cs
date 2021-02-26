using Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public class App
{
    public static Dictionary<string, AutoResetEvent> WaitHandles;
    public static void Main()
    {
        WaitHandles = new Dictionary<string, AutoResetEvent>();
        var monitor = new CurrentProcessMonitor();
        monitor.Dispose();
        monitor = new CurrentProcessMonitor();
        var k = Process.GetCurrentProcess().ProcessName;
        monitor.Dispose();
        Console.WriteLine($"Process {k} start");
        for(var z = 0; z < 5; z++)
        {
            Task.Run(() => ExecuteThreads(monitor, z)).Wait();
        }
    }

    public static Task ExecuteThreads(CurrentProcessMonitor monitor, int z)
    {
        var threads = CreateThreads(monitor, z);
        threads.ForEach(x => x.Start());
        Thread.Sleep(1000);
        WaitHandle.WaitAll(WaitHandles.Select(x => x.Value).ToArray());
        threads.ForEach(x => WaitHandles[x.Name].Dispose());
        return Task.CompletedTask;
    }

    private static List<Thread> CreateThreads(CurrentProcessMonitor monitor, int groupNumber = 0)
    {
        var result = new List<Thread>();
        for (var i = 0; i < 4; i++)
        {
            var workThread = new Thread(() => DoProcess(monitor));
            
            workThread.Name = groupNumber.ToString() + "_" + i.ToString() + "_Thread";
            WaitHandles[workThread.Name] = new AutoResetEvent(true);
            result.Add(workThread);
        }

        return result;
    }

    public static void DoProcess(CurrentProcessMonitor monitor)
    {
        var threadName = Thread.CurrentThread.Name;
        var exist = WaitHandles.TryGetValue(threadName, out var waitHandle);
        waitHandle?.WaitOne();
        Console.WriteLine($"Thread {threadName} start");
        monitor.Wait();
        Console.WriteLine($"Thread {threadName} take semaphore");
        var count = 0;
        foreach (var i in Fibonacci)
        {
            Console.WriteLine(i);
            if(count>10)
                break;
            count++;
        }
        Console.WriteLine($"Thread {threadName} released semaphore");
        waitHandle?.Set();
        monitor.Release();
    }

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
}