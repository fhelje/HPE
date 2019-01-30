using System;
using System.Text;
using HPeSimpleParser.lib.Generic.Model;
using Microsoft.Extensions.ObjectPool;

namespace HPeSimpleParser.lib.Generic.FileWriter
{
    public class CsvOptionsGenerator : ICsvGenerator<Options>
    {
        private readonly DefaultObjectPool<StringBuilder> _pool;

        public CsvOptionsGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }
        public bool TryGenerateLine(Options item, out string line )
        {
            if (item.Items == null || item.Items.Count == 0) {
                line = null;
                return false;
            }
            var sb = _pool.Get();

            sb.Append(item.PartnerPartNumber);
            sb.Append(FileSeparators.ColumnSeparator);
            for (int i = 0; i < item.Items.Count; i++)
            {

                var option = item.Items[i];
                sb.Append(option.PartNumber);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(option.Name ?? string.Empty);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(option.GroupId ?? string.Empty);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(option.GroupName ?? string.Empty);
                if (i < item.Items.Count - 1)
                {
                    sb.Append(FileSeparators.MultiColumnColumnRowSeparator);
                }
            }

            sb.Append(Environment.NewLine);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;

        }
    }
}
