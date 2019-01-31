using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using FSSystem.ContentAdapter.GenericOutput.FileWriter;
using FSSystem.ContentAdapter.HPEAndHPInc.Enums;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public abstract class Runner : IRunner {
        public abstract Task Execute();

        protected async Task Import(WriterConfiguration config, IParserDefinition parserDefinition,
            VariantType variant) {
            var sw = new Stopwatch();
            sw.Start();
            const FileTypes fileTypes = FileTypes.Detail
                                        | FileTypes.Link
                                        | FileTypes.Marketing
                                        | FileTypes.Option
                                        | FileTypes.Option
                                        | FileTypes.Product
                                        | FileTypes.Specification
                                        | FileTypes.Supplier
                                        | FileTypes.Json;

            FileHelpers.DeleteExistingFiles(fileTypes, config);
            Console.WriteLine("Deleting files");

            var (pipeline, targetTask) = new PipelineCreator(parserDefinition, config, variant).CreatePipeline();
            await pipeline.SendAsync(config).ConfigureAwait(false);

            pipeline.Complete();
            await targetTask.ContinueWith(_ => {
                sw.Stop();
                Console.WriteLine($"Done in {sw.Elapsed.ToString()}");
            }).ConfigureAwait(false);

            Console.ReadLine();
        }

        protected void VerifyPaths(WriterConfiguration config) {
            var csvOutputDir = Path.Combine(config.OutputPath, config.CsvDirectory);
            if (!Directory.Exists(csvOutputDir)) {
                Directory.CreateDirectory(csvOutputDir);
            }

            var jsonOutputDir = Path.Combine(config.OutputPath, config.JsonDirectory);
            if (!Directory.Exists(jsonOutputDir)) {
                Directory.CreateDirectory(jsonOutputDir);
            }
        }
    }
}