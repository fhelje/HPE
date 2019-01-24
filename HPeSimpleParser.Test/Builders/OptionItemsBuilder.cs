using System.Collections.Generic;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public class OptionItemsBuilder {
        private List<Option> _options = new List<Option>();

        private OptionItemsBuilder() {
        }
        public static OptionItemsBuilder With() {
            return new OptionItemsBuilder();
        }
        public IReadOnlyList<Option> Build() {
            return _options;
        }

        public OptionItemsBuilder AddDefault() {
            _options.Add(new HPE.Model.Option ("ManufacturerCode","OptionPartnerPartNumber","OptionGroupCode","OptionGroupName"));
            return this;
        }
    }
}