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
            // this.button_1.IsEnabled = false;
            ((Button)sender).IsEnabled = false;
            await runProgressBar();

            MessageBox.Show(@"タスクが完了しました．");
            ((ViewModel)this.DataContext).Progress = 0;
            ((Button)sender).IsEnabled = true;
        }

        private async Task runProgressBar()
        {
            var vm = this.DataContext as ViewModel;
            while( vm.Progress < 100)
            {
                vm.Progress += 1;
                await Task.Delay(10);
            }
        }

        public class ViewModel : INotifyPropertyChanged
        {
            private int _Progress = 0;
            public int Progress
            {
                get { return this._Progress; }
                set {
                    this._Progress = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.Progress)));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}
