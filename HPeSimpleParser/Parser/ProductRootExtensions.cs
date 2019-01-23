using System.Collections.Generic;
using System.Linq;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
    public static class ProductRootExtensions {
        public static Model.Item ToItem(this ProductRoot input) {
            var product = input.Product;
            var detail = input.Detail;
            var links = input.Links;
            var hierarchies = input.Hierarchy;
            var categoryID = GetConcatenatedHierarchyId(input.Branch);
            var marketing = input.Marketing;
            var specifications = input.Specifications;
            var options = input.Options;
            var hpe = hierarchies.FirstOrDefault(x => x.Name == "HPE") ?? hierarchies.First() ?? new Hierarchy("", "", "", "", "");
            var productLine = hierarchies.FirstOrDefault(x => x.Name == "PL") ?? hierarchies.First() ?? new Hierarchy("", "", "", "", "");
            decimal? height = null;
            decimal? width = null;
            decimal? depth = null;
            
            if (specifications.TryFindDimensionsInSpecifications(out var dim, "dimenmet", "dimenus")) {
                var dimension = dim.Value;
                height = dimension.GetHeightInMillimeter();
                width = dimension.GetWidthInMillimeter();
                depth = dimension.GetDepthInMillimeter();
            }
            return new Model.Item {
                PartnerPartNumber = input.PartnerPartNumber,
                Product = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    PartNumber = input.PartNumber,
                    CategoryID = categoryID,
                    CategoryName = hpe.CategoryName,
                    PartnerHierarchyCode = "HPE",
                    AlternateCategoryID = productLine.CategoryID,
                    AlternateCategoryName = productLine.CategoryName,
                    AlternatePartnerHierarchyCode = "PL",
                    Description = product.Description,
                    DescriptionLong= product.DescriptionLong ?? product.Description,
                    ChangeDate = product.ChangeDate,
                    IsEol= product.IsEOL,
                    ManufacturerCode = product.ManufacturerCode,
                    ManufacturerName = product.ManufacturerName,
                    ProductCode = product.ProductCode,
                },
                Detail = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    Unspsc = product.Unspsc,
                    //ProductPartnerID = detail.ProductPartnerID,
                    EndOfSupport = detail.EndOfSupport,
                    Weight = detail.Weight ?? specifications.TryFindWeigthInSpecifications("weightmet", "weightus"),
                    WeightwithPackage = detail.WeightwithPackage,
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
                    WarrantyTime = detail.WarrantyTime,
                },
                Link = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    PdfLinkDataSheet = links.PdfLinkDataSheet,
                    PdfLinkManual = links.PdfLinkManual,
                    Images = links.SelectedImages.Select(x=> new Model.Image {
                            ContentType = x.ContentType,
                            Height = x.Height,
                            Width = x.Width,
                            Url = x.ImageUrlHttp,
                            Title = x.FullTitle,
                        }
                    ).ToList(),

                },
                Hierarchies = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    Items= hierarchies.Select(x=>
                        new Model.Hierarchy{
                            CategoryID = x.CategoryID,
                            CategoryName = x.CategoryName,
                            Level = x.Level,
                            Name = x.Name,
                            ParentCategoryID= x.ParentCategoryID
                        }
                    ).ToList()
                },
                Marketing = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    LanguageId =input.LanguageId,
                    MarketingCode = marketing.MarketingCode,
                    MarketingText = marketing.MarketingText
                },
                Specifications = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    Items= specifications.LabeledItems.Select(x => new Model.Specification {
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
                    SupplierName = product.ManufacturerName,
                },
                Options = {
                    PartnerPartNumber = input.PartnerPartNumber,
                    Items = options.Items.Select(x => new Model.Option {
                        GroupId = x.OptionGroupCode.RemoveLineEndings(),
                        GroupName = x.OptionGroupName.RemoveLineEndings(),
                        Name = x.ManufacturerCode.RemoveLineEndings(),
                        PartNumber =x.OptionPartnerPartNumber.RemoveLineEndings(),
                    }).ToList()
                }
            };
        }

        private static string GetConcatenatedHierarchyId(List<Hierarchy> branch) {
            return string.Join("|", branch.Select(x => x.CategoryID));
        }
    }
}