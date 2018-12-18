using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HPeSimpleParser.Test.Writers
{

    public class CsvDetailWriterTests
    {
        [Fact]
        public void Should_create_line_with_all_properties_null()
        {
            var detail = new Model.Detail {
                PartnerPartNumber = "0",
            };
            var writer = new CsvDetailGenerator();
            var data = writer.GenerateLine(detail);
            data.Should().Be($"0{new string('\t', 21)}{Environment.NewLine}");
        }
        [Fact]
        public void Should_create_line_with_all_properties_set()
        {
            var expected = new object[] { "PartnerPartNumber", 1M, 2M, 3M, 4M, 5M, 6M, 7M, 8, 9M, true, "ManufacturingCountry", "CustomsStatisticsNumber", true, 10, new DateTime(2000, 1, 1), "ErpAltPartNumber", true, "ItemDefFulfillSource", true, 11M, 12 };
            
            var detail = new Model.Detail {
                PartnerPartNumber = (string)expected[(int)CasDetailColumnEnum.PartnerPartNumber],
                Weight = (decimal)expected[(int)CasDetailColumnEnum.Weight],
                WeightwithPackage = (decimal)expected[(int)CasDetailColumnEnum.WeightwithPackage],
                Volume = (decimal)expected[(int)CasDetailColumnEnum.Volume],
                PalletSize = (decimal)expected[(int)CasDetailColumnEnum.PalletSize],
                Width = (decimal)expected[(int)CasDetailColumnEnum.Width],
                Height = (decimal)expected[(int)CasDetailColumnEnum.Height],
                Depth = (decimal)expected[(int)CasDetailColumnEnum.Depth],
                PackQty = (int)expected[(int)CasDetailColumnEnum.PackQty],
                MinimumOrderQty = (decimal)expected[(int)CasDetailColumnEnum.MinimumOrderQty],
                IsRequireSerialNumber = (bool)expected[(int)CasDetailColumnEnum.IsRequireSerialNumber],
                ManufacturingCountry = (string)expected[(int)CasDetailColumnEnum.ManufacturingCountry],
                CustomsStatisticsNumber = (string)expected[(int)CasDetailColumnEnum.CustomsStatisticsNumber],
                ExtendedWarranty = (bool)expected[(int)CasDetailColumnEnum.ExtendedWarranty],
                Unspsc = (int)expected[(int)CasDetailColumnEnum.Unspsc],
                EndOfSupport = (DateTime)expected[(int)CasDetailColumnEnum.EndOfSupport],
                ErpAltPartNumber = (string)expected[(int)CasDetailColumnEnum.ErpAltPartNumber],
                TeleSalesFlag = (bool)expected[(int)CasDetailColumnEnum.TeleSalesFlag],
                ItemDefFulfillSource = (string)expected[(int)CasDetailColumnEnum.ItemDefFulfillSource],
                MeterEnabled = (bool)expected[(int)CasDetailColumnEnum.MeterEnabled],
                SwedishChemicalTaxReduction = (decimal)expected[(int)CasDetailColumnEnum.SwedishChemicalTaxReduction],
                WarrantyTime = (int)expected[(int)CasDetailColumnEnum.WarrantyTime],
            };
            
            var writer = new CsvDetailGenerator();
            var data = writer.GenerateLine(detail);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
    }
}
