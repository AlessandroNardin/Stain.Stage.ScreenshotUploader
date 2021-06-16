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
        private Bitmap _screenShot = new Bitmap("C:\\Users\\utente.elettrico\\Desktop\\Test.png");

        public MainWindow() {
            InitializeComponent();
        }

        private void newScreenshot_Click(object sender, RoutedEventArgs e) {
            _screenShot = Screenshot.Screenshot.Capture();
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            _screenShot.Save(path);
            ScreenshotPreview.Source = new BitmapImage(new Uri(path));
        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            _screenShot = Screenshot.ImageEditor.PaintEdit(_screenShot);
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            _screenShot.Save(path);
            ScreenshotPreview.Source = new BitmapImage(new Uri(path));
        }

        private void Upload_Click(object sender, RoutedEventArgs e) {
            using UploadFile uploader = new();
            UploadData data;
            uploader.TryUploadImage(_screenShot, out data);

            Link.Text = data.Link;
        }

        private void Open_Click(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start(Link.Text);
        }
    }
}
