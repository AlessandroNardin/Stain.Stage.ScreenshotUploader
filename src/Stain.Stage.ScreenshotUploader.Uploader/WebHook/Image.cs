using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class Image : Widget{
        [JsonProperty("image")]
        public ImageUrl ImageUrl { get; set; }

        public Image(string imageUrl) {
            ImageUrl = new ImageUrl(imageUrl);
        }
    }
}
