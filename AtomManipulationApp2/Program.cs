using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AtomManipulationApp2
{
    class Program
    {
        //全局变量
        private static int _result;

        static void Main(string[] args)
        {
            while (true)
            {
                Task[] _tasks = new Task[100];
                int i = 0;

                for (i = 0; i < _tasks.Length; i++)
                {
                    _tasks[i] = Task.Factory.StartNew((num) =>
                   {
                       var taskid = (int)num;
                       Work(taskid);
                   }, i);
                }

                Task.WaitAll(_tasks);
                Console.WriteLine(_result);

                Console.ReadKey();
            }
        }

        //线程调用方法
        private static void Work(int TaskID)
        {
            for (int i = 0; i < 10; i++)
            {
                _result++;
                //Interlocked.Increment(ref _result);
            }
        }
    }
}