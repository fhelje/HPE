using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace HPeSimpleParser.Test.Writers
{
    public class CsvMarketingWriterTests
    {
        [Fact]
        public void Should_create_line_with_all_properties_null()
        {
            var marketing = new Model.Marketing {
                PartnerPartNumber = "0",
            };
            var writer = new CsvMarketingGenerator();
            var data = writer.GenerateLine(marketing);
            data.Should().Be($"0{new string('\t', 3)}{Environment.NewLine}");
        }
        [Fact]
        public void Should_create_line_with_all_properties_set()
        {
            var expected = new object[] { "PartnerPartNumber", "MarketingCode", "MarketingText", "LanguageId" };

            var marketing = new Model.Marketing {
                PartnerPartNumber = (string)expected[(int)CasMarketingColumnEnum.PartnerPartNumber],
                MarketingCode = (string)expected[(int)CasMarketingColumnEnum.MarketingCode],
                MarketingText = (string)expected[(int)CasMarketingColumnEnum.MarketingText],
                LanguageId = (string)expected[(int)CasMarketingColumnEnum.LanguageId],
            };

            var writer = new CsvMarketingGenerator();
            var data = writer.GenerateLine(marketing);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
    }
}
