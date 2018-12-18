using System;

namespace HPeSimpleParser.lib.HPE.Model
{
    public class Detail
    {
        public int ProductPartnerID { get; set; }
        public decimal? Weight { get; set; }
        public decimal? WeightwithPackage { get; set; }
        public decimal? Volume { get; set; }
        public decimal? PalletSize { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Depth { get; set; }
        public int? PackQty { get; set; }
        public decimal? MinimumOrderQty { get; set; }
        public bool? IsRequireSerialNumber { get; set; }
        public string ManufacturingCountry { get; set; }
        public string CustomsStatisticsNumber { get; set; }
        public bool? ExtendedWarranty { get; set; }
        public int? Unspsc { get; set; }
        public DateTime? EndOfSupport { get; set; }
        public string ErpAltPartNumber { get; set; }
        public bool? TeleSalesFlag { get; set; }
        public string ItemDefFulfillSource { get; set; }
        public bool? MeterEnabled { get; set; }
        public decimal? SwedishChemicalTaxReduction { get; set; }
        public int? WarrantyTime { get; set; }
    }
}
