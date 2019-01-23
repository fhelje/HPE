namespace HPeSimpleParser.HPE.Model {
    public class Hierarchy {
        public Hierarchy(string name, string categoryID, string categoryName, string parentCategoryID, string partnerHierarchyCode) {
            Name = name;
            CategoryID = categoryID;
            CategoryName = categoryName;
            ParentCategoryID = parentCategoryID;
            PartnerHierarchyCode = partnerHierarchyCode;
        }

        public string Name { get; }
        // For HPE \item\hierarchy\small_series[pmoid]
        public string CategoryID { get; }
        // For HPE \item\hierarchy\small_series[name]
        public string CategoryName { get; }
        // For HPE \item\hierarchy\big_series[pmoid]
        public string ParentCategoryID { get; }
        public int Level { get; } = 5;
        public string PartnerHierarchyCode { get; set; }
    }
}