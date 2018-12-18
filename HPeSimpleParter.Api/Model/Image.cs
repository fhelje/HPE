namespace HPeSimpleParter.Api.Model {
    public class Image {
        public string CmgAcronym { get; set; }
        public string MasterObjectName { get; set; }
        public string ContentType { get; set; }
        public string PixelHeight { get; set; }
        public string FileName { get; set; }
        public string Orientation { get; set; }
        public string PixelWidth { get; set; }
        public string Size => $"W:{PixelWidth} H:{PixelHeight}";
        public string Action { get; set; }
        public string ImageUrlHttp { get; set; }
        public string DocumentTypeDetail { get; set; }
        public string DocumentType { get; set; }
        public string LanguageCode { get; set; }
        public string SearchKeyword { get; set; }
        public string Background { get; set; }
        public string FullTitle { get; set; }
        public string DpiResolution { get; set; }
    }
}