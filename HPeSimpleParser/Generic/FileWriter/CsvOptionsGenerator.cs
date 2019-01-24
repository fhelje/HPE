using System;
using System.Text;
using HPeSimpleParser.Generic.Model;

namespace HPeSimpleParser.Generic.FileWriter
{
    public class CsvOptionsGenerator : ICsvGenerator<Options>
    {
        private readonly StringBuilder _sb;

        public CsvOptionsGenerator()
        {
            _sb = new StringBuilder();
        }
        public bool TryGenerateLine(Options item, out string line )
        {
            if (item.Items == null || item.Items.Count == 0) {
                line = null;
                return false;
            }
            _sb.Clear();

            _sb.Append(item.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            for (int i = 0; i < item.Items.Count; i++)
            {

                var option = item.Items[i];
                _sb.Append(option.PartNumber);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(option.Name ?? string.Empty);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(option.GroupId ?? string.Empty);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(option.GroupName ?? string.Empty);
                if (i < item.Items.Count - 1)
                {
                    _sb.Append(FileSeparators.MultiColumnColumnRowSeparator);
                }
            }

            _sb.Append(Environment.NewLine);
            line = _sb.ToString();
            return true;

        }
    }
}
