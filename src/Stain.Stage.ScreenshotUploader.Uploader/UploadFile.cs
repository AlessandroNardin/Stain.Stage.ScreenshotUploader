using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Drawing;

namespace Stain.Stage.ScreenshotUploader.Uploader {
    
    public class UploadFile{
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
            request.AddHeader("Authorization", "Client-ID 6168f4a3afb8008");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", ConvertImageToBase64(image));
            request.AddParameter("type", "base64");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            //extraction of the response paramethers
            JObject search = JObject.Parse(response.Content);

            if((bool)search["success"]) {
                UploadData uData = search["data"].ToObject<UploadData>();
                Console.WriteLine(uData.ToString());
                data = uData;
                return true;
            } else {
                ErrorData error = search["data"].ToObject<ErrorData>();
                Console.WriteLine(error.ToString());
                return false;
            }        
        }
    }
}
