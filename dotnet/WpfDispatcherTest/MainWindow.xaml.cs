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

namespace WpfDispatcherTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test();
        }

        // 時間のかかる処理 + TextBlock の変更を行うメソッド
        private async void Test()
        {
            await Task.Run( () =>
            {
                for (int i = 1; i <= 100; ++i)
                {
                    Task.Delay(100).Wait();
                    this.Dispatcher.Invoke(
                        (Action)(
                            () => this.CtrlText.Text = "進捗：" + i.ToString() + "%"
                        )
                    );
                }
            });
        }
    }
}
