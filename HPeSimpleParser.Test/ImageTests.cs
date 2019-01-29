using FluentAssertions;
using HPeSimpleParser.lib.HPE.Model;
using HPeSimpleParser.lib.Parser.HPE;
using Xunit;

namespace HPeSimpleParser.Test {
    public class ImageTests {
        [Theory]
        [InlineData(null, null, SizeCategoryEnum.Wrong)]
        [InlineData(null, "0", SizeCategoryEnum.Wrong)]
        [InlineData("", "0", SizeCategoryEnum.Wrong)]
        [InlineData("X", "0", SizeCategoryEnum.Wrong)]
        [InlineData("0", "", SizeCategoryEnum.Wrong)]
        [InlineData("0", "X", SizeCategoryEnum.Wrong)]
        [InlineData("0", null, SizeCategoryEnum.Wrong)]
        [InlineData("0.0", "0.0", SizeCategoryEnum.Wrong)]
        [InlineData("0", "0", SizeCategoryEnum.Small)]
        [InlineData("150", "200", SizeCategoryEnum.Small)]
        [InlineData("151", "200", SizeCategoryEnum.Medium)]
        [InlineData("150", "201", SizeCategoryEnum.Medium)]
        [InlineData("300", "400", SizeCategoryEnum.Medium)]
        [InlineData("301", "400", SizeCategoryEnum.Large)]
        [InlineData("300", "401", SizeCategoryEnum.Large)]
        [InlineData("600", "800", SizeCategoryEnum.Large)]
        [InlineData("601", "800", SizeCategoryEnum.XLarge)]
        [InlineData("600", "801", SizeCategoryEnum.XLarge)]
        [InlineData("6000", "8000", SizeCategoryEnum.XLarge)]
        public void Should_return_correct_image_category(string height, string width, SizeCategoryEnum category) {
            var ip = new HPEImageParser();
            ip.GetSizeCategory(height, width).Should().Be(category);
        }
    }
}