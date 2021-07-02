using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class ImageUrl {
        [JsonProperty("imageUrl")]
        public string imageUrl;

        public ImageUrl(string imgUrl) {
            imageUrl = imgUrl;
        }
    }
}
