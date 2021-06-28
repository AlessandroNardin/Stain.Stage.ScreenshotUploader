using Prism.Events;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using System.Windows.Controls;

namespace Stain.Stage.ScreenshotUploader.Ui.Dialogs {
    /// <summary>
    /// Logica di interazione per CaptureDialog.xaml
    /// </summary>
    public partial class CaptureDialog : UserControl {
        private IEventAggregator _eventAggregator;

        public CaptureDialog(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            InitializeComponent();
            MouseDown += CaptureDialog_MouseDown;
            MouseUp += CaptureDialog_MouseUp;
            MouseMove += CaptureDialog_MouseMove;
        }

        private void CaptureDialog_MouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            _eventAggregator.GetEvent<MouseMoved>().Publish();
        }

        private void CaptureDialog_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            _eventAggregator.GetEvent<MouseDown>().Publish();
        }

        private void CaptureDialog_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            _eventAggregator.GetEvent<MouseUp>().Publish();
        }
    }
}
