using System;
using System.Text;

namespace HPeSimpleParser.Generic.FileWriter
{
    public class CsvMarketingGenerator : ICsvGenerator<Model.Marketing>
    {
        private readonly StringBuilder _sb;

        public CsvMarketingGenerator()
        {
            _sb = new StringBuilder();
        }

        public bool TryGenerateLine(Model.Marketing marketing, out string line)
        {
            if (string.IsNullOrEmpty(marketing.MarketingText)) {
                line = null;
                return false;
            }
            _sb.Clear();

            _sb.Append(marketing.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(marketing.MarketingCode);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append((int)marketing.MarketingType);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(marketing.MarketingText);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(marketing.MarketingTitle);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(marketing.LanguageId);

            _sb.Append(Environment.NewLine);
            line = _sb.ToString();
            return true;

        }
    }
}