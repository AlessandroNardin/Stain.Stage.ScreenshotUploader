using Stain.Stage.ScreenshotUploader.Uploader;
using System;
using System.Drawing;
using Stain.Stage.ScreenshotUploader.Screenshot;

namespace Stain.Stage.ScreenshotUploader {
    public static class Program {

        public static void Main() {
            Console.WriteLine("START");

            //Captures a screenshot and edits it with paint.
            Point p1 = new(100, 100);
            Point p2 = new(500, 500);
            Bitmap screenshot = Screenshot.Screenshot.Capture(p1,p2);
            Bitmap editedImage = ImageEditor.PaintEdit(screenshot);

#if DEBUG
            //Save the edited image on the desktop for test purposes.
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.jpg");
            editedImage.Save(path);
#endif

            using UploadFile uploader = new();
            uploader.TryUploadImage(editedImage, out UploadData _);
        }
    }
}
