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
            return $"Id : {Id}, \nDeleteHash : {DeleteHash}," +
                $" \nAccount_id : {Account_id}," +
                $" \nAccount_url : {Account_url}," +
                $" \nAd_type : {Ad_type}," +
                $" \nAd_url : {Ad_url}," +
                $" \nTitle : {Title}," +
                $" \nDescription : {Description}," +
                $" \nName : {Name}," +
                $" \nType : {Type}," +
                $" \nWidth : {Width}," +
                $" \nHeight : {Height}," +
                $" \nSize : {Size}," +
                $" \nViews : {Views}," +
                $" \nSection : {Section}," +
                $" \nVote : {Vote}," +
                $" \nBandwidth : {BandWidth}," +
                $" \nAnimated : {Animated}," +
                $" \nFavorite : {Favorite}," +
                $" \nIn_gallery : {In_gallery}," +
                $" \nIn_most_viral : {In_most_viral}," +
                $" \nHas_sound : {Has_sound}," +
                $" \nIs_ad : {Is_ad}," +
                $" \nNsfw : {Nsfw}," +
                $" \nLink : {Link}," +
                $" \nDateTime : {DateTime}," +
                $" \nMp4 : {Mp4}," +
                $" \nls : {Hls}";
        }
    }
}
