using Prism.Events;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

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
            new NotifyIcon(eventAggregator);
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

    internal class NotifyIcon : Form{
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private ContextMenu menu;
        private MenuItem windowedScreenshotItem;
        private MenuItem openItem;
        private MenuItem closeItem;
        private IEventAggregator _eventAggregator;
        private System.ComponentModel.IContainer components;

        public NotifyIcon(IEventAggregator eventAggregator) {

            components = new System.ComponentModel.Container();
            menu = new ContextMenu();
            windowedScreenshotItem = new MenuItem();
            openItem = new MenuItem();
            closeItem = new MenuItem();
            _eventAggregator = eventAggregator;

            // Initialize menu
            menu.MenuItems.AddRange(
                        new MenuItem[] { windowedScreenshotItem, openItem, closeItem });

            // Initialize windowedScreenshotItem
            windowedScreenshotItem.Index = 0;
            windowedScreenshotItem.Text = "New windowed screenshot";
            windowedScreenshotItem.Click += new EventHandler(this.menu_NewWindowedScreenshot);

            // Initialize openItem
            openItem.Index = 1;
            openItem.Text = "Open";
            openItem.Click += new EventHandler(this.menu_Open);

            // Initialize closeItem
            closeItem.Index = 2;
            closeItem.Text = "Close";
            closeItem.Click += new EventHandler(this.menu_Close);

            // Set up how the form should be displayed.
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Text = "Screenshot Uploader";

            // Create the NotifyIcon.
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon.Icon = new Icon(@"..\..\..\Icona.ico");

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon.ContextMenu = this.menu;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon.Text = "Screenshot Uploader";
            notifyIcon.Visible = true;

            // Handle the DoubleClick event to activate the form.
            notifyIcon.DoubleClick += NotifyIcon_Click;
        }

        private void menu_Close(object sender, EventArgs e) {
            _eventAggregator.GetEvent<ClickOnIcon>().Publish("close");
        }

        private void NotifyIcon_Click(object sender, EventArgs e) {
            _eventAggregator.GetEvent<ClickOnIcon>().Publish("newScreenshot");
        }

        protected override void Dispose(bool disposing) {
            // Clean up any components being used.
            if(disposing)
                if(components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        private void menu_NewWindowedScreenshot(object Sender, EventArgs e) {
            _eventAggregator.GetEvent<ClickOnIcon>().Publish("newWindowedScreenshot");
        }

        private void menu_Open(object sender, EventArgs e) {
            _eventAggregator.GetEvent<ClickOnIcon>().Publish("open");
        }
    }
}
