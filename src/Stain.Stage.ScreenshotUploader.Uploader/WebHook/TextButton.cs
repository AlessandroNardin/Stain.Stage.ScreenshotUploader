using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class TextButton {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("onClick")]
        public OnClick OnClick { get; set; } = new OnClick();

        public string ToString() {
            string text = "";

            text += $"Text : {Text}\n";
            text += OnClick.ToString() + "\n";

            return text;
        }
    }
}
