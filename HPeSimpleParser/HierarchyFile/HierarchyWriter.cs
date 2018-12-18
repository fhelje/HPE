using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HPeSimpleParser.HierarchyFile {
    public class CsvHierarchyOutputWriter : IDisposable {
        private readonly CsvHierarchyGenerator _hierarchyGenerator;
        private readonly TextWriter _hierarchyWriter;

        public CsvHierarchyOutputWriter(WriterConfiguration configuration) {
            _hierarchyGenerator = new CsvHierarchyGenerator();
            _hierarchyWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "PureHierarchy.txt")));
        }

        public async Task WriteAsync(HierarchyNode node) {
            await _hierarchyWriter.WriteAsync(_hierarchyGenerator.GenerateLine(node));

        }

        public void Dispose() {
            _hierarchyWriter?.Dispose();
        }
    }

    public class CsvHierarchyGenerator : ICsvGenerator<HierarchyNode>{
        private readonly StringBuilder _sb;

        public CsvHierarchyGenerator()
        {
            _sb = new StringBuilder();
        }
        
        public string GenerateLine(HierarchyNode item) {
            _sb.Clear();

            _sb.Append("PureHierarchy");
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.CategoryID);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.CategoryName);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.ParentCategoryID);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.Level);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(Environment.NewLine);
            return _sb.ToString();
        }
    }
}
