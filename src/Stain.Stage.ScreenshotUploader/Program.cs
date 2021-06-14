using Stain.Stage.ScreenshotUploader.Uploader;
using System;

namespace Stain.Stage.ScreenshotUploader {
    public static class Program {
        public static void Main(string[] args) {

            Console.WriteLine("START");

            using UploadFile uploader = new();
            uploader.UploadImage("C:\\Users\\utente.elettrico\\Desktop\\Test.png");

            Console.WriteLine("START");
        }
     }
}
