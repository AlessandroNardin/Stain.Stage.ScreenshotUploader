using Stain.Stage.ScreenshotUploader.Uploader;
using System;

namespace Stain.Stage.ScreenshotUploader {
    public static class Program {
        public static void Main(string[] args) {

            Console.WriteLine("START");

            using UploadFile uploader = new();
            UploadData data;
            uploader.TryUploadImage("C:\\Users\\utente.elettrico\\Desktop\\Test.png", out data);

            Console.WriteLine("END");
        }
     }
}
