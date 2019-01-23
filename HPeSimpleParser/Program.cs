using System;
using System.Collections.Async;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;
using System.Xml;
using HPeSimpleParser.Generic.FileWriter;
using HPeSimpleParser.HierarchyFile;
using HPeSimpleParser.HPE.Model;
using HPeSimpleParser.Linq;
using HPeSimpleParser.Parser;

namespace HPeSimpleParser {
    class Program {
        //\\prod01\e$\FS\Tillverkare\HPe\CAP\Change fil 2018-11-25\HPE_cap_gb_en_xml_2617_20181125114722\pad_en_gb_global_product_L_JL380A_1009690316.xml
        private static HPEHierarchyNode _hierarchyRoot;
        private static bool _writeLine;
        private static bool _writeLine2;
        private static async Task WriteHierarchy(HierarchyNode root, CsvHierarchyOutputWriter fileWriter) {
            if (_writeLine) {
                await fileWriter.WriteAsync(root);
            }
            else {
                _writeLine = true;
            }
            foreach (var child in root.Children) {
                await WriteHierarchy(child, fileWriter);
            }
        }
        private static async Task WriteHierarchy2(HPEHierarchyNode root, CsvHierarchyOutputWriter2 fileWriter) {
            if (_writeLine2) {
                await fileWriter.WriteAsync(root);
            }
            else {
                _writeLine2 = true;
            }
            foreach (var child in root.Children) {
                await WriteHierarchy2(child.Value, fileWriter);
            }
        }
        private static async Task WriteSupplier(IEnumerable<SupplierNode> supplierNodes, CsvSupplierOutputWriter fileWriter) {
            foreach (var child in supplierNodes) {
                await fileWriter.WriteAsync(child);
            }
        }

