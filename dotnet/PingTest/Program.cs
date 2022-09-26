using System;
using System.Net.NetworkInformation;

namespace PingTest;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(execPing(@"yahoo.com"));
    }

    private static bool execPing(string host)
    {
        var ping = new Ping();
        var result = ping.Send(host, 2000);

        if (result.Status == IPStatus.Success) {
            Console.WriteLine($"{result.Status.ToString()}: Reply from {result.Address}: bytes={result.Buffer.Length} time={result.RoundtripTime}ms TTL={result.Options.Ttl}");
            return true;
        } else {
            return false;
        }
    }
}
