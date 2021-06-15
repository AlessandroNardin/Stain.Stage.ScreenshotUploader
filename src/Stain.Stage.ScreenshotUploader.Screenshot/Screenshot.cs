using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace Stain.Stage.ScreenshotUploader.Screenshot {
    /// <summary>
    /// A Class containing two methods which allows to capture the entire screen or just one part of it
    /// </summary>
    public class Screenshot {
        const int SwHide = 0;
        const int SwShow = 5;

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        /// <summary>
        /// Returns a bitmap object containing a screenshot of the entire screen.
        /// </summary>
        public static Bitmap Capture(){
            // Creates the default points necessary to determine the screen size, and the starting point on the destination image.
            Point imageDestinationPoint = new Point(0, 0);
            Point upperLeftCorner = new Point(0, 0);
            Point bottomRightCorner = new Point(Convert.ToInt32(SystemParameters.PrimaryScreenWidth), Convert.ToInt32(SystemParameters.PrimaryScreenHeight));

            // Determines the size of the screen in pixel
            int sizeWidth = bottomRightCorner.X - upperLeftCorner.X;
            int sizeHeigth = bottomRightCorner.Y - upperLeftCorner.Y;
            Size dimension = new Size(sizeWidth, sizeHeigth);

            // Hides the console Window
            var handle = GetConsoleWindow();
            ShowWindow(handle, SwHide);

            // Takes the screenshot
            Bitmap screenshot = new Bitmap(sizeWidth,sizeHeigth);
            Graphics graph = Graphics.FromImage(screenshot);
            graph.CopyFromScreen(upperLeftCorner, imageDestinationPoint, dimension);

            // Shows the console Window
            ShowWindow(handle, SwShow);
#if DEBUG
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.jpg");
            screenshot.Save(@path);
#endif
            return screenshot;
        }

        /// <summary>
        /// Returns a bitmap object containing a screenshot of a portion of the screen, determined by the parameters.
        /// </summary>
        /// <param name="upperLeftCorner">Upper left corner of the part of the screen to capture.</param>
        /// <param name="bottomRightCorner">Bottom right corner of the part of the screen to capture.</param>
        public static Bitmap Capture(Point upperLeftCorner, Point bottomRightCorner) {
            // Creates the default point necessary to determine the starting point on the destination image.
            Point ImageDestinationPoint = new Point(0, 0);

            // Determines the size of the screen in pixel
            int sizeWidth = bottomRightCorner.X - upperLeftCorner.X;
            int sizeHeigth = bottomRightCorner.Y - upperLeftCorner.Y;
            Size dimension = new Size(sizeWidth, sizeHeigth);

            // Hides the console Window
            var handle = GetConsoleWindow();
            ShowWindow(handle, SwHide);

            // Takes the screenshot
            Bitmap screenshot = new Bitmap(sizeWidth, sizeHeigth);
            Graphics graph = Graphics.FromImage(screenshot);
            graph.CopyFromScreen(upperLeftCorner, ImageDestinationPoint, dimension);

            // Shows the console Window
            ShowWindow(handle, SwShow);
#if DEBUG
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.jpg");
            screenshot.Save(@path);            
#endif
            return screenshot;
        }

    }
}
