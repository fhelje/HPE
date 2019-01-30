using System;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model
{
    public class Detail
    {
        public Detail(string productPartnerID, decimal? weight = default, decimal? weightWithPackage = default, decimal? volume = default, decimal? palletSize = default, decimal? width = default, decimal? height = default, decimal? depth = default, int? packQty = default, decimal? minimumOrderQty = default, bool? isRequireSerialNumber = default, string manufacturingCountry = null, string customsStatisticsNumber = null, bool? extendedWarranty = default, int? unspsc = default, DateTime? endOfSupport = default, string erpAltPartNumber = null, bool? teleSalesFlag = default, string itemDefFulfillSource = null, bool? meterEnabled = default, decimal? swedishChemicalTaxReduction = default, int? warrantyTime = default) {
            ProductPartnerID = productPartnerID;
            Weight = weight;
            WeightWithPackage = weightWithPackage;
            Volume = volume;
            PalletSize = palletSize;
            Width = width;
            Height = height;
            Depth = depth;
            PackQty = packQty;
            MinimumOrderQty = minimumOrderQty;
            IsRequireSerialNumber = isRequireSerialNumber;
            ManufacturingCountry = manufacturingCountry;
            CustomsStatisticsNumber = customsStatisticsNumber;
            ExtendedWarranty = extendedWarranty;
            Unspsc = unspsc;
            EndOfSupport = endOfSupport;
            ErpAltPartNumber = erpAltPartNumber;
            TeleSalesFlag = teleSalesFlag;
            ItemDefFulfillSource = itemDefFulfillSource;
            MeterEnabled = meterEnabled;
            SwedishChemicalTaxReduction = swedishChemicalTaxReduction;
            WarrantyTime = warrantyTime;
        }

        public string ProductPartnerID { get; }
        public decimal? Weight { get; }
        public decimal? WeightWithPackage { get; }
        public decimal? Volume { get; }
        public decimal? PalletSize { get; }
        public decimal? Width { get; }
        public decimal? Height { get; }
        public decimal? Depth { get; }
        public int? PackQty { get; }
        public decimal? MinimumOrderQty { get; }
        public bool? IsRequireSerialNumber { get; }
        public string ManufacturingCountry { get; }
        public string CustomsStatisticsNumber { get; }
        public bool? ExtendedWarranty { get; }
        public int? Unspsc { get; }
        public DateTime? EndOfSupport { get; }
        public string ErpAltPartNumber { get; }
        public bool? TeleSalesFlag { get; }
        public string ItemDefFulfillSource { get; }
        public bool? MeterEnabled { get; }
        public decimal? SwedishChemicalTaxReduction { get; }
        public int? WarrantyTime { get; }
    }
}
