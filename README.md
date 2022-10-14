## dotnet

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




