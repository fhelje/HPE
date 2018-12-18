using System;

namespace HPeSimpleParser.Model
{
    public class Product
    {
        public string PartnerPartNumber { get; set; }
        public string PartNumber { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerCode { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string DescriptionLong { get; set; }
        public string ProductCode { get; set; }
        public bool IsEol { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}