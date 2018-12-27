using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpinWaitApp
{
    class Program
    {
        static volatile bool isCompleted = false;
        static void UserModeWait()
        {
            while (!isCompleted)
            {
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine("Waiting is complete");
        }

        static void HybridSpinWait()
        {
            // 提供对基于自旋的等待的支持
            var w = new SpinWait();
            while (!isCompleted)
            {
                // 执行单一自旋
                w.SpinOnce();
                // 获取对 System.Threading.SpinWait.SpinOnce 的下一次调用是否将产生处理器，同时触发强制上下文切换
                Console.WriteLine(w.NextSpinWillYield);
            }
            Console.WriteLine("Waiting is complete");

            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            var t1 = new Thread(UserModeWait);
            var t2 = new Thread(HybridSpinWait);
            Console.WriteLine("Running user mode waiting");
            t1.Start();
            Thread.Sleep(20);
            isCompleted = true;
            Thread.Sleep(TimeSpan.FromSeconds(1));
            isCompleted = false;
            Console.WriteLine("Running hybrid SpinWait construct waiting");
            t2.Start();
            Thread.Sleep(5);
            isCompleted = true;
        }
    }
}
