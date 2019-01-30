using System;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser.HPE;

namespace HPeSimpleParser.Test.Builders {
    public class ImageBuilder {
        private readonly HPEImageParser _ip;
        private string _groupingKey1;
        private string _groupingKey2;
        private string _contentType;
        private string _pixelHeight;
        private string _orientation;
        private string _pixelWidth;
        private string _imageUrlHttp;
        private string _typeDetail;
        private string _fullTitle;

        private ImageBuilder() {
            _ip = new HPEImageParser();
        }

        public static ImageBuilder With() {
            return new ImageBuilder();
        }

        public ImageBuilder Default() {
            _groupingKey1 = "cmg674";
            _groupingKey2 = "1";
            _contentType = "jpg";
            _pixelHeight = "400";
            _orientation = "Center facing";
            _pixelWidth = "600";
            _imageUrlHttp = Guid.NewGuid().ToString();
            _typeDetail = "product image";
            _fullTitle = "FullTitle";
            
            return this;
        }

        public Image Build() {
            return new Image(
                _groupingKey1,
                _groupingKey2,
                _contentType,
                _pixelHeight,
                _orientation,
                _pixelWidth,
                _imageUrlHttp,
                _typeDetail,
                _fullTitle,
                _ip.ParseContentTypePriority(_contentType),
                _ip.GetSizeCategory(_pixelHeight, _pixelWidth),
                _ip.ParseTypeDetail(_typeDetail),
                _ip.ParseIntWithDefault(_pixelHeight),
                _ip.ParseIntWithDefault(_pixelWidth)
            );;
        }

        public ImageBuilder SetHeight(string height) {
            _pixelHeight = height;
            return this;
        }

        public ImageBuilder SetWidth(string width) {
            _pixelWidth = width;
            return this;
        }

        public ImageBuilder SetSizeCategory(SizeCategoryEnum category) {
            if (category == SizeCategoryEnum.Wrong) {
                _pixelWidth = "";
                _pixelHeight = "";
            }
            return this;
        }

        public ImageBuilder SetMasterObjectName(string masterObjectName) {
            _groupingKey2 = masterObjectName;
            return this;
        }

        public ImageBuilder SetCmgAcronym(string cmgAcronym) {
            _groupingKey1 = cmgAcronym;
            return this;
        }

        public ImageBuilder SetContentType(string contentType) {
            _contentType = contentType;
            return this;
        }

        public ImageBuilder SetDocumentTypeDetail(string docTypeDetail) {
            _typeDetail = docTypeDetail;
            return this;
        }

        public ImageBuilder SetOrientation(string orientation) {
            _orientation = orientation;
            return this;
        }

        public ImageBuilder SetUrl(string url) {
            _imageUrlHttp = url;
            return this;
        }
    }
}