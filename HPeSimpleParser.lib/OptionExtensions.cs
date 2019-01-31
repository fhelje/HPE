using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public static class OptionExtensions {
        public static Item SetPartnerPartNumber(this Item item, string partNumber, string optionCode) {
            var ppn = string.IsNullOrEmpty(optionCode) ? partNumber : $"{partNumber}#{optionCode}";
            item.PartnerPartNumber = ppn;
            item.Options.PartnerPartNumber = ppn;
            item.Detail.PartnerPartNumber = ppn;
            item.Hierarchies.PartnerPartNumber = ppn;
            item.Link.PartnerPartNumber = ppn;
            item.Marketing.PartnerPartNumber = ppn;
            item.Product.PartnerPartNumber = ppn;
            item.Product.PartNumber = ppn;
            item.Specifications.PartnerPartNumber = ppn;
            item.Supplier.PartnerPartNumber = ppn;
            return item;
        }
    }
}