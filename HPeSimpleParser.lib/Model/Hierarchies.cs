using System.Collections.Generic;

namespace HPeSimpleParser.lib.Model
{
    public class Hierarchies
    {
        public Hierarchies()
        {
            Items = new List<Hierarchy>();
        }
        public List<Hierarchy> Items {get;set;}
        public string PartnerPartNumber { get; internal set; }
    }
}