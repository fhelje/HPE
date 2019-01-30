using System.Collections.Generic;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model
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