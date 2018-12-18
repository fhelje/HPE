using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace HPeSimpleParser.HierarchyFile {
    public class HierarchyParser {
        public async Task<HierarchyNode> Parse(string file) {
            var branch = new Stack<HierarchyNode>();
            branch.Push(new HierarchyNode { Level = 0, CategoryName = "Root", ParentCategoryID = "1", CategoryID = "1" });
            using (var stream = File.OpenRead(file)) {
                var settings = new XmlReaderSettings {
                    Async = true
                };
                var indentation = 0;
                using (var reader = XmlReader.Create(stream, settings)) {
                    while (await reader.ReadAsync()) {
                        switch (reader.NodeType) {
                            case XmlNodeType.Element:
                                if (reader.Name != "hp.products") {
                                    if (!reader.IsEmptyElement) {
                                        var categoryId = reader.GetAttribute("oid");
                                        var categoryName = reader.GetAttribute("name");
                                        indentation++;
                                        var current = branch.Peek();
                                        var node = new HierarchyNode {
                                            CategoryID = categoryId,
                                            CategoryName = categoryName,
                                            Level = indentation,
                                            ParentCategoryID = current.CategoryID
                                        };
                                        current.Children.Add(node);
                                        branch.Push(node);
                                    }
                                }
                                break;
                            case XmlNodeType.Text:
                                break;
                            case XmlNodeType.EndElement:
                                if (reader.Name != "hp.products") {
                                    branch.Pop();
                                }

                                indentation--;
                                break;
                        }
                    }
                }
            }
            return branch.Pop();
        }
    }

    public class HierarchyNode {
        public HierarchyNode() {
            Children = new List<HierarchyNode>();
        }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryID { get; set; }
        public int Level { get; set; }
        public List<HierarchyNode> Children { get; set; }
    }
}
