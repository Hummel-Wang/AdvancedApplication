using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEventApp
{
    class Program
    {
        #region 通知单一线程
        //若要将初始状态设置为终止，则为 true；若要将初始状态设置为非终止，则为 false
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() =>
            {
                {
                    Console.WriteLine("客户1在排队等待付钱...");

                    //客户1调用AutoResetEvent上的WaitOne来等待付钱通知
                    autoResetEvent.WaitOne();
                    Console.WriteLine("通知来了，客户1付钱");
                }
            });
            t1.IsBackground = true;
            t1.Start();

            Pay();//发送通知 
            Console.ReadKey();
        }
        static void Pay()
        {
            string tip = Console.ReadLine();
            if (tip == "next")
            {
                autoResetEvent.Set();//收银员发送付钱通知，通过调用Set来通知客户付钱
            }
        }
        #endregion

        #region 通知多个线程
        ////若要将初始状态设置为终止，则为 true；若要将初始状态设置为非终止，则为 false
        //static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        //static void Main(string[] args)
        //{
        //    Thread t1 = new Thread(() =>
        //    {
        //        {
        //            Console.WriteLine("客户1在排队等待付钱...");

        //            //客户1调用AutoResetEvent上的WaitOne来等待付钱通知
        //            autoResetEvent.WaitOne();
        //            Console.WriteLine("通知来了，客户1付钱");
        //            autoResetEvent.Set(); //AutoResetEvent.Set()只能每次通知一个线程，此处若不set便只会执行t1
        //        }
        //    });
        //    t1.IsBackground = true;
        //    t1.Start();

        //    Thread t2 = new Thread(() =>
        //    {
        //        {
        //            Console.WriteLine("客户2在排队等待付钱...");

        //            //客户2调用AutoResetEvent上的WaitOne来等待付钱通知
        //            autoResetEvent.WaitOne();
        //            Console.WriteLine("通知来了，客户2付钱");
        //        }
        //    });
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
        //        autoResetEvent.Set();//收银员发送付钱通知，通过调用Set来通知客户付钱
        //    }
        //}
        #endregion
    }
}
