using FluentAssertions;
using HPeSimpleParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HPeSimpleParser.Test.Writers
{
    public class CsvSpecificationsWriterTests
    {
        [Fact]
        public void Should_create_line_with_all_properties_null()
        {
            var specifications = new Specifications {
                PartnerPartNumber = "0",
            };
            var writer = new CsvSpecificationsGenerator();
            writer.TryGenerateLine(specifications, out var data);
            data.Should().Be($"0{new string('\t', 1)}{Environment.NewLine}");
        }
        [Fact]
        public void Should_create_line_with_all_properties_without_options()
        {
            var expected = new object[] { "PartnerPartNumber", null };
            var specifications = new Specifications {
                PartnerPartNumber = (string)expected[(int)CasSpecificationsColumnEnum.PartnerPartNumber],
                Items = (List<Specification>)expected[(int)CasSpecificationsColumnEnum.Items]
            };
            var writer = new CsvSpecificationsGenerator();
            writer.TryGenerateLine(specifications, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
        [Fact]
        public void Should_create_line_with_all_properties_witho_one_full_specification()
        {
            var expected = new object[] { "PartnerPartNumber", new object[] { new object[] { SpecificationType.Full,"Name","Value","UnitOfMeasure","Id","GroupId","GroupName" } } };
            var specifications = new Specifications {
                PartnerPartNumber = (string)expected[(int)CasSpecificationsColumnEnum.PartnerPartNumber],
                Items = ((object[])expected[(int)CasSpecificationsColumnEnum.Items])
                        .Select(x => {
                            var option = (object[])x;
                            return new Specification {
                                Type = (SpecificationType)option[(int)CasSpecificationColumnEnum.Type],
                                Name = (string)option[(int)CasSpecificationColumnEnum.Name],
                                Value = (string)option[(int)CasSpecificationColumnEnum.Value],
                                UnitOfMeasure = (string)option[(int)CasSpecificationColumnEnum.UnitOfMeasure],
                                Id = (string)option[(int)CasSpecificationColumnEnum.Id],
                                GroupId = (string)option[(int)CasSpecificationColumnEnum.GroupId],
                                GroupName = (string)option[(int)CasSpecificationColumnEnum.GroupName],
                            };
                        })
                        .ToList()
            };
            var writer = new CsvSpecificationsGenerator();
            writer.TryGenerateLine(specifications, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
        [Fact]
        public void Should_create_line_with_all_properties_witho_two_full_specification()
        {
            var expected = new object[] { "PartnerPartNumber", new object[] { new object[] { SpecificationType.Full,"Name","Value","UnitOfMeasure","Id","GroupId","GroupName" }, new object[] { SpecificationType.Full,"Name1","Value1","UnitOfMeasure1","Id1","GroupId1","GroupName1" } } };
            var specifications = new Specifications {
                PartnerPartNumber = (string)expected[(int)CasSpecificationsColumnEnum.PartnerPartNumber],
                Items = ((object[])expected[(int)CasSpecificationsColumnEnum.Items])
                        .Select(x => {
                            var option = (object[])x;
                            return new Specification {
                                Type = (SpecificationType)option[(int)CasSpecificationColumnEnum.Type],
                                Name = (string)option[(int)CasSpecificationColumnEnum.Name],
                                Value = (string)option[(int)CasSpecificationColumnEnum.Value],
                                UnitOfMeasure = (string)option[(int)CasSpecificationColumnEnum.UnitOfMeasure],
                                Id = (string)option[(int)CasSpecificationColumnEnum.Id],
                                GroupId = (string)option[(int)CasSpecificationColumnEnum.GroupId],
                                GroupName = (string)option[(int)CasSpecificationColumnEnum.GroupName],
                            };
                        })
                        .ToList()
            };
            var writer = new CsvSpecificationsGenerator();
            writer.TryGenerateLine(specifications, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }

        [Fact]
        public void Should_create_line_with_all_properties_witho_one_simple_specification()
        {
            var expected = new object[] { "PartnerPartNumber", new object[] { new object[] { SpecificationType.Simple,"Name","Value" } } };
            var specifications = new Specifications {
                PartnerPartNumber = (string)expected[(int)CasSpecificationsColumnEnum.PartnerPartNumber],
                Items = ((object[])expected[(int)CasSpecificationsColumnEnum.Items])
                        .Select(x => {
                            var option = (object[])x;
                            return new Specification {
                                Type = (SpecificationType)option[(int)CasSpecificationColumnEnum.Type],
                                Name = (string)option[(int)CasSpecificationColumnEnum.Name],
                                Value = (string)option[(int)CasSpecificationColumnEnum.Value],
                            };
                        })
                        .ToList()
            };
            var writer = new CsvSpecificationsGenerator();
           writer.TryGenerateLine(specifications, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }

        [Fact]
        public void Should_create_line_with_all_properties_witho_two_simple_specification()
        {
            var expected = new object[] { "PartnerPartNumber", new object[] { new object[] { SpecificationType.Simple,"Name","Value" }, new object[] { SpecificationType.Simple,"Name1","Value1" } } };
            var specifications = new Specifications {
                PartnerPartNumber = (string)expected[(int)CasSpecificationsColumnEnum.PartnerPartNumber],
                Items = ((object[])expected[(int)CasSpecificationsColumnEnum.Items])
                        .Select(x => {
                            var option = (object[])x;
                            return new Specification {
                                Type = (SpecificationType)option[(int)CasSpecificationColumnEnum.Type],
                                Name = (string)option[(int)CasSpecificationColumnEnum.Name],
                                Value = (string)option[(int)CasSpecificationColumnEnum.Value],
                            };
                        })
                        .ToList()
            };
            var writer = new CsvSpecificationsGenerator();
            writer.TryGenerateLine(specifications, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }

        [Fact]
        public void Should_create_line_with_all_properties_witho_mixed_simple_specification()
        {
            var expected = new object[] { "PartnerPartNumber", new object[] { new object[] { SpecificationType.Full,"Name","Value","UnitOfMeasure","Id","GroupId","GroupName" }, new object[] { SpecificationType.Simple,"Name1","Value1" } } };
            var specifications = new Specifications {
                PartnerPartNumber = (string)expected[(int)CasSpecificationsColumnEnum.PartnerPartNumber],
                Items = ((object[])expected[(int)CasSpecificationsColumnEnum.Items])
                        .Select(x => {
                            var option = (object[])x;
                            if ((SpecificationType)option[(int)CasSpecificationColumnEnum.Type] == SpecificationType.Full)
                            {
                                return new Specification {
                                Type = (SpecificationType)option[(int)CasSpecificationColumnEnum.Type],
                                Name = (string)option[(int)CasSpecificationColumnEnum.Name],
                                Value = (string)option[(int)CasSpecificationColumnEnum.Value],
                                UnitOfMeasure = (string)option[(int)CasSpecificationColumnEnum.UnitOfMeasure],
                                Id = (string)option[(int)CasSpecificationColumnEnum.Id],
                                GroupId = (string)option[(int)CasSpecificationColumnEnum.GroupId],
                                GroupName = (string)option[(int)CasSpecificationColumnEnum.GroupName],
                            };
                            }
                            else{
                            return new Specification {
                                Type = (SpecificationType)option[(int)CasSpecificationColumnEnum.Type],
                                Name = (string)option[(int)CasSpecificationColumnEnum.Name],
                                Value = (string)option[(int)CasSpecificationColumnEnum.Value],
                            };
                            }
                        })
                        .ToList()
            };
            var writer = new CsvSpecificationsGenerator();
            writer.TryGenerateLine(specifications, out var data);
            data.Should().Be($"{string.Join("\t", expected.Select(x => x.ToDebugString()))}{Environment.NewLine}");
        }
    }
}
