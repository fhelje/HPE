using System;
using System.Text;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HierarchyFile {
    public class CsvHierarchyGenerator : ICsvGenerator<HierarchyNode> {
        private readonly StringBuilder _sb;

        public CsvHierarchyGenerator() {
            _sb = new StringBuilder();
        }

        public bool TryGenerateLine(HierarchyNode item, out string line) {
            _sb.Clear();

            _sb.Append("PureHierarchy");
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.PartnerHierarchyCode);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.CategoryID);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.CategoryName);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.ParentCategoryID);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.Level);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(Environment.NewLine);
            line = _sb.ToString();
            return true;
        }
    }
}