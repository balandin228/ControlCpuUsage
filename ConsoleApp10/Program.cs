using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class App
{
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
        var fileName = Process.GetCurrentProcess().ProcessName + DateTime.Now.GetHashCode();
        Task.Run(() => DoProcess(fileName)).Wait();
    }

    public static async Task DoProcess(string fileName)
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
            await sw.WriteAsync(i.ToString());

            Console.WriteLine(i);
        }
    }
}