using System;
using HPeSimpleParser.lib.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public class ProductRootBuilder {
        private string _file = "File";
        private string _partNumber = "PartNumber";
        private string _partnerPartNumber = "PartnerPartNumber";
        private string _languageId = "sv-SE";
        private readonly ProductBuilder _product;
        private readonly BranchBuilder _branch;
        private readonly ProductVariantsBuilder _productVariants;
        private readonly MarketingBuilder _marketing;
        private readonly OptionItemsBuilder _optionItems;
        private readonly SpecificationItemsBuilder _specificationItems;
        private readonly LinksBuilder _links;
        private readonly HierarchiesBuilder _hierarchies;
        private readonly DetailBuilder _details;

        private ProductRootBuilder() {
            _product = ProductBuilder.With();
            _branch = BranchBuilder.With();
            _productVariants = ProductVariantsBuilder.With();
            _marketing = MarketingBuilder.With();
            _optionItems = OptionItemsBuilder.With();
            _specificationItems = SpecificationItemsBuilder.With();
            _links = LinksBuilder.With();
            _hierarchies = HierarchiesBuilder.With();
            _details = DetailBuilder.With();
        }

        public static ProductRootBuilder With() {
            return new ProductRootBuilder();
        }

        public ProductRoot Build() {
            return new ProductRoot(
                _file,
                _partNumber,
                _partnerPartNumber,
                _languageId,
                _product.Build(),
                _branch.Build(),
                _productVariants.Build(),
                _marketing.Build(),
                _optionItems.Build(),
                _specificationItems.Build(),
                _links.Build(),
                _hierarchies.Build(),
                _details.Build()
            );
        }

        public ProductRootBuilder WithImages(Action<LinksBuilder> action) {
            action(_links);
            return this;
        }

        public ProductRootBuilder WithDetail(Action<DetailBuilder> action) {
            action(_details);
            return this;
        }

        public ProductRootBuilder WithMarketing(Action<MarketingBuilder> action) {
            action(_marketing);
            return this;
        }

        public ProductRootBuilder WithHierachy(Action<HierarchiesBuilder> action) {
            action(_hierarchies);
            return this;
        }

        public ProductRootBuilder WithSpecifications(Action<SpecificationItemsBuilder> action) {
            action(_specificationItems);
            return this;
        }

        public ProductRootBuilder WithOptions(Action<OptionItemsBuilder> action) {
            action(_optionItems);
            return this;
        }
    }
}