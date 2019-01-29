using FluentAssertions;
using HPeSimpleParser.lib.Parser;
using Xunit;

namespace HPeSimpleParser.Test {
    public class GetPdfUrlTests {
        [Theory]
        [InlineData("https://www.hpe.com/h20195/v2/GetDocument.aspx?docname=a00002908enw","https://h20195.www2.hpe.com/v2/getpdf.aspx/a00002908enw.pdf")]
        [InlineData("https://www.hpe.com/h20195/v2/GetDocument.aspx?docname=a00008203enw","https://h20195.www2.hpe.com/v2/getpdf.aspx/a00008203enw.pdf")]
        [InlineData("https://www.hpe.com/h20195/v2/GetDocument.aspx?docname=c04545463","https://h20195.www2.hpe.com/v2/getpdf.aspx/c04545463.pdf")]
        public void Should_convert_aspx_to_pdf_links(string input, string expected) {
            HPEParserFunctions.GetPdfUrl(input).Should().Be(expected);
        }

        [Theory]
        [InlineData("https://h20195.www2.hpe.com/v2/getpdf.aspx/a00002908enw.pdf",
            "https://h20195.www2.hpe.com/v2/getpdf.aspx/a00002908enw.pdf")]
        [InlineData("https://www.hpe.com/h20195/v2/GetDocument.aspx?wrong=a00002908enw",
            "https://www.hpe.com/h20195/v2/GetDocument.aspx?wrong=a00002908enw")]
        [InlineData("https://www.hpe.com/h20195/v2/GetDocument.aspx?wrong$a00002908enw",
            "https://www.hpe.com/h20195/v2/GetDocument.aspx?wrong$a00002908enw")]
        [InlineData("https://www.hpe.com/h20195/v2/GetDocument.aspx",
            "https://www.hpe.com/h20195/v2/GetDocument.aspx")]
        public void Should_not_convert_correct_pdf_link(string input, string expected) {
            HPEParserFunctions.GetPdfUrl(input).Should().Be(expected);
        }

    }
}
