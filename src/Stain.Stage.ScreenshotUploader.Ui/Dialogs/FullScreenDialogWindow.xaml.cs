using Prism.Services.Dialogs;
using System.Windows;

namespace Stain.Stage.ScreenshotUploader.Ui.Dialogs {
    /// <summary>
    /// Logica di interazione per FullScreenDialogWindow.xaml
    /// </summary>
    public partial class FullScreenDialogWindow : Window, IDialogWindow{
        //The constructor  of the windows which  will contain the dialogs.
        public FullScreenDialogWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            Activate();
            Topmost = true;
            Focus();
        }

        public IDialogResult Result { get; set; }
    }
}
