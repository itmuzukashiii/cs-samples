using System;
using System.Threading.Tasks;

namespace TaskAwaitResult;

class Program
{
    static void Main(string[] args)
    {
        //--- result は long 型
        var result = DoSomethingAsync().Result;
        Console.WriteLine(result);
    }
  
    private static async Task<long> DoSomethingAsync() {
        var result = await Task.Run(() => {
            long sum = 0;
            for (int i = 1; i <= 10000000; i++) {
                sum += i;
            }
            return sum;
        });
        //--- Task<long> 型非同期メソッドの return 式では long 型を返す
        return result;
    }
}
