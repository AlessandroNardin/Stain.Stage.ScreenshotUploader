using System;
using System.Drawing;
using System.Windows;
using Stain.Stage.ScreenshotUploader.Uploader;
using System.ComponentModel;
using System.Threading;

namespace Stain.Stage.ScreenshotUploader.Ui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged{
        private Bitmap imageBitmap;
        private string imagePath = @"C:\Users\utente.elettrico.STAIN\source\repos\Stain.Stage.ScreenshotUploader\Default.png";
        private string uploadedScreenshotLink;
        private bool isScreenshotTaken = false;
        private bool isUploaded = false;
        public event PropertyChangedEventHandler PropertyChanged;

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

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
        }

        private void newScreenshot_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
            Thread.Sleep(250);
            imageBitmap = Screenshot.Screenshot.Capture();
            WindowState = WindowState.Normal;

            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            imageBitmap.Save(tempPath);
            ImagePath = tempPath;

            IsScreenshotTaken = true;
        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            imageBitmap = Screenshot.ImageEditor.PaintEdit(imageBitmap);

            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            imageBitmap.Save(tempPath);
            ImagePath = tempPath;
        }

        private void Upload_Click(object sender, RoutedEventArgs e) {
            UploadData data;
            UploadFile.Instance.TryUploadImage(imageBitmap, out data);
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
