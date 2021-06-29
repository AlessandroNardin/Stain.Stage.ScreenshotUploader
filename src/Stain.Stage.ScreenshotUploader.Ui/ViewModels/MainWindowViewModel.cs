using Microsoft.Toolkit.Uwp.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Stain.Stage.ScreenshotUploader.Screenshot;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using Stain.Stage.ScreenshotUploader.Uploader;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

namespace Stain.Stage.ScreenshotUploader.Ui.ViewModels {
    public class MainWindowViewModel : BindableBase{
        private static Bitmap imageBitmap;

        private BitmapSource _preview;
        public BitmapSource Preview {
            get { return _preview; }
            set { SetProperty(ref _preview, value); }
        }

        private IEventAggregator _eventAggregator;

        private IDialogService _dialogService;       

        // The definition of the delegate Commands binded to diffren buttons in the view
        public DelegateCommand NewScreenshotCommand { get; private set; }
        public DelegateCommand NewWindowedScreenshotCommand { get; private set; }
        public DelegateCommand EditCommand { get; private set; }
        public DelegateCommand UploadCommand { get; private set; }
        public DelegateCommand CopyCommand { get; private set; }
        public DelegateCommand OpenCommand { get; private set; }

        // Contains the link to the uploaded image
        private string _link;
        public string UploadedScreenshotLink {
            get { return _link; }
            set { SetProperty(ref _link, value); }
        }

        // Tells if the screenshot has been taken
        private bool _screenshotted = false;
        public bool Screenshotted {
            get { return _screenshotted; }
            set { SetProperty(ref _screenshotted, value); }
        }

        // Tells if the image has been uploaded
        private bool _uploaded = false;
        public bool Uploaded {
            get { return _uploaded; }
            set { SetProperty(ref _uploaded, value); }
        }

        // Constructor
        public MainWindowViewModel(IEventAggregator eventAggregator, IDialogService dialogService) {
            ToastNotificationManagerCompat.OnActivated += OnOpenedFromNotification;
            SetPreview(new Bitmap(@"C:\Users\utente.elettrico.STAIN\source\repos\Stain.Stage.ScreenshotUploader\src\Stain.Stage.ScreenshotUploader.Ui\Default.png"));

            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ClickOnIcon>().Subscribe(NewScreenshot);

            NewScreenshotCommand = new DelegateCommand(NewScreenshot);
            NewWindowedScreenshotCommand = new DelegateCommand(NewWindowedScreenshot);
            EditCommand = new DelegateCommand(Edit, IsScreenshotted).ObservesProperty(() => Screenshotted);
            UploadCommand = new DelegateCommand(Upload, IsScreenshotted).ObservesProperty(() => Screenshotted);
            CopyCommand = new DelegateCommand(Copy, IsUploaded).ObservesProperty(()=> Uploaded);
            OpenCommand = new DelegateCommand(Open, IsUploaded).ObservesProperty(() => Uploaded);
        }

        // The method that allows to set the preview;
        private void SetPreview(Bitmap image) {
            var bitmapData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, image.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                image.HorizontalResolution, image.VerticalResolution,
                System.Windows.Media.PixelFormats.Pbgra32, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            image.UnlockBits(bitmapData);

            Preview = bitmapSource;
        }

        //The Method that takes allows the user to take a screenshot of a portion of the screen
        private void NewWindowedScreenshot() {
            //Publish the event that communicates the start of the screenshot procedure
            _eventAggregator.GetEvent<ScreenshotProcedureStarted>().Publish();
            Thread.Sleep(250);

            // Shows the dialog and allows the user to select a portion of the screen, than savesthe result of the dialog inside to point structs
            Point topLeftPoint = new Point();
            Point bottomLeftPoint = new Point();
            var param = new DialogParameters();
            param.Add("image", Screenshot.Screenshot.Capture());
            _dialogService.ShowDialog("CaptureDialog",param , result => {
                topLeftPoint = result.Parameters.GetValue<Point>("topLeftPoint");
                bottomLeftPoint = result.Parameters.GetValue<Point>("bottomRightPoint");
            });

            // Takes the screenshot of the portion of the scren specified by the user
            imageBitmap = Screenshot.Screenshot.CapturePortion(topLeftPoint,bottomLeftPoint);
            SetPreview(imageBitmap);

            // Notifies that the screenshot has ben taken
            ScreenshotNotification.ShowNotificationWithImageAndTwoButtons("Screenshot captured", "edit", imageBitmap, "Edit with Paint", "edit", "Upload", "upload");

            // Publish the event that communicates the end of the screenshot procedure
            _eventAggregator.GetEvent<ScreenshotProcedureEnded>().Publish();

            //Updates the Screenshotted property
            Screenshotted = true;
        }

        // Methods that returns the "Is" Properties
        private bool IsUploaded() {
            return Uploaded;
        }

        private bool IsScreenshotted() {
            return Screenshotted;
        }

        //Opens the link where the image has been uploaded
        private void Open() {
            System.Diagnostics.Process.Start(UploadedScreenshotLink);
        }

        //Saves the link into the clipboard
        private void Copy() {
            Clipboard.SetText(UploadedScreenshotLink);
        }

        //Uploads the image
        private void Upload() {
            UploadData data;
            UploadFile.Instance.TryUploadImage(imageBitmap, out data);
            UploadedScreenshotLink = data.Link;

            Uploaded = true;
        }

        //Opens the image in paint
        private void Edit() {
            imageBitmap = ImageEditor.PaintEdit(imageBitmap);

            SetPreview(imageBitmap);
        }

        // Takes a screenshot of the entire screen
        private void NewScreenshot() {
            // Publish the event that communicates the start of the screenshot procedure
            _eventAggregator.GetEvent<ScreenshotProcedureStarted>().Publish();
            Thread.Sleep(250);
            imageBitmap = Screenshot.Screenshot.Capture();

            SetPreview(imageBitmap);

            // Notifies that the screenshot has ben taken
            ScreenshotNotification.ShowNotificationWithImageAndTwoButtons("Screenshot captured","edit",imageBitmap,"Edit with Paint","edit","Upload","upload");

            // Publish the event that communicates the end of the screenshot procedure
            _eventAggregator.GetEvent<ScreenshotProcedureEnded>().Publish();

            //Updates the Screenshotted property
            Screenshotted = true;
        }

        // Gets thrown when the user clicks on a notification 
        private void OnOpenedFromNotification(ToastNotificationActivatedEventArgsCompat e) {
            ToastArguments args = ToastArguments.Parse(e.Argument);

            // Checks the content of the argument
            if(args.Contains("upload")) {
                // If the argument contains the "upload" term the Upload method is called.
                Upload();
                // Opens the browser at the address where the image has been uploaded.
                System.Diagnostics.Process.Start(this.UploadedScreenshotLink);
            } else if(args.Contains("discard")) {
                // If the argument contains the "discard" term does nothing.
            } else {
                // Otherwise the Edit method is called.
                Edit();
                // Shows the 
                ScreenshotNotification.ShowNotificationWithImageAndTwoButtons("Image Modified", "edit", imageBitmap, "Discard", "discard", "Upload", "upload");
            }
        }

    }
}
