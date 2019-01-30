using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser.State;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser {
    public class HPEParserDefinition : IParserDefinition {
        private static Dictionary<InnerState, Func<ParseState, XmlReader, Task>> _stateParser =
            new Dictionary<InnerState, Func<ParseState, XmlReader, Task>> {
                {InnerState.TechnicalSpecifications, InnerStateParsers.TechnicalSpecificationsInnerParser},
                {InnerState.KeySellingPoints, InnerStateParsers.KeySellingPointsInnerParser},
                {InnerState.Option, InnerStateParsers.LinkInnerParser},
            };
        private static Dictionary<string, Func<ParseState, XmlReader, Task>> _parserFunctions =
            new Dictionary<string, Func<ParseState, XmlReader, Task>> {
                // ReSharper disable StringLiteralTypo
                {"item", Parser.HPEParserFunctions.ItemParser},
                {"carepackregistrationflag", Parser.HPEParserFunctions.CarePackRegistrationParser},
                {"info_quickspec_doc_ww", Parser.HPEParserFunctions.QuickSpecParser},
                {"unspsc", Parser.HPEParserFunctions.UnspscParser},
                {"technicalspecifications", Parser.HPEParserFunctions.TechnicalSpecificationsParser},
                {"prodnameshort", Parser.HPEParserFunctions.ProdNameShortParser},
                {"keysellingpoints", Parser.HPEParserFunctions.KeySellingPointsParser},
                {"hierarchy", Parser.HPEParserFunctions.HierarchyRootParser},
                {"product_type", Parser.HPEParserFunctions.BranchParser},
                {"marketing_category", Parser.HPEParserFunctions.BranchParser},
                {"marketing_sub_category", Parser.HPEParserFunctions.BranchParser},
                {"small_series", Parser.HPEParserFunctions.HierarchyParser},
                {"big_series", Parser.HPEParserFunctions.ParentHierarchyParser},
                {"link", Parser.HPEParserFunctions.LinkParser},
                {"upc", Parser.HPEParserFunctions.UpcParser},
                {"content_data", Parser.HPEParserFunctions.UpcInnerParser},
                {"description", Parser.HPEParserFunctions.UpcInnerParser},
                {"opt", Parser.HPEParserFunctions.UpcInnerParser},
                {"image", Parser.HPEParserFunctions.ImageParser},
                {"cmg_acronym", Parser.HPEParserFunctions.ImageInnerParser},
                {"master_object_name", Parser.HPEParserFunctions.ImageInnerParser},
                {"content_type", Parser.HPEParserFunctions.ImageInnerParser},
                {"image_url_https", Parser.HPEParserFunctions.ImageInnerParser},
                {"pixel_height", Parser.HPEParserFunctions.ImageInnerParser},
                {"file_name", Parser.HPEParserFunctions.ImageInnerParser},
                {"language_code", Parser.HPEParserFunctions.ImageInnerParser},
                {"orientation", Parser.HPEParserFunctions.ImageInnerParser},
                {"pixel_width", Parser.HPEParserFunctions.ImageInnerParser},
                {"action", Parser.HPEParserFunctions.ImageInnerParser},
                {"image_url_http", Parser.HPEParserFunctions.ImageInnerParser},
                {"search_keyword", Parser.HPEParserFunctions.ImageInnerParser},
                {"background", Parser.HPEParserFunctions.ImageInnerParser},
                {"full_title", Parser.HPEParserFunctions.ImageInnerParser},
                {"document_type_detail", Parser.HPEParserFunctions.ImageInnerParser},
                {"document_type", Parser.HPEParserFunctions.ImageInnerParser},
                {"dpi_resolution", Parser.HPEParserFunctions.ImageInnerParser},
                // ReSharper restore StringLiteralTypo

            };
        public Dictionary<InnerState, Func<ParseState, XmlReader, Task>> StateParser => _stateParser;
        public Dictionary<string, Func<ParseState, XmlReader, Task>> ParserFunctions => _parserFunctions;
    }

    public class HPIncParserDefinition : IParserDefinition {
         private static Dictionary<InnerState, Func<ParseState, XmlReader, Task>> _stateParser = new Dictionary<InnerState, Func<ParseState, XmlReader, Task>> {
            {InnerState.TechnicalSpecifications, InnerStateParsers.TechnicalSpecificationsInnerParserSkipLevel},
            {InnerState.TechnicalSpecificationsLevel1, InnerStateParsers.TechnicalSpecificationsInnerParserInc},
            {InnerState.KeySellingPoints, InnerStateParsers.KeySellingPointsInnerParser},
            {InnerState.Option, InnerStateParsers.LinkInnerParser},
        };

        private static Dictionary<string, Func<ParseState, XmlReader, Task>> _parserFunctions = new Dictionary<string, Func<ParseState, XmlReader, Task>> {
            // ReSharper disable StringLiteralTypo
            {"item", Parser.HPEParserFunctions.ItemParser},
            {"prodnum", HPEParserFunctions.PartNumber},
            {"carepackregistrationflag", Parser.HPEParserFunctions.CarePackRegistrationParser},
            {"info_quickspec_doc_ww", HPEParserFunctions.QuickSpecParser},
            {"unspsc", HPEParserFunctions.UnspscParser},
            {"tech_specs", HPEParserFunctions.TechnicalSpecificationsParser},
            {"prodnameshort", HPEParserFunctions.ProdNameShortParser},
            {"key_selling_points", HPEParserFunctions.KeySellingPointsParser},
            {"hierarchy", HPEParserFunctions.HierarchyRootParser },
            {"product_type", HPEParserFunctions.BranchParser },
            {"marketing_category", HPEParserFunctions.BranchParser },
            {"marketing_sub_category", HPEParserFunctions.BranchParser },
            {"small_series", HPEParserFunctions.HierarchyParser},
            {"big_series", HPEParserFunctions.ParentHierarchyParser},
            {"link", Parser.HPEParserFunctions.LinkParser},
            {"upc", HPEParserFunctions.UpcParser},
            {"content_data", HPEParserFunctions.UpcInnerParser},
            {"description", HPEParserFunctions.UpcInnerParser},
            {"opt", HPEParserFunctions.UpcInnerParser},
            {"image", HPEParserFunctions.ImageParser},
            {"cmg_acronym", HPEParserFunctions.ImageInnerParser},
            {"master_object_name", HPEParserFunctions.ImageInnerParser},
            {"content_type", HPEParserFunctions.ImageInnerParser},
            {"image_url_https", HPEParserFunctions.ImageInnerParser},
            {"pixel_height", HPEParserFunctions.ImageInnerParser},
            {"file_name", HPEParserFunctions.ImageInnerParser},
            {"language_code", HPEParserFunctions.ImageInnerParser},
            {"orientation", HPEParserFunctions.ImageInnerParser},
            {"pixel_width", HPEParserFunctions.ImageInnerParser},
            {"action", HPEParserFunctions.ImageInnerParser},
            {"image_url_http", HPEParserFunctions.ImageInnerParser},
            {"search_keyword", HPEParserFunctions.ImageInnerParser},
            {"background", HPEParserFunctions.ImageInnerParser},
            {"full_title", HPEParserFunctions.ImageInnerParser},
            {"document_type_detail", HPEParserFunctions.ImageInnerParser},
            {"document_type", HPEParserFunctions.ImageInnerParser},
            {"dpi_resolution", HPEParserFunctions.ImageInnerParser},
            // ReSharper restore StringLiteralTypo

        };
        public Dictionary<InnerState, Func<ParseState, XmlReader, Task>> StateParser => _stateParser;
        public Dictionary<string, Func<ParseState, XmlReader, Task>> ParserFunctions => _parserFunctions;
    }
}