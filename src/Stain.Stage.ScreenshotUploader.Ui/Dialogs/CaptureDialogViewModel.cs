using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Stain.Stage.ScreenshotUploader.Ui.Dialogs {
    class CaptureDialogViewModel : BindableBase, IDialogAware {
        // An attribute rappresenting the top left point of the screen section to capture
        private Point _topLeftPoint;
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

        // Counts the number of clicks done by the user in a single Dialog session
        private int _iteraction;

        //The delegate command binded to the button in the dialog
        public DelegateCommand PointsCommand { get; }

        public CaptureDialogViewModel() {
            TopLeftPoint = new Point();
            BottomRightPoint = new Point();
            PointsCommand = new DelegateCommand(DeterminePoints);
        }

        // The method called when the user clicks on the dialog.
        // The first time the user clicks the position of the mouse gets stored in type Point struct.
        // The second time the user clicks the position of the mouse gets stored in another type Point struct, the iteration parameters gets setted to zero and the dialog gets closed
        private void DeterminePoints() {
            switch(_iteraction) {
                case 0:
                    TopLeftPoint = Cursor.Position;
                    _iteraction++;
                    break;
                case 1:
                    BottomRightPoint = Cursor.Position;
                    _iteraction = 0;
                    CloseDialog();
                    break;
            }
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
        }

        //When the dialog gets closed it returns the two points as its result.
        private void CloseDialog() {
            var parameters = new DialogParameters();
            parameters.Add("topLeftPoint", TopLeftPoint);
            parameters.Add("bottomRightPoint", BottomRightPoint);
            var result = new Prism.Services.Dialogs.DialogResult(ButtonResult.OK, parameters);
            RequestClose?.Invoke(result);
        }
    }
}
