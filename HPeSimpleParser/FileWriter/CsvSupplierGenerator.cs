using System;
using System.Text;

namespace HPeSimpleParser
{
    public class CsvSupplierGenerator : ICsvGenerator<Model.Supplier>
    {
        private readonly StringBuilder _sb;

        public CsvSupplierGenerator()
        {
            _sb = new StringBuilder();
        }

        public string GenerateLine(Model.Supplier supplier)
        {
            _sb.Clear();

            _sb.Append(supplier.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(supplier.SupplierId);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(supplier.SupplierName);

            _sb.Append(Environment.NewLine);
            return _sb.ToString();

        }
    }
}