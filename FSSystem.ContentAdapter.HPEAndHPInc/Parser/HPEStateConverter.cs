using System;
using System.Collections.Generic;
using System.Linq;
using FSSystem.ContentAdapter.HPEAndHPInc.Enums;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser.State;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser {
    public class HPEStateConverter {
        public ProductRoot CreateProductRoot(string file, ParseState state, VariantType variantType) {
            var specificationsLabeledItems = CreateSpecificationsLabeledItems(state);
            if (string.IsNullOrEmpty(state.PartNumber)) {
                Console.WriteLine(state.File);
            }

            var productRoot = CreateProductRoot(
                file,
                state,
                CreateProduct(state, GetHpeHierarchyNode(state), state.Branch, GetProductLineHierarchyNode(state)),
                CreateBranch(state, variantType),
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
                specificationsLabeledItems.TryFindWeightInSpecifications("weightmet", "weightus"),
                height: height,
                width: width,
                depth: depth,
                unspsc: unspsc
            );
            return detail;
        }

        private static List<Specification> CreateSpecificationsLabeledItems(ParseState state) {
            return state.Specifications.Select(x =>
                    new Specification(x.Name, x.Value, x.Type, x.UnitOfMeasure, x.Id, x.GroupId, x.GroupName, x.Label))
                .ToList();
        }

        private static List<Option> CreateOptionsItems(ParseState state) {
            return state.Options.Select(x =>
                    new Option(x.ManufacturerCode, x.OptionPartnerPartNumber, x.OptionGroupCode, x.OptionGroupName))
                .ToList();
        }

        private static Marketing CreateMarketing(ParseState state) {
            return new Marketing(state.Marketing?.MarketingText, "", "", "");
        }

        private static List<ProductVariant> CreateProductVariants(ParseState state) {
            return state.ProductVariants.Select(x => new ProductVariant(x.Description, x.Opt, x.UpcCode)).ToList();
        }

        private static List<Hierarchy> CreateBranch(ParseState state, VariantType variantType) {
            var name = variantType == VariantType.HPE ? "HPE" : "HPInc";
            return new List<Hierarchy> {
                new Hierarchy(name, state.Branch.ProductType.Id, state.Branch.ProductType.Name, null, name, 1),
                new Hierarchy(name, state.Branch.MarketingCategory.Id, state.Branch.MarketingCategory.Name,
                    state.Branch.ProductType.Id, name, 2),
                new Hierarchy(name, state.Branch.MarketingSubCategory.Id, state.Branch.MarketingSubCategory.Name,
                    state.Branch.MarketingCategory.Id, name, 3),
                new Hierarchy(name, state.Branch.BigSeries.Id, state.Branch.BigSeries.Name,
                    state.Branch.MarketingSubCategory.Id, name, 4),
                new Hierarchy(name, state.Branch.SmallSeries.Id, state.Branch.SmallSeries.Name,
                    state.Branch.BigSeries.Id, name)
            };
        }

        private static Product CreateProduct(ParseState state, Hierarchy hpe, Branch branch, Hierarchy productLine) {
            return new Product(
                state.PartnerPartNumber,
                state.PartNumber,
                state.Product.ManufacturerName,
                state.Product.ManufacturerCode,
                GetHierarchyCategoryID(branch),
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
            if (state.Hierarchy == null || state.Hierarchy.Count == 0) {
                return new Hierarchy("", "", "", "", "");
            }

            return state.Hierarchy.Find(x => x.Name == "PL" || x.Name == "HPIncPL") ?? state.Hierarchy[0];
        }

        private static Hierarchy GetHpeHierarchyNode(ParseState state) {
            if (state.Hierarchy == null || state.Hierarchy.Count == 0) {
                return new Hierarchy("", "", "", "", "");
            }

            return state.Hierarchy.Find(x => x.Name == "HPE" || x.Name == "HPInc") ?? state.Hierarchy[0];
        }

        private static string GetHierarchyCategoryID(Branch branch) {
            return
                $"{branch.ProductType.Id}|{branch.MarketingCategory.Id}|{branch.MarketingSubCategory.Id}|{branch.BigSeries.Id}|{branch.SmallSeries.Id}";
        }
    }

    public class HPIncStateConverter {
        public ProductRoot CreateProductRoot(string file, ParseState state) {
            var specificationsLabeledItems = CreateSpecificationsLabeledItems(state);
            var productRoot = CreateProductRoot(
                file,
                state,
                CreateProduct(state, GetHpeHierarchyNode(state), state.Branch, GetProductLineHierarchyNode(state)),
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
                specificationsLabeledItems.TryFindWeightInSpecifications("weightmet", "weightus"),
                height: height,
                width: width,
                depth: depth
            );
            return detail;
        }

        private static List<Specification> CreateSpecificationsLabeledItems(ParseState state) {
            return state.Specifications.Select(x =>
                    new Specification(x.Name, x.Value, x.Type, x.UnitOfMeasure, x.Id, x.GroupId, x.GroupName, x.Label))
                .ToList();
        }

        private static List<Option> CreateOptionsItems(ParseState state) {
            return state.Options.Select(x =>
                    new Option(x.ManufacturerCode, x.OptionPartnerPartNumber, x.OptionGroupCode, x.OptionGroupName))
                .ToList();
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
                new Hierarchy("HPE", state.Branch.MarketingCategory.Id, state.Branch.MarketingCategory.Name,
                    state.Branch.ProductType.Id, "HPE", 2),
                new Hierarchy("HPE", state.Branch.MarketingSubCategory.Id, state.Branch.MarketingSubCategory.Name,
                    state.Branch.MarketingCategory.Id, "HPE", 3),
                new Hierarchy("HPE", state.Branch.BigSeries.Id, state.Branch.BigSeries.Name,
                    state.Branch.MarketingSubCategory.Id, "HPE", 4),
                new Hierarchy("HPE", state.Branch.SmallSeries.Id, state.Branch.SmallSeries.Name,
                    state.Branch.BigSeries.Id, "HPE")
            };
        }

        private static Product CreateProduct(ParseState state, Hierarchy hpe, Branch branch, Hierarchy productLine) {
            return new Product(
                state.PartnerPartNumber,
                state.PartNumber,
                state.Product.ManufacturerName,
                state.Product.ManufacturerCode,
                GetHierarchyCategoryID(branch),
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

        private static string GetHierarchyCategoryID(Branch branch) {
            return
                $"{branch.ProductType.Id}|{branch.MarketingSubCategory.Id}|{branch.MarketingSubCategory.Id}|{branch.BigSeries.Id}|{branch.SmallSeries.Id}";
        }

        private static Hierarchy GetProductLineHierarchyNode(ParseState state) {
            return state.Hierarchy.Find(x => x.Name == "PL") ??
                   state.Hierarchy[0] ?? new Hierarchy("", "", "", "", "");
        }

        private static Hierarchy GetHpeHierarchyNode(ParseState state) {
            return state.Hierarchy.Find(x => x.Name == "HPE") ??
                   state.Hierarchy[0] ?? new Hierarchy("", "", "", "", "");
        }
    }
}