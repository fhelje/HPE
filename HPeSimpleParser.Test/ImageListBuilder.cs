using System;
using System.Collections.Generic;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Test {
    public class ImageListBuilder {
        private readonly List<Image> _data;
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
    }
}