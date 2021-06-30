using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class Section {
        [JsonProperty("widgets")]
        public Widget[] Widgets { get; set; } = { new Widget(), new Widget() };

        public string ToString() {
            string text = "";
            foreach(Widget widget in Widgets) {
                text += widget.ToString() + "\n";
            }
            return text;
        }
    }
}
