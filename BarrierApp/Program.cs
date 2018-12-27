using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierApp
{
    class Program
    {
        static Barrier barrier = new Barrier(2, b => Console.WriteLine($"End of phase {b.CurrentPhaseNumber + 1}"));
        static void PlayMusic(string name, string message, int seconds)
        {
            for (int i = 1; i < 3; i++)
            {
                Console.WriteLine("----------------------------------------------");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine($"{name} starts to {message}");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine($"{name} finishes to {message}");
                barrier.SignalAndWait();
            }
        }

        static void Main(string[] args)
        {
            var t1 = new Thread(() => PlayMusic("the guitarist", "play an amazing solo", 8));
            var t2 = new Thread(() => PlayMusic("the singer", "sing his song", 2));
            t1.Start();
            t2.Start();


        }
    }
}
