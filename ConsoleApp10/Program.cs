using Fs.Processes.JobObjects;
using Library.PrototypeTaskQueue;
using Library.PrototypeTaskQueue.PrototypeTask;
using FxResources.System;
using Library.JobObject;
using Newtonsoft.Json.Linq;

public class App
{
    public static void Main()
    {
        var queue = new PrototypeTaskQueue(new PrototypeFibonacciTask(), 8, 50);
        queue.Run();
    }
}