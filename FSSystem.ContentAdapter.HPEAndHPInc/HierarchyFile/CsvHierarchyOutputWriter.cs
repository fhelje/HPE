using System;
using System.IO;
using FSSystem.ContentAdapter.GenericOutput.FileWriter;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HierarchyFile {
    public class CsvHierarchyOutputWriter : IDisposable {
        private readonly CsvHierarchyGenerator _hierarchyGenerator;
        private readonly TextWriter _hierarchyWriter;
        private readonly bool _shouldDispose = true;

        public CsvHierarchyOutputWriter(WriterConfiguration configuration) {
            _hierarchyGenerator = new CsvHierarchyGenerator();
            _hierarchyWriter =
                new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "pure_hierarchy.txt")));
        }

        public CsvHierarchyOutputWriter(TextWriter writer) {
            _hierarchyGenerator = new CsvHierarchyGenerator();
            _hierarchyWriter = writer;
            _shouldDispose = false;
        }

        public void Dispose() {
            if (_shouldDispose) {
                _hierarchyWriter?.Dispose();
            }
        }

        public void Write(HierarchyNode node) {
            if (_hierarchyGenerator.TryGenerateLine(node, out var line)) {
                _hierarchyWriter.Write(line);
            }
        }
    }
}