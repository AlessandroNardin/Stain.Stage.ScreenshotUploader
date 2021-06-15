using Newtonsoft.Json;

namespace Stain.Stage.ScreenshotUploader.Uploader {
    //this class contanis the parameters of data when the upload is successfull
    public class UploadData {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("deletehash")]
        public string DeleteHash { get; set; }
        [JsonProperty("account_id")]
        public string AccountId { get; set; }
        [JsonProperty("account_url")]
        public string AccountUrl { get; set; }
        [JsonProperty("ad_type")]
        public string AdType { get; set; }
        [JsonProperty("ad_url")]
        public string AdUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("views")]
        public int Views { get; set; }
        [JsonProperty("section")]
        public string Section { get; set; }
        [JsonProperty("vote")]
        public string Vote { get; set; }
        [JsonProperty("bandwidth")]
        public int BandWidth { get; set; }
        [JsonProperty("animated")]
        public bool Animated { get; set; }
        [JsonProperty("favorite")]
        public bool Favorite { get; set; }
        [JsonProperty("in_gallery")]
        public bool InGallery { get; set; }
        [JsonProperty("in_most_viral")]
        public bool InMostViral { get; set; }
        [JsonProperty("has_sound")]
        public bool HasSound { get; set; }
        [JsonProperty("is_ad")]
        public string IsAd { get; set; }
        [JsonProperty("nsfw")]
        public string Nsfw { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("datetime")]
        public string DateTime { get; set; }
        [JsonProperty("mp4")]
        public string Mp4 { get; set; }
        [JsonProperty("hls")]
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
                $" Hls : {Hls}";
        }
    }
}
