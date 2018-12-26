using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AtomManipulationApp
{

    abstract class CounterBase
    {
        public abstract void Increment();
        public abstract void Decrement();
    }

    class Counter : CounterBase
    {
        private int count;
        public int Count => count;
        public override void Increment()
        {
            count++;
        }
        public override void Decrement()
        {
            count--;
        }
    }

    class CounterNoLock : CounterBase
    {
        private int count;
        public int Count => count;
        public override void Increment()
        {
            Interlocked.Increment(ref count);
        }
        public override void Decrement()
        {
            Interlocked.Decrement(ref count);
        }
    }


    class Program
    {
        static void TestCounter(CounterBase c)
        {
            for (int i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Incorrect counter");
            var c1 = new Counter();
            var t1 = new Thread(() => TestCounter(c1));
            var t2 = new Thread(() => TestCounter(c1));
            var t3 = new Thread(() => TestCounter(c1));

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine($"Total count: {c1.Count}");
            Console.WriteLine("--------------------------");
            Console.WriteLine("Correct  counter");
            var c2 = new CounterNoLock();
            t1 = new Thread(() => TestCounter(c2));
            t2 = new Thread(() => TestCounter(c2));
            t3 = new Thread(() => TestCounter(c2));

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine($"Total count: {c2.Count}");

            Console.ReadLine();
        }
    }
}
