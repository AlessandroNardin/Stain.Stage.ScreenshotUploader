namespace Stain.Stage.ScreenshotUploader.Uploader {
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
