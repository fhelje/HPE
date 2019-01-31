using System;
using System.Text;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter
{
    public class CsvOptionsGenerator : ICsvGenerator<Options>
    {
        private readonly DefaultObjectPool<StringBuilder> _pool;

        public CsvOptionsGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }
        public bool TryGenerateLine(Options data, out string line )
        {
            if (data.Items == null || data.Items.Count == 0) {
                line = null;
                return false;
            }
            var sb = _pool.Get();

            sb.Append(data.PartnerPartNumber);
            AddInnerLineData(data, sb);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;

        }

        public bool TryGenerateLine(Options data, string[] variants, Func<int, char[]> func) {
            
            if (data.Items == null || data.Items.Count == 0) {
                func(0);
                return false;
            }
            var sb = _pool.Get();

            sb.Append(data.PartnerPartNumber);
            AddInnerLineData(data, sb);
            var tempLine = sb.ToString();
            sb.Clear();
            for (var i = 0; i < variants.Length; i++) {
                sb.Append(data.PartnerPartNumber);
                if (!string.IsNullOrEmpty(variants[i])) {
                    sb.Append("#");
                    sb.Append(variants[i]);
                }

                sb.Append(tempLine.Length);
            }
            sb.CopyTo(0, func(sb.Length), sb.Length);
            sb.Clear();
            _pool.Return(sb);
            return true;
        }

        private static void AddInnerLineData(Options item, StringBuilder sb) {
            sb.Append(FileSeparators.ColumnSeparator);
            for (int i = 0; i < item.Items.Count; i++) {
                var option = item.Items[i];
                sb.Append(option.PartNumber);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(option.Name ?? string.Empty);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(option.GroupId ?? string.Empty);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(option.GroupName ?? string.Empty);
                if (i < item.Items.Count - 1) {
                    sb.Append(FileSeparators.MultiColumnColumnRowSeparator);
                }
            }

            sb.Append(Environment.NewLine);
        }
    }
}
