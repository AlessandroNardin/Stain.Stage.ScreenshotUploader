using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Stain.Stage.ScreenshotUploader.Uploader;
using System.Windows.Data;
using System.ComponentModel;

namespace Stain.Stage.ScreenshotUploader.Ui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged{
        private Bitmap _screenShot;

        private string imagePath = @"C:\Users\utente.elettrico.STAIN\source\repos\Stain.Stage.ScreenshotUploader\Default.png";
        private string uploadedScreenshotLink;
        private bool isScreenshotTaken = false;
        private bool isUploaded = false;
        public string ImagePath {
            get { return imagePath; }
            set {
                imagePath = value;
                OnPropertyChanged();
            }
        }

        public string UploadedScreenshotLink {
            get { return uploadedScreenshotLink; }
            set {
                uploadedScreenshotLink = value;
                OnPropertyChanged();
            }
        }

        public bool IsScreenshotTaken {
            get { return isScreenshotTaken; }
            set {
                isScreenshotTaken = value;
                OnPropertyChanged();
            }
        }

        public bool IsUploaded {
            get { return isUploaded; }
            set {
                isUploaded = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged(string arg=null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(arg));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
        }

        private void newScreenshot_Click(object sender, RoutedEventArgs e) {
            _screenShot = Screenshot.Screenshot.Capture();
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            _screenShot.Save(tempPath);
            ImagePath = tempPath;
            IsScreenshotTaken = true;
        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            _screenShot = Screenshot.ImageEditor.PaintEdit(_screenShot);
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            _screenShot.Save(tempPath);
            ImagePath = tempPath;
        }

        private void Upload_Click(object sender, RoutedEventArgs e) {
            UploadData data;
            UploadFile.Instance.TryUploadImage(_screenShot, out data);
            UploadedScreenshotLink = data.Link;
            IsUploaded = true;
        }

        private void Open_Click(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start(UploadedScreenshotLink);
        }

        private void Copy_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText(UploadedScreenshotLink);
        }
    }
}
