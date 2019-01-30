using System;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace HPeSimpleParser.lib.Generic.FileWriter
{
    public class CsvMarketingGenerator : ICsvGenerator<Model.Marketing>
    {
        private readonly DefaultObjectPool<StringBuilder> _pool;

        public CsvMarketingGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }

        public bool TryGenerateLine(Model.Marketing marketing, out string line)
        {
            if (string.IsNullOrEmpty(marketing.MarketingText)) {
                line = null;
                return false;
            }

            var sb = _pool.Get();

            sb.Append(marketing.PartnerPartNumber);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(marketing.MarketingCode);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append((int)marketing.MarketingType);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(marketing.MarketingText);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(marketing.MarketingTitle);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(marketing.LanguageId);

            sb.Append(Environment.NewLine);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;

        }
    }
}