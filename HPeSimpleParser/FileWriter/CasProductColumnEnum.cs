namespace HPeSimpleParser
{
    public enum CasProductColumnEnum
    {
        PartnerPartNumber = 0,
        PartNumber = 1,
        ManufacturerName = 2,
        ManufacturerCode = 3,
        CategoryID = 4,
        CategoryName = 5,
        Description = 6,
        DescriptionLong = 7,
        ProductCode = 8,
        IsEol = 9,
        ChangeDate = 10,
    }

    public enum CasDetailColumnEnum
    {
        PartnerPartNumber = 0,
        Weight = 1,
        WeightwithPackage = 2,
        Volume = 3,
        PalletSize = 4,
        Width = 5,
        Height = 6,
        Depth = 7,
        PackQty = 8,
        MinimumOrderQty = 9,
        IsRequireSerialNumber = 10,
        ManufacturingCountry = 11,
        CustomsStatisticsNumber = 12,
        ExtendedWarranty = 13,
        Unspsc = 14,
        EndOfSupport = 15,
        ErpAltPartNumber = 16,
        TeleSalesFlag = 17,
        ItemDefFulfillSource = 18,
        MeterEnabled = 19,
        SwedishChemicalTaxReduction = 20,
        WarrantyTime = 21,
    }

    public enum CasLinkColumnEnum
    {
        PartnerPartNumber = 0,
        PdfLinkDataSheet = 1,
        PdfLinkManual = 2,
        Images = 3,
    }

    public enum CasImageColumnEnum
    {
        Url = 0,
        Height = 1,
        Width = 2,
        ContentType = 3,
        Title = 4,
    }

    public enum CasMarketingColumnEnum
    {
        PartnerPartNumber = 0,
        MarketingCode = 1,
        MarketingText = 2,
        LanguageId = 3,
    }

    public enum CasSupplierColumnEnum
    {
        PartnerPartNumber = 0,
        SupplierId = 1,
        SupplierName = 2,
    }

    public enum CasOptionsColumnEnum
    {
        PartnerPartNumber = 0,
        Items = 1,
    }

    public enum CasOptionColumnEnum
    {
        PartNumber = 0,
        Name = 1,
        GroupId = 2,
        GroupName = 3,
    }

    public enum CasSpecificationsColumnEnum
    {
        PartnerPartNumber = 0,
        Items = 1,
    }

    public enum CasSpecificationColumnEnum
    {
        Type = 0,
        Name = 1,
        Value = 2,
        UnitOfMeasure = 3,
        Id = 4,
        GroupId = 5,
        GroupName = 6
    }
}