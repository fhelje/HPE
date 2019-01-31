using System.Linq;
using FluentAssertions;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser;
using FSSystem.ContentAdapter.Model;
using HPeSimpleParser.Test.Builders;
using Xunit;

namespace HPeSimpleParser.Test {
    public class ToItemTests {
        public ToItemTests() {
            _productRoot = ProductRootBuilder.With()
                .WithDetail(x => x.Default())
                .WithImages(y =>
                    y.AddImage(i =>
                        i.AddImage(x =>
                            x.Default().SetContentType("jpg").SetHeight("100").SetWidth("200")
                                .SetUrl("http://images.com/1234")
                        )
                    )
                )
                .WithMarketing(x => x.Default())
                .WithHierachy(x => x.AddDefaultNode())
                .WithSpecifications(x => x.AddFull().AddSimple())
                .WithOptions(x => x.AddDefault())
                .Build();
            _actual = _productRoot.ToItem();
        }

        protected readonly ProductRoot _productRoot;
        protected readonly Item _actual;

        public class ProductTests : ToItemTests {
            //[Fact]
            //public void Should_copy_values_from_product_CategoryID()
            //{
            //    _actual.Product.CategoryID.Should().Be(_productRoot.Product.CategoryID);
            //}
            //[Fact]
            //public void Should_copy_values_from_product_CategoryName()
            //{
            //    _actual.Product.CategoryName.Should().Be(_productRoot.Product.CategoryName);
            //}
            [Fact]
            public void Should_copy_values_from_product_ChangeDate() {
                _actual.Product.ChangeDate.Should().Be(_productRoot.Product.ChangeDate);
            }

            [Fact]
            public void Should_copy_values_from_product_Description() {
                _actual.Product.Description.Should().Be(_productRoot.Product.Description);
            }

            [Fact]
            public void Should_copy_values_from_product_DescriptionLong() {
                _actual.Product.DescriptionLong.Should().Be(_productRoot.Product.DescriptionLong);
            }

            [Fact]
            public void Should_copy_values_from_product_IsEol() {
                _actual.Product.IsEol.Should().Be(_productRoot.Product.IsEol);
            }

            [Fact]
            public void Should_copy_values_from_product_ManufacturerCode() {
                _actual.Product.ManufacturerCode.Should().Be(_productRoot.Product.ManufacturerCode);
            }

            [Fact]
            public void Should_copy_values_from_product_ManufacturerName() {
                _actual.Product.ManufacturerName.Should().Be(_productRoot.Product.ManufacturerName);
            }

            [Fact]
            public void Should_copy_values_from_product_PartnerPartNumber() {
                _actual.Product.PartnerPartNumber.Should().Be(_productRoot.PartnerPartNumber);
            }

            [Fact]
            public void Should_copy_values_from_product_PartNumber() {
                _actual.Product.PartNumber.Should().Be(_productRoot.PartNumber);
            }

            [Fact]
            public void Should_copy_values_from_product_ProductCode() {
                _actual.Product.ProductCode.Should().Be(_productRoot.Product.ProductCode);
            }
        }

        public class DetailTests : ToItemTests {
            [Fact]
            public void Should_copy_values_from_detail_CustomsStatisticsNumber() {
                _actual.Detail.CustomsStatisticsNumber.Should().Be(_productRoot.Detail.CustomsStatisticsNumber);
            }

            [Fact]
            public void Should_copy_values_from_detail_Depth() {
                _actual.Detail.Depth.Should().Be(_productRoot.Detail.Depth);
            }

            [Fact]
            public void Should_copy_values_from_detail_EndOfSupport() {
                _actual.Detail.EndOfSupport.Should().Be(_productRoot.Detail.EndOfSupport);
            }

            [Fact]
            public void Should_copy_values_from_detail_ErpAltPartNumber() {
                _actual.Detail.ErpAltPartNumber.Should().Be(_productRoot.Detail.ErpAltPartNumber);
            }

