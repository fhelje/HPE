using System;
using System.Collections.Generic;
using System.Text;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter {
    public class CsvLinkGenerator : ICsvGenerator<Link> {
        private readonly DefaultObjectPool<StringBuilder> _pool;

        public CsvLinkGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }

        public bool TryGenerateLine(Link item, out string line) {
            if (string.IsNullOrEmpty(item.PdfLinkDataSheet)
                && string.IsNullOrEmpty(item.PdfLinkManual)
                && item.Images.Count == 0) {
                line = null;
                return false;
            }

            var sb = _pool.Get();

            var arr = item.Images ?? new List<Image>();
            sb.Append(item.PartnerPartNumber);
            AddInnerLineData(item, sb, arr);

            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;
        }

        private static void AddInnerLineData(Link item, StringBuilder sb, List<Image> arr) {
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(item.PdfLinkDataSheet);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(item.PdfLinkManual);
            sb.Append(FileSeparators.ColumnSeparator);
            for (var i = 0; i < arr.Count; i++) {
                var image = arr[i];
                sb.Append(image.Url);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(image.Height.ToStringWithEmptyIfNull());
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(image.Width.ToStringWithEmptyIfNull());
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(image.ContentType ?? string.Empty);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(image.Title ?? string.Empty);
                if (i < arr.Count - 1) {
                    sb.Append(FileSeparators.MultiColumnColumnRowSeparator);
                }
            }

            sb.Append(Environment.NewLine);
        }
    }
}