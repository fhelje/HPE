using System;
using System.IO;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HierarchyFile {
    public class CsvHierarchyOutputWriter2 : IDisposable {
        private readonly CsvHierarchyGenerator2 _hierarchyGenerator;
        private readonly TextWriter _hierarchyWriter;
        private readonly bool _shouldDispose = true;

        public CsvHierarchyOutputWriter2(WriterConfiguration configuration) {
            _hierarchyGenerator = new CsvHierarchyGenerator2();
            _hierarchyWriter =
                new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "pure_hierarchy.txt")));
        }

        public CsvHierarchyOutputWriter2(TextWriter writer) {
            _hierarchyGenerator = new CsvHierarchyGenerator2();
            _hierarchyWriter = writer;
            _shouldDispose = false;
        }

        public void Dispose() {
            if (_shouldDispose) {
                _hierarchyWriter?.Dispose();
            }
        }

        public void Write(HPEHierarchyNode node) {
            if (_hierarchyGenerator.TryGenerateLine(node, out var line)) {
                _hierarchyWriter.Write(line);
            }
        }
    }
}