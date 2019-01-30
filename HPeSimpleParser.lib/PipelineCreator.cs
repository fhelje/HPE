using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using HPeSimpleParser.lib.Enums;
using HPeSimpleParser.lib.Generic.FileWriter;
using HPeSimpleParser.lib.HierarchyFile;
using HPeSimpleParser.lib.HPE.Model;
using HPeSimpleParser.lib.Parser;

namespace HPeSimpleParser.lib {
    public class PipelineCreator {
        // Process import file list
        // Input:  file path for list of product files
        // Output: path to product file
        // ==>
        // Transform block for parser
        // input:  Path to xml
        // Output: ProductRoot
        // ==>
        private static bool _writeLine;
        private static bool _writeLine2;
        private readonly CsvOutputWriter _csvOutputWriter;
        private readonly JsonOutputWriter _jsonOutputWriter;
        private readonly IParserDefinition _definition;
        private readonly WriterConfiguration _configuration;
        private readonly VariantType _variant;
        private readonly ExecutionDataflowBlockOptions _singleWriterExecution;
        private readonly ExecutionDataflowBlockOptions _options;
        private readonly Dictionary<string, HierarchyNode> _plDictionary;
        private readonly HPEHierarchyNode _hierarchyRoot;
        private readonly DataflowLinkOptions _linkOptions;
        private const FileTypes FileTypes = Generic.FileWriter.FileTypes.Detail | Generic.FileWriter.FileTypes.Link | Generic.FileWriter.FileTypes.Marketing | Generic.FileWriter.FileTypes.Option | Generic.FileWriter.FileTypes.Option | Generic.FileWriter.FileTypes.Product | Generic.FileWriter.FileTypes.Specification | Generic.FileWriter.FileTypes.Supplier;

