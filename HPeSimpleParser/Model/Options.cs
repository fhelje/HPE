using System.Collections.Generic;

namespace HPeSimpleParser.Model
{
    public class Options
    {
        public Options()
        {
            Items = new List<Option>();
        }
        public string PartnerPartNumber { get; set; }
        public List<Option> Items { get; set; }
    }
}