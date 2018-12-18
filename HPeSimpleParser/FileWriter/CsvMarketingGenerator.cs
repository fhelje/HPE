using System;
using System.Text;

namespace HPeSimpleParser
{
    public class CsvMarketingGenerator : ICsvGenerator<Model.Marketing>
    {
        private readonly StringBuilder _sb;

        public CsvMarketingGenerator()
        {
            _sb = new StringBuilder();
        }

        public string GenerateLine(Model.Marketing marketing)
        {
            _sb.Clear();

            _sb.Append(marketing.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(marketing.MarketingCode);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(marketing.MarketingText);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(marketing.LanguageId);

            _sb.Append(Environment.NewLine);
            return _sb.ToString();

        }
    }
}