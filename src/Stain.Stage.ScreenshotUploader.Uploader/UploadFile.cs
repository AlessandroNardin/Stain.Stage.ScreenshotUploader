using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Diagnostics;
using System.Drawing;

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
#if DEBUG
            Debug.WriteLine(response.Content);
#endif
            //extraction of the response paramethers
            JObject search = JObject.Parse(response.Content);

            if((bool)search["success"]) {
                UploadData uData = search["data"].ToObject<UploadData>();
#if DEBUG
                Debug.WriteLine(uData.ToString());
#endif
                data = uData;
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
