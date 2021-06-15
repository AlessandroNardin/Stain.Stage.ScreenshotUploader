using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace Stain.Stage.ScreenshotUploader.Uploader {
    public class UploadFile : IDisposable {
        private HttpClient ApiClient { get; } = new HttpClient();

        public Image ImportImage(string path) {
            return Image.FromFile(path);
        }
        
        public string ConvertImageToBase64(Image image) {
            return Convert.ToBase64String(ImageToByteArray(image));
        }

        public byte[] ImageToByteArray(Image imageIn) {
            using(var ms = new MemoryStream()) {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        public bool TryUploadImage(Bitmap image, out UploadData data) {
            data = default;

            Dictionary<string, string> formContent = new() {
                { "image", ConvertImageToBase64(image) },
                { "type", "base64" },
            };
            FormUrlEncodedContent content = new(formContent);

            HttpRequestMessage request = new();
            request.Content = content;
            request.Headers.Add("Authorization", $"Client-ID {ApiConstants.ImgurClientId}");

            HttpResponseMessage response = GetResult(ApiClient.PostAsync("https://api.imgur.com/3/upload", content));
            if(response == null) {
                return false;
            }

            Console.WriteLine(GetResult(response.Content.ReadAsStringAsync()));

            JObject search = JObject.Parse(GetResult(response.Content.ReadAsStringAsync()));


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
