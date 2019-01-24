using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HPeSimpleParser.Parser;
using Xunit;

namespace HPeSimpleParser.Test {
    public class ParserTests {
        [Theory]
        [InlineData("Sample_1.xml", 4, 14, 23)]
        public async Task Should_parse_sample(string fileName, int optionCount, int specificationCount, int opcCodes) {
            var filePath = Path.Combine("Data", fileName);
            var parser = new XmlParser(new HPEParserDefinition());
            var data = await parser.ParseDocument(filePath);

            data.PartNumber.Should().Be("JL380A");
            data.PartnerPartNumber.Should().Be("JL380A");
            data.LanguageId.Should().Be("gb-en");

            data.Product.PartnerPartNumber.Should().Be("JL380A");
            data.Product.PartNumber.Should().Be("JL380A");
            data.Product.CategoryID.Should().Be("1009689650");
            data.Product.CategoryName.Should().Be("HPE OfficeConnect 1920S Switch Series");
            data.Product.PartnerHierarchyCode.Should().Be("HPE");
            data.Product.AlternateCategoryID.Should().Be("I5");
            data.Product.AlternateCategoryName.Should().Be("I5");
            data.Product.AlternatePartnerHierarchyCode.Should().Be("PL");
            data.Product.IsEol.Should().Be(false);
            data.Product.ChangeDate.Should().Be(DateTime.Parse("2018-11-20 14:40:30"));
            data.Product.Description.Should().Be("HPE OfficeConnect 1920S 8G Switch");
            data.Product.DescriptionLong.Should().Be("HPE OfficeConnect 1920S 8G Switch");
            data.Product.ManufacturerCode.Should().Be("HPE");
            data.Product.ManufacturerName.Should().Be("HPE");
            data.Product.ProductCode.Should().BeNull();


            data.Options.Items.Should().HaveCount(optionCount);
            data.Options.Items.First().OptionGroupName.Should().NotBeEmpty();
            data.Specifications.LabeledItems.Should().HaveCount(specificationCount);
            data.ProductVariants.Should().HaveCount(opcCodes);
        }
        [Theory]
        [InlineData("Sample_1.xml")]
        public async Task Should_parse_key_selling_point_sample(string fileName) {
            var filePath = Path.Combine("Data", fileName);
            var parser = new XmlParser(new HPEParserDefinition());
            var data = await parser.ParseDocument(filePath);
            data.Marketing.MarketingText.Should().Be("<h1>Simplifies Network Deployment and Management for Small Organizations</h1><p><span>The HPE OfficeConnect 1920S Switch Series features easy-to-use, out of the box, plug-and-play deployment.</span></p><p><span>The series offers a complete portfolio of choices for increased small business flexibility. It consists of six rack-mountable models including 8G-, 24G- and 48G-port with and without PoE+. The 24G- and 48G-port PoE+ models offer SPF fiber connectivity.</span></p><p><span>These smart-managed switches use an intuitive Web management interface, to simplify deployment and management while offering granular control of key features.</span></p> <h1>Keeps Your Business Protected with Enhanced Security</h1><p><span>The HPE OfficeConnect 1920S Switch Series supports Energy Efficient Ethernet for lower power consumption and an enhanced feature set for more robust operation.</span></p><p><span>Enhanced security features such as Access Control List, IEEE 802.1x and VLANs guard your network from unwanted or unauthorized access.</span></p><p><span>Management security restricts access to critical configuration commands, offers multiple privilege levels with password protection and supports secure http (https).</span></p> <h1>Delivers Better Performance at a Lower Total Cost of Ownership</h1><p><span>The HPE OfficeConnect 1920S Switch Series delivers advanced functionality in smart-managed switches, including Layer 3 static routes, SFP ports, rate limiting, link aggregation and IGMP.</span></p><p><span>The switch series includes PoE+ options to power IP devices without the cost of additional cabling.</span></p><p><span>Green features like port shutdown and Energy Efficient Ethernet compliance for greater energy-efficiency.</span></p><p><span>The switch series is covered by the Limited Lifetime Warranty with 24x7 phone support for 90 days and business hours thereafter.</span></p>");
        }
    }
}
