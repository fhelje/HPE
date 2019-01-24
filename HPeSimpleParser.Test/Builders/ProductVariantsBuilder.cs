using System.Collections.Generic;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    internal class ProductVariantsBuilder {
        private List<ProductVariant> _variants = new List<ProductVariant>();

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