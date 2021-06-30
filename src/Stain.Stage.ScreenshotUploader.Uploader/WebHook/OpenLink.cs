using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class OpenLink {
        [JsonProperty("url")]
        public string Url;

        public string ToString() {

            return $"Url: {Url}\n";
        }
    }
}
