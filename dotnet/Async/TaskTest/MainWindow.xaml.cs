using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_1_Click(object sender, EventArgs e) {
            this.label_1.Content = @"";
            await DoSomething();
            this.label_1.Content = @"完了";
        }

        private async Task DoSomething() {
            for(var k = 10; k > 0; k--) {
                this.label_1.Content = k;
                await Task.Delay(1000);
            }
        }
    }
}
