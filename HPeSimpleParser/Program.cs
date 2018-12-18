using System;
using System.Collections.Async;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.HierarchyFile;
using HPeSimpleParser.HPE.Model;
using HPeSimpleParser.Linq;
using HPeSimpleParser.Parser;

namespace HPeSimpleParser {
    class Program {
        //\\prod01\e$\FS\Tillverkare\HPe\CAP\Change fil 2018-11-25\HPE_cap_gb_en_xml_2617_20181125114722\pad_en_gb_global_product_L_JL380A_1009690316.xml

        private static async Task WriteHierarchy(HierarchyNode root, CsvHierarchyOutputWriter fileWriter) {
            await fileWriter.WriteAsync(root);
            foreach (var child in root.Children) {
                await WriteHierarchy(child, fileWriter);
            }
        }

        static async Task Main(string[] args) {
            //const string rootPath = @"\\prod01\e$\FS\Tillverkare\HPe\CAP\Change fil 2018-11-25\HPE_cap_gb_en_xml_2617_20181125114722";
            //const string deliverFilePath = rootPath + @"\delivery_2617.xml";
            const string rootPath = @"C:\temp\hpesimpleinput\Full fil 2018-11-28";
            const string deliverFilePath = rootPath + @"\delivery_2699.xml";
            const string hierarchyFilePath = rootPath + @"\product_hierarchy.xml";
            int count;
            List<string> files = null;

            Console.WriteLine($"Start hierarchy parsing");
            var hParser = new HierarchyParser();
            var root = await hParser.Parse(hierarchyFilePath);
            var config = new WriterConfiguration { OutputPath = @"c:\temp\hpesimpleoutput" };
            Console.WriteLine($"Start hierarchy output");
            DeleteExistingFiles(FileTypes.PureHierarchy, config);
            using (var fileWriter = new CsvHierarchyOutputWriter(config)) {
                await WriteHierarchy(root, fileWriter);
            }

            using (var stream = File.OpenRead(deliverFilePath)) {
                (count, files) = await TestReader(stream, rootPath);
            }

            Console.WriteLine($"Handle {count} files");

            var indexer = new ElasticIndexer();
            indexer.SetupClient();
            indexer.DeleteIndex();
            indexer.CreateIndex();
            var transformedBatches = new ConcurrentBag<List<ProductRoot>>();
            await files.Batch(1000).ParallelForEachAsync(async batch => {
                var list = new List<ProductRoot>();
                var parser = new HpeParser();
                foreach (var file in batch) {
                    var productRoot = await parser.ParseDocument(file);

                    //fileWriter.Write(productRoot);
                    list.Add(productRoot);
                }
                transformedBatches.Add(list);
                Console.WriteLine("Batch done");
            }, 8);
            var sw = new Stopwatch();
            sw.Start();
            foreach (var batch in transformedBatches) {
                Console.WriteLine("Index Batch");
                await indexer.IndexMany(batch);
                Console.WriteLine("Index Batch done");
            }
            sw.Stop();
            Console.WriteLine($"Elastic indexing took {sw.Elapsed.ToString()}");

            var fileTypes = FileTypes.Detail | FileTypes.Link | FileTypes.Marketing | FileTypes.Option | FileTypes.Option | FileTypes.Product | FileTypes.Specification | FileTypes.Supplier;
            DeleteExistingFiles(fileTypes, config);
            using (var fileWriter = new CsvOutputWriter(config)) {
                sw.Start();
                foreach (var batch in transformedBatches) {
                    Console.WriteLine("Write Batch");
                    foreach (var p in batch) {
                        await fileWriter.WriteAsync(fileTypes, p.ToItem());
                    }
                    Console.WriteLine("Write Batch done");
                }
                sw.Stop();
                Console.WriteLine($"Write took {sw.Elapsed.ToString()}");
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void DeleteExistingFiles(FileTypes fileTypes, WriterConfiguration config) {
            DeleteFile(FileTypes.Detail, fileTypes, config);
            DeleteFile(FileTypes.Link, fileTypes, config);
            DeleteFile(FileTypes.Marketing, fileTypes, config);
            DeleteFile(FileTypes.Option, fileTypes, config);
            DeleteFile(FileTypes.Product, fileTypes, config);
            DeleteFile(FileTypes.Specification, fileTypes, config);
            DeleteFile(FileTypes.Supplier, fileTypes, config);
            DeleteFile(FileTypes.PureHierarchy, fileTypes, config);

        }

        private static void DeleteFile(FileTypes fileTypeToRemove, FileTypes fileTypes, WriterConfiguration config) {
            if (fileTypes.HasFlag(fileTypeToRemove)) {
                var file = Path.Combine(config.OutputPath, $"{fileTypeToRemove.ToString().ToLower()}.txt");
                if (File.Exists(file)) {
                    File.Delete(file);
                }
            }
        }

        private static async Task<(int count, List<string> files)> TestReader(Stream stream,
            string rootPath) {
            var settings = new XmlReaderSettings {
                Async = true
            };

            using (var reader = XmlReader.Create(stream, settings)) {
                var files = new List<string>();
                var count = 0;
                while (await reader.ReadAsync()) {
                    switch (reader.NodeType) {
                        case XmlNodeType.Element:
                            switch (reader.Name) {
                                case "files":
                                    count = int.Parse(reader.GetAttribute("total.files"));
                                    break;
                                case "file":
                                    files.Add(Path.Combine(rootPath, reader.GetAttribute("name")));
                                    break;
                            }

                            break;
                    }
                }

                return (count, files);
            }
        }
    }
}