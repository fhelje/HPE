using System;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model {
    public class ProductState {
        public bool CarePackRegistration { get; set; }
        public string DescriptionLong { get; set; }
        public int Unspsc { get; set; }
        public string ManufacturerCode { get; set; }
        public string ManufacturerName { get; set; }
        public string LanguageId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string PartnerHierarchyCode { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public bool IsEol { get; set; }
        public string AlternateCategoryId { get; set; }
        public string AlternateCategoryName { get; set; }
        public string AlternatePartnerHierarchyCode { get; set; }
    }
}