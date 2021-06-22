using Prism.Ioc;
using Prism.Modularity;
using Stain.Stage.ScreenshotUploader.Screenshot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenshotService = Stain.Stage.ScreenshotUploader.Screenshot.Implementations.IScreenshotImplementations.Screenshot;

namespace Stain.Stage.ScreenshotUploader.Screenshot {
    public class ScreenshotModule : IModule {
        public void OnInitialized(IContainerProvider containerProvider) {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.Register<IScreenshot, ScreenshotService>();
        }
    }
}
