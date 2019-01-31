namespace FSSystem.ContentAdapter.Model {
    public class Marketing {
        public Marketing() {
            MarketingType = MarketingType.UniqueSellingPoint;
        }

        public string PartnerPartNumber { get; set; }

        public string MarketingCode { get; set; }

        public string MarketingText { get; set; }

        public string MarketingTitle { get; set; }

        public MarketingType MarketingType { get; set; }

        public string LanguageId { get; set; }
    }
}