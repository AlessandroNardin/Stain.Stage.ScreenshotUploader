using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stain.Stage.ScreenshotUploader.Uploader {
    class ErrorData {
        public string Error { get; set; }
        public string Request { get; set; }
        public string Method { get; set; }

        public string ToString() {
            return $"Error : {Error}, \nRequest : {Request}, \nMethod : {Method}";
        }
    }
}
