using System;
using System.Drawing;
using System.Windows;
using Stain.Stage.ScreenshotUploader.Uploader;
using System.ComponentModel;
using System.Threading;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Foundation.Collections;

namespace Stain.Stage.ScreenshotUploader.Ui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged{
        private static Bitmap imageBitmap;
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
            ToastNotificationManagerCompat.OnActivated += OnOpenedFromNotification;

            InitializeComponent();
            DataContext = this;
        }

        private void OnOpenedFromNotification(ToastNotificationActivatedEventArgsCompat e) {
            ToastArguments args = ToastArguments.Parse(e.Argument);

            if(args.Contains("upload")) {
                UploadData data;
                UploadFile.Instance.TryUploadImage(imageBitmap, out data);
                UploadedScreenshotLink = data.Link;

                System.Diagnostics.Process.Start(this.UploadedScreenshotLink);
            }else if(args.Contains("discard")) {
            } else {
                imageBitmap = Screenshot.ImageEditor.PaintEdit(imageBitmap);

                string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
                imageBitmap.Save(tempPath);
                ImagePath = tempPath;
                new ToastContentBuilder()
                .AddText("Image Modified")
                .AddHeroImage(new Uri(imagePath))
                .AddButton(new ToastButton()
                            .SetContent("Discard")
                            .AddArgument("discard"))
                .AddButton(new ToastButton()
                            .SetContent("Upload on Imgur")
                            .AddArgument("upload"))
                .Show();
            }   
        }

        private void newScreenshot_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
            Thread.Sleep(250);
            imageBitmap = Screenshot.Screenshot.Capture();
            WindowState = WindowState.Normal;

            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            imageBitmap.Save(tempPath);
            ImagePath = tempPath;

            Screenshot.ScreenshotNotification.Notify(ImagePath);

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
