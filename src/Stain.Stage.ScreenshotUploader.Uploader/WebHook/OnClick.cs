using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class OnClick {
        [JsonProperty("openLink")]
        public OpenLink OpenLink = new OpenLink();

        public string ToString() {
            return OpenLink.ToString(); ;
        }
    }
}
