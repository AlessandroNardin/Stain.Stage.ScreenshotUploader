using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Stain.Stage.ScreenshotUploader.Screenshot {
    public static class ImageEditor {
        /// <summary>
        /// Opens an Image with Paint, after paint is closed returns the Edited bitmap Image.
        /// </summary>
        /// <returns>The bitmap object of the edited image.</returns>
       public static Bitmap PaintEdit(Bitmap screenshot) {
           
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            screenshot.Save(@path);

            //Opens paint with the image.
            Process paint = Process.Start("mspaint", @path);

            //Waits the closure of Paint.
            Console.WriteLine("Currently editing the image on Paint, close the program to continue the process");
            while(!paint.HasExited) {
                Thread.Sleep(500);
            }

            //Converts the image into a Bitmap and returns it.
            return new Bitmap(path);
        }
    }
}
