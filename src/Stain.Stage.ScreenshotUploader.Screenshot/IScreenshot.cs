using System.Drawing;

namespace Stain.Stage.ScreenshotUploader.Screenshot {
    public interface IScreenshot {
        public Bitmap Capture();
    }
}
