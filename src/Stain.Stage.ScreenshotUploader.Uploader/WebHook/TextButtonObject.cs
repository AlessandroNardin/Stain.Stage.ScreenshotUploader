using Newtonsoft.Json;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class TextButtonObject {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("onClick")]
        public OnClick OnClick { get; set; } = new OnClick();

        public TextButtonObject(string text, string url) {
            Text = text;
            OnClick = new OnClick();
            OnClick.OpenLink.Url = url;
        }
    }
}
