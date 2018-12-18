namespace HPeSimpleParter.Api.Model {
    public class Option {
        // HPE
        public string ManufacturerCode { get; set; }
        // \item\links\link\num
        public string OptionPartnerPartNumber { get; set; }
        // \item\links\link\marketing_category[5322802]
        public string OptionGroupCode { get; set; }
        // \item\links\link\marketing_category
        public string OptionGroupName { get; set; }
    }
}