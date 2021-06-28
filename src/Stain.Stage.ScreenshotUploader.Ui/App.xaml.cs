using Prism.DryIoc;
using Prism.Ioc;
using Stain.Stage.ScreenshotUploader.Ui.Dialogs;
using Stain.Stage.ScreenshotUploader.Screenshot.Interfaces;
using Stain.Stage.ScreenshotUploader.Ui.Views;
using System.Windows;

namespace Stain.Stage.ScreenshotUploader.Ui {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication {
        protected override Window CreateShell() {
            return Container.Resolve<MainWindowView>();
            //INotification inst = Container.Resolve<INotification>();
        }

            containerRegistry.RegisterDialogWindow<FullScreenDialogWindow>();
            containerRegistry.RegisterDialog<CaptureDialog, CaptureDialogViewModel>();
            moduleCatalog.AddModule<ScreenshotModule>();
        }
    }
}
