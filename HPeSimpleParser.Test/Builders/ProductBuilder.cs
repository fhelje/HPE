using System;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public class ProductBuilder {
        private string _partnerPartNumber = "PartnerPartNumber";
        private string _partNumber = "PartNumber";
        private string _manufacturerName = "ManufacturerName";
        private string _manufacturerCode = "ManufacturerCode";
        private string _categoryId = "CategoryId";
        private string _categoryName = "CategoryName";
        private string _partnerHierarchyCode = "PartnerHierarchyCode";
        private string _description = "Description";
        private string _descriptionLong = "DescriptionLong";
        private string _productCode = "ProductCode";
        private bool _isEol = true;
        private DateTime _changeDate = new DateTime(2000, 1, 1);
        private string _alternateCategoryID = "AlternateCategoryId";
        private string _alternateCategoryName = "AlternateCategoryName";
        private string _alternatePartnerHierarchyCode = "AlternatePartnerHierarchyCode";
        private ProductBuilder() {
        }
        public static ProductBuilder With() {
            return new ProductBuilder();
        }


        public Product Build() {
            return new Product(
                _partnerPartNumber,
                _partNumber,
                _manufacturerName,
                _manufacturerCode,
                _categoryId,
                _categoryName,
                _partnerHierarchyCode,
                _description,
                _descriptionLong,
                _productCode,
                _isEol,
                _changeDate,
                _alternateCategoryID,
                _alternateCategoryName,
                _alternatePartnerHierarchyCode

            );
        }
    }
}