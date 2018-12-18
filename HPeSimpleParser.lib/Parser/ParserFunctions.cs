using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.lib.HPE.Model;

namespace HPeSimpleParser.lib.Parser
{
    public static class ParserFunctions
    {
        public static async Task CarePackRegistrationParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType == XmlNodeType.Text)
            {
                var text = await reader.GetValueAsync();
                item.Product.CarePackRegistration = text == "Yes";
            }
        }

        public static Task LinkParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType == XmlNodeType.Element && reader.GetAttribute("type") == "child" && !reader.IsEmptyElement)
            {
                state.InnerState = InnerState.Option;
                state.Option = new Option { ManufacturerCode = "HPE" };
            }

            if (state.NodeType == XmlNodeType.EndElement && state.InnerState == InnerState.Option)
            {
                state.InnerState = InnerState.None;
                item.Options.Items.Add(state.Option);
                state.Option = null;
            }

            return Task.CompletedTask;

        }

        public static Task ParentHierarchyParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType == XmlNodeType.Element)
            {
                // TODO: Save parent id
                // ReSharper disable once StringLiteralTypo
                state.ParentHierarchy = reader.GetAttribute("pmoid");
            }

            return Task.CompletedTask;
        }

        public static Task HierarchyParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType == XmlNodeType.Element)
            {
                // TODO: Save parent id
                // ReSharper disable once StringLiteralTypo
                item.Hierarchy.Add(new Hierarchy("HPE", reader.GetAttribute("pmoid"), reader.GetAttribute("name"), state.ParentHierarchy));
            }

            return Task.CompletedTask;
        }

        public static Task ImageParser(ParseState state, XmlReader reader, ProductRoot item)
        {

            if (state.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
            {
                state.InnerState = InnerState.Image;
                state.Image = new Image();
            }

            if (state.NodeType == XmlNodeType.EndElement)
            {
                item.Links.ImageLinks.Add(state.Image);
                state.Image = null;
                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;
        }

        public static async Task ImageInnerParser(ParseState state, XmlReader reader, ProductRoot item)
        {

            if (state.NodeType == XmlNodeType.Text && state.InnerState == InnerState.Image)
            {
                var text = await reader.GetValueAsync();
                switch (state.CurrentName)
                {
                    case "cmg_acronym":
                        state.Image.CmgAcronym = text;
                        break;
                    case "master_object_name":
                        state.Image.MasterObjectName = text;
                        break;
                    case "content_type":
                        state.Image.ContentType = text;
                        break;
                    case "pixel_height":
                        state.Image.PixelHeight = text;
                        break;
                    case "file_name":
                        state.Image.FileName = text;
                        break;
                    case "language_code":
                        state.Image.LanguageCode = text;
                        break;
                    case "orientation":
                        state.Image.Orientation = text;
                        break;
                    case "pixel_width":
                        state.Image.PixelWidth = text;
                        break;
                    case "action":
                        state.Image.Action = text;
                        break;
                    case "image_url_http":
                        state.Image.ImageUrlHttp = text;
                        break;
                    case "search_keyword":
                        state.Image.SearchKeyword = text;
                        break;
                    case "background":
                        state.Image.Background = text;
                        break;
                    case "full_title":
                        state.Image.FullTitle = text;
                        break;
                    case "document_type_detail":
                        state.Image.DocumentTypeDetail = text;
                        break;
                    case "document_type":
                        state.Image.DocumentType = text;
                        break;
                    case "dpi_resolution":
                        state.Image.DpiResolution = text;
                        break;
                }
            }
        }

        public static Task UpcParser(ParseState state, XmlReader reader, ProductRoot item)
        {

            if (state.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
            {
                state.InnerState = InnerState.Upc;
            }

            if (state.NodeType == XmlNodeType.EndElement)
            {

                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;
        }

        public static async Task UpcInnerParser(ParseState state, XmlReader reader, ProductRoot item)
        {

            if (state.NodeType == XmlNodeType.Element && state.InnerState == InnerState.Upc)
            {
                switch (state.CurrentName)
                {
                    case "content_data":
                        state.ProductVariant = new ProductVariant();
                        break;
                    case "opt":
                        if (reader.IsEmptyElement)
                        {
                            var variant = state.ProductVariant;
                            item.ProductVariants.Add(variant);
                            state.ProductVariant = null;
                        }
                        break;
                }
            }

            if (state.NodeType == XmlNodeType.EndElement
                && state.InnerState == InnerState.Upc
                && state.CurrentName == "opt")
            {
                var variant = state.ProductVariant;
                item.ProductVariants.Add(variant);
                state.ProductVariant = null;
            }

            if (state.NodeType == XmlNodeType.Text && state.InnerState == InnerState.Upc)
            {
                var text = await reader.GetValueAsync();
                if (state.ProductVariant != null)
                {
                    switch (state.CurrentName)
                    {
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
                else
                {
                    Console.WriteLine($"Error in file {state.File} with current node {state.CurrentName} {state.InnerState.ToString()}");
                }
            }
        }

        public static Task KeySellingPointsParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
            {
                state.InnerState = InnerState.KeySellingPoints;
                state.MarketingText = new Dictionary<string, Section>();
            }

            if (state.NodeType == XmlNodeType.EndElement)
            {

                if (state.MarketingText != null) item.Marketing.MarketingText = string.Join(" ", state.MarketingText.OrderBy(x => x.Key).Select(x => x.Value.ToString()));
                state.MarketingText = null;
                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;
        }

        public static async Task ProdNameShortParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType != XmlNodeType.Text)
            {
                return;
            }
            var text = await reader.GetValueAsync();
            item.Product.DescriptionLong = text;
        }

        public static Task TechnicalSpecificationsParser(ParseState state, XmlReader reader, ProductRoot item)
        {

            if (state.NodeType == XmlNodeType.Element)
            {
                state.InnerState = InnerState.TechnicalSpecifications;
            }

            if (state.NodeType == XmlNodeType.EndElement)
            {
                state.InnerState = InnerState.None;
            }

            return Task.CompletedTask;
        }

        public static async Task UnspscParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType == XmlNodeType.Text)
            {
                var text = await reader.GetValueAsync();
                if (int.TryParse(text.Replace(" ", "").Replace("-", ""), out var unspsc))
                {
                    item.Product.Unspsc = unspsc;
                }
            }
        }

        public static async Task QuickSpecParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType == XmlNodeType.Text)
            {
                var text = await reader.GetValueAsync();
                item.Links.PdfLinkDataSheet = text;
            }
        }

        public static Task ItemParser(ParseState state, XmlReader reader, ProductRoot item)
        {
            if (state.NodeType != XmlNodeType.Element)
            {
                return Task.CompletedTask;
            }
            // ReSharper disable StringLiteralTypo
            item.Product.ManufacturerCode = "HPE";
            item.Product.ManufacturerName = "HPE";
            item.LanguageId = reader.GetAttribute("culturecode");
            item.PartNumber = reader.GetAttribute("number");
            item.Product.Description = reader.GetAttribute("name");
            if (DateTime.TryParse(reader.GetAttribute("lastupdatedate"), out var changeDate))
            {
                item.Product.ChangeDate = changeDate;
            }
            item.PartnerPartNumber = item.PartNumber;
            var productLine = reader.GetAttribute("productline");
            item.Hierarchy.Add(new Hierarchy("PL", productLine, productLine, null));
            // ReSharper restore StringLiteralTypo
            return Task.CompletedTask;
        }
    }

}