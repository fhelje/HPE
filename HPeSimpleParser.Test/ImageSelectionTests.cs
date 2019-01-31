using System;
using FluentAssertions;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser;
using HPeSimpleParser.Test.Builders;
using Xunit;

namespace HPeSimpleParser.Test {
    public class ImageSelectionTests {
        [Theory]
        [InlineData(400, 600, 301, 600, SizeCategoryEnum.Large)]
        [InlineData(400, 600, 400, 599, SizeCategoryEnum.Large)]
        public void Should_return_largest_image_in_size_category(int img1Height, int img1Width, int img2Height,
            int img2Width, SizeCategoryEnum category) {
            var product = ProductRootBuilder.With()
                .WithImages(x =>
                    x.AddImage(i =>
                            i.AddImage(y =>
                                y.Default().SetHeight(img1Height.ToString()).SetWidth(img1Width.ToString())))
                        .AddImage(i => i.AddImage(y =>
                            y.Default().SetHeight(img2Height.ToString()).SetWidth(img2Width.ToString())))
                ).Build();
            var images = ImageSelector.FilterImages(product.Links.SelectedImages);
            images.Should().HaveCount(1);
            images[0].Width.Should().Be(img1Width);
            images[0].Height.Should().Be(img1Height);
            images[0].SizeCategory.Should().Be(category);
        }

        [Theory]
        [InlineData(400, 600, 1)]
        [InlineData(400, 600, 1, 301, 600)]
        [InlineData(400, 600, 2, 151, 210)]
        [InlineData(400, 600, 2, 100, 150, 200, 300)]
        [InlineData(400, 600, 2, 151, 210, 151, 210, true)]
        public void Should_return_one_image_in_every_size_category(
            int img1Height, int img1Width,
            int count,
            int? img2Height = null, int? img2Width = null,
            int? img3Height = null, int? img3Width = null,
            bool same = false
        ) {
            var imageLinks = ImageListBuilder.With()
                .AddImage(x => x.Default().SetHeight(img1Height.ToString()).SetWidth(img1Width.ToString()));
            // ReSharper disable PossibleInvalidOperationException
            if (img2Height.HasValue) {
                imageLinks.AddImage(x =>
                    x.Default().SetHeight(img2Height.Value.ToString()).SetWidth(img2Width.Value.ToString())
                        .SetUrl(same ? "Same" : Guid.NewGuid().ToString()));
            }

            if (img3Height.HasValue) {
                imageLinks.AddImage(x =>
                    x.Default().SetHeight(img3Height.Value.ToString()).SetWidth(img3Width.Value.ToString())
                        .SetUrl(same ? "Same" : Guid.NewGuid().ToString()));
            }
            // ReSharper restore PossibleInvalidOperationException

            var images = ImageSelector.FilterImages(imageLinks.Build());
            images.Should().HaveCount(count);
        }

        [Fact]
        public void Should_deprioritize_documentTypeDetail_of_certain_types() {
            var imageList = ImageListBuilder.With()
                .AddImage(x => x.Default().SetDocumentTypeDetail("product image - not as shown"))
                .AddImage(x => x.Default().SetDocumentTypeDetail("product image"))
                .Build();
            var images = ImageSelector.FilterImages(imageList);
            images.Should().HaveCount(1);
            images[0].TypeDetail.Should().Be("product image");
        }

        [Fact]
        public void Should_not_add_images_from_wrong_group() {
            var imageList = ImageListBuilder.With()
                .AddImage(x => x.Default())
                .AddImage(x => x.Default().SetSizeCategory(SizeCategoryEnum.Wrong))
                .Build();
            var images = ImageSelector.FilterImages(imageList);
            images.Should().HaveCount(1);
        }

        [Fact]
        public void Should_not_fail_if_images_is_empty() {
            var product = ProductRootBuilder.With().Build();
            ImageSelector.FilterImages(product.Links.SelectedImages).Should().NotBeNull();
        }

        [Fact]
        public void Should_not_fail_if_images_is_null() {
            var product = ProductRootBuilder.With().WithImages(x => x.SetImagesToNull()).Build();
            ImageSelector.FilterImages(product.Links.SelectedImages).Should().NotBeNull();
        }

        [Fact]
        public void Should_pick_one_of_each_orientation() {
            var imageList = ImageListBuilder.With()
                .AddImage(x => x.Default().SetOrientation("Center facing"))
                .AddImage(x => x.Default().SetOrientation("Left facing"))
                .AddImage(x => x.Default().SetOrientation("Rear facing"))
                .Build();
            var images = ImageSelector.FilterImages(imageList);
            images.Should().HaveCount(3);
        }

        [Fact]
        public void Should_remove_all_duplicates() {
            var imageList = ImageListBuilder.With()
                .AddImage(x => x.Default().SetUrl("http://1.jpg"))
                .AddImage(x => x.Default().SetUrl("http://1.jpg").SetMasterObjectName("2"))
                .Build();
            var images = ImageSelector.FilterImages(imageList);
            images.Should().HaveCount(1);
        }

        // Should return image if one exists

        [Fact]
        public void Should_return_image_if_only_one_exists() {
            var product = ProductRootBuilder.With()
                .WithImages(x =>
                    x.AddImage(i => i.AddImage(y => y.Default()))
                ).Build();
            ImageSelector.FilterImages(product.Links.SelectedImages).Should().HaveCount(1);
        }

        [Fact]
        public void Should_return_one_image_per_master_object_name_and_size_group() {
            var imageList = ImageListBuilder.With()
                .AddImage(x => x.Default())
                .AddImage(x => x.Default().SetMasterObjectName("2"))
                .Build();
            var images = ImageSelector.FilterImages(imageList);
            images.Should().HaveCount(2);
        }

        // Should_return_prefer_CmgAcronym_cmg674
        [Fact]
        public void Should_return_prefer_CmgAcronym_cmg674() {
            var imageList = ImageListBuilder.With()
                .AddImage(x => x.Default().SetCmgAcronym("cmg675"))
                .AddImage(x => x.Default())
                .Build();
            var images = ImageSelector.FilterImages(imageList);
            images.Should().HaveCount(1);
            images[0].GroupingKey1.Should().Be("cmg674");
        }

        [Fact]
        public void Should_return_prefer_content_type_jpg_then_png_then_gif() {
            var imageList = ImageListBuilder.With()
                .AddImage(x => x.Default().SetCmgAcronym("cmg675").SetContentType("gif"))
                .AddImage(x => x.Default().SetCmgAcronym("cmg675").SetContentType("png"))
                .AddImage(x => x.Default().SetCmgAcronym("cmg675").SetContentType("jpg"))
                .AddImage(x => x.Default().SetContentType("gif"))
                .AddImage(x => x.Default().SetContentType("jpg"))
                .AddImage(x => x.Default().SetContentType("png"))
                .Build();
            var images = ImageSelector.FilterImages(imageList);
            images.Should().HaveCount(1);
            images[0].GroupingKey1.Should().Be("cmg674");
            images[0].ContentType.Should().Be("jpg");
        }
    }
}