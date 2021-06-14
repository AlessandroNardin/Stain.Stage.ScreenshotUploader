
using Microsoft.Toolkit.Uwp.Notifications;
using System.Drawing;

namespace Stain.Stage.ScreenshotUploader.Screenshot {
    public class ScreenshotNotification {
        public static void NotifyScreenshot(string path) {
            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Screenshot catturato")
                .AddHeroImage(new System.Uri(path))
                .AddButton(new ToastButton()
                    .SetContent("Elimina")
                    .AddArgument("action", "reply")
                    .SetBackgroundActivation())

                .AddButton(new ToastButton()
                    .SetContent("Modifica con Paint")
                    .AddArgument("action", "like")
                    .SetBackgroundActivation())

                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 5, your TFM must be net5.0-windows10.0.17763.0 or greater
        }
    }
}
