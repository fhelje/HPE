namespace FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model {
    public class Option {
        public Option(string manufacturerCode, string optionPartnerPartNumber, string optionGroupCode,
            string optionGroupName) {
            ManufacturerCode = manufacturerCode;
            OptionPartnerPartNumber = optionPartnerPartNumber;
            OptionGroupCode = optionGroupCode;
            OptionGroupName = optionGroupName;
        }

        public string ManufacturerCode { get; }
        public string OptionPartnerPartNumber { get; }
        public string OptionGroupCode { get; }
        public string OptionGroupName { get; }
    }
}