        static async Task Main(string[] args) {
            _hierarchyRoot = new HPEHierarchyNode();
            //const string rootPath = @"\\prod01\e$\FS\Tillverkare\HPe\CAP\Change fil 2018-11-25\HPE_cap_gb_en_xml_2617_20181125114722";
            //const string deliverFilePath = rootPath + @"\delivery_2617.xml";
            const string rootPath = @"C:\temp\hpesimpleinput\Full fil 2018-11-29";
            const string deliverFilePath = rootPath + @"\delivery_2699.xml";
            const string hierarchyFilePath = rootPath + @"\product_hierarchy.xml";
            var fileTypes = FileTypes.Detail | FileTypes.Link | FileTypes.Marketing | FileTypes.Option | FileTypes.Option | FileTypes.Product | FileTypes.Specification | FileTypes.Supplier;
            int count;
            List<string> files = null;
            var config = new WriterConfiguration { OutputPath = @"c:\temp\hpesimpleoutput" };


            using (var stream = File.OpenRead(deliverFilePath)) {
                (count, files) = await TestReader(stream, rootPath);
            }
            var plDictionary = new ConcurrentDictionary<string, HierarchyNode>();
            Console.WriteLine($"Handle {count} files");
            var productLineHierarchyRoot = new HierarchyNode { Level = 1, CategoryName = "HPE PL Root", CategoryID = "Pl" };
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
                    var productLineHierarchy = productRoot.Hierarchy.First(x => x.Name == "PL");
                    plDictionary.TryAdd(productLineHierarchy.CategoryID,
                        new HierarchyNode {
                            CategoryID = productLineHierarchy.CategoryID,
                            CategoryName = productLineHierarchy.CategoryName,
                            Level = 1,
                            PartnerHierarchyCode = "PL"
                        });
                    list.Add(productRoot);
                }
                transformedBatches.Add(list);
                Console.WriteLine("Batch done");
            }, 8);

            foreach (var batch in transformedBatches) {
                Console.Write(":");
                foreach (var p in batch) {
                    UpdateCategoryTree(p.Branch);
                }
            }
            Console.WriteLine("HierarchyUpdated");
            var sw = new Stopwatch();

            Console.WriteLine($"Start hierarchy output");
            productLineHierarchyRoot.Children.AddRange(plDictionary.Values);
            DeleteExistingFiles(FileTypes.Pure_Hierarchy, config);
            using (var stream = new StreamWriter(File.OpenWrite(Path.Combine(config.OutputPath, "pure_hierarchy.txt")))) {
                using (var fileWriter = new CsvHierarchyOutputWriter(stream)) {
                    await WriteHierarchy(productLineHierarchyRoot, fileWriter);
                }
                using (var fileWriter = new CsvHierarchyOutputWriter2(stream)) {
                    await WriteHierarchy2(_hierarchyRoot, fileWriter);
                }

            }


            Console.WriteLine($"Start Pure Supplier output");
            using (var fileWriter = new CsvSupplierOutputWriter(config)) {
                await WriteSupplier(new List<SupplierNode> { new SupplierNode { Code = "HPE", Name = "HPE" } }, fileWriter);
            }

            sw.Start();
            foreach (var batch in transformedBatches) {
                Console.WriteLine("Index Batch");
                await indexer.IndexMany(batch);
                Console.WriteLine("Index Batch done");
            }
            sw.Stop();
            Console.WriteLine($"Elastic indexing took {sw.Elapsed.ToString()}");

            DeleteExistingFiles(fileTypes, config);
            using (var fileWriter = new CsvOutputWriter(config)) {
                sw.Start();
                foreach (var batch in transformedBatches) {
                    Console.Write(".");
                    foreach (var p in batch) {
                        await fileWriter.WriteAsync(fileTypes, p.ToItem());
                    }
                    //Console.WriteLine("Write Batch done");
                }
                sw.Stop();
                Console.WriteLine($"Write took {sw.Elapsed.ToString()}");
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void UpdateCategoryTree(List<Hierarchy> branch) {
            var productTypeId = string.Empty;
            var marketingCategoryId = string.Empty;
            var marketingSubCategoryId = string.Empty;
            var bigSeriesId = string.Empty;
            var smallSeriesId = string.Empty;

            productTypeId = branch[0].CategoryID;
            var productType = _hierarchyRoot.Children.GetOrAdd(
                productTypeId,
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[0].CategoryName,
                    ParentCategoryId = null,
                    Level = 1
                }
            );

            marketingCategoryId = productTypeId + "|" + branch[1].CategoryID;
            var marketingCategory = productType.Children.GetOrAdd(
                marketingCategoryId ,
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[1].CategoryName,
                    ParentCategoryId = productTypeId,
                    Level = 2
                }
                );

            marketingSubCategoryId = marketingCategoryId + "|" + branch[2].CategoryID;
            var marketingSubCategory = marketingCategory.Children.GetOrAdd(
                marketingSubCategoryId,
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[2].CategoryName,
                    ParentCategoryId = marketingCategoryId,
                    Level = 3
                }
                );

            bigSeriesId = marketingSubCategoryId + "|" + branch[3].CategoryID;
            var bigSeries = marketingSubCategory.Children.GetOrAdd(
                bigSeriesId, 
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[3].CategoryName,
                    ParentCategoryId = marketingSubCategoryId,
                    Level = 4
                }
                );

            smallSeriesId = bigSeriesId + "|" + branch[4].CategoryID;
            bigSeries.Children.GetOrAdd(
                smallSeriesId,
                key => new HPEHierarchyNode {
                    Id = key,                    
                    Name = branch[4].CategoryName,
                    ParentCategoryId = bigSeriesId,
                    Level = 5
                }
            );
        }

        private static void DeleteExistingFiles(FileTypes fileTypes, WriterConfiguration config) {
            DeleteFile(FileTypes.Detail, fileTypes, config);
            DeleteFile(FileTypes.Link, fileTypes, config);
            DeleteFile(FileTypes.Marketing, fileTypes, config);
            DeleteFile(FileTypes.Option, fileTypes, config);
            DeleteFile(FileTypes.Product, fileTypes, config);
            DeleteFile(FileTypes.Specification, fileTypes, config);
            DeleteFile(FileTypes.Pure_Hierarchy, fileTypes, config);

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