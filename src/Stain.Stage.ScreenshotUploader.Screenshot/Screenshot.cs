using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;



namespace Stain.Stage.ScreenshotUploader.Screenshot {
    public class Screenshot {

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;


        /// <summary>
        /// Returns a bitmap object containing a screenshot of the entire screen.
        /// </summary>
        public static string Capture(){

            // Creates the default points necessary to determine the screen size, and the starting point on the destination image.
            Point ImageDestinationPoint = new Point(0, 0);
            Point UpperLeftCorner = new Point(0, 0);
            Point BottomRightCorner = new Point(Convert.ToInt32(SystemParameters.PrimaryScreenWidth), Convert.ToInt32(SystemParameters.PrimaryScreenHeight));

            // Determines the size of the screen in pixel
            int SizeWidth = BottomRightCorner.X - UpperLeftCorner.X;
            int SizeHeigth = BottomRightCorner.Y - UpperLeftCorner.Y;
            Size dimension = new Size(SizeWidth, SizeHeigth);

            // Hides the console Window
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            // Takes the screenshot
            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bm);
            g.CopyFromScreen(UpperLeftCorner, ImageDestinationPoint, dimension);

            // Shows the console Window
            ShowWindow(handle, SW_SHOW);

#if DEBUG
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.jpg");
            bm.Save(@path);

            return path;
#else
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            bm.Save(@path);
            return path;
#endif
        }



        /// <summary>
        /// Returns a bitmap object containing a screenshot of a portion of the screen, determined by the parameters.
        /// </summary>
        /// <param name="UpperLeftCorner">Upper left corner of the part of the screen to capture.</param>
        /// <param name="BottomRightCorner">Bottom right corner of the part of the screen to capture.</param>
        public static string Capture(Point UpperLeftCorner, Point BottomRightCorner) {

            // Creates the default point necessary to determine the starting point on the destination image.
            Point ImageDestinationPoint = new Point(0, 0);

            // Determines the size of the screen in pixel
            int SizeWidth = BottomRightCorner.X - UpperLeftCorner.X;
            int SizeHeigth = BottomRightCorner.Y - UpperLeftCorner.Y;
            Size dimension = new Size(SizeWidth, SizeHeigth);

            // Hides the console Window
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            // Takes the screenshot
            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bm);
            g.CopyFromScreen(UpperLeftCorner, ImageDestinationPoint, dimension);

            // Shows the console Window
            ShowWindow(handle, SW_SHOW);

#if DEBUG
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{Guid.NewGuid()}.jpg");
            bm.Save(@path);

            return path;
#else
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            bm.Save(@path);
            return path;
#endif
        }

    }
}
