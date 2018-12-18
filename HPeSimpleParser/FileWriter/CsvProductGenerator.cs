using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HPeSimpleParser
{
    public class CsvProductGenerator : ICsvGenerator<Model.Product>
    {
        private readonly StringBuilder _sb;

        public CsvProductGenerator()
        {
            _sb = new StringBuilder();
        }

        public string GenerateLine(Model.Product product)
        {
            _sb.Clear();

            _sb.Append(product.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.PartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.ManufacturerName);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.ManufacturerCode);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.CategoryID);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.CategoryName);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.Description);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.DescriptionLong);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.ProductCode);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.IsEol);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.ChangeDate.ToIso8601SecondsOnly());

            _sb.Append(Environment.NewLine);
            return _sb.ToString();

        }
    }
}