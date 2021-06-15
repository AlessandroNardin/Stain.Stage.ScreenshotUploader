namespace Stain.Stage.ScreenshotUploader.Uploader {
    //this class contanis the parameters of data when an error in uploading occours
    public class ErrorData {
        public string Error { get; set; }
        public string Request { get; set; }
        public string Method { get; set; }

        public string ToString() {
            return $"Error : {Error}," +
                $" \nRequest : {Request}," +
                $" \nMethod : {Method}";
        }
    }
}
