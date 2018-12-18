using System;

namespace HPeSimpleParter.Api.Model {
    public class Product {
        public string ManufacturerName { get; set; }
        public string ManufacturerCode { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string DescriptionLong { get; set; }
        public string ProductCode { get; set; }
        public string IsEOL { get; set; }
        public string ChangeDate { get; set; }
        public bool CarePackRegistration { get; set; }
        public string Weight { get; set; }
        public string WeightWithPackage { get; set; }
        public string Dimensions { get; set; }
        public string DimensionsWithPackage { get; set; }
        public string Unspsc { get; set; }
        public DateTime? EndOfSupport { get; set; }
    }
}