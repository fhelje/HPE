﻿using System;
using System.Collections.Generic;
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
    public class CsvLinkWriterTests {
        [Fact]
        public void Should_create_line_with_all_properties_null() {
            var link = new Link {
                PartnerPartNumber = "0"
            };
            var writer =
                new CsvLinkGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(link, out var data).Should().BeFalse();
            data.Should().BeNull();
        }

        [Fact]
        public void Should_create_line_with_all_properties_witho_one_image() {
            var expected = new object[] {
                "PartnerPartNumber", "PdfLinkDataSheet", "PdfLinkManual",
                new object[] {new object[] {"url", 1, 2, "contenttype", "title"}}
            };
            var link = new Link {
                PartnerPartNumber = (string) expected[(int) CasLinkColumnEnum.PartnerPartNumber],
                PdfLinkDataSheet = (string) expected[(int) CasLinkColumnEnum.PdfLinkDataSheet],
                PdfLinkManual = (string) expected[(int) CasLinkColumnEnum.PdfLinkManual],
                Images = ((object[]) expected[(int) CasLinkColumnEnum.Images])
                    .Select(x =>
                    {
                        var imageData = (object[]) x;
                        return new Image {
                            Url = (string) imageData[(int) CasImageColumnEnum.Url],
                            Height = (int) imageData[(int) CasImageColumnEnum.Height],
                            Width = (int) imageData[(int) CasImageColumnEnum.Width],
                            ContentType = (string) imageData[(int) CasImageColumnEnum.ContentType],
                            Title = (string) imageData[(int) CasImageColumnEnum.Title]
                        };
                    })
                    .ToList()
            };
            var writer =
                new CsvLinkGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(link, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }

        [Fact]
        public void Should_create_line_with_all_properties_witho_two_image() {
            var expected = new object[] {
                "PartnerPartNumber", "PdfLinkDataSheet", "PdfLinkManual",
                new object[] {
                    new object[] {"url", 1, 2, "contenttype", "title"},
                    new object[] {"url1", 11, 12, "contenttype1", "title1"}
                }
            };
            var link = new Link {
                PartnerPartNumber = (string) expected[(int) CasLinkColumnEnum.PartnerPartNumber],
                PdfLinkDataSheet = (string) expected[(int) CasLinkColumnEnum.PdfLinkDataSheet],
                PdfLinkManual = (string) expected[(int) CasLinkColumnEnum.PdfLinkManual],
                Images = ((object[]) expected[(int) CasLinkColumnEnum.Images])
                    .Select(x =>
                    {
                        var imageData = (object[]) x;
                        return new Image {
                            Url = (string) imageData[(int) CasImageColumnEnum.Url],
                            Height = (int) imageData[(int) CasImageColumnEnum.Height],
                            Width = (int) imageData[(int) CasImageColumnEnum.Width],
                            ContentType = (string) imageData[(int) CasImageColumnEnum.ContentType],
                            Title = (string) imageData[(int) CasImageColumnEnum.Title]
                        };
                    })
                    .ToList()
            };
            var writer =
                new CsvLinkGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(link, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }

        [Fact]
        public void Should_create_line_with_all_properties_without_images() {
            var expected = new object[] {"PartnerPartNumber", "PdfLinkDataSheet", "PdfLinkManual", null};
            var link = new Link {
                PartnerPartNumber = (string) expected[(int) CasLinkColumnEnum.PartnerPartNumber],
                PdfLinkDataSheet = (string) expected[(int) CasLinkColumnEnum.PdfLinkDataSheet],
                PdfLinkManual = (string) expected[(int) CasLinkColumnEnum.PdfLinkManual],
                Images = (List<Image>) expected[(int) CasLinkColumnEnum.Images]
            };
            var writer =
                new CsvLinkGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(link, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
    }
}