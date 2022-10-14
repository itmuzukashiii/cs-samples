// https://tech-lab.sios.jp/archives/15637 の検証

using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWhenAllTest;

class Program
{
    static void Main(string[] args)
    {
        // 1の整数を返すHeavyMethod1のTaskを生成します。
        Task<int> task1 = Task.Run(() => {
            return HeavyMethod1();
        });

        // 2の整数を返すHeavyMethod2のTaskを生成します。
        Task<int> task2 = Task.Run(() => {
            return HeavyMethod2();
        });

        //--- WhenAllの引数に先程生成したTaskを入れると，それらがすべて完了したときに完了となるタスクが新たに生成されます。
        //--- それを .Wait() することで，HeavyMethod1，HeavyMethod2 が共に完了するまで待機させることができます．
        Task.WhenAll(task1,task2).Wait();

        Console.WriteLine(@"ここが先に動く？");

        Console.WriteLine(task1.Result + task2.Result);

        // Console.ReadLine();
    }

    static int HeavyMethod1()
    {
        Console.WriteLine("すごく重い処理その1(´・ω・｀)はじまり");
        Thread.Sleep(5000);
        Console.WriteLine("すごく重い処理その1(´・ω・｀)おわり");

        return 1;
    }

    static int HeavyMethod2()
    {
        Console.WriteLine("すごく重い処理その2(´・ω・｀)はじまり");
        Thread.Sleep(3000);
        Console.WriteLine("すごく重い処理その2(´・ω・｀)おわり");

        return 2;
    }
}
