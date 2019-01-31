using System.Collections.Generic;

namespace FSSystem.ContentAdapter.Model {
    public class Hierarchies {
        public Hierarchies() {
            Items = new List<Hierarchy>();
        }

        public List<Hierarchy> Items { get; set; }

        public string PartnerPartNumber { get; set; }
    }
}