using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class TextButton : Widget{
        [JsonProperty("textButton")]
        public TextButtonProperties button { get; set; }

        public TextButton(string buttonText,string imageUrl) {
            button = new TextButtonProperties(buttonText, imageUrl);
        }
    }
}
