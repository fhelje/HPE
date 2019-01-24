using System.Collections.Generic;

namespace HPeSimpleParser.HPE.Model {
    public class Options {
        public Options(IReadOnlyList<Option> options) {
            Items = options;
        }

        public IReadOnlyList<Option> Items { get; }
    }
}