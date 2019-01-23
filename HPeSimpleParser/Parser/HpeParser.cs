using System.IO;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
    public class HpeParser {
        private readonly NodeParser _nodeParser;

        public HpeParser() {
            _nodeParser = new NodeParser();
        }
        public async Task<ProductRoot> ParseDocument(string file) {
            var productRoot = new ProductRoot(file);
            using (var stream = File.OpenRead(file)) {
                var settings = new XmlReaderSettings {
                    Async = true
                };
                var state = new ParseState(file);                

                using (var reader = XmlReader.Create(stream, settings)) {
                    while (await reader.ReadAsync()) {
                        switch (reader.NodeType) {
                            case XmlNodeType.Element:
                                state.Inc(reader.Name, XmlNodeType.Element, reader.IsEmptyElement);
                                await _nodeParser.Read(state, reader, productRoot);
                                break;
                            case XmlNodeType.Text:
                                state.SetNodeType(XmlNodeType.Text);
                                await _nodeParser.Read(state, reader, productRoot);
                                break;
                            case XmlNodeType.EndElement:
                                state.SetNodeType(XmlNodeType.EndElement);
                                state.SetName(reader.Name);
                                await _nodeParser.Read(state, reader, productRoot);
                                state.Dec();
                                break;
                        }
                    }

                }
            }
            productRoot.Links.SelectedImages = ImageHelpers.FilterImages(productRoot);
            return productRoot;
        }
    }
}
