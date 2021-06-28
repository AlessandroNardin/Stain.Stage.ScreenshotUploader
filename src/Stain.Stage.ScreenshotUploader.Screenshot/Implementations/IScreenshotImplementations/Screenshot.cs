using Stain.Stage.ScreenshotUploader.Screenshot.Interfaces;
using System;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;


namespace Stain.Stage.ScreenshotUploader.Screenshot.Implementations.IScreenshotImplementations {
    /// <summary>
    /// A Class containing two methods which allows to capture the entire screen or just one part of it
    /// </summary>
     class Screenshot : IScreenshot{
        /// <summary>
        /// Returns a bitmap object containing a screenshot of the entire screen.
        /// </summary>
        public Bitmap Capture(){
            // Creates the default points necessary to determine the screen size, and the starting point on the destination image.
            Point imageDestinationPoint = new Point(0, 0);
            Point upperLeftCorner = new Point(0, 0);
            Point bottomRightCorner = new Point(Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth), Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight));

            // Determines the size of the screen in pixel
            int sizeWidth = bottomRightCorner.X - upperLeftCorner.X;
            int sizeHeigth = bottomRightCorner.Y - upperLeftCorner.Y;
            Size dimension = new Size(sizeWidth, sizeHeigth);

            // Takes the screenshot
            Bitmap screenshot = new Bitmap(sizeWidth,sizeHeigth);
            Graphics graph = Graphics.FromImage(screenshot);
            graph.CopyFromScreen(upperLeftCorner, imageDestinationPoint, dimension);
#if DEBUG
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.png");
            screenshot.Save(@path);
#endif
            return screenshot;
        }

        /// <summary>
        /// Returns a bitmap object containing a screenshot of a portion of the screen, determined by the parameters.
        /// </summary>
        /// <param name="upperLeftCorner">Upper left corner of the part of the screen to capture.</param>
        /// <param name="bottomRightCorner">Bottom right corner of the part of the screen to capture.</param>
        public static Bitmap CapturePortion(Point upperLeftCorner, Point bottomRightCorner) {
            // Creates the default point necessary to determine the starting point on the destination image.
            Point imageDestinationPoint = new Point(0, 0);

            // Determines the size of the screen in pixel
            int sizeWidth = bottomRightCorner.X - upperLeftCorner.X;
            int sizeHeigth = bottomRightCorner.Y - upperLeftCorner.Y;
            Size dimension = new Size(sizeWidth, sizeHeigth);

            // Takes the screenshot
            Bitmap screenshot = new Bitmap(sizeWidth, sizeHeigth);
            Graphics graph = Graphics.FromImage(screenshot);
            graph.CopyFromScreen(upperLeftCorner, imageDestinationPoint, dimension);

#if DEBUG
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.png");
            screenshot.Save(@path);
#endif
            return screenshot;
        }

    }
}
