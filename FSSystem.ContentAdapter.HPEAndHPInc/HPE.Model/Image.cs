namespace FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model {
    public class Image {
        public Image(string groupingKey1, string groupingKey2, string contentType, string pixelHeight,
            string orientation, string pixelWidth, string imageUrlHttp, string typeDetail, string fullTitle,
            int contentTypePriority, SizeCategoryEnum sizeCategory, int documentTypeDetailPriority, int height,
            int width) {
            GroupingKey1 = groupingKey1;
            GroupingKey2 = groupingKey2;
            ContentType = contentType;
            PixelHeight = pixelHeight;
            Orientation = orientation;
            PixelWidth = pixelWidth;
            ImageUrlHttp = imageUrlHttp;
            TypeDetail = typeDetail;
            FullTitle = fullTitle;
            ContentTypePriority = contentTypePriority;
            SizeCategory = sizeCategory;
            DocumentTypeDetailPriority = documentTypeDetailPriority;
            Height = height;
            Width = width;
        }

        public string GroupingKey1 { get; }
        public string GroupingKey2 { get; }
        public string ContentType { get; }
        public int ContentTypePriority { get; }
        public string PixelHeight { get; }
        public string Orientation { get; }
        public string PixelWidth { get; }
        public SizeCategoryEnum SizeCategory { get; }
        public string ImageUrlHttp { get; }
        public string TypeDetail { get; }
        public int DocumentTypeDetailPriority { get; }
        public string FullTitle { get; }
        public int Height { get; }
        public int Width { get; }
    }
}