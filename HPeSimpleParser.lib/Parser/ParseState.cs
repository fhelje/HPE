using System.Collections.Generic;
using System.Xml;
using HPeSimpleParser.lib.HPE.Model;

namespace HPeSimpleParser.lib.Parser
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
        }

        public int Level => _level;
        public string CurrentName => _currentName;
        public XmlNodeType NodeType => _nodeType;
        public InnerState InnerState { get; set; }
        public string Label { get; set; }
        public Dictionary<string, Section> MarketingText { get; set; }
        public string ParentHierarchy { get; set; }
        public Option Option { get; set; }
        public ProductVariant ProductVariant { get; set; }
        public Image Image { get; set; }

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
