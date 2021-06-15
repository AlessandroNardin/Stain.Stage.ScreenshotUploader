using Stain.Stage.ScreenshotUploader.Screenshot;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Stain.Stage.ScreenshotUploader {
    public static class Program {


        public static void Main(string[] args) {

            //Captures a screenshot and edits it with paint.
            String screenshot = Screenshot.Screenshot.Capture();
            Bitmap editedImage = Screenshot.ImageEditor.PaintEdit(screenshot);
#if DEBUG 
            //Save the edited image on the desktop for test purposes.
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.jpg");
            editedImage.Save(@path);
#endif
        }


    }
}
