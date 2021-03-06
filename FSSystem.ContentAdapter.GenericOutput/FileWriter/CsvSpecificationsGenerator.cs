﻿using System;
using System.Linq;
using System.Text;
using FSSystem.ContentAdapter.Model;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.GenericOutput.FileWriter {
    public class CsvSpecificationsGenerator : ICsvGenerator<Specifications> {
        private readonly DefaultObjectPool<StringBuilder> _pool;

        public CsvSpecificationsGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }

        public bool TryGenerateLine(Specifications item, out string line) {
            if (item.Items == null || item.Items.Count == 0) {
                line = null;
                return false;
            }

            var sb = _pool.Get();

            sb.Append(item.PartnerPartNumber);
            AddInnerLineData(item, sb);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;
        }

        private static void AddInnerLineData(Specifications item, StringBuilder sb) {
            sb.Append(FileSeparators.ColumnSeparator);
            // Todo can we do this more efficient! By not using linq
            var groupedSpecs = item.Items.GroupBy(x => x.Label).ToArray();
            for (var i = 0; i < groupedSpecs.Length; i++) {
                var group = groupedSpecs[i];
                var value = string.Join(", ", group.Select(x => x.Value));
                var specification = groupedSpecs[i].First();
                sb.Append(specification.Type);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(specification.Label);
                sb.Append(FileSeparators.MultiColumnColumnSeparator);
                sb.Append(value);

                if (specification.Type == SpecificationType.Full) {
                    sb.Append(FileSeparators.MultiColumnColumnSeparator);
                    sb.Append(specification.UnitOfMeasure);
                    sb.Append(FileSeparators.MultiColumnColumnSeparator);
                    sb.Append(specification.Id);
                    sb.Append(FileSeparators.MultiColumnColumnSeparator);
                    sb.Append(specification.GroupId);
                    sb.Append(FileSeparators.MultiColumnColumnSeparator);
                    sb.Append(specification.GroupName);
                }

                if (i < groupedSpecs.Length - 1) {
                    sb.Append(FileSeparators.MultiColumnColumnRowSeparator);
                }
            }

            sb.Append(Environment.NewLine);
        }
    }
}