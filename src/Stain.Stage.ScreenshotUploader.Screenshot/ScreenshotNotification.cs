using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Screenshot {
    public static class ScreenshotNotification {
        public static void ShowNotificationWithImageAndTwoButtons(string notificationMessage, string notificationArgument, string imagePath, string button1Message, string button1Argument, string button2Message, string button2Argument) {
            new ToastContentBuilder()
                .AddArgument(notificationArgument)
                .AddText(notificationMessage)
                .AddHeroImage(new Uri(imagePath))
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
