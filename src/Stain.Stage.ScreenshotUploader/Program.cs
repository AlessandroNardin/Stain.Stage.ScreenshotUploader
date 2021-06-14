using Microsoft.Toolkit.Uwp.Notifications;
using Stain.Stage.ScreenshotUploader.Screenshot;
using System;
using System.Diagnostics;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Stain.Stage.ScreenshotUploader {
    public static class Program {
        public static void Main(string[] args) {
            ToastNotificationManagerCompat.OnActivated += OnOpenedFromNotification;

            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(100, 100);
            ScreenshotNotification.NotifyScreenshot(Screenshot.Screenshot.Capture(pt1,pt2));
        }

        private static void OnOpenedFromNotification(ToastNotificationActivatedEventArgsCompat e) {
            /*Console.WriteLine(e);
            Process.Start("C:\\WINDOWS\\system32\\mspaint.exe");*/
        }
    }
}
