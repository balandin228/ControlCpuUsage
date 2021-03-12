using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Library.PrototypeTaskQueue.PrototypeTask
{
    public class PrototypeFibonacciTask : IPrototypeTask
    {
        private bool toStop { get; set; }
        public PrototypeFibonacciTask()
        {
            toStop = false;
        }

        public void Execute(AutoResetEvent iterationWaitHandle, CurrentProcessMonitor currentProcessMonitor)
        {
            var threadName = Thread.CurrentThread.Name;
            //iterationWaitHandle?.WaitOne();
            Console.WriteLine($"Thread {threadName} start");
            //currentProcessMonitor.Wait();
            //Console.WriteLine($"Thread {threadName} take semaphore");
            while (true)
            {

            }
            //foreach (var i in Fibonacci)
            //{
            //Console.Write(currentProcessMonitor.GetCpuUsage());
            //Console.Write("\r");
            //Console.Write("                 ");
            //Console.Write("\r");
            //    if(count>400)
            //        break;
            //    count++;
            //}
            //Console.WriteLine($"Thread {threadName} released semaphore");
            //iterationWaitHandle?.Set();
            //currentProcessMonitor.Release();
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
}
