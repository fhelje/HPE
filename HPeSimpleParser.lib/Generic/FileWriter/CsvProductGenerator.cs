using System;
using System.Text;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
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
            AddInnerLineData(product, sb);

            sb.Append(Environment.NewLine);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;

        }

        public bool TryGenerateLine(Product data, string[] variants, Func<int, char[]> func) {
            var sb = _pool.Get();

            AddInnerLineData(data, sb);
            var tempLine = sb.ToString();
            sb.Clear();
            foreach (var variant in variants) {
                sb.Append(data.PartnerPartNumber);
                if (!string.IsNullOrEmpty(variant)) {
                    sb.Append("#");
                    sb.Append(variant);
                }
                sb.Append(tempLine);
            }

            sb.CopyTo(0, func(sb.Length), sb.Length);
            _pool.Return(sb);
            return true;
            
        }

        private static void AddInnerLineData(Product product, StringBuilder sb) {
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
        }
    }
}