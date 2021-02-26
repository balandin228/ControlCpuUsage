using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Library
{
    public class CurrentProcessMonitor : IDisposable
    {
        public bool HaveConstraint { get;private set; }
        private Semaphore Semaphore { get; set; }
        public CurrentProcessMonitor()
        {
            var processName = Process.GetCurrentProcess().ProcessName;
            var semaphoreName = processName + nameof(System.Threading.Semaphore);
            HaveConstraint = Semaphore.TryOpenExisting(semaphoreName, out var semaphore);
            if (HaveConstraint)
                Semaphore = semaphore;
        }

        public void Wait()
        {
            Semaphore?.WaitOne();
        }

        public void Release()
        {
            Semaphore?.Release();
        }

        public void Dispose()
        {
            Semaphore?.Close();
        }
    }
}
