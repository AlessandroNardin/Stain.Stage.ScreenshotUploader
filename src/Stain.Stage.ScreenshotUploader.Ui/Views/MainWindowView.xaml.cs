using Prism.Events;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using System;
using System.Windows;

namespace Stain.Stage.ScreenshotUploader.Ui.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView {
        public MainWindowView(IEventAggregator eventAggregator) {           
            eventAggregator.GetEvent<ScreenshotProcedureStarted>().Subscribe(OnScreenshotProcedureStarted);
            eventAggregator.GetEvent<ScreenshotProcedureEnded>().Subscribe(OnOpenWindow);
            eventAggregator.GetEvent<OpenWindow>().Subscribe(OnOpenWindow);
            InitializeComponent();
            IconTrayMenuView ni = new IconTrayMenuView(eventAggregator);
        }

        protected override void OnStateChanged(EventArgs e) {
            if(WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(e);
        }

        private void OnScreenshotProcedureStarted() {
            WindowState = WindowState.Minimized;
        }
        private void OnOpenWindow() {
            Show();
            WindowState = WindowState.Normal;
            Activate();
            Focus();
        }
    }
}
