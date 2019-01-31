using System;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public sealed class DetailBuilder {
        private readonly string _productPartnerID = "ProductPartnerID";
        private string _customsStatisticsNumber;
        private decimal? _depth;
        private DateTime? _endOfSupport;
        private string _erpAltPartNumber;
        private bool? _extendedWarranty;
        private decimal? _height;
        private bool? _isRequireSerialNumber;
        private string _itemDefFulfillSource;
        private string _manufacturingCountry;
        private bool? _meterEnabled;
        private decimal? _minimumOrderQty;
        private int? _packQty;
        private decimal? _palletSize;
        private decimal? _swedishChemicalTaxReduction;
        private bool? _teleSalesFlag;
        private int? _unspsc;
        private decimal? _volume;
        private int? _warrantyTime;
        private decimal? _weight;
        private decimal? _weightWithPackage;
        private decimal? _width;

        private DetailBuilder() {
            _minimumOrderQty = null;
            _manufacturingCountry = null;
        }

        public static DetailBuilder With() {
            return new DetailBuilder();
        }

        public Detail Build() {
            return new Detail(_productPartnerID,
                _weight,
                _weightWithPackage,
                _volume,
                _palletSize,
                _width,
                _height,
                _depth,
                _packQty,
                _minimumOrderQty,
                _isRequireSerialNumber,
                _manufacturingCountry,
                _customsStatisticsNumber,
                _extendedWarranty,
                _unspsc,
                _endOfSupport,
                _erpAltPartNumber,
                _teleSalesFlag,
                _itemDefFulfillSource,
                _meterEnabled,
                _swedishChemicalTaxReduction,
                _warrantyTime);
        }

        public DetailBuilder Default() {
            _weight = 1;
            _weightWithPackage = 2;
            _volume = 3;
            _palletSize = 4;
            _width = 5;
            _height = 6;
            _depth = 7;
            _packQty = 8;
            _minimumOrderQty = 9;
            _isRequireSerialNumber = true;
            _manufacturingCountry = "ManufacturingCountry";
            _customsStatisticsNumber = "CustomsStatisticsNumber";
            _extendedWarranty = true;
            _unspsc = 0;
            _endOfSupport = new DateTime(2000, 1, 1);
            _erpAltPartNumber = "ErpAltPartNumber";
            _teleSalesFlag = true;
            _itemDefFulfillSource = "ItemDefFulfillSource";
            _meterEnabled = true;
            _swedishChemicalTaxReduction = 11;
            _warrantyTime = 12;
            return this;
        }
    }
}