        public PipelineCreator(
            IParserDefinition definition,
            WriterConfiguration config,
            VariantType variant
            ) {
            _definition = definition;
            _configuration = config;
            _variant = variant;
            _singleWriterExecution = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 };
            _options = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = Environment.ProcessorCount, BoundedCapacity = 1000000, MaxMessagesPerTask = 10000 };
            _linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            _plDictionary = new Dictionary<string, HierarchyNode>();
            _hierarchyRoot = new HPEHierarchyNode();
            _csvOutputWriter = new CsvOutputWriter(config);
            _jsonOutputWriter = new JsonOutputWriter(config);
        }
        public (ActionBlock<WriterConfiguration> source, Task targetTask) CreatePipeline(
            
        ) {
            var broadcastBlock = new BroadcastBlock<ProductRoot[]>(x => x);
            var batchBlock = new BatchBlock<ProductRoot>(1000, new GroupingDataflowBlockOptions { BoundedCapacity = 1000000, MaxMessagesPerTask = 10000 });

            var parseBlock = new TransformBlock<string, ProductRoot>(async path => {
                var parser = new XmlParser(_definition);
                return await parser.ParseDocument(path, _variant);
            }, _options);

            var readerBlock = new ActionBlock<WriterConfiguration>(async conf => {
                var parser = new DeliveryFileParser();
                var (count, files) = await parser.DeliveryFileReader(conf);
                Console.WriteLine($"Start processing files. Total product to import {count} ({files.Count}).");

                foreach (var file in files) {
                    await parseBlock.SendAsync(file);
                }

            }, _options);

            var csvWriteBlock = new ActionBlock<ProductRoot[]>(async batch => {
                foreach (var productRoot in batch) {
                    await _csvOutputWriter.WriteAsync(FileTypes, productRoot.ToItem());
                }

                Console.WriteLine("Batch done");
            }, _singleWriterExecution);

            var plHierarchyBlock = new ActionBlock<ProductRoot[]>(batch => { AddProductLineHierarchies(batch); }, _singleWriterExecution);

            var hpIncHierarchyBlock = new ActionBlock<ProductRoot[]>(batch => {
                foreach (var productRoot in batch) {
                    UpdateCategoryTree(_hierarchyRoot, productRoot.Branch, _variant == VariantType.HPE  ? "HPE" : "HPInc");
                }
            }, _singleWriterExecution);

            var jsonWriteBlock = new ActionBlock<ProductRoot[]>(batch => { WriteJson(batch); }, _singleWriterExecution);

            parseBlock.CompleteWhenAll(readerBlock);
            parseBlock.LinkTo(batchBlock, _linkOptions);
            batchBlock.LinkTo(broadcastBlock, _linkOptions);
            broadcastBlock.LinkTo(csvWriteBlock, _linkOptions);
            broadcastBlock.LinkTo(jsonWriteBlock, _linkOptions);
            broadcastBlock.LinkTo(plHierarchyBlock, _linkOptions);
            broadcastBlock.LinkTo(hpIncHierarchyBlock, _linkOptions);

            var task = Task.WhenAll(plHierarchyBlock.Completion, hpIncHierarchyBlock.Completion, jsonWriteBlock.Completion, csvWriteBlock.Completion)
                .ContinueWith(DisposeWriters)
                .ContinueWith(async x => {
                    await OutputHierarchies(_configuration, _plDictionary, _hierarchyRoot);
                })
                .ContinueWith(async x => await WriteSupplierFile(x))
                .ContinueWith(ZipCsv)
                .ContinueWith(ZipJson);
            return (source: readerBlock, targetTask: task);
        }

        private async Task WriteSupplierFile(Task<Task> obj) {
            Console.WriteLine("Write supplier");
            using (var fw = new CsvSupplierOutputWriter(_configuration)) {
                await fw.WriteAsync(new SupplierNode {Code = _variant.ToString(), Name = _variant.ToString()});
            }
            
        }

        private void AddProductLineHierarchies(ProductRoot[] batch) {
            foreach (var productRoot in batch) {
                var productLineHierarchy = productRoot.Hierarchy.FirstOrDefault(x => x.Name == "PL" || x.Name == "HPIncPL");
                if (productLineHierarchy == null) {
                    return;
                }
                _plDictionary.TryAdd(productLineHierarchy.CategoryID,
                    new HierarchyNode {
                        CategoryID = productLineHierarchy.CategoryID,
                        CategoryName = productLineHierarchy.CategoryName,
                        Level = 1,
                        PartnerHierarchyCode = _variant == VariantType.HPE ? "PL" : "HPIncPL"
                    });
            }
        }

        private void WriteJson(ProductRoot[] batch) {
            foreach (var productRoot in batch) {
                _jsonOutputWriter.Write(productRoot.ToItem());
            }

            Console.WriteLine("Json Batch done");
        }

        private void DisposeWriters(Task obj) {
            _csvOutputWriter.Dispose();
            _jsonOutputWriter.Dispose();
        }

        private void ZipCsv(Task _) {
            ZipHelper.Zip(
                Path.Combine(_configuration.OutputPath, _configuration.CsvDirectory), 
                Path.Combine(_configuration.OutputPath, _configuration.CsvZipFileName));
        }

        private void ZipJson(Task _) {
            ZipHelper.Zip(
                Path.Combine(_configuration.OutputPath, _configuration.JsonDirectory), 
                Path.Combine(_configuration.OutputPath, _configuration.JsonZipFileName));
        }

        private static async Task OutputHierarchies(WriterConfiguration config, Dictionary<string, HierarchyNode> plDictionary,
            HPEHierarchyNode hierarchyRoot) {
            var pureHierarchyPath = Path.Combine(config.OutputPath, config.CsvDirectory, config.PureHierarchyFileName);
            Console.WriteLine($"Write hierarchies: {pureHierarchyPath}");
            var productLineHierarchyRoot = new HierarchyNode { Level = 1, CategoryName = "HPE PL Root", CategoryID = "Pl" };
            productLineHierarchyRoot.Children.AddRange(plDictionary.Values);
            FileHelpers.DeleteFile(FileTypes.Pure_Hierarchy, FileTypes.Pure_Hierarchy, pureHierarchyPath);
            using (var stream =
                new StreamWriter(File.OpenWrite(pureHierarchyPath))) {
                using (var fileWriter = new CsvHierarchyOutputWriter(stream)) {
                    await WriteHierarchy(productLineHierarchyRoot, fileWriter);
                }

                using (var fileWriter = new CsvHierarchyOutputWriter2(stream)) {
                    await WriteHierarchy2(hierarchyRoot, fileWriter);
                }
            }
        }

        private static void UpdateCategoryTree(HPEHierarchyNode hierarchyRoot, IReadOnlyList<Hierarchy> branch, string partnerHierarchyCode) {

            if (branch[0].CategoryID == null) {
                return;
            }
            var productTypeId = branch[0].CategoryID ;
            var productType = hierarchyRoot.Children.GetOrAdd(
                productTypeId,
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[0].CategoryName,
                    ParentCategoryId = null,
                    Level = 1,
                    PartnerHierarchyCode = partnerHierarchyCode
                }
            );

            var marketingCategoryId = productTypeId + "|" + branch[1].CategoryID;
            var marketingCategory = productType.Children.GetOrAdd(
                marketingCategoryId,
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[1].CategoryName,
                    ParentCategoryId = productTypeId,
                    Level = 2,
                    PartnerHierarchyCode = partnerHierarchyCode
                }
                );

            var marketingSubCategoryId = marketingCategoryId + "|" + branch[2].CategoryID;
            var marketingSubCategory = marketingCategory.Children.GetOrAdd(
                marketingSubCategoryId,
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[2].CategoryName,
                    ParentCategoryId = marketingCategoryId,
                    Level = 3,
                    PartnerHierarchyCode = partnerHierarchyCode
                }
                );

            var bigSeriesId = marketingSubCategoryId + "|" + branch[3].CategoryID;
            var bigSeries = marketingSubCategory.Children.GetOrAdd(
                bigSeriesId,
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[3].CategoryName,
                    ParentCategoryId = marketingSubCategoryId,
                    Level = 4,
                    PartnerHierarchyCode = partnerHierarchyCode
                }
                );

            var smallSeriesId = bigSeriesId + "|" + branch[4].CategoryID;
            bigSeries.Children.GetOrAdd(
                smallSeriesId,
                key => new HPEHierarchyNode {
                    Id = key,
                    Name = branch[4].CategoryName,
                    ParentCategoryId = bigSeriesId,
                    Level = 5,
                    PartnerHierarchyCode = partnerHierarchyCode
                }
            );
        }

        private static async Task WriteSupplier(IEnumerable<SupplierNode> supplierNodes, CsvSupplierOutputWriter fileWriter) {
            foreach (var child in supplierNodes) {
                await fileWriter.WriteAsync(child);
            }
        }

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


    }
}
