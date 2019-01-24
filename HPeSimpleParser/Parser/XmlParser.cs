using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
    public class XmlParser {
        private readonly NodeParser _nodeParser;

        public XmlParser(IParserDefinition parserDefinition) {
            _nodeParser = new NodeParser(parserDefinition);
        }
        public async Task<ProductRoot> ParseDocument(string file) {
            var state = new ParseState(file);                
            using (var stream = File.OpenRead(file)) {
                var settings = new XmlReaderSettings {
                    Async = true
                };

                using (var reader = XmlReader.Create(stream, settings)) {
                    while (await reader.ReadAsync()) {
                        switch (reader.NodeType) {
                            case XmlNodeType.Element:
                                state.Inc(reader.Name, XmlNodeType.Element, reader.IsEmptyElement);
                                await _nodeParser.Read(state, reader);
                                break;
                            case XmlNodeType.Text:
                                state.SetNodeType(XmlNodeType.Text);
                                await _nodeParser.Read(state, reader);
                                break;
                            case XmlNodeType.EndElement:
                                state.SetNodeType(XmlNodeType.EndElement);
                                state.SetName(reader.Name);
                                await _nodeParser.Read(state, reader);
                                state.Dec();
                                break;
                        }
                    }

                }
            }
            var productRoot = CreateProductRoot(file, state);

            return productRoot;
        }

        private static ProductRoot CreateProductRoot(string file, ParseState state) {
            var hierarchies = state.Hierarchy;
            var hpe = hierarchies.FirstOrDefault(x => x.Name == "HPE") ??
                      hierarchies.First() ?? new Hierarchy("", "", "", "", "");
            var productLine = hierarchies.FirstOrDefault(x => x.Name == "PL") ??
                              hierarchies.First() ?? new Hierarchy("", "", "", "", "");
            var product = new Product(
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
            var branch = new List<Hierarchy> {
                new Hierarchy("HPE", state.Branch.ProductType.Id, state.Branch.ProductType.Name, null, "HPE"),
                new Hierarchy("HPE", state.Branch.MarketingCategory.Id, state.Branch.MarketingCategory.Name,
                    state.Branch.ProductType.Id, "HPE"),
                new Hierarchy("HPE", state.Branch.MarketingSubCategory.Id, state.Branch.MarketingSubCategory.Name,
                    state.Branch.MarketingCategory.Id, "HPE"),
                new Hierarchy("HPE", state.Branch.BigSeries.Id, state.Branch.BigSeries.Name,
                    state.Branch.MarketingSubCategory.Id, "HPE"),
                new Hierarchy("HPE", state.Branch.SmallSeries.Id, state.Branch.SmallSeries.Name, state.Branch.BigSeries.Id,
                    "HPE"),
            };
            var productVariants =
                state.ProductVariants.Select(x => new ProductVariant(x.Description, x.Opt, x.UpcCode)).ToList();

            var marketing = new Marketing(state.Marketing.MarketingText, "", "", "");
            var optionsItems = state.Options.Select(x =>
                new Option(x.ManufacturerCode, x.OptionPartnerPartNumber, x.OptionGroupCode, x.OptionGroupName)).ToList();
            var specificationsLabeledItems = state.Specifications.Select(x =>
                new Specification(x.Name, x.Value, x.Type, x.UnitOfMeasure, x.Id, x.GroupId, x.GroupName, x.Label)).ToList();
            var linksSelectedImages = ImageHelpers.FilterImages(state.Links.ImageLinks);
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
                weight: specificationsLabeledItems.TryFindWeigthInSpecifications("weightmet", "weightus"),
                height: height,
                width: width,
                depth: depth
            );
            var productRoot = new ProductRoot(
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
            return productRoot;
        }
    }
}
