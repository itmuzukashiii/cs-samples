using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            // MessageBox.Show(sender.GetType().ToString());

            //--- ボタン無効化
            this.button_1.IsEnabled = false;

            //--- ラベル表記クリア
            this.label_1.Content = @"";
            //--- カウントダウン開始～終了まで待機
            await countDown();
            //--- ラベル表記 "完了"
            this.label_1.Content = @"完了";

            //--- ボタン有効化
            ((Button)sender).IsEnabled = true;
        }

        private async Task countDown() {
            for(var k = 10; k > 0; k--) {
                this.label_1.Content = k;
                await Task.Delay(1000);
            }
        }

    }
}
