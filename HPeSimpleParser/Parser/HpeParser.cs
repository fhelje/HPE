using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
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
}
