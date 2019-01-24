namespace HPeSimpleParser.Generic.Model
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

        public Product Product { get; }
        public Link Link { get; }
        public Marketing Marketing { get; }
        public Specifications Specifications { get; }
        public Supplier Supplier { get; }
        public Detail Detail { get; }
        public Options Options { get; }
        public Hierarchies Hierarchies { get; }
    }
}