[1]:https://araramistudio.jimdo.com/2017/05/02/c-%E3%81%A7%E5%88%A5%E3%82%B9%E3%83%AC%E3%83%83%E3%83%89%E3%81%8B%E3%82%89%E3%82%B3%E3%83%B3%E3%83%88%E3%83%AD%E3%83%BC%E3%83%AB%E3%82%92%E6%93%8D%E4%BD%9C%E3%81%99%E3%82%8B/  "C#で別スレッドからコントロールを操作する (Dispatcher.Invoke) - Ararami Studio"

# UI コントロールを別スレッドから操作するには

## 参考

[C#で別スレッドからコントロールを操作する (Dispatcher.Invoke) - Ararami Studio"][1]

## ダメな例

別スレッド内から直接 `CtrlText.Text = "進捗"` のようにしようとすると `System.InvalidOperationException` が発生してしまう．
なお，下記の様に `try - catch` で `MessageBox.Show` させない場合無言で UI が落ちる．(サブスレッド内での例外のため？)

```cs
await Task.Run( () =>
{
    for (int i = 1; i <= 100; ++i)
    {
        Task.Delay(100).Wait();

        try {
            this.CtrlText.Text = "進捗：" + i.ToString() + "%";
        } catch (Exception e) {
            MessageBox.Show(e.GetType().ToString() + "\r\n" + e.Message);
            // System.InvalidOperationException
            // このオブジェクトは別のスレッドに所有されているため、呼び出しスレッドはこのオブジェクトにアクセスできません。
        }
    }
});
```

## うまくいく例

- UI 操作は `Dispatcher` 軽油で行う
- 実行したいコールバックメソッドを `Action` デリゲートとして `Dispatcher.Invoke` に渡す

### `delegate void System.Action()`

Encapsulates a method that has no parameters and does not return a value.