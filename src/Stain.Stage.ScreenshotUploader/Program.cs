using Stain.Stage.ScreenshotUploader.Uploader;
using System;
using System.Drawing;

namespace Stain.Stage.ScreenshotUploader {
    public static class Program {
        public static void Main(string[] args) {

            Console.WriteLine("START");

            using UploadFile uploader = new();
            UploadData data;
            Bitmap bm = new Bitmap("C:\\Users\\utente.elettrico\\Desktop\\Test.png");
            uploader.TryUploadImage(bm, out data);

            Console.WriteLine("END");
        }
     }
}
