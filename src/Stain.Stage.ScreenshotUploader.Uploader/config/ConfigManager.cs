using IniParser;
using IniParser.Model;

namespace Stain.Stage.ScreenshotUploader.Uploader.config {
    class ConfigManager {
        public static ConfigManager Instance { get; } = new(@".\config.ini");

        public string Path { get; }
        public Config Config { get; private set; }

        private ConfigManager(string path) {
            Path = path;
            Load();
        }

        public void Load() {
            IniData data = new FileIniDataParser().ReadFile(Path);

            Config = new Config(data["IMGUR_API"]["CLIENT_ID"]);
        }

        public void Save() {
            //
        }
    }
}
