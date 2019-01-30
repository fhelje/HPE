using System.Collections.Generic;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model
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