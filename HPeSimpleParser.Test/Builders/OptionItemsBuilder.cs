using System.Collections.Generic;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public sealed class OptionItemsBuilder {
        private readonly List<Option> _options = new List<Option>();

        private OptionItemsBuilder() {
        }

        public static OptionItemsBuilder With() {
            return new OptionItemsBuilder();
        }

        public IReadOnlyList<Option> Build() {
            return _options;
        }

        public OptionItemsBuilder AddDefault() {
            _options.Add(
                new Option("ManufacturerCode", "OptionPartnerPartNumber", "OptionGroupCode", "OptionGroupName"));
            return this;
        }
    }
}