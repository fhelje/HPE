using System;
using System.IO;
using System.Linq;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public static class FileHelpers {
        public static void DeleteExistingFiles(FileTypes fileTypes, WriterConfiguration config) {
            DeleteFile(FileTypes.Detail, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.DetailFileName));
            DeleteFile(FileTypes.Link, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.LinkFileName));
            DeleteFile(FileTypes.Marketing, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.MarketingFileName));
            DeleteFile(FileTypes.Option, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.OptionFileName));
            DeleteFile(FileTypes.Product, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.ProductFileName));
            DeleteFile(FileTypes.Specification, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.SpecificationFileName));
            DeleteFile(FileTypes.Pure_Hierarchy, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.PureHierarchyFileName));
            DeleteFile(FileTypes.Supplier, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.SupplierFileName));
            DeleteFile(FileTypes.Json, fileTypes,
                Path.Combine(config.OutputPath, config.CsvDirectory, config.JsonFileName));
        }

        public static void DeleteIfExists(string path) {
            if (File.Exists(path)) {
                File.Delete(path);
            }
        }

        public static void DeleteFile(FileTypes fileTypeToRemove, FileTypes fileTypes, string path) {
            if ((fileTypes & fileTypeToRemove) == 0) {
                return;
            }

            if (!File.Exists(path)) {
                return;
            }

            File.Delete(path);
        }

        public static string FindDeliveryFile(string importPath) {
            var files = Directory.EnumerateFiles(importPath, "delivery_*.xml");
            var fileList = files.ToList();
            if (fileList.Count == 0) {
                Console.WriteLine("No delivery file!");
                throw new ArgumentException("No delivery file found!");
            }

            var first = fileList[0];
            return Path.GetFileName(first);
        }
    }
}