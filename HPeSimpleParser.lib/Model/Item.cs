namespace HPeSimpleParser.lib.Model
{
    public class Item
    {
        public string PartnerPartNumber { get; set; }

        public Item()
        {
            Product = new Product();
            Detail = new Detail();
            Link = new Link();
            Marketing = new Marketing();
            Specifications = new Specifications();
            Supplier = new Supplier();
            Options = new Options();
            Hierarchies = new Hierarchies();
        }

        public Product Product { get; set; }
        public Link Link { get; set; }
        public Marketing Marketing { get; set; }
        public Specifications Specifications { get; set; }
        public Supplier Supplier { get; set; }
        public Detail Detail { get; set; }
        public Options Options { get; set; }
        public Hierarchies Hierarchies { get; set; }
    }
}