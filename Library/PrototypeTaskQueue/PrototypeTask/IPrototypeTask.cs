using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Library.PrototypeTaskQueue.PrototypeTask
{
    public interface IPrototypeTask
    {
        void Execute(AutoResetEvent iterationWaitHandle, CurrentProcessMonitor currentProcessMonitor);
    }
}
