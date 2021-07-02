using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class Image {
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
