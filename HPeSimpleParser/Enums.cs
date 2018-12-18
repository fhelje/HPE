namespace HPeSimpleParser {
    public enum ProductColumnV1 {
        PartnerPartNumber,  // \item[num] inclusive # XXX
        PartNumber,         // \item[num] inclusive # XXX
        ManufacturerName,   // HPE
        ManufacturerCode,   // HPE
        CategoryID,         // lövnode i category
        CategoryName,       // lövnode i category
        Description,        // \item\name
        DescriptionLong,    // \item\content\features\technicalspecificationssku\prodnameshort
        ProductCode,        // \
        IsEol,              // HUR?
        ChangeDate          // \item[lastupdatedate]
    }

    public enum PureHierarchyColumnV1 {
        // Hur länkar man ihop denna?
        PartnerPartNumber,  // \item[num] inclusive # XXX
        CategoryID,         //
        CategoryName,       //
        ParentCategoryID,   //
        Level               //
    }

    public enum LinkColumnV1 {
        PartnerPartNumber,  // \item[num] inclusive # XXXType
        PdfLinkDataSheet,   // \item\content\tech_specs\quickspeclinks\info_quickspec_doc_ww 
        PdfLinkManual,      //
        ImageLinks          // \item\images\image lista ut vilka bilder vi ska använda
    }

    public enum MarketingColumnV1 {
        PartnerPartnumber,  // \item[num] inclusive # XXX
        MarketingCode,      // LEAVE EMPTY
        MarketingText,      // \item\content\features\keysellingpoint (Parse node below)
        LanguageId          // \item[culturecode]
    }

    public enum OptionColumnV1 {
        PartnerPartNumber,  // \item[num] inclusive # XXX
        Options             // \
    }

    public enum SpecificationColumnV1 {
        PartnerPartNumber,  // \item[num] inclusive # XXX
        Specifications      //
    }

    public enum ManufacturerColumnV1 {
        SupplierID,         // \item[num] inclusive # XXX
        SupplierName        //
    }
}
