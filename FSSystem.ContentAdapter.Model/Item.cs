namespace FSSystem.ContentAdapter.Model {
    public class Item {
        public Item() {
            Product = new Product();
            Detail = new Detail();
            Link = new Link();
            Marketing = new Marketing();
            Specifications = new Specifications();
            Supplier = new Supplier();
            Options = new Options();
            Hierarchies = new Hierarchies();
        }

        public string PartnerPartNumber { get; set; }

        public Product Product { get; }
        public Link Link { get; }
        public Marketing Marketing { get; }
        public Specifications Specifications { get; }
        public Supplier Supplier { get; }
        public Detail Detail { get; }
        public Options Options { get; }
        public Hierarchies Hierarchies { get; }
        public string[] ProductVariants { get; set; }
    }
}