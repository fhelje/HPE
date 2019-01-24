using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
    public interface IParserDefinition {
        Dictionary<InnerState, Func<ParseState, XmlReader, Task>> StateParser { get; }
        Dictionary<string, Func<ParseState, XmlReader, Task>> ParserFunctions { get; }
    }

    public class HPEParserDefinition : IParserDefinition {
        public Dictionary<InnerState, Func<ParseState, XmlReader, Task>> StateParser => new Dictionary<InnerState, Func<ParseState, XmlReader, Task>> {
            {InnerState.TechnicalSpecifications, InnerStateParsers.TechnicalSpecificationsInnerParser},
            {InnerState.KeySellingPoints, InnerStateParsers.KeySellingPointsInnerParser},
            {InnerState.Option, InnerStateParsers.LinkInnerParser},
        };
        public Dictionary<string, Func<ParseState, XmlReader, Task>> ParserFunctions => new Dictionary<string, Func<ParseState, XmlReader, Task>> {
            // ReSharper disable StringLiteralTypo
            {"item", Parser.ParserFunctions.ItemParser},
            {"carepackregistrationflag", Parser.ParserFunctions.CarePackRegistrationParser},
            {"info_quickspec_doc_ww", Parser.ParserFunctions.QuickSpecParser},
            {"unspsc", Parser.ParserFunctions.UnspscParser},
            {"technicalspecifications", Parser.ParserFunctions.TechnicalSpecificationsParser},
            {"prodnameshort", Parser.ParserFunctions.ProdNameShortParser},
            {"keysellingpoints", Parser.ParserFunctions.KeySellingPointsParser},
            {"hierarchy", Parser.ParserFunctions.HierarchyRootParser },
            {"product_type", Parser.ParserFunctions.BranchParser },
            {"marketing_category", Parser.ParserFunctions.BranchParser },
            {"marketing_sub_category", Parser.ParserFunctions.BranchParser },
            {"small_series", Parser.ParserFunctions.HierarchyParser},
            {"big_series", Parser.ParserFunctions.ParentHierarchyParser},
            {"link", Parser.ParserFunctions.LinkParser},
            {"upc", Parser.ParserFunctions.UpcParser},
            {"content_data", Parser.ParserFunctions.UpcInnerParser},
            {"description", Parser.ParserFunctions.UpcInnerParser},
            {"opt", Parser.ParserFunctions.UpcInnerParser},
            {"image", Parser.ParserFunctions.ImageParser},
            {"cmg_acronym", Parser.ParserFunctions.ImageInnerParser},
            {"master_object_name", Parser.ParserFunctions.ImageInnerParser},
            {"content_type", Parser.ParserFunctions.ImageInnerParser},
            {"image_url_https", Parser.ParserFunctions.ImageInnerParser},
            {"pixel_height", Parser.ParserFunctions.ImageInnerParser},
            {"file_name", Parser.ParserFunctions.ImageInnerParser},
            {"language_code", Parser.ParserFunctions.ImageInnerParser},
            {"orientation", Parser.ParserFunctions.ImageInnerParser},
            {"pixel_width", Parser.ParserFunctions.ImageInnerParser},
            {"action", Parser.ParserFunctions.ImageInnerParser},
            {"image_url_http", Parser.ParserFunctions.ImageInnerParser},
            {"search_keyword", Parser.ParserFunctions.ImageInnerParser},
            {"background", Parser.ParserFunctions.ImageInnerParser},
            {"full_title", Parser.ParserFunctions.ImageInnerParser},
            {"document_type_detail", Parser.ParserFunctions.ImageInnerParser},
            {"document_type", Parser.ParserFunctions.ImageInnerParser},
            {"dpi_resolution", Parser.ParserFunctions.ImageInnerParser},
            // ReSharper restore StringLiteralTypo

        };
    }
}