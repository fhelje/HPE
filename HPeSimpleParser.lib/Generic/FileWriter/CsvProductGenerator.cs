using System;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter
{
    public class CsvProductGenerator : ICsvGenerator<Model.Product>
    {
        private readonly DefaultObjectPool<StringBuilder> _pool;

        public CsvProductGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }

        public bool  TryGenerateLine(Model.Product product, out string line) {
            var sb = _pool.Get();

            sb.Append(product.PartnerPartNumber);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.PartNumber);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.ManufacturerName);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.ManufacturerCode);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.CategoryID);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.CategoryName);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.PartnerHierarchyCode);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.Description);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.DescriptionLong);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.ProductCode);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.IsEol);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.ChangeDate.ToIso8601SecondsOnly());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.AlternateCategoryID);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.AlternateCategoryName);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(product.AlternatePartnerHierarchyCode);

            sb.Append(Environment.NewLine);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;

        }
    }
}