using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader {
    public class UploadFile {
        public string DestinationURL
        {
            get => DestinationURL;
            set
            {
                DestinationURL = value;
            }
        }
        Image ImageToUpload;

        public void ImportImage(string Path) {
            ImageToUpload = Image.FromFile(Path);
        }
        
        public string ConvertImageToBase64() {
            return Convert.ToBase64String(ImageToByteArray(ImageToUpload));
        }
        public byte[] ImageToByteArray(Image ImageIn) {
            using(var ms = new MemoryStream()) {
                ImageIn.Save(ms, ImageIn.RawFormat);
                return ms.ToArray();
            }
        }

        
        public void UploadImage(string ImagePath) {

            ImportImage(ImagePath);
            ConvertImageToBase64();

            WebRequest wb = WebRequest.Create(new Uri("http://imgur.com/api/upload.xml"));
            wb.ContentType = "application/x-www-form-urlencoded";
            wb.Method = "POST";
            Console.WriteLine(wb.Timeout);
            Console.WriteLine(ImageToByteArray(ImageToUpload).Length);
            string parameters = "image=" + ConvertImageToBase64();


            Console.WriteLine("parameters: " + parameters.Length);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] bytes = encoding.GetBytes(parameters);

            try { // send the Post
                wb.ContentLength = bytes.Length;   //Count bytes to send

                using Stream requestStream = wb.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
            } catch(WebException ex) {
                Console.WriteLine(ex.Message);
                
            }

            try { // get the response
                WebResponse webResponse = wb.GetResponse();

                StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                Console.WriteLine(sr.ReadToEnd().Trim());
            } catch(WebException ex) {
                Console.WriteLine(ex.Message, "HttpPost: Response error");
            }

        }

    }
}
