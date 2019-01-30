using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HierarchyFile {
    public class HierarchyParser {
        public async Task<HierarchyNode> Parse(string file) {
            var branch = new Stack<HierarchyNode>();

            branch.Push(new HierarchyNode { Level = 1, CategoryName = "HPE Root", CategoryID = "HPE" });
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
                                        var categoryId = reader.GetAttribute("common.oid");
                                        var categoryName = reader.GetAttribute("name");
                                        indentation++;
                                        var current = branch.Peek();
                                        var node = new HierarchyNode {
                                            CategoryID = categoryId,
                                            CategoryName = categoryName,
                                            Level = indentation,
                                            ParentCategoryID = indentation > 1 ? current.CategoryID : null,
                                            PartnerHierarchyCode = "HPE"
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
}
