using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace HPeSimpleParter.Api.Model {
    public class ProductRoot {
        public ProductRoot() {
            OpcCodes = new List<string> ();
            Hierarchy = new List<Hierarchy>();
            Product = new Product();
            Links = new Links();
            Marketing = new Marketing();
            Options = new Options();
            Specifications = new Specifications();
            ProductVariants = new List<ProductVariant>();
        }
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; } = DateTime.Now;

        // \item[num] inclusive # XXX
        public string PartNumber { get; set; }
        // \item[num] inclusive # XXX
        public string PartnerPartNumber { get; set; }
        // \item[culturecode]
        public string LanguageId { get; set; }
        public List<string> OpcCodes { get; }

        public List<Hierarchy> Hierarchy { get; set; }
        public Product Product { get; set; }
        public Links Links { get; set; }
        public Marketing Marketing { get; set; }
        public Options Options { get; set; }
        public Specifications Specifications { get; set; }
        public List<ProductVariant> ProductVariants { get; }
    }
}