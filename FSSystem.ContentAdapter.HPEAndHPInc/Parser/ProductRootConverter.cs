using System.Linq;
using FSSystem.ContentAdapter.GenericOutput;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using FSSystem.ContentAdapter.Model;
using Image = FSSystem.ContentAdapter.Model.Image;
using Option = FSSystem.ContentAdapter.Model.Option;
using Specification = FSSystem.ContentAdapter.Model.Specification;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser {
    public static class ProductRootConverter {
        public static Item ToItem(this ProductRoot input) {
            var product = input.Product;
            var detail = input.Detail;
            var links = input.Links;
            var hierarchies = input.Hierarchy;
            var marketing = input.Marketing;
            var specifications = input.Specifications;
            var options = input.Options;
            decimal? height = null;
            decimal? width = null;
            decimal? depth = null;

            if (specifications.LabeledItems.TryFindDimensionsInSpecifications(out var dim, "dimenmet", "dimenus")) {
                if (dim != null) {
                    var dimension = dim.Value;
                    height = dimension.GetHeightInMillimeter();
                    width = dimension.GetWidthInMillimeter();
                    depth = dimension.GetDepthInMillimeter();
                }
            }

            var arr = new string[input.ProductVariants.Count + 1];
            arr[0] = "";
            for (var i = 0; i < input.ProductVariants.Count; i++) arr[i + 1] = input.ProductVariants[i].Opt;
            return new Item {
                PartnerPartNumber = input.PartnerPartNumber,
                Product = {
                    PartnerPartNumber = product.PartnerPartNumber,
                    PartNumber = product.PartNumber,
                    CategoryID = product.CategoryID,
                    CategoryName = product.CategoryName,
                    PartnerHierarchyCode = product.PartnerHierarchyCode,
                    AlternateCategoryID = input.Product.AlternateCategoryID,
                    AlternateCategoryName = product.AlternateCategoryName,
                    AlternatePartnerHierarchyCode = product.AlternatePartnerHierarchyCode,
                    Description = product.Description,
                    DescriptionLong = product.DescriptionLong ?? product.Description,
                    ChangeDate = product.ChangeDate,
                    IsEol = product.IsEol,
                    ManufacturerCode = product.ManufacturerCode,
                    ManufacturerName = product.ManufacturerName,
                    ProductCode = product.ProductCode
                },
                Detail = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    Unspsc = detail.Unspsc,
                    //ProductPartnerID = detail.ProductPartnerID,
                    EndOfSupport = detail.EndOfSupport,
                    Weight = detail.Weight ??
                             specifications.LabeledItems.TryFindWeightInSpecifications("weightmet", "weightus"),
                    WeightwithPackage = detail.WeightWithPackage,
                    Volume = detail.Volume,
                    PalletSize = detail.PalletSize,
                    Width = detail.Width ?? width,
                    Height = detail.Height ?? height,
                    Depth = detail.Depth ?? depth,
                    PackQty = detail.PackQty,
                    MinimumOrderQty = detail.MinimumOrderQty,
                    IsRequireSerialNumber = detail.IsRequireSerialNumber,
                    ManufacturingCountry = detail.ManufacturingCountry,
                    CustomsStatisticsNumber = detail.CustomsStatisticsNumber,
                    ExtendedWarranty = detail.ExtendedWarranty,
                    ErpAltPartNumber = detail.ErpAltPartNumber,
                    TeleSalesFlag = detail.TeleSalesFlag,
                    ItemDefFulfillSource = detail.ItemDefFulfillSource,
                    MeterEnabled = detail.MeterEnabled,
                    SwedishChemicalTaxReduction = detail.SwedishChemicalTaxReduction,
                    WarrantyTime = detail.WarrantyTime
                },
                Link = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    PdfLinkDataSheet = links.PdfLinkDataSheet,
                    PdfLinkManual = links.PdfLinkManual,
                    Images = links.SelectedImages.Select(x => new Image {
                            ContentType = x.ContentType,
                            Height = x.Height,
                            Width = x.Width,
                            Url = x.ImageUrlHttp,
                            Title = x.FullTitle
                        }
                    ).ToList()
                },
                Hierarchies = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    Items = hierarchies.Select(x =>
                        new Model.Hierarchy {
                            CategoryID = x.CategoryID,
                            CategoryName = x.CategoryName,
                            Level = x.Level,
                            Name = x.Name,
                            ParentCategoryID = x.ParentCategoryID
                        }
                    ).ToList()
                },
                Marketing = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    LanguageId = input.LanguageId,
                    MarketingCode = marketing.MarketingCode,
                    MarketingText = marketing.MarketingText
                },
                Specifications = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    Items = specifications.LabeledItems.Select(x => new Specification {
                        GroupId = x.GroupId,
                        GroupName = x.GroupName,
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type,
                        UnitOfMeasure = x.UnitOfMeasure,
                        Value = x.Value,
                        Label = x.Label
                    }).ToList()
                },
                Supplier = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    SupplierId = product.ManufacturerCode,
                    SupplierName = product.ManufacturerName
                },
                Options = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    Items = options.Items.Select(x => new Option {
                        GroupId = x.OptionGroupCode.RemoveLineEndings(),
                        GroupName = x.OptionGroupName.RemoveLineEndings(),
                        Name = x.ManufacturerCode.RemoveLineEndings(),
                        PartNumber = x.OptionPartnerPartNumber.RemoveLineEndings()
                    }).ToList()
                },
                ProductVariants = arr
            };
        }
    }
}