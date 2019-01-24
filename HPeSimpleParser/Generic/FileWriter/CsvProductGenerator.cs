using System;
using System.Text;

namespace HPeSimpleParser.Generic.FileWriter
{
    public class CsvProductGenerator : ICsvGenerator<Model.Product>
    {
        private readonly StringBuilder _sb;

        public CsvProductGenerator()
        {
            _sb = new StringBuilder();
        }

        public bool  TryGenerateLine(Model.Product product, out string line)
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
            _sb.Append(product.PartnerHierarchyCode);
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
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.AlternateCategoryID);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.AlternateCategoryName);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(product.AlternatePartnerHierarchyCode);

            _sb.Append(Environment.NewLine);
            line = _sb.ToString();
            return true;

        }
    }
}