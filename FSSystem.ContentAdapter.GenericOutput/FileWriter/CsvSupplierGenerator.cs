using System;
using System.Text;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter {
    public class CsvSupplierGenerator : ICsvGenerator<SupplierNode> {
        private readonly StringBuilder _sb;

        public CsvSupplierGenerator() {
            _sb = new StringBuilder();
        }

        public bool TryGenerateLine(SupplierNode item, out string line) {
            _sb.Clear();

            _sb.Append(item.Code);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.Name);
            _sb.Append(FileSeparators.ColumnSeparator);

            _sb.Append(Environment.NewLine);
            line = _sb.ToString();
            return true;
        }
    }
}