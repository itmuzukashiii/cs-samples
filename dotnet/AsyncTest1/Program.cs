// Thread を使った非同期処理のテスト

/*
DoSomething(): 1秒間の Sleep を5回繰り返した後 var1 = 1 をセットする
Method1(): DoSomething をサブスレッドで開始した後，var1 != 1 である間 0.5 秒ごとに var1 を表示する
*/

/*
実行例: DoSomethin() と while ループが並行して実行されており，開始から約5秒後に while ループが break していることがわかる

2022-09-14 15:35:20.835 Start
2022-09-14 15:35:20.858 var1 = -1
2022-09-14 15:35:21.372 var1 = -1
Sleep 1000
2022-09-14 15:35:21.887 var1 = -1
2022-09-14 15:35:22.402 var1 = -1
Sleep 1000
2022-09-14 15:35:22.907 var1 = -1
2022-09-14 15:35:23.408 var1 = -1
Sleep 1000
2022-09-14 15:35:23.920 var1 = -1
2022-09-14 15:35:24.432 var1 = -1
Sleep 1000
2022-09-14 15:35:24.936 var1 = -1
2022-09-14 15:35:25.438 var1 = -1
Sleep 1000
2022-09-14 15:35:25.952 End
*/

using System;
using System.Threading;

namespace AsyncTest1;

class Program
{
    private static int var1 = -1;
    static void Main(string[] args)
    {
        Method1();
    }
  
    static void Method1()
    {
        var th = new Thread(DoSomething);
        Console.WriteLine($"{DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss.fff")} Start");
        th.Start();
        do {
            Console.WriteLine($"{DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss.fff")} var1 = {var1.ToString()}");
            Thread.Sleep(500);
        } while (var1 != 1);
        Console.WriteLine($"{DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss.fff")} End");
    }

    static void DoSomething()
    {
        DoLongTimeWork();
        var1 = 1;
    }

    static void DoLongTimeWork()
    {
        for(var k = 0; k < 5; k++) {
            Thread.Sleep(1000);
            Console.WriteLine($"Sleep 1000");
        }
    }
}
