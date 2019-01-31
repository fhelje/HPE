using System;
using System.Collections.Generic;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public sealed class ImageListBuilder {
        private List<Image> _data;

        private ImageListBuilder() {
            _data = new List<Image>();
        }

        public static ImageListBuilder With() {
            return new ImageListBuilder();
        }

        public ImageListBuilder AddImage(Action<ImageBuilder> imageBuilderAction) {
            var ib = ImageBuilder.With();
            imageBuilderAction(ib);
            _data.Add(ib.Build());
            return this;
        }

        public List<Image> Build() {
            return _data;
        }

        public ImageListBuilder SetNull() {
            _data = null;
            return this;
        }
    }
}