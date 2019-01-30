using System;
using System.Collections.Generic;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public class LinksBuilder {
        private string _pdfLinkDataSheet = "PdfLinkDataSheet";
        private string _pdfLinkManual = "PdfLinkManual";
        private List<Image> _selectedImages = new List<Image>();
        private ImageListBuilder _imageListBuilder;

        private LinksBuilder() {
            _imageListBuilder = ImageListBuilder.With();
        }
        public static LinksBuilder With() {
            return new LinksBuilder();
        }
        public Links Build() {
            return new Links(_pdfLinkDataSheet, _pdfLinkManual, _imageListBuilder.Build());
        }

        public LinksBuilder SetImagesToNull() {
            _imageListBuilder.SetNull();
            return this;
        }

        public LinksBuilder AddImage(Action<ImageListBuilder> action) {
            action(_imageListBuilder);
            return this;
        }
    }
}