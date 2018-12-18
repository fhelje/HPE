using FluentAssertions;
using HPeSimpleParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HPeSimpleParser.Test.Writers
{
    public class CsvOptionsWriterTests
    {
        [Fact]
        public void Should_create_line_with_all_properties_null()
        {
            var ptions = new Options {
                PartnerPartNumber = "0",
            };
            var writer = new CsvOptionsGenerator();
            var data = writer.GenerateLine(ptions);
            data.Should().Be($"0{new string('\t', 1)}{Environment.NewLine}");
        }
        [Fact]
        public void Should_create_line_with_all_properties_without_options()
        {
            var expected = new object[] { "PartnerPartNumber", null };
            var options = new Options {
                PartnerPartNumber = (string)expected[(int)CasOptionsColumnEnum.PartnerPartNumber],
                Items = (List<Option>)expected[(int)CasOptionsColumnEnum.Items]
            };
            var writer = new CsvOptionsGenerator();
            var data = writer.GenerateLine(options);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
        [Fact]
        public void Should_create_line_with_all_properties_witho_one_option()
        {
            var expected = new object[] { "PartnerPartNumber", new object[] { new object[] { "PartNumber", "Name", "GroupId", "GroupName" } } };
            var link = new Options {
                PartnerPartNumber = (string)expected[(int)CasOptionsColumnEnum.PartnerPartNumber],
                Items = ((object[])expected[(int)CasOptionsColumnEnum.Items])
                        .Select(x => {
                            var option = (object[])x;
                            return new Option {
                                PartNumber = (string)option[(int)CasOptionColumnEnum.PartNumber],
                                Name = (string)option[(int)CasOptionColumnEnum.Name],
                                GroupId = (string)option[(int)CasOptionColumnEnum.GroupId],
                                GroupName = (string)option[(int)CasOptionColumnEnum.GroupName],
                            };
                        })
                        .ToList()
            };
            var writer = new CsvOptionsGenerator();
            var data = writer.GenerateLine(link);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
        [Fact]
        public void Should_create_line_with_all_properties_witho_two_option()
        {
            var expected = new object[] { "PartnerPartNumber", new object[] { new object[] { "PartNumber", "Name", "GroupId", "GroupName" }, new object[] { "PartNumber1", "Name1", "GroupId1", "GroupName1" } } };
            var link = new Options {
                PartnerPartNumber = (string)expected[(int)CasOptionsColumnEnum.PartnerPartNumber],
                Items = ((object[])expected[(int)CasOptionsColumnEnum.Items])
                        .Select(x => {
                            var option = (object[])x;
                            return new Option {
                                PartNumber = (string)option[(int)CasOptionColumnEnum.PartNumber],
                                Name = (string)option[(int)CasOptionColumnEnum.Name],
                                GroupId = (string)option[(int)CasOptionColumnEnum.GroupId],
                                GroupName = (string)option[(int)CasOptionColumnEnum.GroupName],
                            };
                        })
                        .ToList()
            };
            var writer = new CsvOptionsGenerator();
            var data = writer.GenerateLine(link);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }

    }
}