            [Fact]
            public void Should_copy_values_from_detail_ExtendedWarranty() {
                _actual.Detail.ExtendedWarranty.Should().Be(_productRoot.Detail.ExtendedWarranty);
            }

            [Fact]
            public void Should_copy_values_from_detail_Height() {
                _actual.Detail.Height.Should().Be(_productRoot.Detail.Height);
            }

            [Fact]
            public void Should_copy_values_from_detail_IsRequireSerialNumber() {
                _actual.Detail.IsRequireSerialNumber.Should().Be(_productRoot.Detail.IsRequireSerialNumber);
            }

            [Fact]
            public void Should_copy_values_from_detail_ItemDefFulfillSource() {
                _actual.Detail.ItemDefFulfillSource.Should().Be(_productRoot.Detail.ItemDefFulfillSource);
            }

            [Fact]
            public void Should_copy_values_from_detail_ManufacturingCountry() {
                _actual.Detail.ManufacturingCountry.Should().Be(_productRoot.Detail.ManufacturingCountry);
            }

            [Fact]
            public void Should_copy_values_from_detail_MeterEnabled() {
                _actual.Detail.MeterEnabled.Should().Be(_productRoot.Detail.MeterEnabled);
            }

            [Fact]
            public void Should_copy_values_from_detail_MinimumOrderQty() {
                _actual.Detail.MinimumOrderQty.Should().Be(_productRoot.Detail.MinimumOrderQty);
            }

            [Fact]
            public void Should_copy_values_from_detail_PackQty() {
                _actual.Detail.PackQty.Should().Be(_productRoot.Detail.PackQty);
            }

            [Fact]
            public void Should_copy_values_from_detail_PalletSize() {
                _actual.Detail.PalletSize.Should().Be(_productRoot.Detail.PalletSize);
            }

            [Fact]
            public void Should_copy_values_from_detail_SwedishChemicalTaxReduction() {
                _actual.Detail.SwedishChemicalTaxReduction.Should().Be(_productRoot.Detail.SwedishChemicalTaxReduction);
            }

            [Fact]
            public void Should_copy_values_from_detail_TeleSalesFlag() {
                _actual.Detail.TeleSalesFlag.Should().Be(_productRoot.Detail.TeleSalesFlag);
            }

            [Fact]
            public void Should_copy_values_from_detail_Unspsc() {
                _actual.Detail.Unspsc.Should().Be(_productRoot.Detail.Unspsc);
            }

            [Fact]
            public void Should_copy_values_from_detail_Volume() {
                _actual.Detail.Volume.Should().Be(_productRoot.Detail.Volume);
            }

            [Fact]
            public void Should_copy_values_from_detail_WarrantyTime() {
                _actual.Detail.WarrantyTime.Should().Be(_productRoot.Detail.WarrantyTime);
            }

            [Fact]
            public void Should_copy_values_from_detail_Weight() {
                _actual.Detail.Weight.Should().Be(_productRoot.Detail.Weight);
            }

            [Fact]
            public void Should_copy_values_from_detail_WeightwithPackage() {
                _actual.Detail.WeightwithPackage.Should().Be(_productRoot.Detail.WeightWithPackage);
            }

            [Fact]
            public void Should_copy_values_from_detail_Width() {
                _actual.Detail.Width.Should().Be(_productRoot.Detail.Width);
            }
        }

        public class LinkTests : ToItemTests {
            [Fact]
            public void Should_copy_values_from_links_Images() {
                var expectation = _productRoot.Links.SelectedImages[0];

                var actual = _actual.Link.Images[0];
                actual.Height.Should().Be(expectation.Height);
                actual.Width.Should().Be(expectation.Width);
                actual.ContentType.Should().Be(expectation.ContentType);
                actual.Title.Should().Be(expectation.FullTitle);
            }

