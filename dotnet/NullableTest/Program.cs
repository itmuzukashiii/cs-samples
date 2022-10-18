using System;
//--- dotnet add package MSTest.TestFramework
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NullableTest;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        NullNameShouldThrowTest();
    }

    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    public static void NullNameShouldThrowTest()
    {
        //--- ! 無しでは "null リテラルを null 非許容参照型に変換できません。 [NullableTest]csharp(CS8625)" と出る
        var person = new Person(null!);
    }
}

public class Person
{
    public Person(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

    public string Name { get; }
}

