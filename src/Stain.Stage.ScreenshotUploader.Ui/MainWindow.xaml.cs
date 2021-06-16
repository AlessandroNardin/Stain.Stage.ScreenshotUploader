using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Stain.Stage.ScreenshotUploader.Uploader;

namespace Stain.Stage.ScreenshotUploader.Ui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        private Bitmap _screenShot;

        public MainWindow() {
            InitializeComponent();
        }

        private void newScreenshot_Click(object sender, RoutedEventArgs e) {
            _screenShot = Screenshot.Screenshot.Capture();
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            _screenShot.Save(path);
            ScreenshotPreview.Source = new BitmapImage(new Uri(path));
        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            _screenShot = Screenshot.ImageEditor.PaintEdit(_screenShot);
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            _screenShot.Save(path);
            ScreenshotPreview.Source = new BitmapImage(new Uri(path));
        }

        private void Upload_Click(object sender, RoutedEventArgs e) {
            UploadData data;
            UploadFile.Instance.TryUploadImage(_screenShot, out data);

            Link.Text = data.Link;
        }

        private void Open_Click(object sender, RoutedEventArgs e) {
            if(Link.Text == "")
                return;
            System.Diagnostics.Process.Start(Link.Text);
        }
    }
}
