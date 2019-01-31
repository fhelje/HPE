using System;
using System.Text;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HierarchyFile {
    public class CsvHierarchyGenerator2 : ICsvGenerator<HPEHierarchyNode>{
        private readonly StringBuilder _sb;

        public CsvHierarchyGenerator2()
        {
            _sb = new StringBuilder();
        }
        
        public bool TryGenerateLine(HPEHierarchyNode item, out string line) {
            _sb.Clear();

            _sb.Append("PureHierarchy");
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.PartnerHierarchyCode);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.Id);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.Name);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.ParentCategoryId);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(item.Level);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(Environment.NewLine);
            line = _sb.ToString();
            return true;
        }

        public bool TryGenerateLine(HPEHierarchyNode item, string[] variants, Func<int, char[]> func) {
            throw new NotImplementedException();
        }
    }
}