using Prism.Events;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using System.Windows;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;

namespace Stain.Stage.ScreenshotUploader.Ui.Views {
    /// <summary>
    /// Logica di interazione per IconTrayMenuView.xaml
    /// </summary>
    public partial class IconTrayMenuView : Window {
        private WinForms.NotifyIcon notifier = new WinForms.NotifyIcon();
        private IEventAggregator _eventAggregator;

        public IconTrayMenuView(IEventAggregator eventAggregator) {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            this.notifier.MouseDown += new WinForms.MouseEventHandler(notifier_MouseDown);
            this.notifier.Icon = new System.Drawing.Icon(@"..\..\..\Icona.ico"); ;
            this.notifier.Visible = true;
        }

        void notifier_MouseDown(object sender, WinForms.MouseEventArgs e) {
            if(e.Button == WinForms.MouseButtons.Right) {
                ContextMenu menu = (ContextMenu)FindResource("NotifierContextMenu");
                menu.IsOpen = true;
            }
            if(e.Button == WinForms.MouseButtons.Left) {
                _eventAggregator.GetEvent<ClickOnIcon>().Publish("newScreenshot");
            }
        }

        private void Menu_Open(object sender, RoutedEventArgs e) {
            _eventAggregator.GetEvent<ClickOnIcon>().Publish("open");
        }

        private void Menu_NewWindowedScreenshot(object sender, RoutedEventArgs e) {
            _eventAggregator.GetEvent<ClickOnIcon>().Publish("newWindowedScreenshot");
        }
    }
}

