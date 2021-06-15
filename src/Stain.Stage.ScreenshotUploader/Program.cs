using Stain.Stage.ScreenshotUploader.Uploader;
using System;
using System.Drawing;
using Stain.Stage.ScreenshotUploader.Screenshot;
using System.Diagnostics;
using System.Threading;

namespace Stain.Stage.ScreenshotUploader {
    public static class Program {

        public static void Main(string[] args) {
            Console.WriteLine("START");
          
            //Captures a screenshot and edits it with paint.
            Point p1 = new Point(100, 100);
            Point p2 = new Point(500, 500);
            string screenshot = Screenshot.Screenshot.Capture(p1,p2);
            Bitmap editedImage = Screenshot.ImageEditor.PaintEdit(screenshot);
#if DEBUG 
            //Save the edited image on the desktop for test purposes.
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.jpg");
            editedImage.Save(@path);

            using UploadFile uploader = new();
            UploadData data;
            Bitmap bm = new Bitmap("C:\\Users\\utente.elettrico\\Desktop\\Test.png");
            uploader.TryUploadImage(bm, out data);
#endif
        }
    }
}
