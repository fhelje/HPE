namespace FSSystem.ContentAdapter.HPEAndHPInc.Enums {
    public enum ProductColumnV1 {
        PartnerPartNumber, // \item[num] inclusive # XXX
        PartNumber, // \item[num] inclusive # XXX
        ManufacturerName, // HPE
        ManufacturerCode, // HPE
        CategoryID, // lövnode i category
        CategoryName, // lövnode i category
        Description, // \item\name
        DescriptionLong, // \item\content\features\technicalspecificationssku\prodnameshort
        ProductCode, // \
        IsEol, // HUR?
        ChangeDate // \item[lastupdatedate]
    }
}