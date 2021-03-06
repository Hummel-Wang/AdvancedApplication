﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountdownEventApp
{
    class Program
    {
        private static CountdownEvent countdown = new CountdownEvent(2);
        static void PerformOperation(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine(message);
            countdown.Signal();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Starting two operations");
            var t1 = new Thread(() => PerformOperation("Operation 1 is completed", 4));
            var t2 = new Thread(() => PerformOperation("Operation 2 is completed", 8));

            t1.Start();
            t2.Start();
            countdown.Wait();
            Console.WriteLine("Both operations have been completed.");
            countdown.Dispose();
            Console.ReadLine();
        }
    }
}
