using System.IO;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.lib.Enums;
using HPeSimpleParser.lib.HPE.Model;
using HPeSimpleParser.lib.Parser.State;

namespace HPeSimpleParser.lib.Parser {
    public class XmlParser {
        private readonly NodeParser _nodeParser;

        public XmlParser(IParserDefinition parserDefinition) {
            _nodeParser = new NodeParser(parserDefinition);
        }
        public async Task<ProductRoot> ParseDocument(string file, VariantType variant) {
            var state = new ParseState(file, variant);                
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
            var productRoot =new HPEStateConverter().CreateProductRoot(file, state, variant);

            return productRoot;
        }
    }
}
