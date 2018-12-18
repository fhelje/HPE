namespace HPeSimpleParser.lib.Model
{
    public class Hierarchy
    {
        public string Name { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryID { get; set; }
        public int Level { get; set; }
    }
}