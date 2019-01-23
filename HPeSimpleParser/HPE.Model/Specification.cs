using HPeSimpleParser.Model;

namespace HPeSimpleParser.HPE.Model {
    public class Specification {
        public string Name { get; set; }
        public string Value { get; set; }
        public SpecificationType Type { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string Label { get; set; }
    }
}