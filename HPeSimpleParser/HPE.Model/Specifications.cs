using System.Collections.Generic;
using System.Linq;

namespace HPeSimpleParser.HPE.Model {
    public class Specifications {
        public Specifications() {
            Items = new List<Specification>();
        }

        public List<Specification> Items { get; set;}
        public Dictionary<string, string> Specs
        {
            get { return Items.ToDictionary(x => x.Name, x => x.Value); }
        }
    }
}