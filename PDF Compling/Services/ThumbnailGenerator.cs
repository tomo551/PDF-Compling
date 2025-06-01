using PdfiumViewer.Core;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace PDF_Compling.Services
{
    internal class ThumbnailGenerator
    {
        public static BitmapImage Generate(string pdfPath, int pageIndex)
        {
            using var doc = PdfDocument.Load(pdfPath);
            using var image = doc.Render(pageIndex, 120, 160, true);

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
