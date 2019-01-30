using System;
using System.Collections.Generic;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model {
    public class ProductRoot {
        public string File { get; }

        public ProductRoot(string file,
            string partNumber,
            string partnerPartNumber,
            string languageId,
            Product product,
            IReadOnlyList<Hierarchy> branch, 
            IReadOnlyList<ProductVariant> productVariants,
            Marketing marketing, 
            IReadOnlyList<Option> optionsItems, 
            IReadOnlyList<Specification> specificationsLabeledItems, 
            Links links,
            IReadOnlyList<Hierarchy> hierarchies,
            Detail detail
            ) {
            File = file;
            Product = product;
            PartNumber = partNumber;
            PartnerPartNumber = partnerPartNumber;
            LanguageId = languageId;

            Id = Guid.NewGuid();
            //OpcCodes = opcCodes;
            Hierarchy = hierarchies;
            Branch = branch;
            Product = product;
            Links = links;
            Marketing = marketing;
            Options = new Options(optionsItems);
            Specifications = new Specifications(specificationsLabeledItems);
            ProductVariants = productVariants;
            Detail = detail;
        }


        public DateTime TimeStamp { get; } = DateTime.Now;

        // \item[num] inclusive # XXX
        public string PartNumber { get; }
        // \item[num] inclusive # XXX
        public string PartnerPartNumber { get; }
        // \item[culturecode]
        public string LanguageId { get; }
        public Guid Id { get; }
        //public IReadOnlyList<string> OpcCodes { get; }

        public IReadOnlyList<Hierarchy> Hierarchy { get; set; }
        public IReadOnlyList<Hierarchy> Branch { get; }
        public Product Product { get; }
        public Links Links { get;}
        public Marketing Marketing { get; }
        public Options Options { get; }
        public Specifications Specifications { get; }
        public IReadOnlyList<ProductVariant> ProductVariants { get; }
        public Detail Detail { get; }
    }
}