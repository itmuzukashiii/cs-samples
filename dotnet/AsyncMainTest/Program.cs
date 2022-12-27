using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncMainTest;

class Program
{
    static async Task Main(string[] args)
    {
        //--- HTTP Client Handler の初期化と WinINET プロキシの読み込み
        var ch = new HttpClientHandler()
        {
            Proxy = WebRequest.GetSystemWebProxy(),
            // UseProxy = true
        };
        //--- HTTP Client の初期化
        var hc = new HttpClient(ch);

        //--- 読み込み対象 URL リスト
        var urls = new string[] {
            @"https://example.com",
            @"https://www.yahoo.co.jp",
            @"https://www.google.com"
        };

        //--- プロキシ URI のチェック＆表示
        foreach (string url in urls) {
            Console.WriteLine(ch.Proxy.GetProxy(new Uri(url))?.AbsoluteUri);
        }
        
        //--- 非同期で GET 実行
        var tasks = urls.Select(url => hc.GetAsync(url)).ToArray();
        Console.WriteLine("tasks started.");

        // Task.WaitAll(tasks);    // void Main のとき
        await Task.WhenAll(tasks); // async Task Main とき

        Console.WriteLine(
            String.Join("\r\n",
                tasks.Select(task => $"{task.Result.RequestMessage?.ToString()}: {task.Result.StatusCode}")
            )
        );
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