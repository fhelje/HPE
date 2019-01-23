using System.Collections.Generic;

namespace HPeSimpleParser.HierarchyFile {
    public class HierarchyNode {
        public HierarchyNode() {
            Children = new List<HierarchyNode>();
        }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryID { get; set; }
        public int Level { get; set; }
        public List<HierarchyNode> Children { get; set; }
        public string PartnerHierarchyCode { get; set; }
    }
}