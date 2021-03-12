using Library.PrototypeTaskQueue;
using Library.PrototypeTaskQueue.PrototypeTask;

public class App
{
    public static void Main()
    {
        var queue = new PrototypeTaskQueue(new PrototypeFibonacciTask(), 64, 50);
        queue.Run();
    }
}