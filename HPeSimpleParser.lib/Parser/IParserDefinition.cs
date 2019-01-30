using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser.State;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser {
    public interface IParserDefinition {
        Dictionary<InnerState, Func<ParseState, XmlReader, Task>> StateParser { get; }
        Dictionary<string, Func<ParseState, XmlReader, Task>> ParserFunctions { get; }
    }
}