namespace Stain.Stage.ScreenshotUploader.Uploader.config {
    class Config {
        public string ImgurClientId { get; }

        public Config(string imgurClientId) {
            ImgurClientId = imgurClientId;
        }
    }
}
