using System;
using System.Collections.Generic;
using FluentAssertions;
using HPeSimpleParser.HPE.Model;
using HPeSimpleParser.Parser;
using Xunit;

namespace HPeSimpleParser.Test {
    public class ImageSelectionTests {
        [Fact]
        public void Should_not_fail_if_images_is_null() {
            var product = new ProductRoot { Links = { ImageLinks = null } };
            HpeParser.FilterImages(product).Should().NotBeNull();
        }

        [Fact]
        public void Should_not_fail_if_images_is_empty() {
            var product = new ProductRoot { Links = { ImageLinks = new List<Image>() } };
            HpeParser.FilterImages(product).Should().NotBeNull();
        }

        // Should return image if one exists

        [Fact]
        public void Should_return_image_if_only_one_exists() {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With().AddImage(x => x.Default()).Build(),
                }
            };
            HpeParser.FilterImages(product).Should().HaveCount(1);
        }
        [Theory]
        [InlineData(400, 600, 301, 600, SizeCategoryEnum.Large)]
        [InlineData(400, 600, 400, 599, SizeCategoryEnum.Large)]
        public void Should_return_largest_image_in_size_category(int img1Height, int img1Width, int img2Height, int img2Width, SizeCategoryEnum category) {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With()
                                                 .AddImage(x => x.Default().SetHeight(img1Height).SetWidth(img1Width))
                                                 .AddImage(x => x.Default().SetHeight(img2Height).SetWidth(img2Width))
                                                 .Build()
                }
            };
            var images = HpeParser.FilterImages(product);
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
        public void  Should_return_one_image_in_every_size_category(
            int img1Height, int img1Width, 
            int count,
            int? img2Height = null, int? img2Width = null, 
            int? img3Height = null, int? img3Width = null,
            bool same = false
            ) {
            var imageLinks = ImageListBuilder.With()
                .AddImage(x => x.Default().SetHeight(img1Height).SetWidth(img1Width));
            if (img2Height.HasValue) {
                imageLinks.AddImage(x => x.Default().SetHeight(img2Height.Value).SetWidth(img2Width.Value).SetUrl(same ? "Same" : Guid.NewGuid().ToString()));
            }
            if (img3Height.HasValue) {
                imageLinks.AddImage(x => x.Default().SetHeight(img3Height.Value).SetWidth(img3Width.Value).SetUrl(same ? "Same" : Guid.NewGuid().ToString()));
            }
            var product = new ProductRoot {
                Links = {
                    ImageLinks = imageLinks.Build()
                }
            };
            var images = HpeParser.FilterImages(product);
            images.Should().HaveCount(count);
        }

        [Fact]
        public void Should_not_add_images_from_wrong_group() {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With()
                        .AddImage(x => x.Default())
                        .AddImage(x => x.Default().SetSizeCategory(SizeCategoryEnum.Wrong))
                        .Build()
                }
            };
            var images = HpeParser.FilterImages(product);
            images.Should().HaveCount(1);
        }

        [Fact]
        public void Should_return_one_image_per_master_object_name_and_size_group() {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With()
                        .AddImage(x => x.Default())
                        .AddImage(x => x.Default().SetMasterObjectName("2"))
                        .Build()
                }
            };
            var images = HpeParser.FilterImages(product);
            images.Should().HaveCount(2);
        }
        // Should_return_prefer_CmgAcronym_cmg674
        [Fact]
        public void Should_return_prefer_CmgAcronym_cmg674() {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With()
                        .AddImage(x => x.Default().SetCmgAcronym("cmg675"))
                        .AddImage(x => x.Default())
                        .Build()
                }
            };
            var images = HpeParser.FilterImages(product);
            images.Should().HaveCount(1);
            images[0].CmgAcronym.Should().Be("cmg674");
        }

        [Fact]
        public void Should_pick_one_of_each_orientation() {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With()
                        .AddImage(x => x.Default().SetOrientation("Center facing"))
                        .AddImage(x => x.Default().SetOrientation("Left facing"))
                        .AddImage(x => x.Default().SetOrientation("Rear facing"))
                        .Build()
                }
            };
            var images = HpeParser.FilterImages(product);
            images.Should().HaveCount(3);
        }

        [Fact]
        public void Should_return_prefer_content_type_jpg_then_png_then_gif() {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With()
                        .AddImage(x => x.Default().SetCmgAcronym("cmg675").SetContentType("gif"))
                        .AddImage(x => x.Default().SetCmgAcronym("cmg675").SetContentType("png"))
                        .AddImage(x => x.Default().SetCmgAcronym("cmg675").SetContentType("jpg"))
                        .AddImage(x => x.Default().SetContentType("gif"))
                        .AddImage(x => x.Default().SetContentType("jpg"))
                        .AddImage(x => x.Default().SetContentType("png"))
                        .Build()
                }
            };
            var images = HpeParser.FilterImages(product);
            images.Should().HaveCount(1);
            images[0].CmgAcronym.Should().Be("cmg674");
            images[0].ContentType.Should().Be("jpg");
        }

        [Fact]
        public void Should_deprioritize_documentTypeDetail_of_certain_types() {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With()
                        .AddImage(x => x.Default().SetDocumentTypeDetail("product image - not as shown"))
                        .AddImage(x => x.Default().SetDocumentTypeDetail("product image"))
                        .Build()
                }
            };
            var images = HpeParser.FilterImages(product);
            images.Should().HaveCount(1);
            images[0].DocumentTypeDetail.Should().Be("product image");
        }

        [Fact]
        public void Should_remove_all_duplicates() {
            var product = new ProductRoot {
                Links = {
                    ImageLinks = ImageListBuilder.With()
                        .AddImage(x => x.Default().SetUrl("http://1.jpg"))
                        .AddImage(x => x.Default().SetUrl("http://1.jpg").SetMasterObjectName("2"))
                        .Build()
                }
            };
            var images = HpeParser.FilterImages(product);
            images.Should().HaveCount(1);
        }
        
    }

    public class ImageListBuilder {
        private readonly List<Image> _data;
        private ImageListBuilder() {
            _data = new List<Image>();
        }

        public static ImageListBuilder With() {
            return new ImageListBuilder();
        }

        public ImageListBuilder AddImage(Action<ImageBuilder> imageBuilderAction) {
            var ib = ImageBuilder.With();
            imageBuilderAction(ib);
            _data.Add(ib.Build());
            return this;
        }

        public List<Image> Build() {
            return _data;
        }
    }

    public class ImageBuilder {
        private Image _data;

        private ImageBuilder() {
            _data = new Image();
        }

        public static ImageBuilder With() {
            return new ImageBuilder();
        }

        public ImageBuilder Default() {
            _data = new Image {
                MasterObjectName = "1",
                CmgAcronym = "cmg674",
                Action = "update",
                ContentType = "jpg",
                DocumentTypeDetail = "product image",
                Orientation = "Center facing",
                PixelHeight = "400",
                PixelWidth = "600",
                ImageUrlHttp = Guid.NewGuid().ToString()
            };
            return this;
        }

        public Image Build() {
            return _data;
        }

        public ImageBuilder SetHeight(int height) {
            _data.PixelHeight = height.ToString();
            return this;
        }

        public ImageBuilder SetWidth(int width) {
            _data.PixelWidth = width.ToString();
            return this;
        }

        public ImageBuilder SetSizeCategory(SizeCategoryEnum category) {
            if (category == SizeCategoryEnum.Wrong) {
                _data.PixelWidth = "";
                _data.PixelHeight = "";
            }
            return this;
        }

        public ImageBuilder SetMasterObjectName(string masterObjectName) {
            _data.MasterObjectName = masterObjectName;
            return this;
        }

        public ImageBuilder SetCmgAcronym(string cmgAcronym) {
            _data.CmgAcronym = cmgAcronym;
            return this;
        }

        public ImageBuilder SetContentType(string contentType) {
            _data.ContentType = contentType;
            return this;
        }

        public ImageBuilder SetDocumentTypeDetail(string docTypeDetail) {
            _data.DocumentTypeDetail = docTypeDetail;
            return this;
        }

        public ImageBuilder SetOrientation(string orientation) {
            _data.Orientation = orientation;
            return this;
        }

        public ImageBuilder SetUrl(string url) {
            _data.ImageUrlHttp = url;
            return this;
        }
    }
}
