using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

using Stain.Stage.ScreenshotUploader.Screenshot;
using Stain.Stage.ScreenshotUploader.Uploader;

namespace Stain.Stage.ScreenshotUploader.Ui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Bitmap ScreenShot = new Bitmap("C:\\Users\\utente.elettrico\\Desktop\\Test.png");
      
        public MainWindow() {
            InitializeComponent();
        }

        private void newScreenshot_Click(object sender, RoutedEventArgs e) {
            screenshot = Screenshot.Screenshot.Capture();
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            screenshot.Save(path);
            ScreenshotPreview.Source = new BitmapImage(new Uri(path));
        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            screenshot = Screenshot.ImageEditor.PaintEdit(screenshot);
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            screenshot.Save(path);
            ScreenshotPreview.Source = new BitmapImage(new Uri(path));
        }
      
        private void Upload_Click(object sender, RoutedEventArgs e) {
            using UploadFile uploader = new();
            UploadData data;
            uploader.TryUploadImage(ScreenShot, out data);

            Link.Text = data.Link;
        }

        private void Open_Click(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start(Link.Text);
        }
    }
}
