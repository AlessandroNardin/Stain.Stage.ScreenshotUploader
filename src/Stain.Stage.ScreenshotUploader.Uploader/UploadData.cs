namespace Stain.Stage.ScreenshotUploader.Uploader {
    //this class contanis the parameters of data when the upload is successfull
    public class UploadData {
        public string Id { get; set; }
        public string DeleteHash { get; set; }
        public string Account_id { get; set; }
        public string Account_url { get; set; }
        public string Ad_type { get; set; }
        public string Ad_url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public int Views { get; set; }
        public string Section { get; set; }
        public string Vote { get; set; }
        public int BandWidth { get; set; }
        public bool Animated { get; set; }
        public bool Favorite { get; set; }
        public bool In_gallery { get; set; }
        public bool In_most_viral { get; set; }
        public bool Has_sound { get; set; }
        public string Is_ad { get; set; }
        public string Nsfw { get; set; }
        public string Link { get; set; }
        public string DateTime { get; set; }
        public string Mp4 { get; set; }
        public string Hls { get; set; }

        public string ToString() {
            return $"Id : {Id}," +
                $" DeleteHash : {DeleteHash}," +
                $" Account_id : {Account_id}," +
                $" Account_url : {Account_url}," +
                $" Ad_type : {Ad_type}," +
                $" Ad_url : {Ad_url}," +
                $" Title : {Title}," +
                $" Description : {Description}," +
                $" Name : {Name}," +
                $" Type : {Type}," +
                $" Width : {Width}," +
                $" Height : {Height}," +
                $" Size : {Size}," +
                $" Views : {Views}," +
                $" Section : {Section}," +
                $" Vote : {Vote}," +
                $" Bandwidth : {BandWidth}," +
                $" Animated : {Animated}," +
                $" Favorite : {Favorite}," +
                $" In_gallery : {In_gallery}," +
                $" In_most_viral : {In_most_viral}," +
                $" Has_sound : {Has_sound}," +
                $" Is_ad : {Is_ad}," +
                $" Nsfw : {Nsfw}," +
                $" Link : {Link}," +
                $" DateTime : {DateTime}," +
                $" Mp4 : {Mp4}," +
                $" ls : {Hls}";
        }
    }
}
