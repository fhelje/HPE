using System.Collections.Concurrent;

namespace HPeSimpleParser {
    public class HPEHierarchyNode {
        public HPEHierarchyNode() {
            Children = new ConcurrentDictionary<string, HPEHierarchyNode>();
        }
        public string Id { get; set; }
        public ConcurrentDictionary<string, HPEHierarchyNode> Children { get; private set; }
        public string Name { get; set; }
        public string ParentCategoryId { get; set; }

        public string PartnerHierarchyCode => "HPE";
        public int Level { get; set; }
    }
}