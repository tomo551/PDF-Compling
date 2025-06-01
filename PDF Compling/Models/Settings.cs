namespace PDF_Compling.Models
{
    /// <summary>
    /// アプリケーション設定
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// 最後に使用したPDF保存先フォルダ
        /// </summary>
        public string LastSaveDirectory { get; set; } = string.Empty;
    }
}
