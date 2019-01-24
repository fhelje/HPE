using System.Collections.Generic;

namespace HPeSimpleParser.HPE.Model {
    public class Specifications {
        public Specifications(IReadOnlyList<Specification> specifications) {
            LabeledItems = specifications;
        }

        public IReadOnlyList<Specification> LabeledItems { get; }
    }
}