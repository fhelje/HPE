using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.lib.HPE.Model;

namespace HPeSimpleParser.lib.Parser {

    public static class ProductRootExtensions {
        public static Model.Item ToItem(this ProductRoot input) {
            var product = input.Product;
            var detail = input.Detail;
            var links = input.Links;
            var hierarchies = input.Hierarchy;
            var marketing = input.Marketing;
            var specifications = input.Specifications;
            var options = input.Options;
            var h = hierarchies.FirstOrDefault(x => x.Name == "HPE") ?? hierarchies.First() ?? new Hierarchy("", "", "", "");
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
                    CategoryID = h.CategoryID,
                    CategoryName = h.CategoryName,
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
                        Value = x.Value
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
    }

    public class HpeParser {
        public async Task<ProductRoot> ParseDocument(string file) {
            var productRoot = new ProductRoot();
            using (var stream = File.OpenRead(file)) {
                var settings = new XmlReaderSettings {
                    Async = true
                };
                var state = new ParseState(file);
                var nodeParser = new NodeParser();

                using (var reader = XmlReader.Create(stream, settings)) {
                    while (await reader.ReadAsync()) {
                        switch (reader.NodeType) {
                            case XmlNodeType.Element:
                                state.Inc(reader.Name, XmlNodeType.Element, reader.IsEmptyElement);
                                await nodeParser.Read(state, reader, productRoot);
                                break;
                            case XmlNodeType.Text:
                                state.SetNodeType(XmlNodeType.Text);
                                await nodeParser.Read(state, reader, productRoot);
                                break;
                            case XmlNodeType.EndElement:
                                state.SetNodeType(XmlNodeType.EndElement);
                                state.SetName(reader.Name);
                                await nodeParser.Read(state, reader, productRoot);
                                state.Dec();
                                break;
                        }
                    }

                }
            }
            productRoot.Links.SelectedImages = FilterImages(productRoot);
            return productRoot;
        }

        public static Image[] FilterImages(ProductRoot item) {
            var selectedImages = new List<Image>();
            if (item?.Links?.ImageLinks == null || item.Links.ImageLinks.Count < 1) {
                return new Image[0];
            }

            // Group by master object name
            var monGroups = item.Links.ImageLinks.GroupBy(x => x.MasterObjectName);
            foreach (var monGroup in monGroups) {

                // Group by SizeCategory
                var sizeGroups = monGroup.GroupBy(x => x.SizeCategory);
                foreach (var sizeGroup in sizeGroups) {
                    if (sizeGroup.Key == SizeCategoryEnum.Wrong || sizeGroup.Key == SizeCategoryEnum.XLarge ||
                        sizeGroup.Key == SizeCategoryEnum.Small) {
                        continue;
                    }

                    var orientationGroups = sizeGroup.GroupBy(g => g.Orientation);
                    foreach (var orientationGroup in orientationGroups) {
                        selectedImages.Add(orientationGroup.OrderBy(x => x.CmgAcronym)
                            .ThenBy(x => x.ContentTypePriority)
                            .ThenBy(x => x.DocumentTypeDetailPriority)
                            .ThenByDescending(x => x.Height)
                            .ThenByDescending(x => x.Width)

                            .First());
                    }
                }
            }

            if (selectedImages.Count > selectedImages.Where(RemoveSpecificDocDetailTypes).Count()) {
                selectedImages = selectedImages.Where(RemoveSpecificDocDetailTypes).ToList();
            }

            return selectedImages.DistinctBy(x => x.ImageUrlHttp).ToArray();
            // Order secondly by contentType png, jpg, gif
            // Pick images by size range
        }

        private static bool RemoveSpecificDocDetailTypes(Image arg) {
            switch (arg.DocumentTypeDetail) {
                case "product image hero":
                case "product image - not as shown":
                case "Concept Graphic":
                case "product image hero,product image":
                case "icon":
                    return false;
                default:
                    return true;
            }
        }
    }

    public static class LinqHelpers {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source) {
                if (seenKeys.Add(keySelector(element))) {
                    yield return element;
                }
            }
        }

        public static decimal? TryFindWeigthInSpecifications(this Specifications specifications, params string[] names) {
            if (specifications == null) {
                return null;
            }
            foreach (var name in names) {
                var spec = specifications.LabeledItems.Find(x => x.Name == name);
                if (spec != null) {
                    var (weight, uom) = DetailValueParser.TryParseWeight(spec.Value);
                    if (uom != WeightUnitOfMeasure.None) {
                        switch (uom) {
                            case WeightUnitOfMeasure.Kilogram:
                                return weight.Value;
                            case WeightUnitOfMeasure.Gram:
                                return weight.Value * 1000;
                            case WeightUnitOfMeasure.Pounds:
                                return weight.Value * 0.45359237M;
                            case WeightUnitOfMeasure.Ounces:
                                return weight.Value * 0.02834952M;
                        }
                    }
                }
            }
            return null;
        }

        public static bool TryFindDimensionsInSpecifications(this Specifications specifications, out Dimension? dimension, params string[] names) {
            dimension = null;
            if (specifications == null) {
                return false;
            }
            foreach (var name in names) {
                var spec = specifications.LabeledItems.Find(x => x.Name == name);
                if (spec != null) {
                    var dim = DetailValueParser.ParseDimensions(spec.Value);
                    if (dim.HasValues) {
                        dimension = dim;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
