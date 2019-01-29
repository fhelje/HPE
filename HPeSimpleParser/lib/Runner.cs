﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using HPeSimpleParser.lib.Generic.FileWriter;
using HPeSimpleParser.lib.Parser;

namespace HPeSimpleParser.lib {
    public abstract class Runner : IRunner {
        public abstract Task Execute();
        protected async Task Import(WriterConfiguration config, IParserDefinition parserDefinition) {
            var sw = new Stopwatch();
            sw.Start();
            var fileTypes = FileTypes.Detail | FileTypes.Link | FileTypes.Marketing | FileTypes.Option | FileTypes.Option |
                            FileTypes.Product | FileTypes.Specification | FileTypes.Supplier | FileTypes.Json;

            FileHelpers.DeleteExistingFiles(fileTypes, config);
            Console.WriteLine("Deleting files");

            var (pipeline, targetTask) = new PipelineCreator(parserDefinition, config).CreatePipeline();
            await pipeline.SendAsync(config);

            pipeline.Complete();
            await targetTask.ContinueWith(x =>
            {
                sw.Stop();
                Console.WriteLine($"Done in {sw.Elapsed.ToString()}");
            });

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