using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Library.PrototypeTaskQueue.PrototypeTask;

namespace Library.PrototypeTaskQueue
{
    public class PrototypeTaskQueue
    {
        private readonly  int _oneIterationThreads;
        private readonly int _iterationCount;
        private readonly CurrentProcessMonitor _currentProcessMonitor;
        private readonly IPrototypeTask _task;

        public readonly Dictionary<string, AutoResetEvent> WaitHandles;

        public PrototypeTaskQueue(IPrototypeTask task, int oneIterationThreads, int iterationCount)
        {
            WaitHandles = new Dictionary<string, AutoResetEvent>();
            _currentProcessMonitor = new CurrentProcessMonitor();
            _iterationCount = iterationCount;
            _oneIterationThreads = oneIterationThreads;
            _task = task;
        }

        public void Run()
        { 
            Task.Run(() => ExecuteIterationThreads(0)).Wait();
        }

        private Task ExecuteIterationThreads(int groupNumber)
        {
            var threads = CreateThreads(_currentProcessMonitor,_oneIterationThreads, groupNumber );
            foreach (var thread in threads)
            {
                thread.Start();
                Console.WriteLine(thread.Name);
            }
            return Task.CompletedTask;
        }

        private IEnumerable<Thread> CreateThreads(CurrentProcessMonitor monitor,int threadsCount,int groupNumber)
        {
            var result = new List<Thread>();
            for (var i = 0; i < threadsCount; i++)
            {
                var workThread = new Thread(() => _task.Execute(null, monitor));
                workThread.Name = groupNumber.ToString() + "_" + i.ToString() + "_Thread";
                yield return workThread;
                if(i%4 == 0 )
                    Thread.Sleep(20000);
            }

        }
    }
}
