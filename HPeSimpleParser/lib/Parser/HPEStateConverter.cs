using System.Collections.Generic;
using System.Linq;
using HPeSimpleParser.lib.HPE.Model;
using HPeSimpleParser.lib.Parser.State;

namespace HPeSimpleParser.lib.Parser {
    public class HPEStateConverter {
        public ProductRoot CreateProductRoot(string file, ParseState state) {
            var specificationsLabeledItems = CreateSpecificationsLabeledItems(state);
            var productRoot = CreateProductRoot(
                file, 
                state,
                CreateProduct(state, GetHpeHierarchyNode(state), GetProductLineHierarchyNode(state)), 
                CreateBranch(state), 
                CreateProductVariants(state), 
                CreateMarketing(state), 
                CreateOptionsItems(state), 
                specificationsLabeledItems, 
                ImageSelector.FilterImages(state.Links.ImageLinks), 
                state.Hierarchy, 
                CreateDetail(state, specificationsLabeledItems, state.Product.Unspsc)
            );
            return productRoot;
        }

        private static ProductRoot CreateProductRoot(
            string file, 
            ParseState state, 
            Product product, 
            IReadOnlyList<Hierarchy> branch, 
            IReadOnlyList<ProductVariant> productVariants, 
            Marketing marketing, 
            IReadOnlyList<Option> optionsItems, 
            IReadOnlyList<Specification> specificationsLabeledItems, 
            IReadOnlyList<Image> linksSelectedImages, 
            IReadOnlyList<Hierarchy> hierarchies, 
            Detail detail) {
            return new ProductRoot(
                file,
                state.PartNumber,
                state.PartnerPartNumber,
                state.LanguageId,
                product,
                branch,
                productVariants,
                marketing,
                optionsItems,
                specificationsLabeledItems,
                new Links(state.Links.PdfLinkDataSheet, state.Links.PdfLinkManual, linksSelectedImages),
                hierarchies,
                detail
            );
        }

        private static Detail CreateDetail(ParseState state, IReadOnlyList<Specification> specificationsLabeledItems,
            int unspsc) {
            decimal? height = null;
            decimal? width = null;
            decimal? depth = null;
            if (specificationsLabeledItems.TryFindDimensionsInSpecifications(out var dim, "dimenmet", "dimenus")) {
                if (dim != null) {
                    var dimension = dim.Value;
                    height = dimension.GetHeightInMillimeter();
                    width = dimension.GetWidthInMillimeter();
                    depth = dimension.GetDepthInMillimeter();
                }
            }

            var detail = new Detail(
                state.PartnerPartNumber,
                weight: specificationsLabeledItems.TryFindWeightInSpecifications("weightmet", "weightus"),
                height: height,
                width: width,
                depth: depth,
                unspsc: unspsc
            );
            return detail;
        }

        private static List<Specification> CreateSpecificationsLabeledItems(ParseState state) {
            return state.Specifications.Select(x =>
                new Specification(x.Name, x.Value, x.Type, x.UnitOfMeasure, x.Id, x.GroupId, x.GroupName, x.Label)).ToList();
        }

        private static List<Option> CreateOptionsItems(ParseState state) {
            return state.Options.Select(x =>
                new Option(x.ManufacturerCode, x.OptionPartnerPartNumber, x.OptionGroupCode, x.OptionGroupName)).ToList();
        }

        private static Marketing CreateMarketing(ParseState state) {
            return new Marketing(state.Marketing?.MarketingText, "", "", "");
        }

        private static List<ProductVariant> CreateProductVariants(ParseState state) {
            return state.ProductVariants.Select(x => new ProductVariant(x.Description, x.Opt, x.UpcCode)).ToList();
        }

        private static List<Hierarchy> CreateBranch(ParseState state) {
            return new List<Hierarchy> {
                new Hierarchy("HPE", state.Branch.ProductType.Id, state.Branch.ProductType.Name, null, "HPE", 1),
                new Hierarchy("HPE", state.Branch.MarketingCategory.Id, state.Branch.MarketingCategory.Name,state.Branch.ProductType.Id, "HPE", 2),
                new Hierarchy("HPE", state.Branch.MarketingSubCategory.Id, state.Branch.MarketingSubCategory.Name,state.Branch.MarketingCategory.Id, "HPE", 3),
                new Hierarchy("HPE", state.Branch.BigSeries.Id, state.Branch.BigSeries.Name,state.Branch.MarketingSubCategory.Id, "HPE", 4),
                new Hierarchy("HPE", state.Branch.SmallSeries.Id, state.Branch.SmallSeries.Name, state.Branch.BigSeries.Id,"HPE"),
            };
        }

        private static Product CreateProduct(ParseState state, Hierarchy hpe, Hierarchy productLine) {
            return new Product(
                state.PartnerPartNumber,
                state.PartNumber,
                state.Product.ManufacturerName,
                state.Product.ManufacturerCode,
                hpe.CategoryID,
                hpe.CategoryName,
                hpe.PartnerHierarchyCode,
                state.Product.Description,
                state.Product.DescriptionLong ?? state.Product.Description,
                state.Product.ProductCode,
                state.Product.IsEol,
                state.Product.ChangeDate,
                productLine.CategoryID,
                productLine.CategoryName,
                productLine.PartnerHierarchyCode
            );
        }

        private static Hierarchy GetProductLineHierarchyNode(ParseState state) {
            return state.Hierarchy.FirstOrDefault(x => x.Name == "PL") ??
                   state.Hierarchy.First() ?? new Hierarchy("", "", "", "", "");
        }

