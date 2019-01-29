using System;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace HPeSimpleParser.lib.Generic.FileWriter
{
    public class CsvDetailGenerator : ICsvGenerator<Model.Detail>
    {
        private readonly DefaultObjectPool<StringBuilder> _pool;
        public CsvDetailGenerator(DefaultObjectPool<StringBuilder> pool) {
            _pool = pool;
        }
        public bool TryGenerateLine(Model.Detail detail, out string line) {
            var sb = _pool.Get();

            sb.Append(detail.PartnerPartNumber);
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
            line = sb.ToString();
            sb.Clear();
            _pool.Return(sb);
            return true;

        }
    }
}