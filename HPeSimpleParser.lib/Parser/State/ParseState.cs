﻿using System.Collections.Generic;
using System.Xml;
using FSSystem.ContentAdapter.HPEAndHPInc.Enums;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using Hierarchy = FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model.Hierarchy;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser.State
{
    public class ParseState
    {
        private readonly VariantType _variant;
        public string File { get; }
        private int _level;
        private string _currentName;
        private readonly Stack<string> _nodes;
        private XmlNodeType _nodeType;

        public ParseState(string file, VariantType variant)
        {
            _variant = variant;
            PLHierarchyName = _variant == VariantType.HPE ? "PL" : "HPIncPL";
            HierarchyName = _variant.ToString();
            ManufacturerCode = _variant.ToString();
            ManufacturerName = _variant.ToString();
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

        public string PLHierarchyName { get; }
        public string HierarchyName { get; }

        public Branch Branch { get; }

        public string CurrentName => _currentName;
        public XmlNodeType NodeType => _nodeType;
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
}
