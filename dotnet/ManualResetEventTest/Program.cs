// http://okwakatta.net/topic/topic026.html

/*
ManualResetEvent:

Represents a thread synchronization event that, when signaled, must be reset manually.
This class cannot be inherited.
*/

/* 実行結果
スレッドを開始します
スレッドを開始します
スレッドを開始します
スレッドを開始します
 シグナル状態になるのを待ちます [Thread: 5]
 シグナル状態になるのを待ちます [Thread: 6]
 シグナル状態になるのを待ちます [Thread: 4]
 シグナル状態になるのを待ちます [Thread: 7]
シグナル状態にします
 シグナル状態になりました [Thread: 5]
 シグナル状態になりました [Thread: 6]
 シグナル状態になりました [Thread: 4]
 シグナル状態になりました [Thread: 7]
*/

/*
確かに1回シグナルを true にすると停止していた全スレッドが動く
*/

using System;
using System.Threading;


namespace ManualResetEventTest;

class Program
{
    static void Main(string[] args)
    {
        var p = new TestClass();
        p.Button1_Click();
        p.Button1_Click();
        p.Button1_Click();
        p.Button1_Click();
        Thread.Sleep(1000);
        p.Button2_Click();
    }
}

class TestClass
{
    private readonly ManualResetEvent manualResetEvent = new ManualResetEvent(false);

    public void Button1_Click()
    {
        Console.WriteLine("スレッドを開始します");
        var t = new Thread(doWork);
        t.Start();
    }

    public void Button2_Click()
    {
        Console.WriteLine("シグナル状態にします");
        manualResetEvent.Set();
    }

    private void doWork()
    {
        Console.WriteLine($" シグナル状態になるのを待ちます [Thread: {Thread.CurrentThread.ManagedThreadId}]");
        manualResetEvent.WaitOne();
        Console.WriteLine($" シグナル状態になりました [Thread: {Thread.CurrentThread.ManagedThreadId}]");
    }
}
