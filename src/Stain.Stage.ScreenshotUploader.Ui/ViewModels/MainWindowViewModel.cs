using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using Stain.Stage.ScreenshotUploader.Uploader;
using System;
using System.Drawing;
using System.Threading;
using System.Windows;

namespace Stain.Stage.ScreenshotUploader.Ui.ViewModels {
    public class MainWindowViewModel : BindableBase{
        private static Bitmap imageBitmap;

        public IEventAggregator _eventAggregator { get; private set; }
        public DelegateCommand NewScreenshotCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand UploadCommand { get; set; }
        public DelegateCommand CopyCommand { get; set; }
        public DelegateCommand OpenCommand { get; set; }
    
        private string _imagePath = @"C:\Users\utente.elettrico.STAIN\source\repos\Stain.Stage.ScreenshotUploader\Default.png";
        public string ImagePath {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }

        private string _link;
        public string UploadedScreenshotLink {
            get { return _link; }
            set { SetProperty(ref _link, value); }
        }

        private bool _screenshotted = false;
        public bool Screenshotted {
            get { return _screenshotted; }
            set { SetProperty(ref _screenshotted, value); }
        }

        private bool _uploaded = false;
        public bool Uploaded {
            get { return _uploaded; }
            set { SetProperty(ref _uploaded, value); }
        }

        public MainWindowViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            NewScreenshotCommand = new DelegateCommand(NewScreenshot);
            EditCommand = new DelegateCommand(Edit, IsScreenshotted).ObservesProperty(() => Screenshotted);
            UploadCommand = new DelegateCommand(Upload, IsScreenshotted).ObservesProperty(() => Screenshotted);
            CopyCommand = new DelegateCommand(Copy, IsUploaded).ObservesProperty(()=> Uploaded);
            OpenCommand = new DelegateCommand(Open, IsUploaded).ObservesProperty(() => Uploaded);

        }

        private bool IsUploaded() {
            return Uploaded;
        }

        private bool IsScreenshotted() {
            return Screenshotted;
        }

        private void Open() {
            System.Diagnostics.Process.Start(UploadedScreenshotLink);
        }

        private void Copy() {
            Clipboard.SetText(UploadedScreenshotLink);
        }

        private void Upload() {
            UploadData data;
            UploadFile.Instance.TryUploadImage(imageBitmap, out data);
            UploadedScreenshotLink = data.Link;

            Uploaded = true;
        }

        private void Edit() {
            imageBitmap = Screenshot.ImageEditor.PaintEdit(imageBitmap);

            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            imageBitmap.Save(tempPath);
            ImagePath = tempPath;
        }

        private void NewScreenshot() {
            _eventAggregator.GetEvent<ScreenshotProcedureStarted>().Publish();

            Thread.Sleep(250);
            imageBitmap = Screenshot.Screenshot.Capture();

            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            imageBitmap.Save(tempPath);
            ImagePath = tempPath;
            //Screenshot.ScreenshotNotification.Notify(ImagePath);

            _eventAggregator.GetEvent<ScreenshotProcedureEnded>().Publish();

            Screenshotted = true;
        }

    }
}
