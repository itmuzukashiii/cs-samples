# 進捗度を示すプログレスバーのサンプル

[1]:https://webbibouroku.com/Blog/Article/wpf-progressbar "[WPF] プログレスバーの使い方まとめ │ Web備忘録"
[2]:https://webbibouroku.com/Blog/Article/wpf-progressbar#outline__3 "進捗度を示すプログレスバー"

## 概要

[こちら][2] をコピペして，進捗度を示すプログレスバーを表示させることができた．

実行ボタンをクリックするとプログレスバーが伸び始め，伸び切ると完了メッセージが表示される．

## プロジェクト作成

- `dotnet new wpf --nullable false -n WpfProgress2`
- csproj 編集

## ポイント

### MainWindow.xaml

- `Grid` の中にプログレスバー (`ProgressBar`) と実行ボタン (`Button`) を定義する
- 動作制御ロジックはコードビハインド (`MainWindow.xaml.cs`) に書く

#### ProgressBar

- `Minimum` と `Maximum` を定義することでバーが表示される `Value` 値の範囲を設定する
  * Min = 90, Max = 100 とすると Value が 90 以上にならないとバーが伸びない
  * Min = 0, Max = 0 とすると常にバーは伸びきった状態
- `Value` は変化させたいので `{Binding Progress}` とする
  * ウィンドウの `DataContext` にバインドさせたオブジェクトの `Progress` プロパティ値を反映させるということ
  
#### Button

- `Click=Button_Click` とし，クリック時に `Button_Click` メソッドを起動させる


### MainWindow.xaml.cs

`MainWindow.xaml` のコードビハインド．実現させたい動作を記述する．

実行ボタンクリックにより `Button_Click` メソッドが実行される．動作内容としては `Progress` プロパティ値を1ずつ増やしていき，100になったら while ループを抜け，完了メッセージを表示させるというもの．

プログレスバーの `Value` は Progress 値にバインドされているため，Progress の増加に合わせて勝手にプログレスバーも伸びていくように思えるが，実はそうはならない．

- Progress 値が変化する
- Progress 値が変化したことを UI が認識する
- UI がデータソースの再バインディングを行う
- UI 上の値が変化する

というステップを踏む必要があり，そのために `INotifyPropertyChanged` を用いた仕掛けが必要となる．

#### UI が全く変化しない例

参考としてこのような形から考えてみる．`Progress` というプロパティのみを持つ `ViewModel` クラスを定義し，`MainWindow` のコンストラクタにて `DataContext` にバインディングする．

Progress の規定値は 50 なので，ウィンドウが表示されると最初からプログレスバーは半分まで伸びた状態である．

この状態で実行ボタンをクリックすると，UI に何も変化は起きないまましばらく後に完了メッセージが「タスクが完了しました．Progress = 100」のように表示される．

裏で Progress 値は変わっているはずなのに，UI には反映されていない．

なお，`Button_Click` を単純な同期メソッドにしているためだが，実行ボタンを押してから完了メッセージが表示されるまでの間，ウィンドウの移動や最小化をしようとしてもできないこともわかる．

```cs
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new ViewModel();
    }

    public class ViewModel
    {
        public int Progress {get; set;} = 50;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var vm = this.DataContext as ViewModel;

            while( vm.Progress < 100 )
            {
                vm.Progress += 1;
                System.Threading.Thread.Sleep(20);
            }

        MessageBox.Show($"タスクが完了しました．Progress = {vm.Progress}");
        vm.Progress = 0;
    }
}
```

#### UI に状態通知を行う

データソースの変更が UI に伝達されるようにするには，<u>データソースオブジェクトが `INotifyPropertyChanged` インターフェースを実装している必要がある</u>．

- INotifyPropetyChanged インターフェースはただ一つのイベント `PropertyChanged` を持つ
- データソースに変更があった場合は PropertyChanged イベントを通して WPF ランタイムに通知する
- WPF ランタイムは通知を受けて最新のデータで再バインディングを実行する

コードへの落とし方としては

- データソースオブジェクトである ViewModel に INotifyPropertyChanged インターフェースを実装する
  * `ViewModel: INotifyPropertyChanged` とする
  * `public event PropertyChangedEventHandler PropertyChanged` を追加
- ViewModel.Progress が変更された場合に PropertyChanged イベントを発行する
  * `Progress` に setter を設け，値の更新と併せて `this.PropertyChanged()` を起動させる
  * 変化したプロパティ名 `Progress` を `PropertyChangedEventArgs` を通じて通知する

```cs
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new ViewModel();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var vm = this.DataContext as ViewModel;

            while( vm.Progress < 100 )
            {
                vm.Progress += 1;
                System.Threading.Thread.Sleep(20);
            }

        MessageBox.Show($"タスクが完了しました．Progress = {vm.Progress}");
        vm.Progress = 0;
    }

    //--- INotifyPropertyChanged I/F を実装
    public class ViewModel: INotifyPropertyChanged
    {
        private int _Progress = 0;
        public int Progress {
            get { return this._Progress; }
            set {
                this._Progress = value;
                //--- プロパティ更新イベントを発行する
                this.PropertyChanged(this, new PropertyChangedEventArgs("Progress"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
```

この形で実行してみると，しばらくは何も変化が起きないが，完了メッセージと表示と共にプログレスバーが 100% になる．これは，`Button_Click` が同期メソッドになっているために，UI の更新がブロックされているためと考えられる．

一つ発見だったのは，`MessageBox` によって UI が更新されることだ．`while` ブロックを下記の様に修正すると，10%ずつプログレスバーが伸びていく．昔のVBアプリケーションの `DoEvents` 的な動きになっているのだろうか．

下記のように書き換えると，10%ずつプログレスバーが伸びていく動きになる．

```cs
while( vm.Progress < 100 )
{
    vm.Progress += 1;
    System.Threading.Thread.Sleep(20);
    if (vm.Progress % 10  == 0)
        MessageBox.Show($"タスクが完了しました．Progress = {vm.Progress}");
}
```

Button_Click の動作と UI の更新を同時並行させるためには，マルチスレッド化しなければならないことが分かる．

## ボタンクリック時のメソッド動作と UI 更新を同時並行させる



## 参考

- [[WPF] プログレスバーの使い方まとめ │ Web備忘録][1]
- パーフェクト C# 改訂4版