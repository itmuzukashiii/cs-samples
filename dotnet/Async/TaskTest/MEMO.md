# Task を使って UI 動作をブロックせずに並行処理させる

## プロジェクト作成

- `dotnet new console --user-program-main -n AsyncTest3`
- csproj 編集

## 概要

- ボタンクリックでラベルのテキスト表記を継続して変更し続けるアプリ
- 同期処理で行おうとするとラベルテキストの変更が完了するまでの間 UI がフリーズしてしまう
- `Task` と `async/await` を使うことで UI 処理をブロックさせずに並行処理が可能

## 同期メソッドで書いた場合どうなるか

下記のようにボタンクリック時の動作を定義すると，10秒間 UI がフリーズしてしまう．

```cs
private void button_1_Click(object sender, EventArgs e) {
    this.label_1.Content = @"";
    countDown();
    this.label_1.Content = @"完了";
}

private void countDown() {
    for(var k = 10; k > 0; k--) {
        this.label_1.Content = k;
        Thread.Sleep(1000);
    }
}
```

## 別の書き方

`Task.Run` と匿名メソッド表記を用いることで一つのメソッドで書くこともできる．

匿名メソッドの法にも `async` を付けるのがミソ．

```cs
private async void button_1_Click(object sender, EventArgs e) {

    ((Button)sender).IsEnabled = false;
    this.label_1.Content = @"";

    await Task.Run(async () => {
        for(var k = 10; k > 0; k--) {
            this.label_1.Content = k;
            await Task.Delay(1000);
        }
    });
    
    this.label_1.Content = @"完了";
    ((Button)sender).IsEnabled = true;
}
```

## ボタンの有効化/無効化

下記のように書いた場合，ボタンを何度も押せてしまい，押した回数分 `button_1_Click` が同時実行されてしまう．

```cs
private async void button_1_Click(object sender, EventArgs e) {
    this.label_1.Content = @"";
    await countDown();
    this.label_1.Content = @"完了";
}

private async Task countDown() {
    for(var k = 10; k > 0; k--) {
        this.label_1.Content = k;
        await Task.Delay(1000);
    }
}
```

```cs
this.button_1.IsEnabled = false;
```

または

```cs
//--- sender.GetType() == System.Windows.Controls.Button であることが前提
((Button)sender).IsEnabled = false;
```

のように書くことで，ボタンを無効化できる．


## 参考

- 実践で役立つC#プログラミングのイディオム/定石&パターン
