// Windows 10 の .NET Framework 4 付属の csc.exe (C# 5.0) で WPF アプリを作る

/*
参考: https://ufcpp.net/study/csharp/st_basis.html
*/

using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfWelcome {
    public class Program {

        [STAThread]
        static void Main() {
            var button = new Button { Content = "Push Here" };
            button.Click += (sender, e) => MessageBox.Show("Welcome!");
      
            var win = new Window {
                Title = "Wpf Welcome"
                ,Width = 300
                ,Height = 200
                ,Content = button
            };
        
            var app = new Application();
            app.Run(win);
        }
    }
}
