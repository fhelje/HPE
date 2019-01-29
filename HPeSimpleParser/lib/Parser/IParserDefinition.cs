using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.lib.Parser.State;

namespace HPeSimpleParser.lib.Parser {
    public interface IParserDefinition {
        Dictionary<InnerState, Func<ParseState, XmlReader, Task>> StateParser { get; }
        Dictionary<string, Func<ParseState, XmlReader, Task>> ParserFunctions { get; }
    }
}