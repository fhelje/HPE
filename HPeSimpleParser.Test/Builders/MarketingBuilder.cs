using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public sealed class MarketingBuilder {
        private const string ChangeCode = "ChangeCode";
        private const string MarketingCode = "MarketingCode";
        private const string MarketingText = "MarketingText";
        private const string Url = "Url";

        private MarketingBuilder() {
        }

        public static MarketingBuilder With() {
            return new MarketingBuilder();
        }

        public Marketing Build() {
            return new Marketing(MarketingText, MarketingCode, Url, ChangeCode);
        }

        public MarketingBuilder Default() {
            return this;
        }
    }
}