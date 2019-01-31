using System.Collections.Generic;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    internal sealed class ProductVariantsBuilder {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly List<ProductVariant> _variants = new List<ProductVariant>();

        private ProductVariantsBuilder() {
        }

        public static ProductVariantsBuilder With() {
            return new ProductVariantsBuilder();
        }

        public IReadOnlyList<ProductVariant> Build() {
            return _variants;
        }
    }
}