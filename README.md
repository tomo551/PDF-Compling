PDF Compling — PDF まとめ
===================================

![PDF Compling](https://img.shields.io/badge/Version-1.0.0-blue.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)

複数のPDFファイルから必要なページを選択して、1つのPDFファイルに結合するWindows用デスクトップアプリケーションです。

## 目次

1. [概要](#概要)
2. [動作環境](#動作環境)
3. [インストール方法](#インストール方法)
4. [使い方](#使い方)
5. [主な機能](#主な機能)
6. [ライセンス](#ライセンス)
7. [免責事項](#免責事項)
8. [サポート](#サポート)

## 概要

**PDF Compling**は、複数のPDFファイルから必要なページだけを選択して、1つのPDFファイルに結合できるシンプルで使いやすいアプリケーションです。

### 特徴
- 📁 **簡単操作**: ドラッグ&ドロップでPDFファイルを追加
- 🖼️ **ビジュアル**: ページごとのサムネイル表示
- 🔄 **柔軟な並び替え**: 直感的なドラッグ&ドロップ操作
- 👀 **プレビュー機能**: 選択したページの高解像度プレビュー
- 💾 **設定保存**: 前回の保存先フォルダを自動記憶
- ⌨️ **キーボードショートカット**: 効率的な操作

## 動作環境

### 必須環境
- **OS**: Windows 10 (1903以降) / Windows 11
- **アーキテクチャ**: x64 (64bit)
- **メモリ**: 4GB RAM以上推奨
- **ストレージ**: 100MB以上の空き容量

### 推奨環境
- **メモリ**: 8GB RAM以上
- **ディスプレイ**: 1920×1080以上の解像度

## インストール方法

### MSIインストーラーを使用する場合（推奨）

1. [リリースページ](https://github.com/tomo551/PDF-Compling/tree/master/PDF%20Compling%20Setup)から最新版の`PDF_Compling_Setup.msi`をダウンロード
2. ダウンロードしたMSIファイルをダブルクリック
3. インストールウィザードの指示に従ってインストールを完了
4. デスクトップまたはスタートメニューからアプリケーションを起動

### ポータブル版を使用する場合

1. `PDF_Compling_Portable.zip`をダウンロード
2. 任意のフォルダに解凍
3. `PDF_Compling.exe`を実行

## 使い方

### 基本的な使用手順

#### 1. PDFファイルの追加
- アプリケーションを起動
- PDFファイルを左側のリストエリアにドラッグ&ドロップ
- または、ファイルエクスプローラーから直接ドラッグ

#### 2. ページの確認と選択
- 追加されたPDFのページがサムネイル形式で表示されます
- サムネイルをクリックすると右側にプレビューが表示されます
- 不要なページがある場合は選択して「Remove Selected」ボタンまたは`Delete`キーで削除

#### 3. ページの並び替え
**方法1: ドラッグ&ドロップ**
- サムネイルをドラッグして希望の位置にドロップ

**方法2: ボタン操作**
- ページを選択して「Move Up」「Move Down」ボタンで移動

#### 4. PDFの結合と保存
- 「Merge PDFs」ボタンをクリック
- 保存先とファイル名を指定
- 「保存」をクリックして結合完了

### キーボードショートカット

| キー | 動作 |
|------|------|
| `Delete` | 選択したページを削除 |
| `↑` `↓` | ページ選択の移動 |

## 主な機能

### 📂 ファイル管理機能
- **マルチファイル対応**: 複数のPDFファイルを同時に処理
- **自動ページ分割**: 各PDFファイルのページを個別に管理
- **ファイル情報表示**: ファイル名とページ番号を確認可能

### 🖼️ ビジュアル機能
- **サムネイル生成**: 各ページの内容を一目で確認
- **高解像度プレビュー**: 選択したページの詳細表示
- **リアルタイム更新**: 並び替え結果を即座に反映

### 🔄 編集機能
- **ドラッグ&ドロップ並び替え**: 直感的なページ順序変更
- **選択削除**: 不要なページの除去
- **一括クリア**: すべてのページを一度に削除
- **上下移動**: ボタンによる精密な位置調整

### 💾 設定機能
- **保存先記憶**: 前回使用したフォルダを自動記憶
- **JSON設定**: 設定情報をJSONファイルで管理
- **自動復元**: アプリケーション再起動時に設定を復元

### 🔧 技術仕様
- **対応フォーマット**: PDF (Portable Document Format)
- **出力品質**: 元ファイルと同等の品質を維持
- **処理速度**: 軽量で高速な処理
- **メモリ効率**: 大きなファイルでも安定動作

## ライセンス

このソフトウェアは**MITライセンス**の下で配布されています。

```
MIT License

Copyright (c) 2025 [IgaSystems]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

### 使用しているライブラリ
- **PdfSharpCore** - PDF操作ライブラリ
- **PdfiumViewer** - PDFレンダリング・サムネイル生成
- **CommunityToolkit.Mvvm** - MVVMパターン実装

## 免責事項

1. **データの安全性**: 本ソフトウェアの使用により生じたデータの損失、破損等について、開発者は一切の責任を負いません。

2. **使用結果**: 本ソフトウェアを使用して作成されたPDFファイルの内容について、開発者は責任を負いません。

3. **動作保証**: すべての環境での動作を保証するものではありません。使用前に十分なテストを行ってください。

4. **第三者の権利**: PDFファイルの著作権、知的財産権等については、ユーザーの責任で適切に管理してください。

5. **セキュリティ**: パスワード保護されたPDFファイルや、機密情報を含むファイルの取り扱いには十分注意してください。

## サポート

### ヘルプとサポート

**よくある質問**

**Q: 大きなPDFファイルを処理できますか？**
A: 数百ページのPDFファイルでも処理可能ですが、メモリ使用量が増加する可能性があります。

**Q: パスワード保護されたPDFは使用できますか？**
A: 現在のバージョンではパスワード保護されたPDFファイルには対応していません。

**Q: 作成されたPDFのファイルサイズが大きくなります**
A: 結合されるページ数が多い場合や、高解像度の画像が含まれている場合にファイルサイズが大きくなることがあります。

**技術的な問題**

問題が発生した場合は、以下の情報を含めてお問い合わせください：

- Windows のバージョン
- アプリケーションのバージョン
- エラーメッセージ（表示される場合）
- 問題が発生した操作手順
- 使用していたPDFファイルの特徴（ページ数、ファイルサイズなど）


### 今後の予定

- [ ] パスワード保護PDFの対応
- [ ] 複数の出力フォーマット対応
- [ ] バッチ処理機能
- [ ] ページ回転機能
- [ ] OCR機能の検討

---

**最終更新**: 2025年6月1日  
**バージョン**: 1.0.0
