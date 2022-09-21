using System;
using System.Text.Json;

namespace JsonSerializerTest;

class Program
{
    static void Main(string[] args)
    {
        Method1(args[0]);
    }

    private static void Method1(string jsonPath)
    {
        var jsonText = System.IO.File.ReadAllText(jsonPath);
        var students = JsonSerializer.Deserialize<JsonSchemaStudent[]>(jsonText);
        foreach(var s in students)
        {
            Console.WriteLine($"{s.Name}, {s.ID}, {s.Age}");
        }

    }

    private class JsonSchemaStudent
    {
        public string Name {get;set;}
        public string ID {get;set;}
        public int Age {get;set;}
    }
}

/*
出力:

Sakamoto, A002, 12
John, B013, 15
Lucy, C024, 13
*/