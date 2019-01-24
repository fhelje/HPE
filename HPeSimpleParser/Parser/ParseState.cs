using System.Collections.Generic;
using System.Xml;
using HPeSimpleParser.Generic.Model;
using HPeSimpleParser.HPE.Model;
using Hierarchy = HPeSimpleParser.HPE.Model.Hierarchy;
using Image = HPeSimpleParser.HPE.Model.Image;
using Specification = HPeSimpleParser.HPE.Model.Specification;

namespace HPeSimpleParser.Parser
{
    public class ParseState
    {
        public string File { get; }
        private int _level;
        private string _currentName;
        private readonly Stack<string> _nodes;
        private XmlNodeType _nodeType;

        public ParseState(string file)
        {
            File = file;
            _level = 0;
            _nodes = new Stack<string>();
            Branch = new Branch();
            Hierarchy = new List<Hierarchy>();
            Product = new ProductState();
            ProductVariants = new List<ProductVariantState>();
            Links = new LinksState();
            Options = new List<OptionState>();
            Specifications = new List<SpecificationState>();
        }

        public Branch Branch { get; }

        public int Level => _level;
        public string CurrentName => _currentName;
        public XmlNodeType NodeType => _nodeType;
        public InnerState InnerState { get; set; }
        public string Label { get; set; }
        public Dictionary<string, Section> MarketingText { get; set; }
        public string ParentHierarchy { get; set; }
        public OptionState Option { get; set; }
        public ProductVariantState ProductVariant { get; set; }
        public ImageTemp Image { get; set; }
        public MarketingState Marketing { get; set; }
        public ProductState Product { get; set; }
        public string LanguageId { get; set; }
        public string PartNumber { get; set; }
        public string PartnerPartNumber { get; set; }
        public List<Hierarchy> Hierarchy { get; set; }
        public List<ProductVariantState> ProductVariants { get; set; }
        public LinksState Links { get; set; }
        public List<OptionState> Options { get; set; }
        public List<SpecificationState> Specifications { get; set; }

        public void Inc(string name, XmlNodeType nodeType, bool isEmpty)
        {
            _level++;
            _currentName = name;
            _nodes.Push(name);
            _nodeType = nodeType;

        }
        public void Dec()
        {
            _level--;
            _nodes.Pop();
            _currentName = _nodes.Peek();
            _nodeType = XmlNodeType.EndElement;
        }

        public void SetNodeType(XmlNodeType type)
        {
            _nodeType = type;
        }

        public void SetName(string name)
        {
            _currentName = name;
        }

    }

    public class SpecificationState {
        public string Name { get; set; }
        public string Value { get; set; }
        public SpecificationType Type { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string Label { get; set; }
    }

    public class OptionState {
        public string OptionGroupCode { get; set; }
        public string OptionGroupName { get; set; }
        public string OptionPartnerPartNumber { get; set; }
        public string ManufacturerCode { get; set; }
    }

    public class MarketingState {
        public string MarketingText { get; set; }
    }
    public class LinksState {
        public LinksState() {
            ImageLinks = new List<Image>();
            SelectedImages = new Image[0];
        }

        // TODO: Add constructor
        // TODO: Add Links
        public string PdfLinkDataSheet { get; set; }
        public string PdfLinkManual { get; set; }
        public List<Image> ImageLinks { get; set; }
        public Image[] SelectedImages { get; set; }
    }
    public class ProductVariantState {
        public string UpcCode { get; set; }
        public string Description { get; set; }
        public string Opt { get; set; }
    }
}
