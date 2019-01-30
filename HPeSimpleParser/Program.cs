using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HPeSimpleParser.lib;
using HPeSimpleParser.lib.Enums;
using HPeSimpleParser.lib.Generic.FileWriter;
using McMaster.Extensions.CommandLineUtils;

namespace HPeSimpleParser {
    [Command(Name = "HPImport", Description = "Import HPE and HP Inc products")]
    [HelpOption("-?")]
    class Program {
        //\\prod01\e$\FS\Tillverkare\HPe\CAP\Change fil 2018-11-25\HPE_cap_gb_en_xml_2617_20181125114722\pad_en_gb_global_product_L_JL380A_1009690316.xml
        // TODO! vvvvvvv
        [Option("-v")]
        [Required]
        public VariantType Variant { get; set; }

        [Option("-i")]
        [Required]
        public string ImportDirectory { get; set; }

        [Option("-o")]
        [Required]
        public string OutputDirectory { get; set; }

        public static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

        public async Task OnExecuteAsync() {
            var name = Variant.ToString();
            var deliveryFile = FileHelpers.FindDeliveryFile(ImportDirectory);

            var config = new WriterConfiguration {
                ImportPath = ImportDirectory,
                DeliveryFile = deliveryFile,
                OutputPath = OutputDirectory,
                CsvZipFileName = $"{name}.zip",
                JsonZipFileName = $"{name}All.zip",
            };

            var runner = Variant == VariantType.HPE 
                        ? (IRunner)new HpEnterpriseImportParser(config) 
                        : (IRunner)new HpIncImportParser(config);
            await runner.Execute();
        }
    }

}