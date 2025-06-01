using PDF_Compling.Models;
using System.IO;
using System.Text.Json;

namespace PDF_Compling.Services
{
    /// <summary>
    /// アプリケーション設定ファイルの読み書きを管理するサービス
    /// </summary>
    internal class SettingsService
    {
        private static readonly string SettingsDirectoryPath =
             Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PDF_Compling");

        private static readonly string SettingsFilePath =
            Path.Combine(SettingsDirectoryPath, "settings.json");

        /// <summary>
        /// 設定ファイルから設定を読み込みます
        /// </summary>
        /// <returns>設定オブジェクト</returns>
        public static Settings LoadSettings()
        {
            try
            {
                if (!File.Exists(SettingsFilePath))
                {
                    return new Settings();
                }

                string json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<Settings>(json);
                return settings ?? new Settings();
            }
            catch (Exception ex)
            {
                // エラーが発生した場合はデフォルト設定を返す
                System.Diagnostics.Debug.WriteLine($"Settings load error: {ex.Message}");
                return new Settings();
            }
        }

        /// <summary>
        /// 設定をファイルに保存します
        /// </summary>
        /// <param name="settings">保存する設定</param>
        public static void SaveSettings(Settings settings)
        {
            try
            {
                // ディレクトリが存在しない場合は作成
                if (!Directory.Exists(SettingsDirectoryPath))
                {
                    Directory.CreateDirectory(SettingsDirectoryPath);
                }

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // 整形して保存
                };

                string json = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch(Exception ex)
            {
                // エラーハンドリング（ログ出力等）
                System.Diagnostics.Debug.WriteLine($"Settings save error: {ex.Message}");
            }
        }
    }
}
