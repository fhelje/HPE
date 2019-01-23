using System;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Test {
    public class ImageBuilder {
        private Image _data;

        private ImageBuilder() {
            _data = new Image();
        }

        public static ImageBuilder With() {
            return new ImageBuilder();
        }

        public ImageBuilder Default() {
            _data = new Image {
                MasterObjectName = "1",
                CmgAcronym = "cmg674",
                Action = "update",
                ContentType = "jpg",
                DocumentTypeDetail = "product image",
                Orientation = "Center facing",
                PixelHeight = "400",
                PixelWidth = "600",
                ImageUrlHttp = Guid.NewGuid().ToString()
            };
            return this;
        }

        public Image Build() {
            return _data;
        }

        public ImageBuilder SetHeight(int height) {
            _data.PixelHeight = height.ToString();
            return this;
        }

        public ImageBuilder SetWidth(int width) {
            _data.PixelWidth = width.ToString();
            return this;
        }

        public ImageBuilder SetSizeCategory(SizeCategoryEnum category) {
            if (category == SizeCategoryEnum.Wrong) {
                _data.PixelWidth = "";
                _data.PixelHeight = "";
            }
            return this;
        }

        public ImageBuilder SetMasterObjectName(string masterObjectName) {
            _data.MasterObjectName = masterObjectName;
            return this;
        }

        public ImageBuilder SetCmgAcronym(string cmgAcronym) {
            _data.CmgAcronym = cmgAcronym;
            return this;
        }

        public ImageBuilder SetContentType(string contentType) {
            _data.ContentType = contentType;
            return this;
        }

        public ImageBuilder SetDocumentTypeDetail(string docTypeDetail) {
            _data.DocumentTypeDetail = docTypeDetail;
            return this;
        }

        public ImageBuilder SetOrientation(string orientation) {
            _data.Orientation = orientation;
            return this;
        }

        public ImageBuilder SetUrl(string url) {
            _data.ImageUrlHttp = url;
            return this;
        }
    }
}