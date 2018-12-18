using HPeSimpleParser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPeSimpleParser
{
    public class CsvOptionsGenerator : ICsvGenerator<Options>
    {
        private readonly StringBuilder _sb;

        public CsvOptionsGenerator()
        {
            _sb = new StringBuilder();
        }
        public string GenerateLine(Options item)
        {
            _sb.Clear();

            var arr = item.Items ?? new List<Option>();
            _sb.Append(item.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            for (int i = 0; i < arr.Count; i++)
            {

                var option = arr[i];
                _sb.Append(option.PartNumber);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(option.Name ?? string.Empty);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(option.GroupId ?? string.Empty);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(option.GroupName ?? string.Empty);
                if (i < arr.Count - 1)
                {
                    _sb.Append(FileSeparators.MultiColumnColumnRowSeparator);
                }
            }

            _sb.Append(Environment.NewLine);
            return _sb.ToString();

        }
    }
}