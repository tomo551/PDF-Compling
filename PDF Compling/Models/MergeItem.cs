using System.Windows.Media.Imaging;

namespace PDF_Compling.Models
{
    internal class MergeItem
    {
        public required string FilePath { get; init; }
        public required int PageIndex {  get; init; }
        public required BitmapImage Thumbnail {  get; init; }
        public string FileName => System.IO.Path.GetFileName(FilePath);
        /// <summary>人間向けページ番号</summary>
        public int PageNumber => PageIndex + 1;
    }
}
