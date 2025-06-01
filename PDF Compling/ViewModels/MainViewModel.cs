using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PDF_Compling.Models;
using PDF_Compling.Services;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;

namespace PDF_Compling.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<MergeItem> Items { get; } = [];

        [ObservableProperty]
        private MergeItem? selectedItem;

        [ObservableProperty]
        private BitmapImage? previewImage;

        public bool HasSelectedItem => selectedItem is not null;

        partial void OnSelectedItemChanged(MergeItem? value)
        {
            OnPropertyChanged(nameof(HasSelectedItem));
            UpdatePreviewImage();
        }

        private void UpdatePreviewImage()
        {
            if (selectedItem is null)
            {
                PreviewImage = null;
                return;
            }

            try
            {
                // より高解像度でプレビュー画像を生成
                PreviewImage = PreviewImageGenerator.Generate(selectedItem.FilePath, selectedItem.PageIndex);
            }
            catch (Exception ex)
            {
                // エラーハンドリング（ログ出力等）
                System.Diagnostics.Debug.WriteLine($"Preview generation error: {ex.Message}");
                PreviewImage = null;
            }
        }

        [RelayCommand]
        private void RemoveSelected()
        {
            if (selectedItem is not null)
                Items.Remove(selectedItem);
        }

        [RelayCommand]
        private void ClearAll() => Items.Clear();

        [RelayCommand]
        private async Task MergeAsync()
        {
            if (Items.Count == 0) return;

            // 設定を読み込み
            var settings = SettingsService.LoadSettings();

            var dlg = new SaveFileDialog
            {
                Filter = "PDF|*.pdf",
                FileName = "merged.pdf"
            };

            // 前回の保存先フォルダが存在する場合は初期ディレクトリに設定
            if (!string.IsNullOrEmpty(settings.LastSaveDirectory) &&
                Directory.Exists(settings.LastSaveDirectory))
            {
                dlg.InitialDirectory = settings.LastSaveDirectory;
            }

            if (dlg.ShowDialog() != true) return;

            try
            {
                using var outDoc = new PdfDocument();

                foreach (var item in Items)
                {
                    using var input = PdfReader.Open(item.FilePath, PdfDocumentOpenMode.Import);
                    var page = input.Pages[item.PageIndex];
                    outDoc.AddPage(page);
                }
                outDoc.Save(dlg.FileName);

                // 保存が成功した場合、保存先フォルダを設定に記録
                string saveDirectory = Path.GetDirectoryName(dlg.FileName);
                if (!string.IsNullOrEmpty(saveDirectory))
                {
                    settings.LastSaveDirectory = saveDirectory;
                    SettingsService.SaveSettings(settings);
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                System.Diagnostics.Debug.WriteLine($"PDF merge error: {ex.Message}");
                // 必要に応じてユーザーにエラーメッセージを表示
                System.Windows.MessageBox.Show(
                    $"PDFの結合中にエラーが発生しました: {ex.Message}",
                    "エラー",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }

        public void AddFiles(IEnumerable<string> paths)
        {
            foreach (var path in paths.Where(p => p.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)))
            {
                using var pdf = PdfiumViewer.Core.PdfDocument.Load(path);
                int pageCount = pdf.PageCount;
                for (int i = 0; i < pageCount; i++)
                {
                    var thumb = ThumbnailGenerator.Generate(path, i);
                    Items.Add(new MergeItem
                    {
                        FilePath = path,
                        PageIndex = i,
                        Thumbnail = thumb
                    });
                }
            }
        }

        /// <summary>
        /// アイテムを指定したインデックス位置に移動します
        /// </summary>
        /// <param name="item">移動するアイテム</param>
        /// <param name="newIndex">新しいインデックス位置</param>
        public void MoveItem(MergeItem item, int newIndex)
        {
            if (item == null) return;

            int oldIndex = Items.IndexOf(item);
            if (oldIndex == -1) return;

            // 範囲チェック
            if (newIndex < 0) newIndex = 0;
            if (newIndex > Items.Count) newIndex = Items.Count;

            // 同じ位置への移動は無視
            if (oldIndex == newIndex) return;

            // アイテムを移動
            Items.RemoveAt(oldIndex);

            // 削除後のインデックス調整
            if (newIndex > oldIndex)
            {
                newIndex--;
            }

            // 末尾に挿入する場合
            if (newIndex >= Items.Count)
            {
                Items.Add(item);
            }
            else
            {
                Items.Insert(newIndex, item);
            }
        }

        /// <summary>
        /// 2つのアイテムの位置を交換します
        /// </summary>
        /// <param name="item1">1つ目のアイテム</param>
        /// <param name="item2">2つ目のアイテム</param>
        /// 
        public void SwapItems(MergeItem item1, MergeItem item2)
        {
            if (item1 == null || item2 == null) return;

            int index1 = Items.IndexOf(item1);
            int index2 = Items.IndexOf(item2);

            if (index1 == -1 || index2 == -1) return;

            Items[index1] = item2;
            Items[index2] = item1;
        }

        /// <summary>
        /// 選択されたアイテムを上に移動します
        /// </summary>
        [RelayCommand]
        private void MoveSelectedUp()
        {
            if (selectedItem == null) return;

            int currentIndex = Items.IndexOf(selectedItem);
            if (currentIndex > 0)
            {
                SwapItems(Items[currentIndex], Items[currentIndex - 1]);
            }
        }

        /// <summary>
        /// 選択されたアイテムを下に移動します
        /// </summary>
        [RelayCommand]
        private void MoveSelectedDown()
        {
            if (selectedItem == null) return;

            int currentIndex = Items.IndexOf(selectedItem);
            if (currentIndex < Items.Count - 1)
            {
                SwapItems(Items[currentIndex], Items[currentIndex + 1]);
            }
        }
    }
}