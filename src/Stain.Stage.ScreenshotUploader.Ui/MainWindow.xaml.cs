using Stain.Stage.ScreenshotUploader.Screenshot;
using Stain.Stage.ScreenshotUploader.Uploader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stain.Stage.ScreenshotUploader.Ui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Bitmap ScreenShot = new Bitmap("C:\\Users\\utente.elettrico\\Desktop\\Test.png");
        public MainWindow() {
            InitializeComponent();
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
