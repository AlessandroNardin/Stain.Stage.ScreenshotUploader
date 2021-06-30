using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class Card {
        [JsonProperty("sections")]
        public Section[] Sections { get; set; } = { new Section() };

        public string ToString() {
            string text = "";
            foreach(Section section in Sections) {
                text += section.ToString() + "\n";
            }
            return text;
        }
    }
}
