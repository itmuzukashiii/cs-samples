using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfProgress2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as ViewModel;

            await Task.Run(async () =>
            {
                while( vm.Progress < 100 )
                {
                    vm.Progress += 1;
                    await Task.Delay(10);
                }
            });

            MessageBox.Show(@"タスクが完了しました．");
            vm.Progress = 0;
        }

        public class ViewModel : INotifyPropertyChanged
        {
            private int _Progress = 0;
            public int Progress
            {
                get { return this._Progress; }
                set {
                    this._Progress = value;
                    this.NotifyPropertyChanged(nameof(this.Progress));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged(string name)
            {
                this.PropertyChanged.Invoke(
                    this,
                    new PropertyChangedEventArgs(name)
                );
            }
        }
    }
}
