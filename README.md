# AdvancedApplication
Advanced application on C#

目的：具体业务场景下的处理方式学习。

ITC【inter-thread communication】线程间通信

a.	AtomManipulation【原子操作】，在一个对象上执行基本的原子操作并能阻止竞争条件（race condition）的发生。

b.	Mutex构造，即进程间的同步。

c.	SemaphoreSlim构造，限制同时访问资源的线程数量。

d.	AutoResetEvent构造一次只能通知一个等待线程，通知后自动关闭; 

e.	ManualResetEvent构造一次可通知很多个等待的线程，但要关闭需要调用Reset方法手动关闭。

f.	CountdownEvent构造，等待发送一定数量的通知后，才继续执行被阻塞的线程。

g.	Barrier构造可以帮助我们控制多个等待线程达到指定数量后，才发送通知信号，然后所有等待线程才能继续执行，并且在每次等待线程达到指定数量后，还能执行一个回调方法。

h.	ReaderWriterLockSlim构造，线程安全地使用多线程读写集合中的数据。

i.	SpinWait构造，该构造是一种混合同步构造，主要用于设计在用户模式中等待一段时间后，然后将其切换到内核模式，以节省CUP时间。