            [Fact]
            public void Should_copy_values_from_links_PartnerPartNumber() {
                _actual.Link.PartnerPartNumber.Should().Be(_productRoot.PartnerPartNumber);
            }

            [Fact]
            public void Should_copy_values_from_links_PdfLinkDataSheet() {
                _actual.Link.PdfLinkDataSheet.Should().Be(_productRoot.Links.PdfLinkDataSheet);
            }

            [Fact]
            public void Should_copy_values_from_links_PdfLinkManual() {
                _actual.Link.PdfLinkManual.Should().Be(_productRoot.Links.PdfLinkManual);
            }
        }

        public class MarketingTests : ToItemTests {
            [Fact]
            public void Should_copy_values_for_marketing_ChangeCode() {
                _actual.Marketing.LanguageId.Should().Be(_productRoot.LanguageId);
            }

            [Fact]
            public void Should_copy_values_for_marketing_MarketingCode() {
                _actual.Marketing.MarketingCode.Should().Be(_productRoot.Marketing.MarketingCode);
            }

            [Fact]
            public void Should_copy_values_for_marketing_MarketingText() {
                _actual.Marketing.MarketingText.Should().Be(_productRoot.Marketing.MarketingText);
            }

            [Fact]
            public void Should_copy_values_for_marketing_PartnerPartNumber() {
                _actual.Marketing.PartnerPartNumber.Should().Be(_productRoot.PartnerPartNumber);
            }
        }

        public class SpecificationsTests : ToItemTests {
            [Fact]
            public void Should_map_full_specifications() {
                var expectation = _productRoot.Specifications.LabeledItems[0];
                var actual = _actual.Specifications.Items[0];
                expectation.Should().NotBeNull();
                actual.Should().NotBeNull();
            }

            [Fact]
            public void Should_map_simple_specifications() {
                var expectation = _productRoot.Specifications.LabeledItems.Last();
                var actual = _actual.Specifications.Items.Last();
                expectation.Should().NotBeNull();
                actual.Should().NotBeNull();
            }
        }

        public class SupplierTests : ToItemTests {
            [Fact]
            public void Should_cpoy_values_for_SupplierPartnerPartNumber() {
                _actual.Supplier.PartnerPartNumber.Should().Be(_productRoot.PartnerPartNumber);
            }

            [Fact]
            public void Should_cpoy_values_for_SupplierSupplierId() {
                _actual.Supplier.SupplierId.Should().Be(_productRoot.Product.ManufacturerCode);
            }

            [Fact]
            public void Should_cpoy_values_for_SupplierSupplierName() {
                _actual.Supplier.SupplierName.Should().Be(_productRoot.Product.ManufacturerName);
            }
        }

        public class OptionsTests : ToItemTests {
            [Fact]
            public void Should_map_option() {
                var actual = _actual.Options.Items[0];
                var expectation = _productRoot.Options.Items[0];
                actual.PartNumber.Should().Be(expectation.OptionPartnerPartNumber);
                actual.Name.Should().Be(expectation.ManufacturerCode);
                actual.GroupId.Should().Be(expectation.OptionGroupCode);
                actual.GroupName.Should().Be(expectation.OptionGroupName);
            }
        }

        public class HierarchyTests : ToItemTests {
            [Fact]
            public void Should_copy_hierarchy() {
                var actual = _actual.Hierarchies.Items[0];
                var expectation = _productRoot.Hierarchy[0];
                actual.Name.Should().Be(expectation.Name);
                actual.CategoryID.Should().Be(expectation.CategoryID);
                actual.CategoryName.Should().Be(expectation.CategoryName);
                actual.ParentCategoryID.Should().Be(expectation.ParentCategoryID);
                actual.Level.Should().Be(expectation.Level);
            }
        }

        [Fact]
        public void Should_add_partnerpartnumber() {
            _actual.PartnerPartNumber.Should().Be(_productRoot.PartnerPartNumber);
        }
    }
}