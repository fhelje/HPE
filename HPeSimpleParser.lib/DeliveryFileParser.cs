using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.lib.Generic.FileWriter;

namespace HPeSimpleParser.lib {
    public class DeliveryFileParser {
        public async Task<(int count, List<string> files)> DeliveryFileReader(WriterConfiguration conf) {
            var settings = new XmlReaderSettings {
                Async = true
            };

            var files = new List<string>();
            var count = 0;
            using (var stream = File.OpenRead(Path.Combine(conf.ImportPath, conf.DeliveryFile))) {
                using (var reader = XmlReader.Create(stream, settings)) {
                    while (await reader.ReadAsync()) {
                        switch (reader.NodeType) {
                            case XmlNodeType.Element:
                                switch (reader.Name) {
                                    case "files":
                                        count = int.Parse(reader.GetAttribute("total.files"));
                                        break;
                                    case "file":
                                        files.Add(Path.Combine(conf.ImportPath, reader.GetAttribute("name")));
                                        break;
                                }

                                break;
                        }
                    }

                }
                return (count, files);
            }
        }
    }
}