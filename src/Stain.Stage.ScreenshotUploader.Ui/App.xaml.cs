using Prism.DryIoc;
using Prism.Ioc;
using Stain.Stage.ScreenshotUploader.Ui.Dialogs;
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterDialogWindow<FullScreenDialogWindow>();
            containerRegistry.RegisterDialog<CaptureDialog>();  
        }
    }
}