        private static Hierarchy GetHpeHierarchyNode(ParseState state) {
            return state.Hierarchy.FirstOrDefault(x => x.Name == "HPE") ??
                   state.Hierarchy.First() ?? new Hierarchy("", "", "", "", "");
        }
    }
    public class HPIncStateConverter {
        public ProductRoot CreateProductRoot(string file, ParseState state) {
            var specificationsLabeledItems = CreateSpecificationsLabeledItems(state);
            var productRoot = CreateProductRoot(
                file, 
                state,
                CreateProduct(state, GetHpeHierarchyNode(state), GetProductLineHierarchyNode(state)), 
                CreateBranch(state), 
                CreateProductVariants(state), 
                CreateMarketing(state), 
                CreateOptionsItems(state), 
                specificationsLabeledItems, 
                ImageSelector.FilterImages(state.Links.ImageLinks), 
                state.Hierarchy, 
                CreateDetail(state, specificationsLabeledItems)
            );
            return productRoot;
        }

        private static ProductRoot CreateProductRoot(
            string file, 
            ParseState state, 
            Product product, 
            IReadOnlyList<Hierarchy> branch, 
            IReadOnlyList<ProductVariant> productVariants, 
            Marketing marketing, 
            IReadOnlyList<Option> optionsItems, 
            IReadOnlyList<Specification> specificationsLabeledItems, 
            IReadOnlyList<Image> linksSelectedImages, 
            IReadOnlyList<Hierarchy> hierarchies, 
            Detail detail) {
            return new ProductRoot(
                file,
                state.PartNumber,
                state.PartnerPartNumber,
                state.LanguageId,
                product,
                branch,
                productVariants,
                marketing,
                optionsItems,
                specificationsLabeledItems,
                new Links(state.Links.PdfLinkDataSheet, state.Links.PdfLinkManual, linksSelectedImages),
                hierarchies,
                detail
            );
        }

        private static Detail CreateDetail(ParseState state, IReadOnlyList<Specification> specificationsLabeledItems) {
            decimal? height = null;
            decimal? width = null;
            decimal? depth = null;
            if (specificationsLabeledItems.TryFindDimensionsInSpecifications(out var dim, "dimenmet", "dimenus")) {
                if (dim != null) {
                    var dimension = dim.Value;
                    height = dimension.GetHeightInMillimeter();
                    width = dimension.GetWidthInMillimeter();
                    depth = dimension.GetDepthInMillimeter();
                }
            }

            var detail = new Detail(
                state.PartnerPartNumber,
                weight: specificationsLabeledItems.TryFindWeightInSpecifications("weightmet", "weightus"),
                height: height,
                width: width,
                depth: depth
            );
            return detail;
        }

        private static List<Specification> CreateSpecificationsLabeledItems(ParseState state) {
            return state.Specifications.Select(x =>
                new Specification(x.Name, x.Value, x.Type, x.UnitOfMeasure, x.Id, x.GroupId, x.GroupName, x.Label)).ToList();
        }

        private static List<Option> CreateOptionsItems(ParseState state) {
            return state.Options.Select(x =>
                new Option(x.ManufacturerCode, x.OptionPartnerPartNumber, x.OptionGroupCode, x.OptionGroupName)).ToList();
        }

        private static Marketing CreateMarketing(ParseState state) {
            return new Marketing(state.Marketing.MarketingText, "", "", "");
        }

        private static List<ProductVariant> CreateProductVariants(ParseState state) {
            return state.ProductVariants.Select(x => new ProductVariant(x.Description, x.Opt, x.UpcCode)).ToList();
        }

        private static List<Hierarchy> CreateBranch(ParseState state) {
            return new List<Hierarchy> {
                new Hierarchy("HPE", state.Branch.ProductType.Id, state.Branch.ProductType.Name, null, "HPE", 1),
                new Hierarchy("HPE", state.Branch.MarketingCategory.Id, state.Branch.MarketingCategory.Name,state.Branch.ProductType.Id, "HPE", 2),
                new Hierarchy("HPE", state.Branch.MarketingSubCategory.Id, state.Branch.MarketingSubCategory.Name,state.Branch.MarketingCategory.Id, "HPE", 3),
                new Hierarchy("HPE", state.Branch.BigSeries.Id, state.Branch.BigSeries.Name,state.Branch.MarketingSubCategory.Id, "HPE", 4),
                new Hierarchy("HPE", state.Branch.SmallSeries.Id, state.Branch.SmallSeries.Name, state.Branch.BigSeries.Id,"HPE"),
            };
        }

        private static Product CreateProduct(ParseState state, Hierarchy hpe, Hierarchy productLine) {
            return new Product(
                state.PartnerPartNumber,
                state.PartNumber,
                state.Product.ManufacturerName,
                state.Product.ManufacturerCode,
                hpe.CategoryID,
                hpe.CategoryName,
                hpe.PartnerHierarchyCode,
                state.Product.Description,
                state.Product.DescriptionLong ?? state.Product.Description,
                state.Product.ProductCode,
                state.Product.IsEol,
                state.Product.ChangeDate,
                productLine.CategoryID,
                productLine.CategoryName,
                productLine.PartnerHierarchyCode
            );
        }

        private static Hierarchy GetProductLineHierarchyNode(ParseState state) {
            return state.Hierarchy.FirstOrDefault(x => x.Name == "PL") ??
                   state.Hierarchy.First() ?? new Hierarchy("", "", "", "", "");
        }

        private static Hierarchy GetHpeHierarchyNode(ParseState state) {
            return state.Hierarchy.FirstOrDefault(x => x.Name == "HPE") ??
                   state.Hierarchy.First() ?? new Hierarchy("", "", "", "", "");
        }
    }
}