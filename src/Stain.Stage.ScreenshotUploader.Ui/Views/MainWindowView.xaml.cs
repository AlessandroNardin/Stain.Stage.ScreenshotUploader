using Microsoft.Toolkit.Uwp.Notifications;
using Prism.Events;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using Stain.Stage.ScreenshotUploader.Ui.ViewModels;
using Stain.Stage.ScreenshotUploader.Uploader;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows;

namespace Stain.Stage.ScreenshotUploader.Ui.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView {
        public MainWindowView(IEventAggregator eventAggregator) {
            eventAggregator.GetEvent<ScreenshotProcedureStarted>().Subscribe(OnScreenshotProcedureStarted);
            eventAggregator.GetEvent<ScreenshotProcedureEnded>().Subscribe(OnScreenshotProcedureEnded);
            InitializeComponent();
        }

        private void OnScreenshotProcedureStarted() {
            WindowState = WindowState.Minimized;
        }
        private void OnScreenshotProcedureEnded() {
            WindowState = WindowState.Normal;
        }
    }
}
