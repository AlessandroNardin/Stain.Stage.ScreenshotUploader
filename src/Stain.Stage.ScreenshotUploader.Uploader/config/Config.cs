namespace Stain.Stage.ScreenshotUploader.Uploader.config {
    public class Config {
        public string ImgurClientId { get; }
        public string WebHookUrl { get; }

        public Config(string imgurClientId, string webHookUrl) {
            ImgurClientId = imgurClientId;
            WebHookUrl = webHookUrl;
        }
    }
}
