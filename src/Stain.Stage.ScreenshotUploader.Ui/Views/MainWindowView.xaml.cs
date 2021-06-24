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
            eventAggregator.GetEvent<ScreenshotProcedureEnded>().Subscribe(OnScreenshotProcedureEnded);
            InitializeComponent();
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon(@"..\..\..\Icona.ico");
            ni.Visible = true;
            ni.Click +=
                delegate (object sender, EventArgs args) {
                    eventAggregator.GetEvent<ClickOnIcon>().Publish();
                };
            ni.DoubleClick +=
                delegate (object sender, EventArgs args) {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        protected override void OnStateChanged(EventArgs e) {
            if(WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void OnScreenshotProcedureStarted() {
            WindowState = WindowState.Minimized;
        }
        private void OnScreenshotProcedureEnded() {
            WindowState = WindowState.Normal;
        }
    }
}
