using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace HPeSimpleParser.Test.Writers
{
    public class CsvSupplierWriterTests
    {
        //[Fact]
        //public void Should_create_line_with_all_properties_null()
        //{
        //    var marketing = new Model.Supplier {
        //        PartnerPartNumber = "0",
        //    };
        //    var writer = new CsvSupplierGenerator();
        //    var data = writer.GenerateLine(marketing);
        //    data.Should().Be($"0{new string('\t', 2)}{Environment.NewLine}");
        //}
        //[Fact]
        //public void Should_create_line_with_all_properties_set()
        //{
        //    var expected = new object[] { "PartnerPartNumber", "SupplierId", "SupplierName" };

        //    var marketing = new Model.Supplier {
        //        PartnerPartNumber = (string)expected[(int)CasSupplierColumnEnum.PartnerPartNumber],
        //        SupplierId = (string)expected[(int)CasSupplierColumnEnum.SupplierId],
        //        SupplierName = (string)expected[(int)CasSupplierColumnEnum.SupplierName],
        //    };

        //    var writer = new CsvSupplierGenerator();
        //    var data = writer.GenerateLine(marketing);
        //    data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        //}
    }
}
