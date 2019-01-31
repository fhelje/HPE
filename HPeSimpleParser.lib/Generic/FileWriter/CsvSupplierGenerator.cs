using System;
using System.Text;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter
{
    public class CsvSupplierGenerator : ICsvGenerator<SupplierNode>
    {
        private readonly StringBuilder _sb;

        public CsvSupplierGenerator()
        {
            _sb = new StringBuilder();
        }

        public bool TryGenerateLine(SupplierNode supplier, out string line)
        {
            _sb.Clear();

            _sb.Append(supplier.Code);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(supplier.Name);
            _sb.Append(FileSeparators.ColumnSeparator);

            _sb.Append(Environment.NewLine);
            line = _sb.ToString();
            return true;
        }

        public bool TryGenerateLine(SupplierNode supplier, string[] variants, Func<int, char[]> func) {
            _sb.Clear();

            _sb.Append(supplier.Code);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(supplier.Name);
            _sb.Append(FileSeparators.ColumnSeparator);

            _sb.Append(Environment.NewLine);
            _sb.CopyTo(0, func(_sb.Length), _sb.Length);
            return true;
        }
    }
}