using System;

namespace StaticTest;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(StaticTest.Name);
        StaticTest.Name = "Mary";
        Console.WriteLine(StaticTest.Name);
        // StaticTest.Age = 10; // 'StaticTest.Age' は読み取り専用であるため、割り当てることはできません
    }
}

class StaticTest
{
    public static string Name = "John";
    public static int Age {get;} = 29;
}

/*
static メンバはインスタンス化なしに参照できる
*/