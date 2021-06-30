using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text;
using System.Web;

namespace Stain.Stage.ScreenshotUploader.Uploader {

    public class UploadFile {
        public RestClient client;
        public static UploadFile Instance { get; } = new UploadFile();

        private UploadFile() {
            client = new RestClient("https://api.imgur.com/3/upload");
        }
        public string ConvertImageToBase64(Image image) {
            return Convert.ToBase64String(ImageToByteArray(image));
        }

        public byte[] ImageToByteArray(Image image) {
            return new ImageConverter().ConvertTo(image, typeof(byte[])) as byte[];
        }

        //method to call to upload an image
        public bool TryUploadImage(Bitmap image, out UploadData data) {
            data = default;
            
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Client-ID {config.ConfigManager.Instance.Config.ImgurClientId}");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", ConvertImageToBase64(image));
            request.AddParameter("type", "base64");
            IRestResponse response = client.Execute(request);

            //extraction of the response paramethers
            JObject search = JObject.Parse(response.Content);

            if((bool)search["success"]) {
                UploadData uData = search["data"].ToObject<UploadData>();

                data = uData;

                //WebHook
                WebHook.WebHook whook = new WebHook.WebHook(data.Link, "Open on Imgur");
                string stringjson = JsonConvert.SerializeObject(whook);

                Console.WriteLine(stringjson);

                request.RequestFormat = DataFormat.Json;
                IRestRequest restRequest = new RestRequest(Method.POST);
                restRequest.AddBody(stringjson);

                IRestResponse Jsonresponse = client.Execute(restRequest);
#if DEBUG
                Console.WriteLine(Jsonresponse.Content);
#endif
                return true;
            } else {
                ErrorData error = search["data"].ToObject<ErrorData>();
#if DEBUG
                Debug.WriteLine(error.ToString());
#endif
                return false;
            }        
        }
    }
}
