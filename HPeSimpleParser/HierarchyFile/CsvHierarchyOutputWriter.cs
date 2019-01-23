using System;
using System.IO;
using System.Threading.Tasks;

namespace HPeSimpleParser.HierarchyFile {
    public class CsvHierarchyOutputWriter : IDisposable {
        private readonly CsvHierarchyGenerator _hierarchyGenerator;
        private readonly TextWriter _hierarchyWriter;
        private readonly bool _shouldDispose = true;

        public CsvHierarchyOutputWriter(WriterConfiguration configuration) {
            _hierarchyGenerator = new CsvHierarchyGenerator();
            _hierarchyWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "pure_hierarchy.txt")));
        }

        public CsvHierarchyOutputWriter(TextWriter writer) {
            _hierarchyGenerator = new CsvHierarchyGenerator();
            _hierarchyWriter = writer;
            _shouldDispose = false;
        }

        public async Task WriteAsync(HierarchyNode node) {
            if (_hierarchyGenerator.TryGenerateLine(node, out var line)) {
                await _hierarchyWriter.WriteAsync(line);                
            }
        }

        public void Dispose() {
            if(_shouldDispose) _hierarchyWriter?.Dispose();
        }
    }
}
