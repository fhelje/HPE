using System.Collections.Generic;

namespace HPeSimpleParser.Model
{
    public class Specifications
    {
        public Specifications()
        {
            Items = new List<Specification>();
        }

        public string PartnerPartNumber { get; set; }
        
        public List<Specification> Items { get; set; }
    }
}