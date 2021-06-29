using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

namespace Stain.Stage.ScreenshotUploader.Ui.Dialogs {
    class CaptureDialogViewModel : BindableBase, IDialogAware {
        //The dimensions of the dialog, they depends on the screen resolution.
        public int Width { get; set; } = Convert.ToInt32(SystemParameters.PrimaryScreenWidth);
        public int Height { get; set; } = Convert.ToInt32(SystemParameters.PrimaryScreenHeight);

        private IEventAggregator _eventAggregator;

        public string Title => "Capture Dialog";

        public event Action<IDialogResult> RequestClose;

        private bool _isFirstPointSetted = false;

        private Brush _brush = new SolidBrush(Color.FromArgb(150, Color.Black));

        // Two properties rappresenting the first an the last point of the selection
        private Point _firstPoint;
        public Point FirstPoint {
            get { return _firstPoint; }
            set { SetProperty(ref _firstPoint, value); }
        }

        private Point _secondPoint;
        public Point SecondPoint {
            get { return _secondPoint; }
            set { SetProperty(ref _secondPoint, value); }
        }

        // A property that rapresent the margins  of the selection area.
        private string _margins = "0";
        public string Margins {
            get { return _margins; }
            set { SetProperty(ref _margins, value); }
        }

        // The Bitmap of the basic image, to a copy o this file will be applied a filter in order to make the ara outside the selection darker.
        private Bitmap _baseImage;
        public Bitmap BaseImage {
            get { return _baseImage; }
            set {
                SetProperty(ref _baseImage, value);
                FilteredImage = new Bitmap(value);
            }
        }

        // The Bitmap of the filtered image.
        private Bitmap _filtered;
        public Bitmap FilteredImage {
            get { return _filtered; }
            set { SetProperty(ref _filtered, value); }
        }

        // The source of the Dialog background, it's obtaineb by converting the Filtered image in a BitmapSource 
        private BitmapSource _background;
        public BitmapSource Background {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }

        //The method that sets the beckground
        private void setBackground(Bitmap image) {
            Background = ConvertToBitmapSource(image);
        }

        //The method that converts a Bitmap in a BitmapSource
        public static BitmapSource ConvertToBitmapSource(Bitmap bitmap) {
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                System.Windows.Media.PixelFormats.Pbgra32, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            return bitmapSource;
        }

        public CaptureDialogViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;

            // Subscription to the events
            _eventAggregator.GetEvent<MouseUp>().Subscribe(DetermineFirstPoint);
            _eventAggregator.GetEvent<MouseDown>().Subscribe(DetermineSecondPoint);
            _eventAggregator.GetEvent<MouseMoved>().Subscribe(MoveRectangle);

            FirstPoint = new Point();
            SecondPoint = new Point();
        }

        //The method called when the Mouse Moved event is published. It handles margins and graphic implementation of the selection area.
        private void MoveRectangle() {
            //setting the margins to hide the selection area behind the cursor
            int marginLeft = Cursor.Position.X;
            int marginRight = Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - marginLeft;
            int marginUp = Cursor.Position.Y;
            int marginDown = Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - marginUp;
            //if the user is holding the mouse button 
            if(_isFirstPointSetted) {
                //it adapts the margins so the selection area is correctly displayed
                if(FirstPoint.X > Cursor.Position.X) {
                    marginLeft = Cursor.Position.X;
                    marginRight = Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - FirstPoint.X;

                } else {
                    marginLeft = FirstPoint.X;
                    marginRight = Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - Cursor.Position.X;
                }
                if(FirstPoint.Y > Cursor.Position.Y) {
                    marginUp = Cursor.Position.Y;
                    marginDown = Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - FirstPoint.Y;
                } else {
                    marginUp = FirstPoint.Y;
                    marginDown = Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - Cursor.Position.Y;
                }

                //it applies the filter to to background image
                Rectangle rectLeft = new Rectangle(0, marginUp, marginLeft, Convert.ToInt32(SystemParameters.PrimaryScreenHeight)-marginDown - marginUp);
                Rectangle rectUp = new Rectangle(0, 0, Convert.ToInt32(SystemParameters.PrimaryScreenWidth), marginUp);
                Rectangle rectRight = new Rectangle(Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - marginRight, marginUp, marginRight, Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - marginDown - marginUp);
                Rectangle rectDown = new Rectangle(0, Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - marginDown, Convert.ToInt32(SystemParameters.PrimaryScreenWidth), marginDown);

                Rectangle srcRegion = new Rectangle(0, 0, Convert.ToInt32(SystemParameters.PrimaryScreenWidth), Convert.ToInt32(SystemParameters.PrimaryScreenHeight));
                using(Graphics grD = Graphics.FromImage(FilteredImage)) {
                        grD.DrawImage(BaseImage, srcRegion, srcRegion, GraphicsUnit.Pixel);
                }

                Graphics gr = Graphics.FromImage(FilteredImage);
                gr.FillRectangle(_brush, rectLeft);
                gr.FillRectangle(_brush, rectUp);
                gr.FillRectangle(_brush, rectRight);
                gr.FillRectangle(_brush, rectDown);

               setBackground(FilteredImage);
            }
            //updates the margins
            Margins = $"{marginLeft},{marginUp},{marginRight},{marginDown}";        
        }

        // The method called when the user holds the mouse button.
        // The position of the mouse gets stored in type Point struct.
        private void DetermineFirstPoint() {
            FirstPoint = Cursor.Position;
            _isFirstPointSetted = true;
        }

        // The method called when the user releses the mouse button.
        // The position of the mouse gets stored in type Point struct and the dialog is closed.
        private void DetermineSecondPoint() {
            SecondPoint = Cursor.Position;
            CloseDialog();
        }

        //Determines if the dialogcen be closed, in this case the dialog can be always closed.
        public bool CanCloseDialog() {
            return true;
        }

        // the events called when the dialog gets opened and closed
        public void OnDialogClosed() {
        }
        public void OnDialogOpened(IDialogParameters parameters) {
            BaseImage = parameters.GetValue<Bitmap>("image");

            Rectangle rect = new Rectangle(0, 0, BaseImage.Width, BaseImage.Height);
            Graphics gr = Graphics.FromImage(FilteredImage);
            gr.FillRectangle(_brush, rect);

            setBackground(FilteredImage);
        }


        //When the dialog gets closed it returns the two points as its result.
        public void CloseDialog() {
            Point _maxPoint = new Point();
            Point _minPoint = new Point();

            //rearrangement of the points
            if(FirstPoint.X > SecondPoint.X) {
                _maxPoint.X = FirstPoint.X;
                _minPoint.X = SecondPoint.X;
            } else {
                _maxPoint.X = SecondPoint.X;
                _minPoint.X = FirstPoint.X;
            }
            if(FirstPoint.Y > SecondPoint.Y) {
                _maxPoint.Y = FirstPoint.Y;
                _minPoint.Y = SecondPoint.Y;
            } else {
                _maxPoint.Y = SecondPoint.Y;
                _minPoint.Y = FirstPoint.Y;
            }

            var parameters = new DialogParameters();
            parameters.Add("topLeftPoint", _minPoint);
            parameters.Add("bottomRightPoint", _maxPoint);
            var result = new Prism.Services.Dialogs.DialogResult(ButtonResult.OK, parameters);
            RequestClose?.Invoke(result);
        }       
    }
}
