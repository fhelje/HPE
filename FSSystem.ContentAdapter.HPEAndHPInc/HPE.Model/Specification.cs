using FSSystem.ContentAdapter.Model;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model {
    public class Specification {
        public Specification(string name, string value, SpecificationType type, string unitOfMeasure, string id,
            string groupId, string groupName, string label) {
            Name = name;
            Value = value;
            Type = type;
            UnitOfMeasure = unitOfMeasure;
            Id = id;
            GroupId = groupId;
            GroupName = groupName;
            Label = label;
        }

        public string Name { get; }
        public string Value { get; private set; }
        public SpecificationType Type { get; }
        public string UnitOfMeasure { get; private set; }
        public string Id { get; }
        public string GroupId { get; }
        public string GroupName { get; }
        public string Label { get; private set; }

        public void UpdateUnitOfMeasure(string value) {
            UnitOfMeasure = value;
        }

        public void UpdateLabel(string label) {
            Label = label;
        }

        public void UpdateValue(string value) {
            Value = value;
        }
    }
}