using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
    internal class NodeParser {
        private readonly Dictionary<string, Func<ParseState, XmlReader, ProductRoot, Task>> _nodeParser = new Dictionary<string, Func<ParseState, XmlReader, ProductRoot, Task>> {
            // ReSharper disable StringLiteralTypo
            {"item", ParserFunctions.ItemParser},
            {"carepackregistrationflag", ParserFunctions.CarePackRegistrationParser},
            {"info_quickspec_doc_ww", ParserFunctions.QuickSpecParser},
            {"unspsc", ParserFunctions.UnspscParser},
            {"technicalspecifications", ParserFunctions.TechnicalSpecificationsParser},
            {"prodnameshort", ParserFunctions.ProdNameShortParser},
            {"keysellingpoints", ParserFunctions.KeySellingPointsParser},
            {"hierarchy", ParserFunctions.HierarchyRootParser },
            {"product_type", ParserFunctions.BranchParser },
            {"marketing_category", ParserFunctions.BranchParser },
            {"marketing_sub_category", ParserFunctions.BranchParser },
            {"small_series", ParserFunctions.HierarchyParser},
            {"big_series", ParserFunctions.ParentHierarchyParser},
            {"link", ParserFunctions.LinkParser},
            {"upc", ParserFunctions.UpcParser},
            {"content_data", ParserFunctions.UpcInnerParser},
            {"description", ParserFunctions.UpcInnerParser},
            {"opt", ParserFunctions.UpcInnerParser},
            {"image", ParserFunctions.ImageParser},
            {"cmg_acronym", ParserFunctions.ImageInnerParser},
            {"master_object_name", ParserFunctions.ImageInnerParser},
            {"content_type", ParserFunctions.ImageInnerParser},
            {"image_url_https", ParserFunctions.ImageInnerParser},
            {"pixel_height", ParserFunctions.ImageInnerParser},
            {"file_name", ParserFunctions.ImageInnerParser},
            {"language_code", ParserFunctions.ImageInnerParser},
            {"orientation", ParserFunctions.ImageInnerParser},
            {"pixel_width", ParserFunctions.ImageInnerParser},
            {"action", ParserFunctions.ImageInnerParser},
            {"image_url_http", ParserFunctions.ImageInnerParser},
            {"search_keyword", ParserFunctions.ImageInnerParser},
            {"background", ParserFunctions.ImageInnerParser},
            {"full_title", ParserFunctions.ImageInnerParser},
            {"document_type_detail", ParserFunctions.ImageInnerParser},
            {"document_type", ParserFunctions.ImageInnerParser},
            {"dpi_resolution", ParserFunctions.ImageInnerParser},
            // ReSharper restore StringLiteralTypo

        };

        private readonly Dictionary<InnerState, Func<ParseState, XmlReader, ProductRoot, Task>> _stateParser = new Dictionary<InnerState, Func<ParseState, XmlReader, ProductRoot, Task>> {
            {InnerState.TechnicalSpecifications, InnerStateParsers.TechnicalSpecificationsInnerParser},
            {InnerState.KeySellingPoints, InnerStateParsers.KeySellingPointsInnerParser},
            {InnerState.Option, InnerStateParsers.LinkInnerParser},
        };


        public async Task Read(ParseState state, XmlReader reader, ProductRoot item) {
            if (_nodeParser.ContainsKey(state.CurrentName)) {
                await _nodeParser[state.CurrentName](state, reader, item);
            }
            if (_stateParser.ContainsKey(state.InnerState)) {
                await _stateParser[state.InnerState](state, reader, item);

            }
        }
    }
}