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
        // An attribute rappresenting the top left point of the screen section to capture
        

        private Point _maxPoint;
        private Point _minPoint;

        public int Width { get; set; } = Convert.ToInt32(SystemParameters.PrimaryScreenWidth);
        public int Height { get; set; } = Convert.ToInt32(SystemParameters.PrimaryScreenHeight);

        private string _margins = "0";
        public string Margins {
            get { return _margins; }
            set { SetProperty(ref _margins, value); }
        }

        private Bitmap _baseImage;
        public Bitmap BaseImage {
            get { return _baseImage; }
            set {
                SetProperty(ref _baseImage, value);
                FilteredImage = new Bitmap(value);
            }
        }

        private Bitmap _filtered;
        public Bitmap FilteredImage {
            get { return _filtered; }
            set { SetProperty(ref _filtered, value); }
        }

        private BitmapSource _background;
        public BitmapSource Background {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }

        private void setBackground(Bitmap image) {
            Background = ConvertToBitmapSource(image);
        }

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

        private IEventAggregator _eventAggregator;

        private Point _firstPoint;
        public Point FirstPoint {
            get { return _firstPoint; }
            set { SetProperty(ref _firstPoint, value); }
        }

        // An attribute rappresenting the top left point of the screen section to capture
        private Point _secondPoint;
        public Point SecondPoint {
            get { return _secondPoint; }
            set { SetProperty(ref _secondPoint, value); }
        }

        //The delegate command binded to the button in the dialog
        public DelegateCommand PointsCommand { get; }

        public CaptureDialogViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MouseUp>().Subscribe(DetermineFirstPoint);
            _eventAggregator.GetEvent<MouseDown>().Subscribe(DetermineSecondPoint);
            _eventAggregator.GetEvent<MouseMoved>().Subscribe(MoveRectangle);
            FirstPoint = new Point();
            SecondPoint = new Point();
            PointsCommand = new DelegateCommand(DetermineSecondPoint);
        }

        private bool firstPointSetted = false;


        private Brush brush = new SolidBrush(Color.FromArgb(150, Color.Black));

        private void MoveRectangle() {
            int marginLeft = Cursor.Position.X;
            int marginRight = Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - marginLeft;
            int marginUp = Cursor.Position.Y;
            int marginDown = Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - marginUp;
            if(firstPointSetted) {
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

                Rectangle rectLeft = new Rectangle(0, marginUp, marginLeft, Convert.ToInt32(SystemParameters.PrimaryScreenHeight)-marginDown - marginUp);
                Rectangle rectUp = new Rectangle(0, 0, Convert.ToInt32(SystemParameters.PrimaryScreenWidth), marginUp);
                Rectangle rectRight = new Rectangle(Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - marginRight, marginUp, marginRight, Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - marginDown - marginUp);
                Rectangle rectDown = new Rectangle(0, Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - marginDown, Convert.ToInt32(SystemParameters.PrimaryScreenWidth), marginDown);

                Rectangle srcRegion = new Rectangle(0, 0, Convert.ToInt32(SystemParameters.PrimaryScreenWidth), Convert.ToInt32(SystemParameters.PrimaryScreenHeight));
                using(Graphics grD = Graphics.FromImage(FilteredImage)) {
                        grD.DrawImage(BaseImage, srcRegion, srcRegion, GraphicsUnit.Pixel);
                }

                Graphics gr = Graphics.FromImage(FilteredImage);
                gr.FillRectangle(brush, rectLeft);
                gr.FillRectangle(brush, rectUp);
                gr.FillRectangle(brush, rectRight);
                gr.FillRectangle(brush, rectDown);

               setBackground(FilteredImage);

            }
            Margins = $"{marginLeft},{marginUp},{marginRight},{marginDown}";

            
        }

        private void DetermineFirstPoint() {
            FirstPoint = Cursor.Position;
            firstPointSetted = true;
        }

        // The method called when the user clicks on the dialog.
        // The first time the user clicks the position of the mouse gets stored in type Point struct.
        // The second time the user clicks the position of the mouse gets stored in another type Point struct, the iteration parameters gets setted to zero and the dialog gets closed
        private void DetermineSecondPoint() {
            SecondPoint = Cursor.Position;
            CloseDialog();
        }

        public string Title => "Capture Dialog";

        public event Action<IDialogResult> RequestClose;

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
            gr.FillRectangle(brush, rect);

            setBackground(FilteredImage);
        }


            //When the dialog gets closed it returns the two points as its result.
        public void CloseDialog() {
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
