// 非同期処理を学ぶにあたり，Thread.Sleep の動作確認

/*
実行結果例．指定時間 sleep していることが確認できた．

2022/09/14 15:31:14 Start
2022/09/14 15:31:19 End
*/

using System;
using System.Threading;

namespace AsyncTest;

class Program
{
    static void Main(string[] args)
    {
        Method1();
    }
  
    static void Method1()
    {
        Console.WriteLine($"{DateTime.Now.ToString()} Start");
        DoLongTimeWork();
        Console.WriteLine($"{DateTime.Now.ToString()} End");
    }

    static void DoLongTimeWork()
    {
        Thread.Sleep(5000);
    }
}

