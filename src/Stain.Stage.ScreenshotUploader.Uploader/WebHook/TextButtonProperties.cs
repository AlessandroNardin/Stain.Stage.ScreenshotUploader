using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class TextButtonProperties {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("onClick")]
        public OnClick OnClick { get; set; } = new OnClick();

        public TextButtonProperties(string text, string url) {
            Text = text;
            OnClick = new OnClick();
            OnClick.OpenLink.Url = url;
        }
    }
}
