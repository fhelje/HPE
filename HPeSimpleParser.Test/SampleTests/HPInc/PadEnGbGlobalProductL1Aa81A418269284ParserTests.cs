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
using Xunit.Abstractions;
using Hierarchy = FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model.Hierarchy;
using Specification = FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model.Specification;

namespace HPeSimpleParser.Test.SampleTests.HPInc {
    public class PadEnGbGlobalProductL1Aa81A418269284ParserTests : IAsyncLifetime {
        public PadEnGbGlobalProductL1Aa81A418269284ParserTests(ITestOutputHelper output) {
            _output = output;
        }

        private readonly ITestOutputHelper _output;
        private ProductRoot _data;
        private Specification _weightSpecification;
        private Specification _dimSpecification;
        private Hierarchy _hpeNode;
        private Hierarchy _plNode;

        public async Task InitializeAsync() {
            var filePath = Path.Combine("Data", "HPInc", "pad_en_gb_global_product_L_1AA81A4_18269284.xml");
            var parser = new XmlParser(new HPIncParserDefinition());
            _data = await parser.ParseDocument(filePath, VariantType.HPInc).ConfigureAwait(false);
            _weightSpecification = _data.Specifications.LabeledItems.First(x => x.Name == "weightmet");
            _dimSpecification = _data.Specifications.LabeledItems.First(x => x.Name == "dimenmet");
            _plNode = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "HPIncPL");
            _hpeNode = _data.Hierarchy.First(x => x.PartnerHierarchyCode == "HPInc");
        }

        public Task DisposeAsync() {
            return Task.CompletedTask;
        }

        [Fact]
        public void Dimension_specification_should_have_correct_property_groupId() {
            var spec = _dimSpecification;
            spec.GroupId.Should().BeNull();
        }

        [Fact]
        public void Dimension_specification_should_have_correct_property_groupName() {
            var spec = _dimSpecification;
            spec.GroupName.Should().BeNull();
        }

        [Fact]
        public void Dimension_specification_should_have_correct_property_id() {
            var spec = _dimSpecification;
            spec.Id.Should().BeNull();
        }

        [Fact]
        public void Dimension_specification_should_have_correct_property_label() {
            var spec = _dimSpecification;
            spec.Label.Should().Be("Dimensions (H x W x D) mm");
        }

        [Fact]
        public void Dimension_specification_should_have_correct_property_name() {
            var spec = _dimSpecification;
            spec.Name.Should().Be("dimenmet");
        }

        [Fact]
        public void Dimension_specification_should_have_correct_property_type() {
            var spec = _dimSpecification;
            spec.Type.Should().Be(SpecificationType.Simple);
        }

        [Fact]
        public void Dimension_specification_should_have_correct_property_unitOfMeasure() {
            var spec = _dimSpecification;
            spec.UnitOfMeasure.Should().Be("mm");
        }

        [Fact]
        public void Dimension_specification_should_have_correct_property_value() {
            var spec = _dimSpecification;
            spec.Value.Should().Be("714,50 x 44,50 x 410,90 mm");
        }

        [Fact]
        public void First_product_variant_should_have_correct_property_description() {
            var pv = _data.ProductVariants[0];
            pv.Description.Should().Be("UNAVAILABLE - Southern Africa - English loc - SA plug");
        }

        [Fact]
        public void First_product_variant_should_have_correct_property_opt() {
            var pv = _data.ProductVariants[0];
            pv.Opt.Should().Be("ACQ");
        }

        [Fact]
        public void First_product_variant_should_have_correct_property_upc_code() {
            var pv = _data.ProductVariants[0];
            pv.UpcCode.Should().Be("190781013754");
        }

        [Fact]
        public void Should_have_branch_level_1_with_correct_property_CategoryID() {
            _data.Branch[0].CategoryID.Should().Be("382087");
        }

        [Fact]
        public void Should_have_branch_level_1_with_correct_property_CategoryName() {
            _data.Branch[0].CategoryName.Should().Be("Monitors");
        }

        [Fact]
        public void Should_have_branch_level_1_with_correct_property_Level() {
            _data.Branch[0].Level.Should().Be(1);
        }

