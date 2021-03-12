using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Library
{
    public class ProcessesSystemMonitor : IDisposable

    {
        public Dictionary<string, PerformanceCounter> Counters { get; }
        public Dictionary<string, int> ThreadsCount { get; }

        private readonly Dictionary<string, string> _semaphoreNames;
        private readonly Dictionary<string, Semaphore> _semaphores;

        public ProcessesSystemMonitor()
        {
            _semaphores = new Dictionary<string, Semaphore>();
            Counters = new Dictionary<string, PerformanceCounter>();
            _semaphoreNames = new Dictionary<string, string>();
            ThreadsCount = new Dictionary<string, int>();

        }

        public bool HasProcess(string processName)
        {
            return Counters.ContainsKey(processName);
        }

        public void AddProcess(string processName, int threadsCount)
        {
            Counters[processName] = new PerformanceCounter("Process", "% Processor Time", processName);
            //var semaphoreName = processName + nameof(Semaphore);
            //var semaphore = new Semaphore(4, threadsCount, semaphoreName);
            //_semaphores[processName] = semaphore;
            //_semaphoreNames[processName] = semaphoreName;
            //ThreadsCount[processName] = threadsCount;
        }

        public float GetProcessCpuUsage(string processName)
        {
            return Counters[processName].NextValue() / Environment.ProcessorCount;
        }

        public void SuspendProcess(string processName)
        {
            var exist = _semaphores.TryGetValue(processName, out var semaphore);
            if (!exist) return;
            for (var i = 0; i < ThreadsCount[processName]; i++)
                semaphore.WaitOne();
        }

        public void ReleaseProcess(string processName)
        {
            var exist = _semaphores.TryGetValue(processName, out var semaphore);
            if(!exist) return;
            if(semaphore.WaitOne(0))
                semaphore.Release();
        }

        public void Dispose()
        {
            foreach (var k in _semaphores)
            {
                k.Value.Dispose();
            }
        }
    }
}
