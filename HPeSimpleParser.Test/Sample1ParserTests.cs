using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HPeSimpleParser.HPE.Model;
using HPeSimpleParser.Parser;
using Xunit;

namespace HPeSimpleParser.Test {
    public class Sample1ParserTests : IAsyncLifetime {
        private ProductRoot data;

        [Fact]
        public void Should_have_correct_part_number() {

            data.PartNumber.Should().Be("JL380A");
        }        
        
        [Fact]
        public void Should_have_correct_PartnerPartNumber() {
            data.PartnerPartNumber.Should().Be("JL380A");
        }        
        
        [Fact]
        public void Should_have_correct_LanguageId() {
            data.LanguageId.Should().Be("gb-en");
        }        
        
        [Fact]
        public void Should_have_correct_product_PartnerPartNumber() {

            data.Product.PartnerPartNumber.Should().Be("JL380A");
        }        
        
        [Fact]
        public void Should_have_correct_product_CategoryID() {
            data.Product.CategoryID.Should().Be("1009689650");
        }        
        
        [Fact]
        public void Should_have_correct_product_CategoryName() {
            data.Product.CategoryName.Should().Be("HPE OfficeConnect 1920S Switch Series");
        }        
        
        [Fact]
        public void Should_have_correct_product_PartnerHierarchyCode() {
            data.Product.PartnerHierarchyCode.Should().Be("HPE");
        }        
        
        [Fact]
        public void Should_have_correct_product_AlternateCategoryID() {
            data.Product.AlternateCategoryID.Should().Be("I5");
        }        
        
        [Fact]
        public void Should_have_correct_product_AlternateCategoryName() {
            data.Product.AlternateCategoryName.Should().Be("I5");
        }        
        
        [Fact]
        public void Should_have_correct_product_AlternatePartnerHierarchyCode() {
            data.Product.AlternatePartnerHierarchyCode.Should().Be("PL");
        }        
        
        [Fact]
        public void Should_have_correct_product_IsEol() {
            data.Product.IsEol.Should().Be(false);
        }        
        
        [Fact]
        public void Should_have_correct_product_ChangeDate() {
            data.Product.ChangeDate.Should().Be(DateTime.Parse("2018-11-20 14:40:30"));
        }        
        
        [Fact]
        public void Should_have_correct_product_Description() {
            data.Product.Description.Should().Be("HPE OfficeConnect 1920S 8G Switch");
        }        
        
        [Fact]
        public void Should_have_correct_product_DescriptionLong() {
            data.Product.DescriptionLong.Should().Be("HPE OfficeConnect 1920S 8G Switch");
        }        
        
        [Fact]
        public void Should_have_correct_product_ManufacturerCode() {
            data.Product.ManufacturerCode.Should().Be("HPE");
        }        
        
        [Fact]
        public void Should_have_correct_product_ManufacturerName() {
            data.Product.ManufacturerName.Should().Be("HPE");
        }        
        
        [Fact]
        public void Should_have_correct_product_ProductCode() {
            data.Product.ProductCode.Should().BeNull();
        }        
        
        [Fact]
        public void Should_have_correct_options_count() {


            data.Options.Items.Should().HaveCount(4);
        }        
        
        [Fact]
        public void Should_have_data_in_first_option() {
            data.Options.Items.First().OptionGroupName.Should().NotBeEmpty();
        }        
        
        [Fact]
        public void Should_have_correct_specification_count() {
            data.Specifications.LabeledItems.Should().HaveCount(14);
        }        
        
        [Fact]
        public void should_have_correct_product_options_count() {
            data.ProductVariants.Should().HaveCount(23);
        }

        public async Task InitializeAsync() {
            var filePath = Path.Combine("Data", "Sample_1.xml");
            var parser = new XmlParser(new HPEParserDefinition());
            data = await parser.ParseDocument(filePath);
        }

        public Task DisposeAsync() {
            return Task.CompletedTask;
        }
    }
}