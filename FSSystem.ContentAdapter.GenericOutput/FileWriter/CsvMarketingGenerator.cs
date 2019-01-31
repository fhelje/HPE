using System;
using System.Text;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter {
    public class CsvMarketingGenerator : ICsvGenerator<Marketing> {
        private readonly DefaultObjectPool<StringBuilder> _pool;

        public CsvMarketingGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }

        public bool TryGenerateLine(Marketing item, out string line) {
            if (string.IsNullOrEmpty(item.MarketingText)) {
                line = null;
                return false;
            }

            var sb = _pool.Get();

            sb.Append(item.PartnerPartNumber);
            AddInnerLineData(item, sb);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;
        }

        private static void AddInnerLineData(Marketing marketing, StringBuilder sb) {
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(marketing.MarketingCode);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append((int) marketing.MarketingType);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(marketing.MarketingText);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(marketing.MarketingTitle);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(marketing.LanguageId);

            sb.Append(Environment.NewLine);
        }
    }
}