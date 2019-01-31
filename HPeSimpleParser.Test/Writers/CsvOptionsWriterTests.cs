using System;
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
    public class CsvOptionsWriterTests {
        [Fact]
        public void Should_create_line_with_all_properties_null() {
            var options = new Options {
                PartnerPartNumber = "0"
            };
            var writer =
                new CsvOptionsGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(options, out var data).Should().BeFalse();
            data.Should().BeNull();
        }

        [Fact]
        public void Should_create_line_with_all_properties_without_one_option() {
            var expected = new object[]
                {"PartnerPartNumber", new object[] {new object[] {"PartNumber", "Name", "GroupId", "GroupName"}}};
            var options = new Options {
                PartnerPartNumber = (string) expected[(int) CasOptionsColumnEnum.PartnerPartNumber],
                Items = ((object[]) expected[(int) CasOptionsColumnEnum.Items])
                    .Select(x =>
                    {
                        var option = (object[]) x;
                        return new Option {
                            PartNumber = (string) option[(int) CasOptionColumnEnum.PartNumber],
                            Name = (string) option[(int) CasOptionColumnEnum.Name],
                            GroupId = (string) option[(int) CasOptionColumnEnum.GroupId],
                            GroupName = (string) option[(int) CasOptionColumnEnum.GroupName]
                        };
                    })
                    .ToList()
            };
            var writer =
                new CsvOptionsGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(options, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }

        [Fact]
        public void Should_create_line_with_all_properties_without_two_option() {
            var expected = new object[] {
                "PartnerPartNumber",
                new object[] {
                    new object[] {"PartNumber", "Name", "GroupId", "GroupName"},
                    new object[] {"PartNumber1", "Name1", "GroupId1", "GroupName1"}
                }
            };
            var options = new Options {
                PartnerPartNumber = (string) expected[(int) CasOptionsColumnEnum.PartnerPartNumber],
                Items = ((object[]) expected[(int) CasOptionsColumnEnum.Items])
                    .Select(x =>
                    {
                        var option = (object[]) x;
                        return new Option {
                            PartNumber = (string) option[(int) CasOptionColumnEnum.PartNumber],
                            Name = (string) option[(int) CasOptionColumnEnum.Name],
                            GroupId = (string) option[(int) CasOptionColumnEnum.GroupId],
                            GroupName = (string) option[(int) CasOptionColumnEnum.GroupName]
                        };
                    })
                    .ToList()
            };
            var writer =
                new CsvOptionsGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(options, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }

        [Fact]
        public void Should_skip_line_with_all_properties_without_options() {
            var expected = new object[] {"PartnerPartNumber", null};
            var options = new Options {
                PartnerPartNumber = (string) expected[(int) CasOptionsColumnEnum.PartnerPartNumber],
                Items = (List<Option>) expected[(int) CasOptionsColumnEnum.Items]
            };
            var writer =
                new CsvOptionsGenerator(new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy()));
            writer.TryGenerateLine(options, out var data).Should().BeFalse();
            data.Should().BeNull();
        }
    }
}