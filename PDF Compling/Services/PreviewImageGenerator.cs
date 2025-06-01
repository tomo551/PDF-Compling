using PdfiumViewer;
using PdfiumViewer.Core;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace PDF_Compling.Services
{
    internal class PreviewImageGenerator
    {
        /// <summary>
        /// プレビュー用の高解像度画像を生成します
        /// </summary>
        /// <param name="pdfPath">PDFファイルのパス</param>
        /// <param name="pageIndex">ページインデックス</param>
        /// <returns>プレビュー画像</returns>
        public static BitmapImage Generate(string pdfPath, int pageIndex)
        {
            using var doc = PdfDocument.Load(pdfPath);

            // プレビュー用により高い解像度で生成（400x600ピクセル程度）
            using var image = doc.Render(pageIndex, 400, 600, true);

            using var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            ms.Position = 0;

            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.StreamSource = ms;
            bmp.EndInit();
            bmp.Freeze();
            return bmp;
        }

        /// <summary>
        /// 指定したDPIでプレビュー画像を生成します
        /// </summary>
        /// <param name="pdfPath">PDFファイルのパス</param>
        /// <param name="pageIndex">ページインデックス</param>
        /// <param name="dpi">DPI（解像度）</param>
        /// <returns>プレビュー画像</returns>
        public static BitmapImage GenerateWithDpi(string pdfPath, int pageIndex, int dpi = 150)
        {
            using var doc = PdfDocument.Load(pdfPath);

            // DPI指定で画像を生成
            using var image = doc.Render(pageIndex, dpi, dpi, true);

            using var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            ms.Position = 0;

            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.StreamSource = ms;
            bmp.EndInit();
            bmp.Freeze();
            return bmp;
        }
    }
}