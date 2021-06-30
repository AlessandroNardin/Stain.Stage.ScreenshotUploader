using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader.WebHook {
    public class WebHook {
        [JsonProperty("cards")]
        public Card[] Cards { get; set; } = { new Card() };

        public WebHook(string imageUrl, string buttonText) {
            Cards[0].Sections[0].Widgets[0].ImageUrl = imageUrl;
            Cards[0].Sections[0].Widgets[1].TextButton.Text = buttonText;
            Cards[0].Sections[0].Widgets[1].TextButton.OnClick.OpenLink.Url = imageUrl;
        }


        public string ToString() {
            string text = "";
            foreach(Card card in Cards) {
                text += card.ToString()+"\n";
            }
            return text;
        }
    }
}
