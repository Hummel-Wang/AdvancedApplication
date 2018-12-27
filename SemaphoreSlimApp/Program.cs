using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlimApp
{
    class Program
    {
        static SemaphoreSlim semaphore = new SemaphoreSlim(4);
        static void AccessDatabase(string name, int seconds)
        {
            Console.WriteLine($"{name} waits to access a database");
            semaphore.Wait();
            Console.WriteLine($"{name} was granted an access to a database");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine($"{name} is completed");
            semaphore.Release();
        }

        static void Main(string[] args)
        {
            for (int i = 1; i <= 6; i++)
            {
                string threadName = "Thread" + i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(() => AccessDatabase(threadName, secondsToWait));
                t.Start();
            }
           // Console.ReadLine();
        }
    }
}
