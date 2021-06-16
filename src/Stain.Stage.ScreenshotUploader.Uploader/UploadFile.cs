using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace Stain.Stage.ScreenshotUploader.Uploader {
    
    public class UploadFile : IDisposable {
        private HttpClient ApiClient { get; } = new HttpClient();
        
        public string ConvertImageToBase64(Image image) {
            return Convert.ToBase64String(ImageToByteArray(image));
        }

        public byte[] ImageToByteArray(Image image) {
            return new ImageConverter().ConvertTo(image, typeof(byte[])) as byte[];
        }

        //method to call to upload an image
        public bool TryUploadImage(Bitmap image, out UploadData data) {
            data = default;

            RestClient client = new RestClient("https://api.imgur.com/3/upload");
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Client-ID 6168f4a3afb8008");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", ConvertImageToBase64(image));
            request.AddParameter("type", "base64");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

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

        public void Dispose() {
            ApiClient.Dispose();
        }

        private static T GetResult<T>(Task<T> input) {
            T result = default;
            input.Wait();

            try {
                result = input.Result;
            } catch(AggregateException e) {
                Console.WriteLine($"Task Cancelled: {e.Message}");
            } catch(Exception e) {
                Console.WriteLine($"Error occurred: {e.Message}");
                Exception inner = e.InnerException;
                Console.WriteLine($"Inner Exception Error: {inner?.Message ?? "<NULL>"}");
            }

            return result;
        }
    }
}
