using System;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public sealed class LinksBuilder {
        private const string PdfLinkDataSheet = "PdfLinkDataSheet";
        private const string PdfLinkManual = "PdfLinkManual";
        private readonly ImageListBuilder _imageListBuilder;

        private LinksBuilder() {
            _imageListBuilder = ImageListBuilder.With();
        }

        public static LinksBuilder With() {
            return new LinksBuilder();
        }

        public Links Build() {
            return new Links(PdfLinkDataSheet, PdfLinkManual, _imageListBuilder.Build());
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