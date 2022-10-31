using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndSemaphoreSlimTest;

class Program
{
    static void Main(string[] args)
    {
        //--- 同時実行数を 5 に制限
        var semaphore = new SemaphoreSlim(5,5);

        //--- 10 タスクを並行処理
        var taskList = Enumerable.Range(0,10).Select(
            async x => {
                //--- Semaphore 獲得待ち
                await semaphore.WaitAsync();

                var task = await HeavyProcess(x);

                //--- Semaphore 解放
                semaphore.Release();

                //--- HeavyProcess の実行結果を返却 (Task 型)
                return task;
            }
        ).ToArray();

        var taskResult = Task.WhenAll(taskList).Result;

        //--- .Select に与えるデリゲートの第２引数は要素インデックスになる
        taskResult.Select((result, index) => new {i = index, r = result}).ToList().ForEach(
            x => Console.WriteLine($"[ {x.i} ] Start: {x.r[@"startTime"]} End: {x.r[@"endTime"]}")
        );

        /*
            //--- LINQ を使わない方法
            for(var k = 0; k < taskResult.Count(); k++)
            {
                var r = taskResult[k];
                Console.WriteLine($"[ {k} ] Start: {r[@"startTime"]} End: {r[@"endTime"]}");
            }
        */
    }

    static async Task<Dictionary<string,string>> HeavyProcess(int pid)
    {
        int waitSecond = pid;

        var startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine($"[ {pid} ] {startTime} (Sleep {waitSecond}) Process Start ");

        await Task.Delay(waitSecond * 1000);

        var endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine($"[ {pid} ] {endTime} (Sleep {waitSecond}) Process End ");

        return new Dictionary<string,string>()
        {
            { @"startTime", startTime },
            { @"endTime",   endTime }
        };
    }
}

/*
> dotnet run
[ 0 ] 2022-10-31 17:06:18 (Sleep 0) Process Start 
[ 0 ] 2022-10-31 17:06:18 (Sleep 0) Process End   
[ 1 ] 2022-10-31 17:06:18 (Sleep 1) Process Start 
[ 2 ] 2022-10-31 17:06:18 (Sleep 2) Process Start 
[ 3 ] 2022-10-31 17:06:18 (Sleep 3) Process Start 
[ 4 ] 2022-10-31 17:06:18 (Sleep 4) Process Start 
[ 5 ] 2022-10-31 17:06:18 (Sleep 5) Process Start

ここまでは一気にタスク開始．以降は一つ完了するごとに追加でタスク開始

[ 1 ] 2022-10-31 17:06:19 (Sleep 1) Process End 
[ 6 ] 2022-10-31 17:06:19 (Sleep 6) Process Start 
[ 2 ] 2022-10-31 17:06:20 (Sleep 2) Process End 
[ 7 ] 2022-10-31 17:06:20 (Sleep 7) Process Start 
[ 3 ] 2022-10-31 17:06:21 (Sleep 3) Process End 
[ 8 ] 2022-10-31 17:06:21 (Sleep 8) Process Start 
[ 4 ] 2022-10-31 17:06:22 (Sleep 4) Process End 
[ 9 ] 2022-10-31 17:06:22 (Sleep 9) Process Start 

ここですべてのタスクが開始された

[ 5 ] 2022-10-31 17:06:23 (Sleep 5) Process End 
[ 6 ] 2022-10-31 17:06:25 (Sleep 6) Process End
[ 7 ] 2022-10-31 17:06:27 (Sleep 7) Process End
[ 8 ] 2022-10-31 17:06:29 (Sleep 8) Process End
[ 9 ] 2022-10-31 17:06:31 (Sleep 9) Process End

全タスクが完了したら，最後に結果をまとめて表示

[ 0 ] Start: 2022-10-31 17:06:18 End: 2022-10-31 17:06:18
[ 1 ] Start: 2022-10-31 17:06:18 End: 2022-10-31 17:06:19
[ 2 ] Start: 2022-10-31 17:06:18 End: 2022-10-31 17:06:20
[ 3 ] Start: 2022-10-31 17:06:18 End: 2022-10-31 17:06:21
[ 4 ] Start: 2022-10-31 17:06:18 End: 2022-10-31 17:06:22
[ 5 ] Start: 2022-10-31 17:06:18 End: 2022-10-31 17:06:23
[ 6 ] Start: 2022-10-31 17:06:19 End: 2022-10-31 17:06:25
[ 7 ] Start: 2022-10-31 17:06:20 End: 2022-10-31 17:06:27
[ 8 ] Start: 2022-10-31 17:06:21 End: 2022-10-31 17:06:29
[ 9 ] Start: 2022-10-31 17:06:22 End: 2022-10-31 17:06:31
*/