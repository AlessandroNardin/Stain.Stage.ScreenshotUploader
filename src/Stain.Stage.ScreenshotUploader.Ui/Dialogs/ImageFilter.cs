using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Ui.Dialogs {
    public static class ImageFilter {
        public static Image FilteredImage(this Image inputImage) {
            Bitmap outputImage = new Bitmap(inputImage.Width, inputImage.Height);
            Graphics imageGraphics = Graphics.FromImage(outputImage);
            imageGraphics.DrawImage(inputImage, 0, 0);
            imageGraphics.FillRectangle(new SolidBrush(Color.Red), 0, 0, outputImage.Width, outputImage.Height);

            return outputImage;
        }
    }
}
