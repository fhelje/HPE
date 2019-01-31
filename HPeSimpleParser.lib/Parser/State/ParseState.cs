using System.Collections.Generic;
using System.Xml;
using FSSystem.ContentAdapter.HPEAndHPInc.Enums;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser.State {
    public class ParseState {
        private readonly Stack<string> _nodes;
        private readonly VariantType _variant;

        public ParseState(string file, VariantType variant) {
            _variant = variant;
            PLHierarchyName = _variant == VariantType.HPE ? "PL" : "HPIncPL";
            HierarchyName = _variant.ToString();
            ManufacturerCode = _variant.ToString();
            ManufacturerName = _variant.ToString();
            File = file;
            _nodes = new Stack<string>();
            Branch = new Branch();
            Hierarchy = new List<Hierarchy>();
            Product = new ProductState();
            ProductVariants = new List<ProductVariantState>();
            Links = new LinksState();
            Options = new List<OptionState>();
            Specifications = new List<SpecificationState>();
        }

        public string File { get; }

        public string PLHierarchyName { get; }
        public string HierarchyName { get; }

        public Branch Branch { get; }

        public string CurrentName { get; private set; }
        public XmlNodeType NodeType { get; private set; }
        public InnerState InnerState { get; set; }
        public string Label { get; set; }
        public Dictionary<string, Section> MarketingText { get; set; }
        public string ParentHierarchy { get; set; }
        public OptionState Option { get; set; }
        public ProductVariantState ProductVariant { get; set; }
        public ImageState Image { get; set; }
        public MarketingState Marketing { get; set; }
        public ProductState Product { get; }
        public string LanguageId { get; set; }
        public string PartNumber { get; set; }
        public string PartnerPartNumber { get; set; }
        public List<Hierarchy> Hierarchy { get; }
        public List<ProductVariantState> ProductVariants { get; }
        public LinksState Links { get; }
        public List<OptionState> Options { get; }
        public List<SpecificationState> Specifications { get; }
        public string ManufacturerName { get; }
        public string ManufacturerCode { get; }

        public void Inc(string name, XmlNodeType nodeType) {
            CurrentName = name;
            _nodes.Push(name);
            NodeType = nodeType;
        }

        public void Dec() {
            _nodes.Pop();
            CurrentName = _nodes.Peek();
            NodeType = XmlNodeType.EndElement;
        }

        public void SetNodeType(XmlNodeType type) {
            NodeType = type;
        }

        public void SetName(string name) {
            CurrentName = name;
        }
    }
}