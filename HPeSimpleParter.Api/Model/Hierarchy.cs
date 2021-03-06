﻿namespace HPeSimpleParter.Api.Model {
    public class Hierarchy {
        public Hierarchy(string name, string categoryID, string categoryName, string parentCategoryID) {
            Name = name;
            CategoryID = categoryID;
            CategoryName = categoryName;
            ParentCategoryID = parentCategoryID;
        }

        public string Name { get; }
        // For HPE \item\hierarchy\small_series[pmoid]
        public string CategoryID { get; }
        // For HPE \item\hierarchy\small_series[name]
        public string CategoryName { get; }
        // For HPE \item\hierarchy\big_series[pmoid]
        public string ParentCategoryID { get; }
        public int Level { get; } = 5;
    }
}