using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
    internal class NodeParser {
        private readonly IParserDefinition _parserDefinition;

        public NodeParser(IParserDefinition parserDefinition) {
            _parserDefinition = parserDefinition;
        }
        public async Task Read(ParseState state, XmlReader reader) {
            if (_parserDefinition.ParserFunctions.ContainsKey(state.CurrentName)) {
                await _parserDefinition.ParserFunctions[state.CurrentName](state, reader);
            }
            if (_parserDefinition.StateParser.ContainsKey(state.InnerState)) {
                await _parserDefinition.StateParser[state.InnerState](state, reader);

            }
        }
    }
}