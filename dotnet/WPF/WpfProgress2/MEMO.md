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

まず，実行ボタンクリックにより `Button_Click` メソッドが実行される．動作内容としては `Progress` プロパティ値を1ずつ増やしていき，100になったら while ループを抜け，完了メッセージを表示させるというもの．

プログレスバーの `Value` は Progress 値にバインドされているため，Progress の増加に合わせてプログレスバーも伸びていくように思えるが，実はそうはならない．

- Progress 値が変化する
- Progress 値が変化したことを UI が認識する
- UI がデータソースの再バインディングを行う
- UI 上の値が変化する

というステップを踏む必要があるらしい．


## 参考

[[WPF] プログレスバーの使い方まとめ │ Web備忘録][1]