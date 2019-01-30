using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public class MarketingBuilder {
        private string _marketingText = "MarketingText";
        private string _marketingCode = "MarketingCode";
        private string _url = "Url";
        private string _changeCode = "ChangeCode";
        private MarketingBuilder() {
        }
        public static MarketingBuilder With() {
            return new MarketingBuilder();
        }
        public Marketing Build() {
            return new Marketing(_marketingText, _marketingCode, _url, _changeCode);
        }

        public MarketingBuilder Default() {
            return this;
        }
    }
}