using Newtonsoft.Json;

namespace Stain.Stage.ScreenshotUploader.Uploader {
    //this class contanis the parameters of data when an error in uploading occours
    public class ErrorData {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("request")]
        public string Request { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }

        public override string ToString() {
            return $"Error : {Error}," +
                $" Request : {Request}," +
                $" Method : {Method}";
        }
    }
}
