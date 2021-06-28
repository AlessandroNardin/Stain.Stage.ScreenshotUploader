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
        

        private Point _maxPoint;
        private Point _minPoint;

        public int Width { get; set; } = Convert.ToInt32(SystemParameters.PrimaryScreenWidth);
        public int Height { get; set; } = Convert.ToInt32(SystemParameters.PrimaryScreenHeight);

        private string _margins = "0";
        public string Margins {
            get { return _margins; }
            set { SetProperty(ref _margins, value); }
        }

        private string _imagePath;
        public string ImagePath {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
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

        private void MoveRectangle() {
            int marginLeft = Cursor.Position.X;
            int marginRight = Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - marginLeft;
            int marginUp = Cursor.Position.Y;
            int marginDown = Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - marginRight;
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
            ImagePath = parameters.GetValue<string>("path");
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
