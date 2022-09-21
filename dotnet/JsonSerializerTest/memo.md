# JsonSerializerTest

## Students.json

サンプル JSON ファイル．

## ReadJson.ps1

PowerShell で JSON を読み込むサンプル．

- `Get-Content -Raw` で JSON 読み込み
- `ConvertFrom-Json` で PowerShell オブジェクト化

## ReadJson.bat

上記 `ReadJson.ps1` にサンプル JSON を引数で指定して実行する bat．

## Program.cs

C# で JSON を読み込むサンプル．

- JSON のデータ構造を定義したスキーマクラスを定義
- `System.IO.File.ReadAllText()` で JSON 読み込み
- `System.Text.Json.JsonSerializer.Deserialize<データ構造クラス>()` でオブジェクト化