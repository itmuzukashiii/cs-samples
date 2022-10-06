using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncMainTest;

class Program
{
    static async Task Main(string[] args)
    {
        var hc = new HttpClient();
        var urls = new string[] {
            @"https://example.com",
            @"https://www.yahoo.co.jp",
            @"https://www.google.com"
        };
        var tasks = urls.Select(url => hc.GetAsync(url)).ToArray();
        Console.WriteLine("tasks started.");

        // Task.WaitAll(tasks);    // void Main のとき
        await Task.WhenAll(tasks); // async Task Main とき

        Console.WriteLine(String.Join("\r\n", tasks.Select(task => $"{task.Result.RequestMessage.ToString()}: {task.Result.StatusCode.ToString()}")));
    }
}

/*
tasks started.
Method: GET, RequestUri: 'https://example.com/', Version: 1.1, Content: <null>, Headers:
{
}: OK
Method: GET, RequestUri: 'https://www.yahoo.co.jp/', Version: 1.1, Content: <null>, Headers:
{
}: OK
Method: GET, RequestUri: 'https://www.google.com/', Version: 1.1, Content: <null>, Headers:
{
}: OK
*/