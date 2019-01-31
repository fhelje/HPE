using System;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public class ProductBuilder {
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

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // ReSharper disable ConvertToConstant.Local
        private readonly string _partnerPartNumber = "PartnerPartNumber";
        private readonly string _partNumber = "PartNumber";
        private readonly string _manufacturerName = "ManufacturerName";
        private readonly string _manufacturerCode = "ManufacturerCode";
        private readonly string _categoryId = "CategoryId";
        private readonly string _categoryName = "CategoryName";
        private readonly string _partnerHierarchyCode = "PartnerHierarchyCode";
        private readonly string _description = "Description";
        private readonly string _descriptionLong = "DescriptionLong";
        private readonly string _productCode = "ProductCode";
        private readonly bool _isEol = true;
        private readonly DateTime _changeDate = new DateTime(2000, 1, 1);
        private readonly string _alternateCategoryID = "AlternateCategoryId";
        private readonly string _alternateCategoryName = "AlternateCategoryName";
        private readonly string _alternatePartnerHierarchyCode = "AlternatePartnerHierarchyCode";
        // ReSharper restore ConvertToConstant.Local
        // ReSharper restore FieldCanBeMadeReadOnly.Local
    }
}