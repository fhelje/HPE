using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.lib.HPE.Model;
using HPeSimpleParser.lib.Parser.State;

namespace HPeSimpleParser.lib.Parser {
    public static class InnerStateParsers {
        public static async Task LinkInnerParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element && state.InnerState == InnerState.Option) {
                switch (state.CurrentName) {
                    case Item.Links.Link.MarketingCategory:
                        state.Option.OptionGroupCode = reader.GetAttribute("pmoid");
                        break;
                }

                return;
            }

            if (state.NodeType == XmlNodeType.Text && state.InnerState == InnerState.Option) {
                string text;
                switch (state.CurrentName) {
                    case Item.Links.Link.MarketingCategory:
                        text = await reader.GetValueAsync();
                        state.Option.OptionGroupName = text;
                        break;
                    case Item.Links.Link.Num:
                        text = await reader.GetValueAsync();
                        state.Option.OptionPartnerPartNumber = text;
                        break;

                }
            }
        }

        public static async Task KeySellingPointsInnerParser(ParseState state, XmlReader reader) {
            if (reader.NodeType != XmlNodeType.Text) {
                return;
            }
            var text = await reader.GetValueAsync();

            var part = state.CurrentName.Split("_");

            if (!state.MarketingText.ContainsKey(part[1])) {
                state.MarketingText[part[1]] = new Section();
            }

            if (part[2] == "headline") {
                state.MarketingText[part[1]].Header = text;
            }
            else {
                state.MarketingText[part[1]].AddParagraph(text, part[3]);
            }
        }

        public static async Task TechnicalSpecificationsInnerParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element) {
                state.Label = reader.GetAttribute("label");
                return;
            }
            if (state.NodeType != XmlNodeType.Text) {
                return;
            }
            var text = await reader.GetValueAsync();
            state.Specifications.Add(new SpecificationState { Name = state.CurrentName, Value = text, Label = state.Label });
        }
        public static async Task TechnicalSpecificationsInnerParserInc(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element) {
                state.Label = reader.GetAttribute("label");
                return;
            }
            if (state.NodeType != XmlNodeType.Text) {
                return;
            }
            var text = await reader.GetValueAsync();
            if (state.CurrentName.EndsWith("ftntnbr") 
                || state.CurrentName.StartsWith("tsfootnote")
                || state.CurrentName.StartsWith("servicefeaturestandard")
                || state.CurrentName.StartsWith("filter_")) {
                return;
            }
            state.Specifications.Add(new SpecificationState { Name = state.CurrentName, Value = text, Label = state.Label });
        }
        public static Task TechnicalSpecificationsInnerParserSkipLevel(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element) {
                if (reader.Name == "footnotes") {
                    return Task.CompletedTask;
                }
                state.InnerState = InnerState.TechnicalSpecificationsLevel1;
                return Task.CompletedTask;
            }

            if (state.NodeType == XmlNodeType.EndElement) {
                state.InnerState = InnerState.TechnicalSpecifications;
            }
            return Task.CompletedTask;
        }
    }
}