        [Fact]
        public void Should_have_branch_level_1_with_correct_property_Name() {
            _data.Branch[0].Name.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_1_with_correct_property_ParentCategoryID() {
            _data.Branch[0].ParentCategoryID.Should().BeNull();
        }

        [Fact]
        public void Should_have_branch_level_1_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[0].PartnerHierarchyCode.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_2_with_correct_property_CategoryID() {
            _data.Branch[1].CategoryID.Should().Be("64283");
        }

        [Fact]
        public void Should_have_branch_level_2_with_correct_property_CategoryName() {
            _data.Branch[1].CategoryName.Should().Be("Business Monitors");
        }

        [Fact]
        public void Should_have_branch_level_2_with_correct_property_Level() {
            _data.Branch[1].Level.Should().Be(2);
        }

        [Fact]
        public void Should_have_branch_level_2_with_correct_property_Name() {
            _data.Branch[1].Name.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_2_with_correct_property_ParentCategoryID() {
            _data.Branch[1].ParentCategoryID.Should().Be("382087");
        }

        [Fact]
        public void Should_have_branch_level_2_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[1].PartnerHierarchyCode.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_3_with_correct_property_CategoryID() {
            _data.Branch[2].CategoryID.Should().Be("3884471");
        }

        [Fact]
        public void Should_have_branch_level_3_with_correct_property_CategoryName() {
            _data.Branch[2].CategoryName.Should().Be("Z Displays");
        }

        [Fact]
        public void Should_have_branch_level_3_with_correct_property_Level() {
            _data.Branch[2].Level.Should().Be(3);
        }

        [Fact]
        public void Should_have_branch_level_3_with_correct_property_Name() {
            _data.Branch[2].Name.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_3_with_correct_property_ParentCategoryID() {
            _data.Branch[2].ParentCategoryID.Should().Be("64283");
        }

        [Fact]
        public void Should_have_branch_level_3_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[2].PartnerHierarchyCode.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_4_with_correct_property_CategoryID() {
            _data.Branch[3].CategoryID.Should().Be("7155490");
        }

        [Fact]
        public void Should_have_branch_level_4_with_correct_property_CategoryName() {
            _data.Branch[3].CategoryName.Should().Be("HP 31-inch Z Displays");
        }

        [Fact]
        public void Should_have_branch_level_4_with_correct_property_Level() {
            _data.Branch[3].Level.Should().Be(4);
        }

        [Fact]
        public void Should_have_branch_level_4_with_correct_property_Name() {
            _data.Branch[3].Name.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_4_with_correct_property_ParentCategoryID() {
            _data.Branch[3].ParentCategoryID.Should().Be("3884471");
        }

        [Fact]
        public void Should_have_branch_level_4_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[3].PartnerHierarchyCode.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_5_with_correct_property_CategoryID() {
            _data.Branch[4].CategoryID.Should().Be("18269280");
        }

        [Fact]
        public void Should_have_branch_level_5_with_correct_property_CategoryName() {
            _data.Branch[4].CategoryName.Should().Be("HP Z32 31.5-inch 4K UHD Display");
        }

        [Fact]
        public void Should_have_branch_level_5_with_correct_property_Level() {
            _data.Branch[4].Level.Should().Be(5);
        }

        [Fact]
        public void Should_have_branch_level_5_with_correct_property_Name() {
            _data.Branch[4].Name.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_level_5_with_correct_property_ParentCategoryID() {
            _data.Branch[4].ParentCategoryID.Should().Be("7155490");
        }

        [Fact]
        public void Should_have_branch_level_5_with_correct_property_PartnerHierarchyCode() {
            _data.Branch[4].PartnerHierarchyCode.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_branch_with_count_5() {
            _data.Branch.Should().HaveCount(5);
        }

        [Fact]
        public void Should_have_correct_detail_CustomsStatisticsNumber() {
            _data.Detail.CustomsStatisticsNumber.Should().BeNull();
        }

        [Fact]
        public void Should_have_correct_detail_Depth() {
            _data.Detail.Depth.Should().Be(410.90M);
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
            _data.Detail.Height.Should().Be(714.50M);
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
            _data.Detail.ProductPartnerID.Should().Be("1AA81A4");
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
            _data.Detail.Unspsc.Should().Be(43211902);
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
            _data.Detail.Weight.Should().Be(12.16M);
        }

        [Fact]
        public void Should_have_correct_detail_WeightWithPackage() {
            _data.Detail.WeightWithPackage.Should().BeNull();
        }

        [Fact]
        public void Should_have_correct_detail_Width() {
            _data.Detail.Width.Should().Be(44.50M);
        }

        [Fact]
        public void Should_have_correct_Hierarchy_Count() {
            _data.Hierarchy.Should().HaveCount(2);
        }

        [Fact]
        public void Should_have_correct_LanguageId() {
            _data.LanguageId.Should().Be("gb-en");
        }

        [Fact]
        public void Should_have_correct_part_number() {
            _data.PartNumber.Should().Be("1AA81A4");
        }

        [Fact]
        public void Should_have_correct_PartnerPartNumber() {
            _data.PartnerPartNumber.Should().Be("1AA81A4");
        }

        [Fact]
        public void Should_have_correct_product_AlternateCategoryID() {
            _data.Product.AlternateCategoryID.Should().Be("TB");
        }

        [Fact]
        public void Should_have_correct_product_AlternateCategoryName() {
            _data.Product.AlternateCategoryName.Should().Be("TB");
        }

        [Fact]
        public void Should_have_correct_product_AlternatePartnerHierarchyCode() {
            _data.Product.AlternatePartnerHierarchyCode.Should().Be("HPIncPL");
        }

        [Fact]
        public void Should_have_correct_product_CategoryID() {
            _data.Product.CategoryID.Should().Be("382087|64283|3884471|7155490|18269280");
        }

        [Fact]
        public void Should_have_correct_product_CategoryName() {
            _data.Product.CategoryName.Should().Be("HP Z32 31.5-inch 4K UHD Display");
        }

        [Fact]
        public void Should_have_correct_product_ChangeDate() {
            _data.Product.ChangeDate.Should().Be(DateTime.Parse("2019-01-16 17:47:17"));
        }

        [Fact]
        public void Should_have_correct_product_Description() {
            _data.Product.Description.Should().Be("HP Z32 31.5-inch 4K UHD Display");
        }

        [Fact]
        public void Should_have_correct_product_DescriptionLong() {
            _data.Product.DescriptionLong.Should().Be("HP Z32 31.5-inch 4K UHD Display");
        }

        [Fact]
        public void Should_have_correct_product_IsEol() {
            _data.Product.IsEol.Should().Be(false);
        }

        [Fact]
        public void Should_have_correct_product_ManufacturerCode() {
            _data.Product.ManufacturerCode.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_correct_product_ManufacturerName() {
            _data.Product.ManufacturerName.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_correct_product_PartnerHierarchyCode() {
            _data.Product.PartnerHierarchyCode.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_correct_product_PartnerPartNumber() {
            _data.Product.PartnerPartNumber.Should().Be("1AA81A4");
        }

        [Fact]
        public void Should_have_correct_product_ProductCode() {
            _data.Product.ProductCode.Should().BeNull();
        }

        [Fact]
        public void Should_have_correct_productVariants_count() {
            _data.ProductVariants.Should().HaveCount(18);
        }

        [Fact]
        public void Should_have_correct_specifications_count() {
            _data.Specifications.LabeledItems.Should().HaveCount(46);
        }

        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_CategoryID() {
            var node = _hpeNode;
            node.CategoryID.Should().Be("18269280");
        }

        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_CategoryName() {
            var node = _hpeNode;
            node.CategoryName.Should().Be("HP Z32 31.5-inch 4K UHD Display");
        }

        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_Level() {
            var node = _hpeNode;
            node.Level.Should().Be(5);
        }

        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_Name() {
            var node = _hpeNode;
            node.Name.Should().Be("HPInc");
        }

        [Fact]
        public void Should_have_hpe_Hierarchy_with_correct_prop_ParentCategoryID() {
            var node = _hpeNode;
            node.ParentCategoryID.Should().Be("7155490");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_ContentType() {
            var img = _data.Links.SelectedImages[0];
            img.ContentType.Should().Be("jpg");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_ContentTypePriority() {
            var img = _data.Links.SelectedImages[0];
            img.ContentTypePriority.Should().Be(0);
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_DocumentTypeDetailPriority() {
            var img = _data.Links.SelectedImages[0];
            img.DocumentTypeDetailPriority.Should().Be(0);
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_FullTitle() {
            var img = _data.Links.SelectedImages[0];
            img.FullTitle.Should().Be("HP Z32 31.5-inch 4K UHD Display");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_GroupingKey1() {
            var img = _data.Links.SelectedImages[0];
            img.GroupingKey1.Should().Be("cmg518");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_GroupingKey2() {
            var img = _data.Links.SelectedImages[0];
            img.GroupingKey2.Should().Be("c05874301");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_Height() {
            var img = _data.Links.SelectedImages[0];
            img.Height.Should().Be(240);
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_ImageUrlHttp() {
            var img = _data.Links.SelectedImages[0];
            img.ImageUrlHttp.Should().Be("http://product-images.www8-hp.com/digmedialib/prodimg/lowres/c05874305.jpg");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_Orientation() {
            var img = _data.Links.SelectedImages[0];
            img.Orientation.Should().Be("Rear facing");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_PixelHeight() {
            var img = _data.Links.SelectedImages[0];
            img.PixelHeight.Should().Be("240");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_PixelWidth() {
            var img = _data.Links.SelectedImages[0];
            img.PixelWidth.Should().Be("320");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_SizeCategory() {
            var img = _data.Links.SelectedImages[0];
            img.SizeCategory.Should().Be(SizeCategoryEnum.Medium);
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_TypeDetail() {
            var img = _data.Links.SelectedImages[0];
            img.TypeDetail.Should().Be("product image");
        }

        [Fact]
        public void Should_have_Links_first_image_should_have_property_Width() {
            var img = _data.Links.SelectedImages[0];
            img.Width.Should().Be(320);
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_ContentType() {
            var img = _data.Links.SelectedImages.Last();
            img.ContentType.Should().Be("jpg");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_ContentTypePriority() {
            var img = _data.Links.SelectedImages.Last();
            img.ContentTypePriority.Should().Be(0);
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_DocumentTypeDetailPriority() {
            var img = _data.Links.SelectedImages.Last();
            img.DocumentTypeDetailPriority.Should().Be(0);
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_FullTitle() {
            var img = _data.Links.SelectedImages.Last();
            img.FullTitle.Should().Be("HP Z32 31.5-inch 4K UHD Display");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_GroupingKey1() {
            var img = _data.Links.SelectedImages.Last();
            img.GroupingKey1.Should().Be("cmg115");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_GroupingKey2() {
            var img = _data.Links.SelectedImages.Last();
            img.GroupingKey2.Should().Be("c05874260");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_Height() {
            var img = _data.Links.SelectedImages.Last();
            img.Height.Should().Be(190);
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_ImageUrlHttp() {
            var img = _data.Links.SelectedImages.Last();
            img.ImageUrlHttp.Should().Be("http://product-images.www8-hp.com/digmedialib/prodimg/lowres/c05874264.jpg");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_Orientation() {
            var img = _data.Links.SelectedImages.Last();
            img.Orientation.Should().Be("Left profile open");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_PixelHeight() {
            var img = _data.Links.SelectedImages.Last();
            img.PixelHeight.Should().Be("190");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_PixelWidth() {
            var img = _data.Links.SelectedImages.Last();
            img.PixelWidth.Should().Be("170");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_SizeCategory() {
            var img = _data.Links.SelectedImages.Last();
            img.SizeCategory.Should().Be(SizeCategoryEnum.Medium);
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_TypeDetail() {
            var img = _data.Links.SelectedImages.Last();
            img.TypeDetail.Should().Be("product image");
        }

        [Fact]
        public void Should_have_Links_last_image_should_have_property_Width() {
            var img = _data.Links.SelectedImages.Last();
            img.Width.Should().Be(170);
        }

        [Fact]
        public void Should_have_Links_with_correct_property_PdfLinkDataSheet() {
            _data.Links.PdfLinkDataSheet.Should().BeNull();
        }

        [Fact]
        public void Should_have_Links_with_correct_property_PdfLinkManual() {
            _data.Links.PdfLinkManual.Should().BeNull();
        }

        [Fact]
        public void Should_have_Links_with_correct_property_SelectedImages_of_count_1() {
            _data.Links.SelectedImages.Should().HaveCount(20);
        }

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
            _data.Marketing.MarketingText.Should().Be(
                "<h1>Incredible clarity for maximum productivity</h1><p><span>Optimize your workflows with a large, 4K[1] display. View more lines of code, bigger spreadsheets, and vibrant colors, all with minimal toggling and scrolling.</span></p><p><span>Optimize your workflows with a large, 4K[1] display. View more lines of code, bigger spreadsheets, and vibrant colors, all with minimal toggling and scrolling.</span></p> <h1>One powerful connection</h1><p><span>Work fast and clutter-free with USB-C™ connectivity that not only receives video and data from your device, but also delivers up to 65W[2] of power—with just one cable.</span></p><p><span>Work fast and clutter-free with USB-C™ connectivity that not only receives video and data from your device, but also delivers up to 65W[2] of power—with just one cable.</span></p><p><span>https://h10003.www1.hp.com/digmedialib/prodimg/lowres/c05790062.png</span></p> <h1>Reliable visual solution</h1><p><span>Perfect your designs, projects, and documents on a display built to meet your demands. All HP Z Displays are rigorously tested and backed by an exclusive HP Zero Bright Dot Guarantee.[3]</span></p><p><span>Perfect your designs, projects, and documents on a display built to meet your demands. All HP Z Displays are rigorously tested and backed by an exclusive HP Zero Bright Dot Guarantee[3] that replaces the screen if even one bright sub-pixel fails.</span></p>");
        }

        [Fact]
        public void Should_have_Marketing_with_correct_property_Url() {
            _data.Marketing.Url.Should().Be("");
        }

        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_CategoryID() {
            var node = _plNode;
            node.CategoryID.Should().Be("TB");
        }

        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_CategoryName() {
            var node = _plNode;
            node.CategoryName.Should().Be("TB");
        }

        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_Level() {
            var node = _plNode;
            node.Level.Should().Be(1);
        }

        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_Name() {
            var node = _plNode;
            node.Name.Should().Be("HPIncPL");
        }

        [Fact]
        public void Should_have_PL_Hierarchy_with_correct_prop_ParentCategoryID() {
            var node = _plNode;
            node.ParentCategoryID.Should().BeNull();
        }

        [Fact]
        public void Specifications_should_not_contain_footnotes() {
            _data.Specifications.LabeledItems.Where(x => x.Name.EndsWith("ftntnbr")).Should().HaveCount(0);
            var footnotes = _data.Specifications.LabeledItems.Where(x => x.Label.Contains("footnote")).ToList();
            foreach (var footnote in footnotes)
                _output.WriteLine($"{footnote.Name}: {footnote.Label}={footnote.Value}");
            footnotes.Should().HaveCount(0);
        }

        [Fact]
        public void Weight_specification_should_have_correct_property_groupId() {
            var spec = _weightSpecification;
            spec.GroupId.Should().BeNull();
        }

        [Fact]
        public void Weight_specification_should_have_correct_property_groupName() {
            var spec = _weightSpecification;
            spec.GroupName.Should().BeNull();
        }

        [Fact]
        public void Weight_specification_should_have_correct_property_id() {
            var spec = _weightSpecification;
            spec.Id.Should().BeNull();
        }

        [Fact]
        public void Weight_specification_should_have_correct_property_label() {
            var spec = _weightSpecification;
            spec.Label.Should().Be("Weight");
        }

        [Fact]
        public void Weight_specification_should_have_correct_property_name() {
            var spec = _weightSpecification;
            spec.Name.Should().Be("weightmet");
        }

        [Fact]
        public void Weight_specification_should_have_correct_property_type() {
            var spec = _weightSpecification;
            spec.Type.Should().Be(SpecificationType.Simple);
        }

        [Fact]
        public void Weight_specification_should_have_correct_property_unitOfMeasure() {
            var spec = _weightSpecification;
            spec.UnitOfMeasure.Should().Be("Kg");
        }

        [Fact]
        public void Weight_specification_should_have_correct_property_value() {
            var spec = _weightSpecification;
            spec.Value.Should().Be("12.16 kg");
        }
    }
}