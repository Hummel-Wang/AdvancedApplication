using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEventApp
{
    /// <summary>
    /// 在AutoResetEvent中，如果要通知两个线程，则Set方法要被执行两次，也以快餐店的例子做了举例，
    /// 但如果有一天客户1中彩票了，要请部门的10个同事吃饭，也就是说只要Set一次，所有在等待的线程都会解除等待，
    /// 相当于收银员只收一次钱，10个人都可以通过收银去吃饭，这时我们就要用到ManualResetEvent类，
    /// 它的用法和AutoResetEvent基本一样，区别就在于它是一单Set方法发出通知后，要再次阻塞的话就需要手动去修改，
    /// 也就是调用Reset方法
    /// </summary>
    class Program
    {
        #region 线程间通信不中断
        ////若要将初始状态设置为终止，则为 true；若要将初始状态设置为非终止，则为 false。
        //static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        //static void Main(string[] args)
        //{
        //    Thread t1 = new Thread(() =>
        //       {
        //           Console.WriteLine("客户1在排队等待付钱...");

        //           //客户1调用manualResetEvent上的WaitOne来等待付钱通知
        //           manualResetEvent.WaitOne();
        //           Console.WriteLine("已经有人请客，客户1不用付钱");
        //       });
        //    t1.IsBackground = true;
        //    t1.Start();

        //    Thread t2 = new Thread(() =>
        //     {
        //         Console.WriteLine("客户2在排队等待付钱...");

        //         //客户2调用manualResetEvent上的WaitOne来等待付钱通知
        //         manualResetEvent.WaitOne();
        //         Console.WriteLine("已经有人请客，客户2不用付钱！");
        //     });
        //    t2.IsBackground = true;
        //    t2.Start();

        //    Pay();//发送通知

        //    Console.ReadKey();
        //}

        //static void Pay()
        //{
        //    string tip = Console.ReadLine();
        //    if (tip == "next")
        //    {
        //        manualResetEvent.Set();//收银员发送付钱通知，通过调用Set来通知客户付钱
        //    }
        //}
        #endregion


        #region 线程间通信需要中断
        //若要将初始状态设置为终止，则为 true；若要将初始状态设置为非终止，则为 false。
        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() =>
            {
                Console.WriteLine("客户1在排队等待付钱...");

                //客户1调用manualResetEvent上的WaitOne来等待付钱通知
                manualResetEvent.WaitOne();
                Console.WriteLine("已经有人请客，客户1不用付钱");
            });
            t1.IsBackground = true;
            t1.Start();

            Thread t2 = new Thread(() =>
            {
                Console.WriteLine("客户2在排队等待付钱...");
                Thread.Sleep(8000);//客户2发呆了8秒，这时收银员已经下班，要继续等待
                //客户2调用manualResetEvent上的WaitOne来等待付钱通知
                manualResetEvent.WaitOne();
                Console.WriteLine("已经有人请客，客户2不用付钱！");
            });
            t2.IsBackground = true;
            t2.Start();

            Pay();//发送通知
            Timer timer = new Timer(StopPay, null, 0, 5000);//5秒钟后收银员下班了，线程要重新等待了
            Console.ReadKey();
        }

        static void Pay()
        {
            string tip = Console.ReadLine();
            if (tip == "next")
            {
                manualResetEvent.Set();//收银员发送付钱通知，通过调用Set来通知客户付钱
            }
        }

        static void StopPay(object s)
        {
            manualResetEvent.Reset();
            Console.WriteLine("收银员下班, 后面的客户要继续等待");
        }
        #endregion
    }
}
