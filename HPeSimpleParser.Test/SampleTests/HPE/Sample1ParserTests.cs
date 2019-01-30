using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FSSystem.ContentAdapter.HPEAndHPInc.Enums;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser;
using Xunit;

namespace HPeSimpleParser.Test.SampleTests {

    public class Sample1ParserTests : IAsyncLifetime {
        private ProductRoot _data;

        public async Task InitializeAsync() {
            var filePath = Path.Combine("Data", "Sample_1.xml");
            var parser = new XmlParser(new HPEParserDefinition());
            _data = await parser.ParseDocument(filePath, VariantType.HPE);
        }

        public Task DisposeAsync() {
            return Task.CompletedTask;
        }

        #region Root
        [Fact]
        public void Should_have_correct_part_number() {

            _data.PartNumber.Should().Be("JL380A");
        }

        [Fact]
        public void Should_have_correct_PartnerPartNumber() {
            _data.PartnerPartNumber.Should().Be("JL380A");
        }

        [Fact]
        public void Should_have_correct_LanguageId() {
            _data.LanguageId.Should().Be("gb-en");
        }
        #endregion

        #region Product
        [Fact]
        public void Should_have_correct_product_PartnerPartNumber() {

            _data.Product.PartnerPartNumber.Should().Be("JL380A");
        }

        [Fact]
        public void Should_have_correct_product_CategoryID() {
            _data.Product.CategoryID.Should().Be("12883|4172267|4172281|4177614|1009689650");
        }

        [Fact]
        public void Should_have_correct_product_CategoryName() {
            _data.Product.CategoryName.Should().Be("HPE OfficeConnect 1920S Switch Series");
        }

        [Fact]
        public void Should_have_correct_product_PartnerHierarchyCode() {
            _data.Product.PartnerHierarchyCode.Should().Be("HPE");
        }

        [Fact]
        public void Should_have_correct_product_AlternateCategoryID() {
            _data.Product.AlternateCategoryID.Should().Be("I5");
        }

        [Fact]
        public void Should_have_correct_product_AlternateCategoryName() {
            _data.Product.AlternateCategoryName.Should().Be("I5");
        }

        [Fact]
        public void Should_have_correct_product_AlternatePartnerHierarchyCode() {
            _data.Product.AlternatePartnerHierarchyCode.Should().Be("PL");
        }

        [Fact]
        public void Should_have_correct_product_IsEol() {
            _data.Product.IsEol.Should().Be(false);
        }

        [Fact]
        public void Should_have_correct_product_ChangeDate() {
            _data.Product.ChangeDate.Should().Be(DateTime.Parse("2018-11-20 14:40:30"));
        }

        [Fact]
        public void Should_have_correct_product_Description() {
            _data.Product.Description.Should().Be("HPE OfficeConnect 1920S 8G Switch");
        }

        [Fact]
        public void Should_have_correct_product_DescriptionLong() {
            _data.Product.DescriptionLong.Should().Be("HPE OfficeConnect 1920S 8G Switch");
        }

        [Fact]
        public void Should_have_correct_product_ManufacturerCode() {
            _data.Product.ManufacturerCode.Should().Be("HPE");
        }

        [Fact]
        public void Should_have_correct_product_ManufacturerName() {
            _data.Product.ManufacturerName.Should().Be("HPE");
        }

        [Fact]
        public void Should_have_correct_product_ProductCode() {
            _data.Product.ProductCode.Should().BeNull();
        }

        [Fact]
        public void Should_have_correct_options_count() {


            _data.Options.Items.Should().HaveCount(4);
        }

        [Fact]
        public void Should_have_data_in_first_option() {
            _data.Options.Items.First().OptionGroupName.Should().NotBeEmpty();
        }

        [Fact]
        public void Should_have_correct_specification_count() {
            _data.Specifications.LabeledItems.Should().HaveCount(14);
        }

        [Fact]
        public void should_have_correct_product_options_count() {
            _data.ProductVariants.Should().HaveCount(23);
        }
        #endregion

