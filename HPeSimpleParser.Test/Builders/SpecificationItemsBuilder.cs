using System.Collections.Generic;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
using Specification = FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model.Specification;

namespace HPeSimpleParser.Test.Builders {
    public sealed class SpecificationItemsBuilder {
        private readonly List<Specification> _specifications = new List<Specification>();

        private SpecificationItemsBuilder() {
        }

        public static SpecificationItemsBuilder With() {
            return new SpecificationItemsBuilder();
        }

        public IReadOnlyList<Specification> Build() {
            return _specifications;
        }

        public SpecificationItemsBuilder AddFull() {
            _specifications.Add(new Specification("Name", "Value", SpecificationType.Full, "UnitOfMeasure", "Id",
                "GroupId", "GroupName", "Label"));
            return this;
        }

        public SpecificationItemsBuilder AddSimple() {
            _specifications.Add(new Specification("Name", "Value", SpecificationType.Simple, "UnitOfMeasure", "Id",
                "GroupId", "GroupName", "Label"));
            return this;
        }
    }
}