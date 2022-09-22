using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JsonSerializerTest2;
class Program
{
    static void Main(string[] args)
    {
        var studentList = GetStudentList();
        //--- インデント付きでシリアライズするためのオプション
        var opt1 = new JsonSerializerOptions(){ WriteIndented = true };

        var str         = JsonSerializer.Serialize<Student[]>(studentList.ToArray());
        var strIndented = JsonSerializer.Serialize<Student[]>(studentList.ToArray(), opt1);
        
        //--- インデント無しで出力
        using (var fileStream = File.Create(@"StudentInfo.json"))
        using (var writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
        {
            writer.WriteLine(str);
        }

        //--- インデント付きで出力
        using (var fileStream = File.Create(@"StudentInfoIndented.json"))
        using (var writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
        {
            writer.WriteLine(strIndented);
        }

    }

    private static ReadOnlyCollection<Student> GetStudentList()
    {
        var list = new List<Student>();

        list.Add(new Student(){Name = "坂本", ID = "A002", Age = 12});
        list.Add(new Student(){Name = "John", ID = "B013", Age = 15});
        list.Add(new Student(){Name = "Lucy", ID = "C024", Age = 13});

        return new ReadOnlyCollection<Student>(list);
    }

    private class Student
    {
        public string Name {get;set;}
        public string ID   {get;set;}
        public int    Age  {get;set;}
    }
}
