# C# いろいろテストするリポジトリ

## dotnet

### AsyncAwaitTest

- Main を async にする意味があるのかテスト
- 結論このコードにおいては無さそう
- Main 内で await するなら async にするべき？

### AsyncMainTest

- Main 内で await し，Main を async にしてみるテスト

### AsyncTest

- 非同期処理何もわからないのでまず `Thread.Sleep()` を試してみる

### AsyncTest1

- Thread を使った非同期処理のテスト

### AsyncTest2

- AsyncTest2 と同様の処理を BackgroundWorker を使って書き直してみる

### AutoResetEventTest

- AutoResetEvent でスレッドをシグナル制御するテスト

### JsonSerializer

- json を読み込み Schema クラスのオブジェクトにするサンプル

### JsonSerializer2

- オブジェクトを json として出力するサンプル

### LockTest

- 大量の入出金処理が同時に行われる状況で，口座残高が矛盾なく更新されるように lock の仕掛けを使うサンプル

### ManualResetEventTest

- ManualResetEvent でスレッドをシグナル制御するテスト

### NullableTest

- Null 許容演算子のサンプル
- csproj で Nullable はデフォルトで enable にしておいた方がいいかも

### TaskAwaitResult

- 書籍からのコピペ
- メソッドシグネチャが `async Task<long>` にもかかわらずメソッド内部では long 型を返していることが不思議に感じた
- 結論，これでいいらしい
- 本来 long 型を返す同期メソッドを非同期メソッドにした，と考えると納得感がある
- ちなみにむりやり `Task<long>` を return させようとすると下記のように出る

```
これは非同期メソッドであるため、return 式は 'Task<long>' ではなく 'long' 型である必要があります [TaskAwaitResult]
```


## csc




