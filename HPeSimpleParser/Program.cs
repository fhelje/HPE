using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HPeSimpleParser.lib;
using HPeSimpleParser.lib.Generic.FileWriter;
using McMaster.Extensions.CommandLineUtils;

namespace HPeSimpleParser {
    [Command(Name = "HPImport", Description = "Import HPE and HP Inc products")]
    [HelpOption("-?")]
    class Program {
        //\\prod01\e$\FS\Tillverkare\HPe\CAP\Change fil 2018-11-25\HPE_cap_gb_en_xml_2617_20181125114722\pad_en_gb_global_product_L_JL380A_1009690316.xml
        // TODO! vvvvvvv
        private static async Task WriteSupplier(IEnumerable<SupplierNode> supplierNodes, CsvSupplierOutputWriter fileWriter) {
            foreach (var child in supplierNodes) {
                await fileWriter.WriteAsync(child);
            }
        }
        [Option("-v")]
        [Required]
        public VariantType Variant { get; set; }

        public static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

        public async Task OnExecuteAsync() {
            // SourceFiles root path
            // Target directory?
            // Zip task at the end? What name?
            // TODO: Search for delivery file?

            string name;
            string outputPath;
            string importPath;
            switch (Variant) {
                case VariantType.HPE:
                    importPath = @"C:\temp\hpesimpleinput\Full fil 2018-11-29";
                    outputPath = @"c:\temp\hpesimpleoutput";
                    name = "hpe";
                    break;
                case VariantType.HPInc:
                    importPath = @"D:\Temp\cap_gb_en_xml_1716_20190121132536";
                    outputPath = @"c:\temp\hpincsimpleoutput";
                    name = "hpinc";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var deliveryFile = FindDeliveryFile(importPath);

            var config = new WriterConfiguration {
                ImportPath = importPath,
                DeliveryFile = deliveryFile,
                OutputPath = outputPath,
                CsvDirectory = "csv",
                JsonDirectory = "json",
                JsonFileName = "all.json",
                DetailFileName = "detail.txt",
                LinkFileName = "link.txt",
                MarketingFileName = "marketing.txt",
                OptionFileName = "option.txt",
                ProductFileName = "product.txt",
                SpecificationFileName = "specification.txt",
                PureHierarchyFileName = "pure_hierarchy.txt",
                CsvZipFileName = $"{name}.zip",
                JsonZipFileName = $"{name}All.zip",
            };


            var runner = Variant == VariantType.HPE 
                        ? (IRunner)new HpEnterpriseImportParser(config) 
                        : (IRunner)new HpIncImportParser(config);
            await runner.Execute();
        }

        private string FindDeliveryFile(string importPath) {
            var files = Directory.EnumerateFiles(importPath, "delivery_*.xml");
            if (files.Count() < 1) {
                Console.WriteLine("No delivery file!");
                throw new ArgumentException("No delivery file found!");
            }

            var first = files.First();
            return Path.GetFileName(first);
        }
    }

    internal enum VariantType {
        HPE,
        HPInc
    }
}