using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class TextButton : Widget{
        [JsonProperty("buttons")]
        public TextButtonProperties[] buttons{ get; set; }

        public TextButton(string buttonText,string imageUrl) {
            buttons = new TextButtonProperties[1];
            buttons[0] = new TextButtonProperties(buttonText, imageUrl);
        }
    }
}
