using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Stain.Stage.ScreenshotUploader.Uploader {
    public class UploadFile : IDisposable {
        private HttpClient ApiClient { get; } = new HttpClient();

        public Image ImportImage(string path) {
            return Image.FromFile(path);
        }
        
        public string ConvertImageToBase64(Image image) {
            return Convert.ToBase64String(ImageToByteArray(image));
        }

        public byte[] ImageToByteArray(Image ImageIn) {
            using(var ms = new MemoryStream()) {
                ImageIn.Save(ms, ImageIn.RawFormat);
                return ms.ToArray();
            }
        }


        public void UploadImage(string ImagePath) {

            Image image = ImportImage(ImagePath);

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
                return;
            }

            Console.WriteLine(GetResult(response.Content.ReadAsStringAsync()));


            JObject googleSearch = JObject.Parse(GetResult(response.Content.ReadAsStringAsync()));

            // get JSON result objects into a list
            ErrorData error = googleSearch["data"].ToObject<ErrorData>();

            // serialize JSON results into .NET objects
            Console.WriteLine(error.ToString());
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
