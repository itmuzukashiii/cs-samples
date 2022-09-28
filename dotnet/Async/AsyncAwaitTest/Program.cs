// https://tech-lab.sios.jp/archives/15711 の検証

using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitTest;

class Program
{
    //--- Main を async Task にする意味は無いのでは…？
    // static async Task Main(string[] args)
    static void Main(string[] args)
    {   
        Task<string> task = HeavyMethod1();
        HeavyMethod2();

        Console.WriteLine(task.Result);

        // Console.ReadLine();
    }   

    static async Task<string> HeavyMethod1()
    {   
        Console.WriteLine("すごく重い処理その1(´・ω・｀)はじまり"); 
        await Task.Delay(5000);
        Console.WriteLine("すごく重い処理その1(´・ω・｀)おわり"); 
        return "hoge";
    }   

    static void HeavyMethod2()
    {   
        Console.WriteLine("すごく重い処理その2(´・ω・｀)はじまり"); 
        Thread.Sleep(6000);
        Console.WriteLine("すごく重い処理その2(´・ω・｀)おわり"); 
    }   
}
