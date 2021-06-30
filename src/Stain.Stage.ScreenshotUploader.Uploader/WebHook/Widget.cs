using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class Widget {
        [JsonProperty("textButton")]
        public TextButton TextButton = new TextButton();
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        public string ToString() {
            return $"ImageUrl: {ImageUrl}\n" + TextButton.ToString() + "\n";
        }
    }
}
