using System;
using System.Text;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter
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
            AddInnerLineData(marketing, sb);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;

        }

        public bool TryGenerateLine(Marketing data, string[] variants, Func<int, char[]> func) {
            if (string.IsNullOrEmpty(data.MarketingText)) {
                func(0);
                return false;
            }

            var sb = _pool.Get();

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