namespace HPeSimpleParser.HPE.Model {
    public class Image {
        public string CmgAcronym { get; set; }
        public string MasterObjectName { get; set; }
        public string ContentType { get; set; }
        public int ContentTypePriority {
            get {
                switch (ContentType) {
                    case "jpg":
                        return 0;
                    case "png":
                        return 1;
                    default:
                        return 2;
                }
            }
        }
        public string PixelHeight { get; set; }
        public string FileName { get; set; }
        public string Orientation { get; set; }
        public string PixelWidth { get; set; }
        public string Size => $"W:{PixelWidth} H:{PixelHeight}";
        public SizeCategoryEnum SizeCategory {
            get {
                if (!int.TryParse(PixelHeight, out var height)) {
                    return SizeCategoryEnum.Wrong;
                }
                if (!int.TryParse(PixelWidth, out var width)) {
                    return SizeCategoryEnum.Wrong;
                }

                if (height <= 150 && width <= 200) {
                    return SizeCategoryEnum.Small;
                }

                if (height <= 300 && width <= 400) {
                    return SizeCategoryEnum.Medium;
                }

                if (height <= 600 && width <= 800) {
                    return SizeCategoryEnum.Large;
                }
                return SizeCategoryEnum.XLarge;
            }
        }
        public string Action { get; set; }
        public string ImageUrlHttp { get; set; }
        public string DocumentTypeDetail { get; set; }
        public int DocumentTypeDetailPriority
        {
            get
            {
                switch (DocumentTypeDetail) {
                    case "product image":
                        return 0;
                    case "product image hero":
                        return 1000;
                    case "product image - not as shown":
                        return 1000;
                    case "Concept Graphic":
                        return 1000;
                    case "Product Only":
                        return 1;
                    case "product image hero,product image":
                        return 1000;
                    case "icon":
                        return 1000;
                    case "Person - Face":
                        return 2;
                    default:
                        return 3;

                }
            }
        }

        public string DocumentType { get; set; }
        public string LanguageCode { get; set; }
        public string SearchKeyword { get; set; }
        public string Background { get; set; }
        public string FullTitle { get; set; }
        public string DpiResolution { get; set; }
        public int Height => int.TryParse(PixelHeight, out var height) ? height : 0;
        public int Width => int.TryParse(PixelWidth, out var width) ? width : 0;
    }
}