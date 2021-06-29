using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Drawing;

namespace Stain.Stage.ScreenshotUploader.Screenshot {
    public static class ScreenshotNotification {
        public static void ShowNotificationWithImageAndTwoButtons(string notificationMessage, string notificationArgument, Bitmap image, string button1Message, string button1Argument, string button2Message, string button2Argument) {
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            image.Save(tempPath);

            // Shows the Toast notification
            new ToastContentBuilder()
                .AddArgument(notificationArgument)
                .AddText(notificationMessage)
                .AddHeroImage(new Uri(tempPath))
                .AddButton(new ToastButton()
                            .SetContent(button1Message)
                            .AddArgument(button1Argument))
                .AddButton(new ToastButton()
                            .SetContent(button2Message)
                            .AddArgument(button2Argument))
                .Show();
        }
    }
}
