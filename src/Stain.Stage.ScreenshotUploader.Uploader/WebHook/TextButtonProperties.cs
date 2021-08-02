using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class TextButtonProperties {
        [JsonProperty("textButton")]

        public TextButtonObject textButton { get; set; }

        public TextButtonProperties(string text, string url) {
            textButton = new TextButtonObject(text, url);
        }
    }
}
