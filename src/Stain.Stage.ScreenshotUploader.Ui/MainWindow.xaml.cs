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
        private string path;
        public string ImagePath {
            get { return path; }
            set {
                path = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged(string path=null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(path));
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
        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            //control on the screenshot existance
            if(_screenShot == null)
                return;

            _screenShot = Screenshot.ImageEditor.PaintEdit(_screenShot);
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            _screenShot.Save(tempPath);
            ImagePath = tempPath;
        }

        private void Upload_Click(object sender, RoutedEventArgs e) {
            //control on the screenshot existance
            if(_screenShot == null)
                return;

            UploadData data;
            UploadFile.Instance.TryUploadImage(_screenShot, out data);
            Link.Text = data.Link;
        }

        private void Open_Click(object sender, RoutedEventArgs e) {
            //control on the link existance before open
            if(Link.Text == "")
                return;
            System.Diagnostics.Process.Start(Link.Text);
        }

        private void Copy_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText(Link.Text);
        }
    }
}
