namespace Stain.Stage.ScreenshotUploader.Uploader {
    //this class contanis the parameters of data when the upload is successfull
    public class UploadData {
        public string Id { get; set; }
        public string DeleteHash { get; set; }
        public string AccountId { get; set; }
        public string AccountUrl { get; set; }
        public string AdType { get; set; }
        public string AdUrl { get; set; }
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
        public bool InGallery { get; set; }
        public bool InMostViral { get; set; }
        public bool HasSound { get; set; }
        public string IsAd { get; set; }
        public string Nsfw { get; set; }
        public string Link { get; set; }
        public string DateTime { get; set; }
        public string Mp4 { get; set; }
        public string Hls { get; set; }

        public string ToString() {
            return $"Id : {Id}," +
                $" DeleteHash : {DeleteHash}," +
                $" Account_id : {AccountId}," +
                $" Account_url : {AccountUrl}," +
                $" Ad_type : {AdType}," +
                $" Ad_url : {AdUrl}," +
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
                $" In_gallery : {InGallery}," +
                $" In_most_viral : {InMostViral}," +
                $" Has_sound : {HasSound}," +
                $" Is_ad : {IsAd}," +
                $" Nsfw : {Nsfw}," +
                $" Link : {Link}," +
                $" DateTime : {DateTime}," +
                $" Mp4 : {Mp4}," +
                $" ls : {Hls}";
        }
    }
}
