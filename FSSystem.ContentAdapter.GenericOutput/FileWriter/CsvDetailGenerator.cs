using System;
using System.Text;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter {
    public class CsvDetailGenerator : ICsvGenerator<Detail> {
        private readonly DefaultObjectPool<StringBuilder> _pool;

        public CsvDetailGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }

        public bool TryGenerateLine(Detail item, out string line) {
            var sb = _pool.Get();

            sb.Append(item.PartnerPartNumber);
            AddInnerLineData(item, sb);
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;
        }

        public bool TryGenerateLine(Detail detail, string[] variants, Func<int, char[]> func) {
            var sb = _pool.Get();

            AddInnerLineData(detail, sb);
            var tempLine = sb.ToString();
            sb.Clear();
            foreach (var variant in variants) {
                sb.Append(detail.PartnerPartNumber);
                if (!string.IsNullOrEmpty(variant)) {
                    sb.Append("#");
                    sb.Append(variant);
                }

                sb.Append(tempLine);
            }

            sb.CopyTo(0, func(sb.Length), sb.Length);
            _pool.Return(sb);
            return true;
        }

        private static void AddInnerLineData(Detail detail, StringBuilder sb) {
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.Weight.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.WeightwithPackage.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.Volume.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.PalletSize.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.Width.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.Height.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.Depth.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.PackQty.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.MinimumOrderQty.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.IsRequireSerialNumber.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.ManufacturingCountry);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.CustomsStatisticsNumber);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.ExtendedWarranty.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.Unspsc.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.EndOfSupport.ToIso8601SecondsOnly());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.ErpAltPartNumber);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.TeleSalesFlag.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.ItemDefFulfillSource);
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.MeterEnabled.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.SwedishChemicalTaxReduction.ToStringWithEmptyIfNull());
            sb.Append(FileSeparators.ColumnSeparator);
            sb.Append(detail.WarrantyTime.ToStringWithEmptyIfNull());

            sb.Append(Environment.NewLine);
        }
    }
}