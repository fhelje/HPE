namespace HPeSimpleParser.lib.HPE.Model {
    public class ProductVariant {
        public ProductVariant(string description, string opt, string upcCode) {
            Description = description;
            Opt = opt;
            UpcCode = upcCode;
        }

        public string UpcCode { get; }
        public string Description { get; }
        public string Opt { get; }
    }
}