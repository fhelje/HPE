using System.Collections.Generic;
using System.Linq;

namespace HPeSimpleParser.lib.HPE.Model {
    public class Specifications {
        public Specifications() {
            LabeledItems = new List<Specification>();
        }

        public List<Specification> LabeledItems { get; set;}
        public Dictionary<string, string> Specs
        {
            get { return LabeledItems.ToDictionary(x => x.Name, x => x.Value); }
        }
    }
}