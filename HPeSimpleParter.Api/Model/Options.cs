using System.Collections.Generic;

namespace HPeSimpleParter.Api.Model {
    public class Options {
        public Options() {
            Items = new List<Option>();
        }
        public int Count => Items.Count;
        public List<Option> Items { get; }
    }
}