using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.HPE.Model;
using HPeSimpleParser.Parser.HPE;

namespace HPeSimpleParser.Parser {
    public static class ParserFunctions {
        public static async Task CarePackRegistrationParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Text) {
                var text = await reader.GetValueAsync();
                state.Product.CarePackRegistration = text == "Yes";
            }
        }

        public static Task LinkParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element && reader.GetAttribute("type") == "child" && !reader.IsEmptyElement) {
                state.InnerState = InnerState.Option;
                state.Option = new OptionState { ManufacturerCode = "HPE" };
            }

            if (state.NodeType == XmlNodeType.EndElement && state.InnerState == InnerState.Option) {
                state.InnerState = InnerState.None;
                state.Options.Add(state.Option);
                state.Option = null;
            }

            return Task.CompletedTask;

        }

        public static Task ParentHierarchyParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element && state.InnerState == InnerState.Hierarchy) {
                // TODO: Save parent id
                // ReSharper disable once StringLiteralTypo
                var id = reader.GetAttribute("pmoid");
                var name = reader.GetAttribute("name");
                state.ParentHierarchy = id;
                state.Branch.BigSeries = (name, id);
            }

            return Task.CompletedTask;
        }

        public static Task HierarchyParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element && state.InnerState == InnerState.Hierarchy) {
                // TODO: Save parent id
                // ReSharper disable once StringLiteralTypo
                var id = reader.GetAttribute("pmoid");
                var name = reader.GetAttribute("name");
                state.Hierarchy.Add(new Hierarchy("HPE", id, name, state.ParentHierarchy, "HPE"));
                state.Branch.SmallSeries = (name, id);
                if (string.IsNullOrWhiteSpace(state.Branch.ProductType.Name)) {
                    Console.Write("x");
                }
            }

            return Task.CompletedTask;
        }

        public static Task ImageParser(ParseState state, XmlReader reader) {

            if (state.NodeType == XmlNodeType.Element && !reader.IsEmptyElement) {
                state.InnerState = InnerState.Image;
                state.Image = new ImageTemp();
            }

            if (state.NodeType == XmlNodeType.EndElement) {
                var ip = new HPEImageParser();
                var image = new Image(
                    state.Image.GroupingKey1,
                    state.Image.GroupingKey2, 
                    state.Image.ContentType, 
                    state.Image.PixelHeight, 
                    state.Image.Orientation, 
                    state.Image.PixelWidth, 
                    state.Image.ImageUrlHttp, 
                    state.Image.TypeDetail, 
                    state.Image.FullTitle,
                    ip.ParseContentTypePriority(state.Image.ContentType),
                    ip.GetSizeCategory(state.Image.PixelHeight, state.Image.PixelWidth),
                    ip.ParseTypeDetail(state.Image.TypeDetail),
                    ip.ParseIntWithDefault(state.Image.PixelHeight),
                    ip.ParseIntWithDefault(state.Image.PixelWidth)
                    );
                state.Links.ImageLinks.Add(image);
                state.Image = null;
                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;
        }

        public static async Task ImageInnerParser(ParseState state, XmlReader reader) {

            if (state.NodeType == XmlNodeType.Text && state.InnerState == InnerState.Image) {
                var text = await reader.GetValueAsync();
                switch (state.CurrentName) {
                    case "cmg_acronym":
                        state.Image.GroupingKey1 = text;
                        break;
                    case "master_object_name":
                        state.Image.GroupingKey2 = text;
                        break;
                    case "content_type":
                        state.Image.ContentType = text;
                        break;
                    case "pixel_height":
                        state.Image.PixelHeight = text;
                        break;
                    case "orientation":
                        state.Image.Orientation = text;
                        break;
                    case "pixel_width":
                        state.Image.PixelWidth = text;
                        break;
                    case "image_url_http":
                        state.Image.ImageUrlHttp = text;
                        break;
                    case "full_title":
                        state.Image.FullTitle = text;
                        break;
                    case "document_type_detail":
                        state.Image.TypeDetail = text;
                        break;
                }
            }
        }

        public static Task UpcParser(ParseState state, XmlReader reader) {

            if (state.NodeType == XmlNodeType.Element && !reader.IsEmptyElement) {
                state.InnerState = InnerState.Upc;
            }

            if (state.NodeType == XmlNodeType.EndElement) {

                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;
        }

        public static async Task UpcInnerParser(ParseState state, XmlReader reader) {

            if (state.NodeType == XmlNodeType.Element && state.InnerState == InnerState.Upc) {
                switch (state.CurrentName) {
                    case "content_data":
                        state.ProductVariant = new ProductVariantState();
                        break;
                    case "opt":
                        if (reader.IsEmptyElement) {
                            var variant = state.ProductVariant;
                            state.ProductVariants.Add(variant);
                            state.ProductVariant = null;
                        }
                        break;
                }
            }

            if (state.NodeType == XmlNodeType.EndElement
                && state.InnerState == InnerState.Upc
                && state.CurrentName == "opt") {
                var variant = state.ProductVariant;
                state.ProductVariants.Add(variant);
                state.ProductVariant = null;
            }

            if (state.NodeType == XmlNodeType.Text && state.InnerState == InnerState.Upc) {
                var text = await reader.GetValueAsync();
                if (state.ProductVariant != null) {
                    switch (state.CurrentName) {
                        case "content_data":
                            state.ProductVariant.UpcCode = text.Replace(" ", "").Replace("-", "");
                            break;
                        case "description":
                            state.ProductVariant.Description = text;
                            break;
                        case "opt":
                            state.ProductVariant.Opt = text;
                            break;
                    }
                }
                else {
                    Console.WriteLine($"Error in file {state.File} with current node {state.CurrentName} {state.InnerState.ToString()}");
                }
            }
        }

        public static Task KeySellingPointsParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element && !reader.IsEmptyElement) {
                state.InnerState = InnerState.KeySellingPoints;
                state.MarketingText = new Dictionary<string, Section>();
                state.Marketing = new MarketingState();
            }

            if (state.NodeType == XmlNodeType.EndElement) {

                if (state.MarketingText != null) state.Marketing.MarketingText = string.Join(" ", state.MarketingText.OrderBy(x => x.Key).Select(x => x.Value.ToString()));
                state.MarketingText = null;
                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;
        }

        public static async Task ProdNameShortParser(ParseState state, XmlReader reader) {
            if (state.NodeType != XmlNodeType.Text) {
                return;
            }
            var text = await reader.GetValueAsync();
            state.Product.DescriptionLong = text;
        }

        public static Task TechnicalSpecificationsParser(ParseState state, XmlReader reader) {

            if (state.NodeType == XmlNodeType.Element) {
                state.InnerState = InnerState.TechnicalSpecifications;
            }

            if (state.NodeType == XmlNodeType.EndElement) {
                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;
        }

        public static async Task UnspscParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Text) {
                var text = await reader.GetValueAsync();
                if (int.TryParse(text.Replace(" ", "").Replace("-", ""), out var unspsc)) {
                    state.Product.Unspsc = unspsc;
                }
            }
        }

        public static async Task QuickSpecParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Text) {
                var text = await reader.GetValueAsync();
                var url = GetPdfUrl(text);
                state.Links.PdfLinkDataSheet = url;
            }
        }

        public static string GetPdfUrl(string url) {
            if (!url.StartsWith("https://www")) {
                return url;                
            }

            var parts = url.Split('/');
            var code = parts[3];
            var idPart = parts.LastOrDefault()
                             ?.Split('?')
                             ?.LastOrDefault()
                             ?.Split('&')
                             ?.FirstOrDefault(x => x.StartsWith("docname")) 
                            ?? string.Empty;
            if (idPart == string.Empty) {
                return url;
            }
            var idSubParts = idPart.Split('=');
            if (idSubParts.Length != 2) {
                return url;
            }
            return $"https://{code}.www2.hpe.com/v2/getpdf.aspx/{idSubParts.Last()}.pdf";
        }

        public static Task ItemParser(ParseState state, XmlReader reader) {
            if (state.NodeType != XmlNodeType.Element) {
                return Task.CompletedTask;
            }
            // ReSharper disable StringLiteralTypo
            state.Product.ManufacturerCode = "HPE";
            state.Product.ManufacturerName = "HPE";
            state.LanguageId = reader.GetAttribute("culturecode");
            state.PartNumber = reader.GetAttribute("number");
            state.Product.Description = reader.GetAttribute("name");
            if (DateTime.TryParse(reader.GetAttribute("lastupdatedate"), out var changeDate)) {
                state.Product.ChangeDate = changeDate;
            }
            state.PartnerPartNumber = state.PartNumber;
            var productLine = reader.GetAttribute("productline");
            state.Hierarchy.Add(new Hierarchy("PL", productLine, productLine, null, "PL"));
            // ReSharper restore StringLiteralTypo
            return Task.CompletedTask;
        }

        public static Task BranchParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element && state.InnerState == InnerState.Hierarchy) {

                var tuple = (Name: reader.GetAttribute("name"), Item: reader.GetAttribute("pmoid"));
                if (string.IsNullOrEmpty(tuple.Name)) {
                    Console.Write("x");
                }
                switch (reader.Name) {
                    case "product_type":
                        state.Branch.ProductType = tuple;
                        break;
                    case "marketing_category":
                        state.Branch.MarketingCategory = tuple;
                        break;
                    case "marketing_sub_category":
                        state.Branch.MarketingSubCategory = tuple;
                        break;
                }
            }

            return Task.CompletedTask;
        }

        public static Task HierarchyRootParser(ParseState state, XmlReader reader) {
            if (state.NodeType == XmlNodeType.Element) {
                state.InnerState = InnerState.Hierarchy;
            }

            if (state.NodeType == XmlNodeType.EndElement) {
                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;


        }
    }

    public class ImageTemp  {
        public string GroupingKey1 { get; set; }
        public string GroupingKey2 { get; set; }
        public string ContentType { get; set; }
        public string PixelHeight { get; set; }
        public string Orientation { get; set; }
        public string PixelWidth { get; set; }
        public string ImageUrlHttp { get; set; }
        public string FullTitle { get; set; }
        public string TypeDetail { get; set; }
    }
}