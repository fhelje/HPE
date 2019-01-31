using System.Collections.Concurrent;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public class HPEHierarchyNode {
        public HPEHierarchyNode() {
            Children = new ConcurrentDictionary<string, HPEHierarchyNode>();
        }

        public string Id { get; set; }
        public ConcurrentDictionary<string, HPEHierarchyNode> Children { get; }
        public string Name { get; set; }
        public string ParentCategoryId { get; set; }

        public string PartnerHierarchyCode { get; set; }
        public int Level { get; set; }
    }
}