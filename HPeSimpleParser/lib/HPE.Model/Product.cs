using System;

namespace HPeSimpleParser.lib.HPE.Model {
    public class Product {
        public Product(string partnerPartNumber, string partNumber, string manufacturerName, string manufacturerCode, string categoryID, string categoryName, string partnerHierarchyCode, string description, string descriptionLong, string productCode, bool isEol, DateTime changeDate, string alternateCategoryID, string alternateCategoryName, string alternatePartnerHierarchyCode) {
            PartnerPartNumber = partnerPartNumber;
            PartNumber = partNumber;
            ManufacturerName = manufacturerName;
            ManufacturerCode = manufacturerCode;
            CategoryID = categoryID;
            CategoryName = categoryName;
            PartnerHierarchyCode = partnerHierarchyCode;
            Description = description;
            DescriptionLong = descriptionLong;
            ProductCode = productCode;
            IsEol = isEol;
            ChangeDate = changeDate;
            AlternateCategoryID = alternateCategoryID;
            AlternateCategoryName = alternateCategoryName;
            AlternatePartnerHierarchyCode = alternatePartnerHierarchyCode;
        }

        public string PartnerPartNumber { get; set; }
        public string PartNumber { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerCode { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string PartnerHierarchyCode { get; set; }
        public string Description { get; set; }
        public string DescriptionLong { get; set; }
        public string ProductCode { get; set; }
        public bool IsEol { get; set; }
        public DateTime ChangeDate { get; set; }
        public string AlternateCategoryID { get; set; }
        public string AlternateCategoryName { get; set; }
        public string AlternatePartnerHierarchyCode { get; set; }
    }
}