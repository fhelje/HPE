using FluentAssertions;
using System;
using System.Text;
using HPeSimpleParser.lib.Generic.FileWriter;
using HPeSimpleParser.lib.Generic.Model;
using Microsoft.Extensions.ObjectPool;
using Xunit;

namespace HPeSimpleParser.Test.Writers {
    public class CsvProductWriterTests {
        [Fact]
        public void Should_create_line_with_all_properties() {
            var product = new Product {
                PartnerPartNumber = "0",
                PartNumber = "1",
                ManufacturerName = "2",
                ManufacturerCode = "3",
                CategoryID = "4",
                CategoryName = "5",
                Description = "6",
                PartnerHierarchyCode = "6.1",
                DescriptionLong = "7",
                ProductCode = "8",
                IsEol = true,
                ChangeDate = new DateTime(2000, 1, 1),
                AlternateCategoryID = "10",
                AlternateCategoryName = "11",
                AlternatePartnerHierarchyCode = "12"
            };
            var writer = new CsvProductGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(product, out var data);
            data.Should().Be($"0{FileSeparators.ColumnSeparator}1{FileSeparators.ColumnSeparator}2{FileSeparators.ColumnSeparator}3{FileSeparators.ColumnSeparator}4{FileSeparators.ColumnSeparator}5{FileSeparators.ColumnSeparator}6.1{FileSeparators.ColumnSeparator}6{FileSeparators.ColumnSeparator}7{FileSeparators.ColumnSeparator}8{FileSeparators.ColumnSeparator}True{FileSeparators.ColumnSeparator}2000-01-01T00:00:00{FileSeparators.ColumnSeparator}10{FileSeparators.ColumnSeparator}11{FileSeparators.ColumnSeparator}12{Environment.NewLine}");
        }
    }
}
