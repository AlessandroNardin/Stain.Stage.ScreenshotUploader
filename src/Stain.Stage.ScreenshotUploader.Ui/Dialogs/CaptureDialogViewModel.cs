using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Stain.Stage.ScreenshotUploader.Ui.Events;
using System;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace Stain.Stage.ScreenshotUploader.Ui.Dialogs {
    class CaptureDialogViewModel : BindableBase, IDialogAware {
        // An attribute rappresenting the top left point of the screen section to capture
        private Point _topLeftPoint;

        public int Width { get; set; } = Convert.ToInt32(SystemParameters.PrimaryScreenWidth);
        public int Height { get; set; } = Convert.ToInt32(SystemParameters.PrimaryScreenHeight);

        private string _margins;
        public string Margins {
            get { return _margins; }
            set { SetProperty(ref _margins, value); }
        }

        private int _originX;
        private int _originY;
        private string _imagePath;
        public string ImagePath {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }

        private IEventAggregator _eventAggregator;

        public Point TopLeftPoint {
            get { return _topLeftPoint; }
            set { SetProperty(ref _topLeftPoint, value); }
        }

        // An attribute rappresenting the top left point of the screen section to capture
        private Point _bottomRightPoint;
        public Point BottomRightPoint {
            get { return _bottomRightPoint; }
            set { SetProperty(ref _bottomRightPoint, value); }
        }

        //The delegate command binded to the button in the dialog
        public DelegateCommand PointsCommand { get; }

        public CaptureDialogViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MouseUp>().Subscribe(DetermineOriginPoint);
            _eventAggregator.GetEvent<MouseDown>().Subscribe(DeterminePoints);
            _eventAggregator.GetEvent<MouseMoved>().Subscribe(DetermineOtherPoint);
            TopLeftPoint = new Point();
            BottomRightPoint = new Point();
            PointsCommand = new DelegateCommand(DeterminePoints);
        }

        private bool originSetted = false;

        private void DetermineOtherPoint() {
            if(originSetted) {
                Margins = $"{_originX},{_originY},{Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - Cursor.Position.X},{Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - Cursor.Position.Y}";
            } else {
                Margins = $"{Cursor.Position.X},{Cursor.Position.Y},{Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - Cursor.Position.X},{Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - Cursor.Position.Y}";
            }

            }

        private void DetermineOriginPoint() {
            Margins = $"{Cursor.Position.X},{Cursor.Position.Y},{Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - Cursor.Position.X},{Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - Cursor.Position.Y}";
            _originX = Cursor.Position.X;
            _originY = Cursor.Position.Y;
            TopLeftPoint = Cursor.Position;
            originSetted = true;
        }

        // The method called when the user clicks on the dialog.
        // The first time the user clicks the position of the mouse gets stored in type Point struct.
        // The second time the user clicks the position of the mouse gets stored in another type Point struct, the iteration parameters gets setted to zero and the dialog gets closed
        private void DeterminePoints() {
                    BottomRightPoint = Cursor.Position;
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
            ImagePath = parameters.GetValue<string>("path");
        }


            //When the dialog gets closed it returns the two points as its result.
        public void CloseDialog() {
                var parameters = new DialogParameters();
                parameters.Add("topLeftPoint", TopLeftPoint);
                parameters.Add("bottomRightPoint", BottomRightPoint);
                var result = new Prism.Services.Dialogs.DialogResult(ButtonResult.OK, parameters);
                RequestClose?.Invoke(result);
        }

        
    }
}