        #region Detail
        [Fact]
        public void Should_have_correct_detail_CustomsStatisticsNumber() {

            _data.Detail.CustomsStatisticsNumber.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_Depth() {

            _data.Detail.Depth.Should().Be(43.9M);
        }
        [Fact]
        public void Should_have_correct_detail_EndOfSupport() {

            _data.Detail.EndOfSupport.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_ErpAltPartNumber() {

            _data.Detail.ErpAltPartNumber.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_ExtendedWarranty() {

            _data.Detail.ExtendedWarranty.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_Height() {

            _data.Detail.Height.Should().Be(254M);
        }
        [Fact]
        public void Should_have_correct_detail_IsRequireSerialNumber() {

            _data.Detail.IsRequireSerialNumber.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_ItemDefFulfillSource() {

            _data.Detail.ItemDefFulfillSource.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_ManufacturingCountry() {

            _data.Detail.ManufacturingCountry.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_MeterEnabled() {

            _data.Detail.MeterEnabled.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_MinimumOrderQty() {

            _data.Detail.MinimumOrderQty.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_PackQty() {

            _data.Detail.PackQty.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_PalletSize() {

            _data.Detail.PalletSize.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_ProductPartnerID() {

            _data.Detail.ProductPartnerID.Should().Be("JL380A");
        }
        [Fact]
        public void Should_have_correct_detail_SwedishChemicalTaxReduction() {

            _data.Detail.SwedishChemicalTaxReduction.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_TeleSalesFlag() {

            _data.Detail.TeleSalesFlag.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_Unspsc() {

            _data.Detail.Unspsc.Should().Be(43222612);
        }
        [Fact]
        public void Should_have_correct_detail_Volume() {

            _data.Detail.Volume.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_WarrantyTime() {

            _data.Detail.WarrantyTime.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_Weight() {

            _data.Detail.Weight.Should().Be(0.82M);
        }
        [Fact]
        public void Should_have_correct_detail_WeightWithPackage() {

            _data.Detail.WeightWithPackage.Should().BeNull();
        }
        [Fact]
        public void Should_have_correct_detail_Width() {

            _data.Detail.Width.Should().Be(159.5M);
        }
        #endregion

        #region Hierarchy
        [Fact]
        public void Should_have_correct_Hierarchy_Count() {

            _data.Hierarchy.Should().HaveCount(2);
        }
        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_CategoryID() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "HPE");
            node.CategoryID.Should().Be("1009689650");
        }
        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_CategoryName() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "HPE");
            node.CategoryName.Should().Be("HPE OfficeConnect 1920S Switch Series");
        }
        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_Level() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "HPE");
            node.Level.Should().Be(5);
        }
        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_Name() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "HPE");
            node.Name.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_ParentCategoryID() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "HPE");
            node.ParentCategoryID.Should().Be("4177614");
        }

        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_CategoryID() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "PL");
            node.CategoryID.Should().Be("I5");
        }
        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_CategoryName() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "PL");
            node.CategoryName.Should().Be("I5");
        }
        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_Level() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "PL");
            node.Level.Should().Be(1);
        }
        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_Name() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "PL");
            node.Name.Should().Be("PL");
        }
        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_ParentCategoryID() {
            var node = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "PL");
            node.ParentCategoryID.Should().BeNull();
        }
        #endregion
        
        #region Branch
        [Fact]
        public void Should_have_branch_with_count_5() {
            _data.Branch.Should().HaveCount(5);
        }
        [Fact]
        public void Should_have_branch_level_1_with_correct_property_CategoryID() {
            _data.Branch[0].CategoryID.Should().Be("12883");
        }
        [Fact]
        public void Should_have_branch_level_1_with_correct_property_CategoryName() {
            _data.Branch[0].CategoryName.Should().Be("Networking");
        }
        [Fact]
        public void Should_have_branch_level_1_with_correct_property_Level() {
            _data.Branch[0].Level.Should().Be(1);
        }
        [Fact]
        public void Should_have_branch_level_1_with_correct_property_Name() {
            _data.Branch[0].Name.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_1_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[0].PartnerHierarchyCode.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_1_with_correct_property_ParentCategoryID() {
            _data.Branch[0].ParentCategoryID.Should().BeNull();
        }
        [Fact]
        public void Should_have_branch_level_2_with_correct_property_CategoryID() {
            _data.Branch[1].CategoryID.Should().Be("4172267");
        }
        [Fact]
        public void Should_have_branch_level_2_with_correct_property_CategoryName() {
            _data.Branch[1].CategoryName.Should().Be("Switches");
        }
        [Fact]
        public void Should_have_branch_level_2_with_correct_property_Level() {
            _data.Branch[1].Level.Should().Be(2);
        }
        [Fact]
        public void Should_have_branch_level_2_with_correct_property_Name() {
            _data.Branch[1].Name.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_2_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[1].PartnerHierarchyCode.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_2_with_correct_property_ParentCategoryID() {
            _data.Branch[1].ParentCategoryID.Should().Be("12883");
        }
        [Fact]
        public void Should_have_branch_level_3_with_correct_property_CategoryID() {
            _data.Branch[2].CategoryID.Should().Be("4172281");
        }
        [Fact]
        public void Should_have_branch_level_3_with_correct_property_CategoryName() {
            _data.Branch[2].CategoryName.Should().Be("Fixed Port Web Managed Ethernet Switches");
        }
        [Fact]
        public void Should_have_branch_level_3_with_correct_property_Level() {
            _data.Branch[2].Level.Should().Be(3);
        }
        [Fact]
        public void Should_have_branch_level_3_with_correct_property_Name() {
            _data.Branch[2].Name.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_3_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[2].PartnerHierarchyCode.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_3_with_correct_property_ParentCategoryID() {
            _data.Branch[2].ParentCategoryID.Should().Be("4172267");
        }
        [Fact]
        public void Should_have_branch_level_4_with_correct_property_CategoryID() {
            _data.Branch[3].CategoryID.Should().Be("4177614");
        }
        [Fact]
        public void Should_have_branch_level_4_with_correct_property_CategoryName() {
            _data.Branch[3].CategoryName.Should().Be("1900 Switch Products");
        }
        [Fact]
        public void Should_have_branch_level_4_with_correct_property_Level() {
            _data.Branch[3].Level.Should().Be(4);
        }
        [Fact]
        public void Should_have_branch_level_4_with_correct_property_Name() {
            _data.Branch[3].Name.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_4_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[3].PartnerHierarchyCode.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_4_with_correct_property_ParentCategoryID() {
            _data.Branch[3].ParentCategoryID.Should().Be("4172281");
        }
        [Fact]
        public void Should_have_branch_level_5_with_correct_property_CategoryID() {
            _data.Branch[4].CategoryID.Should().Be("1009689650");
        }
        [Fact]
        public void Should_have_branch_level_5_with_correct_property_CategoryName() {
            _data.Branch[4].CategoryName.Should().Be("HPE OfficeConnect 1920S Switch Series");
        }
        [Fact]
        public void Should_have_branch_level_5_with_correct_property_Level() {
            _data.Branch[4].Level.Should().Be(5);
        }
        [Fact]
        public void Should_have_branch_level_5_with_correct_property_Name() {
            _data.Branch[4].Name.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_5_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[4].PartnerHierarchyCode.Should().Be("HPE");
        }
        [Fact]
        public void Should_have_branch_level_5_with_correct_property_ParentCategoryID() {
            _data.Branch[4].ParentCategoryID.Should().Be("4177614");
        }
        #endregion
        
        #region Links
        [Fact]
        public void Should_have_Links_with_correct_property_PdfLinkDataSheet() {
            _data.Links.PdfLinkDataSheet.Should().Be("https://h20195.www2.hpe.com/v2/getpdf.aspx/a00001630enw.pdf");
        }
        [Fact]
        public void Should_have_Links_with_correct_property_PdfLinkManual() {
            _data.Links.PdfLinkManual.Should().BeNull();
        }
        [Fact]
        public void Should_have_Links_with_correct_property_SelectedImages_of_count_1() {
            _data.Links.SelectedImages.Should().HaveCount(2);
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_GroupingKey1() {
            var img = _data.Links.SelectedImages.First();
            img.GroupingKey1.Should().Be("cmg674");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_GroupingKey2() {
            var img = _data.Links.SelectedImages.First();
            img.GroupingKey2.Should().Be("f7b615f4-5dea-4e7e-ab5e-f8fd80b1126f");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_ContentType() {
            var img = _data.Links.SelectedImages.First();
            img.ContentType.Should().Be("jpg");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_ContentTypePriority() {
            var img = _data.Links.SelectedImages.First();
            img.ContentTypePriority.Should().Be(0);
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_PixelHeight() {
            var img = _data.Links.SelectedImages.First();
            img.PixelHeight.Should().Be("240");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_Orientation() {
            var img = _data.Links.SelectedImages.First();
            img.Orientation.Should().Be("Center facing");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_PixelWidth() {
            var img = _data.Links.SelectedImages.First();
            img.PixelWidth.Should().Be("320");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_SizeCategory() {
            var img = _data.Links.SelectedImages.First();
            img.SizeCategory.Should().Be(SizeCategoryEnum.Medium);
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_ImageUrlHttp() {
            var img = _data.Links.SelectedImages.First();
            img.ImageUrlHttp.Should().Be("http://h50003.www5.hpe.com/digmedialib/prodimg/lowres/i00005168.jpg");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_TypeDetail() {
            var img = _data.Links.SelectedImages.First();
            img.TypeDetail.Should().Be("product image");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_DocumentTypeDetailPriority() {
            var img = _data.Links.SelectedImages.First();
            img.DocumentTypeDetailPriority.Should().Be(0);
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_FullTitle() {
            var img = _data.Links.SelectedImages.First();
            img.FullTitle.Should().Be("HPE 1920S 8G Switch");
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_Height() {
            var img = _data.Links.SelectedImages.First();
            img.Height.Should().Be(240);
        }
        [Fact]
        public void Should_have_Links_first_image_should_have_property_Width() {
            var img = _data.Links.SelectedImages.First();
            img.Width.Should().Be(320);
        }
                [Fact]
        public void Should_have_Links_last_image_should_have_property_GroupingKey1() {
            var img = _data.Links.SelectedImages.Last();
            img.GroupingKey1.Should().Be("cmg674");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_GroupingKey2() {
            var img = _data.Links.SelectedImages.Last();
            img.GroupingKey2.Should().Be("f7b615f4-5dea-4e7e-ab5e-f8fd80b1126f");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_ContentType() {
            var img = _data.Links.SelectedImages.Last();
            img.ContentType.Should().Be("png");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_ContentTypePriority() {
            var img = _data.Links.SelectedImages.Last();
            img.ContentTypePriority.Should().Be(1);
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_PixelHeight() {
            var img = _data.Links.SelectedImages.Last();
            img.PixelHeight.Should().Be("430");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_Orientation() {
            var img = _data.Links.SelectedImages.Last();
            img.Orientation.Should().Be("Center facing");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_PixelWidth() {
            var img = _data.Links.SelectedImages.Last();
            img.PixelWidth.Should().Be("573");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_SizeCategory() {
            var img = _data.Links.SelectedImages.Last();
            img.SizeCategory.Should().Be(SizeCategoryEnum.Large);
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_ImageUrlHttp() {
            var img = _data.Links.SelectedImages.Last();
            img.ImageUrlHttp.Should().Be("http://h50003.www5.hpe.com/digmedialib/prodimg/lowres/i00005170.png");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_TypeDetail() {
            var img = _data.Links.SelectedImages.Last();
            img.TypeDetail.Should().Be("product image");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_DocumentTypeDetailPriority() {
            var img = _data.Links.SelectedImages.Last();
            img.DocumentTypeDetailPriority.Should().Be(0);
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_FullTitle() {
            var img = _data.Links.SelectedImages.Last();
            img.FullTitle.Should().Be("HPE 1920S 8G Switch");
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_Height() {
            var img = _data.Links.SelectedImages.Last();
            img.Height.Should().Be(430);
        }
        [Fact]
        public void Should_have_Links_last_image_should_have_property_Width() {
            var img = _data.Links.SelectedImages.Last();
            img.Width.Should().Be(573);
        }

        #endregion
        
        #region Marketing
        [Fact]
        public void Should_have_Marketing_with_correct_property_ChangeCode() {
            _data.Marketing.ChangeCode.Should().Be("");
        }
        [Fact]
        public void Should_have_Marketing_with_correct_property_MarketingCode() {
            _data.Marketing.MarketingCode.Should().Be("");
        }
        [Fact]
        public void Should_have_Marketing_with_correct_property_MarketingText() {
            _data.Marketing.MarketingText.Should().Be("<h1>Simplifies Network Deployment and Management for Small Organizations</h1><p><span>The HPE OfficeConnect 1920S Switch Series features easy-to-use, out of the box, plug-and-play deployment.</span></p><p><span>The series offers a complete portfolio of choices for increased small business flexibility. It consists of six rack-mountable models including 8G-, 24G- and 48G-port with and without PoE+. The 24G- and 48G-port PoE+ models offer SPF fiber connectivity.</span></p><p><span>These smart-managed switches use an intuitive Web management interface, to simplify deployment and management while offering granular control of key features.</span></p> <h1>Keeps Your Business Protected with Enhanced Security</h1><p><span>The HPE OfficeConnect 1920S Switch Series supports Energy Efficient Ethernet for lower power consumption and an enhanced feature set for more robust operation.</span></p><p><span>Enhanced security features such as Access Control List, IEEE 802.1x and VLANs guard your network from unwanted or unauthorized access.</span></p><p><span>Management security restricts access to critical configuration commands, offers multiple privilege levels with password protection and supports secure http (https).</span></p> <h1>Delivers Better Performance at a Lower Total Cost of Ownership</h1><p><span>The HPE OfficeConnect 1920S Switch Series delivers advanced functionality in smart-managed switches, including Layer 3 static routes, SFP ports, rate limiting, link aggregation and IGMP.</span></p><p><span>The switch series includes PoE+ options to power IP devices without the cost of additional cabling.</span></p><p><span>Green features like port shutdown and Energy Efficient Ethernet compliance for greater energy-efficiency.</span></p><p><span>The switch series is covered by the Limited Lifetime Warranty with 24x7 phone support for 90 days and business hours thereafter.</span></p>");
        }
        [Fact]
        public void Should_have_Marketing_with_correct_property_Url() {
            _data.Marketing.Url.Should().Be("");
        }
        #endregion
        
        #region Options
        #endregion
        
        #region Specifications
        [Fact]
        public void Should_have_correct_specifications_count() {

            _data.Specifications.LabeledItems.Should().HaveCount(14);
        }
        [Fact]
        public void Weight_specification_should_have_correct_property_name() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "weightmet");
            spec.Name.Should().Be("weightmet");
        }
        [Fact]
        public void Weight_specification_should_have_correct_property_value() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "weightmet");
            spec.Value.Should().Be("0.82 kg");
        }
        [Fact]
        public void Weight_specification_should_have_correct_property_type() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "weightmet");
            spec.Type.Should().Be(SpecificationType.Simple);
        }
        [Fact]
        public void Weight_specification_should_have_correct_property_unitOfMeasure() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "weightmet");
            spec.UnitOfMeasure.Should().Be("Kg");
        }
        [Fact]
        public void Weight_specification_should_have_correct_property_id() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "weightmet");
            spec.Id.Should().BeNull();
        }
        [Fact]
        public void Weight_specification_should_have_correct_property_groupId() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "weightmet");
            spec.GroupId.Should().BeNull();
        }
        [Fact]
        public void Weight_specification_should_have_correct_property_groupName() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "weightmet");
            spec.GroupName.Should().BeNull();
        }
        [Fact]
        public void Weight_specification_should_have_correct_property_label() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "weightmet");
            spec.Label.Should().Be("Weight");
        }
                [Fact]
        public void Dimension_specification_should_have_correct_property_name() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "dimenmet");
            spec.Name.Should().Be("dimenmet");
        }
        [Fact]
        public void Dimension_specification_should_have_correct_property_value() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "dimenmet");
            spec.Value.Should().Be("254,0 x 159,50 x 43,90 mm");
        }
        [Fact]
        public void Dimension_specification_should_have_correct_property_type() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "dimenmet");
            spec.Type.Should().Be(SpecificationType.Simple);
        }
        [Fact]
        public void Dimension_specification_should_have_correct_property_unitOfMeasure() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "dimenmet");
            spec.UnitOfMeasure.Should().Be("mm");
        }
        [Fact]
        public void Dimension_specification_should_have_correct_property_id() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "dimenmet");
            spec.Id.Should().BeNull();
        }
        [Fact]
        public void Dimension_specification_should_have_correct_property_groupId() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "dimenmet");
            spec.GroupId.Should().BeNull();
        }
        [Fact]
        public void Dimension_specification_should_have_correct_property_groupName() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "dimenmet");
            spec.GroupName.Should().BeNull();
        }
        [Fact]
        public void Dimension_specification_should_have_correct_property_label() {
            var spec = _data.Specifications.LabeledItems.First(x=>x.Name == "dimenmet");
            spec.Label.Should().Be("Dimensions (H x W x D) mm");
        }

        #endregion
        
        #region ProductVariants
        [Fact]
        public void Should_have_correct_productVariants_count() {

            _data.ProductVariants.Should().HaveCount(23);
        }
        [Fact]
        public void First_product_variant_should_have_correct_property_upc_code() {
            var pv = _data.ProductVariants.First();
            pv.UpcCode.Should().Be("190017137339");
        }
        [Fact]
        public void First_product_variant_should_have_correct_property_description() {
            var pv = _data.ProductVariants.First();
            pv.Description.Should().Be("UNAVAILABLE - Argentina - English localization");
        }
        [Fact]
        public void First_product_variant_should_have_correct_property_opt() {
            var pv = _data.ProductVariants.First();
            pv.Opt.Should().Be("ARM");
        }

        #endregion
    }
}