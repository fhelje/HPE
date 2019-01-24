namespace HPeSimpleParser.HPE.Model {
    public class Marketing {

        public Marketing(string marketingText, string marketingCode, string url, string changeCode) {
            MarketingText = marketingText;
            MarketingCode = marketingCode;
            Url = url;
            ChangeCode = changeCode;
        }

        public string MarketingText { get; }

        public string MarketingCode { get; }

        public string Url { get; }

        public string ChangeCode { get; }
    }
}