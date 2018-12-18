namespace HPeSimpleParser.HPE.Model {
    public class Marketing {

        public Marketing()
        {
            MarketingCode =string.Empty;
        }
        public string MarketingText { get; set; }

        public string MarketingCode { get; set; }

        public string Url { get; set; }

        public string ChangeCode { get; set; }
    }
}