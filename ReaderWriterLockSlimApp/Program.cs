using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLockSlimApp
{
    class Program
    {
        // 表示用于管理资源访问的锁定状态，可实现多线程读取或进行独占式写入访问
        static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
        static Dictionary<int, int> items = new Dictionary<int, int>();

        static void Read()
        {
            Console.WriteLine("Reading contents of a dictionary");
            while(true)
            {
                try
                {
                    // 尝试进入读取模式锁定状态
                    rw.EnterReadLock();
                    foreach (var key in items.Keys)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }
                }
                finally
                {
                    // 减少读取模式的递归计数，并在生成的计数为 0（零）时退出读取模式
                    rw.ExitReadLock();
                }
            }
        }

        static void Write(string threadName)
        {
            while(true)
            {
                try
                {
                    int newKey = new Random().Next(250);
                    // 尝试进入可升级模式锁定状态
                    rw.EnterUpgradeableReadLock();
                    if (!items.ContainsKey(newKey))
                    {
                        try
                        {
                            // 尝试进入写入模式锁定状态
                            rw.EnterWriteLock();
                            items[newKey] = 1;
                            Console.WriteLine($"New key {newKey} is added to a dictionary by a {threadName}");
                        }
                        finally
                        {
                            // 减少写入模式的递归计数，并在生成的计数为 0（零）时退出写入模式
                            rw.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    // 减少可升级模式的递归计数，并在生成的计数为 0（零）时退出可升级模式
                    rw.ExitUpgradeableReadLock();
                }
            }
        }
        static void Main(string[] args)
        {
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(() => Write("Thread 1")) { IsBackground = true }.Start();
            new Thread(() => Write("Thread 2")) { IsBackground = true }.Start();
            Thread.Sleep(TimeSpan.FromSeconds(20));
        }
    }
}
