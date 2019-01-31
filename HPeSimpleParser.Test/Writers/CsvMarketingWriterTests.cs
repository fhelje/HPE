using System;
using System.Linq;
using System.Text;
using FluentAssertions;
using FSSystem.ContentAdapter.GenericOutput;
using FSSystem.ContentAdapter.GenericOutput.FileWriter;
using FSSystem.ContentAdapter.GenericOutput.FileWriter.Enums;
using FSSystem.ContentAdapter.Model;
using Microsoft.Extensions.ObjectPool;
using Xunit;

namespace HPeSimpleParser.Test.Writers {
    public class CsvMarketingWriterTests {
        [Fact]
        public void Should_create_line_with_all_properties_null() {
            var marketing = new Marketing {
                PartnerPartNumber = "0"
            };
            var writer =
                new CsvMarketingGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(marketing, out var data).Should().BeFalse();
            data.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Should_create_line_with_all_properties_set() {
            var expected = new object[] {
                "PartnerPartNumber", "MarketingCode", MarketingType.UniqueSellingPoint, "MarketingText",
                "MarketingTitle", "LanguageId"
            };

            var marketing = new Marketing {
                PartnerPartNumber = (string) expected[(int) CasMarketingColumnEnum.PartnerPartNumber],
                MarketingCode = (string) expected[(int) CasMarketingColumnEnum.MarketingCode],
                MarketingText = (string) expected[(int) CasMarketingColumnEnum.MarketingText],
                MarketingType = (MarketingType) expected[(int) CasMarketingColumnEnum.MarketingType],
                MarketingTitle = (string) expected[(int) CasMarketingColumnEnum.MarketingTitle],
                LanguageId = (string) expected[(int) CasMarketingColumnEnum.LanguageId]
            };

            var writer =
                new CsvMarketingGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(marketing, out var data).Should().BeTrue();
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
    }
}