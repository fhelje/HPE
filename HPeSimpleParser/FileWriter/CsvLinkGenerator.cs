using HPeSimpleParser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPeSimpleParser
{
    public class CsvLinkGenerator : ICsvGenerator<Model.Link>
    {
        private readonly StringBuilder _sb;

        public CsvLinkGenerator()
        {
            _sb = new StringBuilder();
        }
        public string GenerateLine(Model.Link data)
        {
            _sb.Clear();

            var arr = data.Images ?? new List<Image>();
            _sb.Append(data.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(data.PdfLinkDataSheet);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(data.PdfLinkManual);
            _sb.Append(FileSeparators.ColumnSeparator);
            for (int i = 0; i < arr.Count; i++)
            {
                var image = arr[i];
                _sb.Append(image.Url);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(image.Height.ToStringWithEmptyIfNull());
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(image.Width.ToStringWithEmptyIfNull());
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(image.ContentType ?? string.Empty);
                _sb.Append(FileSeparators.MultiColumnColumnSeparator);
                _sb.Append(image.Title ?? string.Empty);
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