using HPeSimpleParser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPeSimpleParser
{
    public class CsvSpecificationsGenerator : ICsvGenerator<Specifications>
    {
        private readonly StringBuilder _sb;

        public CsvSpecificationsGenerator()
        {
            _sb = new StringBuilder();
        }
        public string GenerateLine(Specifications item)
        {
            _sb.Clear();

            var arr = item.Items ?? new List<Specification>();
            _sb.Append(item.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            for (int i = 0; i < arr.Count; i++)
            {

                var specification = arr[i];
                _sb.Append(specification.Type);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(specification.Name);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(specification.Value);

                if (specification.Type == SpecificationType.Full)
                {
                    _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                    _sb.Append(specification.UnitOfMeasure);
                    _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                    _sb.Append(specification.Id);
                    _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                    _sb.Append(specification.GroupId);
                    _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                    _sb.Append(specification.GroupName);
                }
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