using System;
using System.Collections.Generic;

namespace HPeSimpleParser.lib.HPE.Model {
    public class ProductRoot {
        public ProductRoot() {

            Id = Guid.NewGuid();
            OpcCodes = new List<string> ();
            Hierarchy = new List<Hierarchy>();
            Product = new Product();
            Links = new Links();
            Marketing = new Marketing();
            Options = new Options();
            Specifications = new Specifications();
            ProductVariants = new List<ProductVariant>();
            Detail = new Detail();
        }
        public DateTime TimeStamp { get; } = DateTime.Now;

        // \item[num] inclusive # XXX
        public string PartNumber { get; set; }
        // \item[num] inclusive # XXX
        public string PartnerPartNumber { get; set; }
        // \item[culturecode]
        public string LanguageId { get; set; }
        public Guid Id { get; }
        public List<string> OpcCodes { get; }

        public List<Hierarchy> Hierarchy { get; set; }
        public Product Product { get; set; }
        public Links Links { get; set; }
        public Marketing Marketing { get; set; }
        public Options Options { get; set; }
        public Specifications Specifications { get; set; }
        public List<ProductVariant> ProductVariants { get; }
        public Detail Detail { get; set; }
    }
}