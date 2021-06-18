using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Screenshot {
    public static class ScreenshotNotification {
        public static void Notify(string path) {
            new ToastContentBuilder()
                .AddArgument("edit")
                .AddText("Screenshot Captured")
                .AddHeroImage(new Uri(path))
                .AddButton(new ToastButton()
                            .SetContent("Edit with Paint")
                            .AddArgument("edit"))
                .AddButton(new ToastButton()
                            .SetContent("Upload on Imgur")
                            .AddArgument("upload"))
                .Show();
        }
    }
}
