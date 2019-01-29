using System.Collections.Generic;

namespace HPeSimpleParser.lib.HPE.Model {
    public class Specifications {
        public Specifications(IReadOnlyList<Specification> specifications) {
            LabeledItems = specifications;
        }

        public IReadOnlyList<Specification> LabeledItems { get; }
    }
}