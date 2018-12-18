using System.Collections.Generic;

namespace HPeSimpleParser.HPE.Model {
    public class Options {
        public Options() {
            Items = new List<Option>();
        }
        public int Count => Items.Count;
        public List<Option> Items { get; set; }
    }
}