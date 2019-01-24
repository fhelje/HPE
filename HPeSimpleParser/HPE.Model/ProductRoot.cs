using System;
using System.Collections.Generic;

namespace HPeSimpleParser.HPE.Model {
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

    public class ProductState {
        public bool CarePackRegistration { get; set; }
        public string DescriptionLong { get; set; }
        public int Unspsc { get; set; }
        public string ManufacturerCode { get; set; }
        public string ManufacturerName { get; set; }
        public string LanguageId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string PartnerHierarchyCode { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public bool IsEol { get; set; }
        public string AlternateCategoryId { get; set; }
        public string AlternateCategoryName { get; set; }
        public string AlternatePartnerHierarchyCode { get; set; }
    }
}