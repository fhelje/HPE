using System;
using System.IO;
using System.Threading.Tasks;

namespace HPeSimpleParser.lib.Generic.FileWriter {
    public class CsvSupplierOutputWriter : IDisposable {
        private readonly CsvSupplierGenerator _supplierGenerator;
        private readonly TextWriter _supplierWriter;

        public CsvSupplierOutputWriter(WriterConfiguration configuration) {
            _supplierGenerator = new CsvSupplierGenerator();
            _supplierWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, configuration.CsvDirectory, configuration.SupplierFileName)));
        }

        public async Task WriteAsync(SupplierNode node) {
            if (_supplierGenerator.TryGenerateLine(node, out var line)) {
                await _supplierWriter.WriteAsync(line);                
            }
        }

        public void Dispose() {
            _supplierWriter?.Dispose();
        }
    }
}