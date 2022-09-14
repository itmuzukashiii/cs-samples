// BackgroundWorker を使った非同期処理

/*
AsyncTest1 と同様の処理を BackgroundWorker を用いて書き直したもの．

実行例:

2022-09-14 16:06:09.155 Start
2022-09-14 16:06:09.178 var1 = -1
2022-09-14 16:06:09.688 var1 = -1
2022-09-14 16:06:10.190 var1 = -1
Sleep 1000 (1/5)
2022-09-14 16:06:10.701 var1 = -1
Sleep 1000 (2/5)
2022-09-14 16:06:11.217 var1 = -1
2022-09-14 16:06:11.720 var1 = -1
Sleep 1000 (3/5)
2022-09-14 16:06:12.234 var1 = -1
2022-09-14 16:06:12.751 var1 = -1
Sleep 1000 (4/5)
2022-09-14 16:06:13.264 var1 = -1
2022-09-14 16:06:13.781 var1 = -1
Sleep 1000 (5/5)
2022-09-14 16:06:14.287 var1 = 1
2022-09-14 16:06:14.288 End
*/

using System;
using System.Threading;
using System.ComponentModel;

namespace AsyncTest2;

class Program
{
    private int var1 = -1;
    private BackgroundWorker _worker = new BackgroundWorker();

    static void Main(string[] args)
    {
        (new Program()).Method1();
    }
  
    private void Method1()
    {
        //--- DoWork イベントハンドラ．サブスレッドに実行させたいメソッド
        _worker.DoWork += _worker_DoWork;
        //--- RunWorkerCompleted イベントハンドラ．DoWork 完了時に呼ばれるメソッド
        _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;

        Console.WriteLine($"{DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss.fff")} Start");

        //--- DoWork イベントハンドラを非同期で起動
        _worker.RunWorkerAsync();

        do {
            Console.WriteLine($"{DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss.fff")} var1 = {var1.ToString()}");
            Thread.Sleep(500);
        } while (var1 != 1);

        Console.WriteLine($"{DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss.fff")} var1 = {var1.ToString()}");
        Console.WriteLine($"{DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss.fff")} End");
    }

    private void _worker_DoWork(object sender, DoWorkEventArgs e)
    {
        DoLongTimeWork();
    }

    private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        this.var1 = 1;
    }

    private static void DoLongTimeWork()
    {
        for(var k = 1; k <= 5; k++) {
            Thread.Sleep(1000);
            Console.WriteLine($"Sleep 1000 ({k}/5)");
        }
    }
}
