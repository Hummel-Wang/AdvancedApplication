# AdvancedApplication
Advanced application on C#

目的：具体业务场景下的处理方式学习。

ITC【inter-thread communication】线程间通信,AutoResetEvent一次只能通知一个等待线程，通知后自动关闭; 而ManualResetEvent一次可通知很多个等待的线程，但要关闭需要调用Reset方法手动关闭。
