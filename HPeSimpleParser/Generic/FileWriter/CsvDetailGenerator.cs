using System;
using System.Text;

namespace HPeSimpleParser.Generic.FileWriter
{
    public class CsvDetailGenerator : ICsvGenerator<Model.Detail>
    {
        private readonly StringBuilder _sb;
        public CsvDetailGenerator()
        {
            _sb = new StringBuilder();
        }
        public bool TryGenerateLine(Model.Detail detail, out string line)
        {
            _sb.Clear();

            _sb.Append(detail.PartnerPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.Weight.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.WeightwithPackage.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.Volume.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.PalletSize.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.Width.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.Height.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.Depth.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.PackQty.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.MinimumOrderQty.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.IsRequireSerialNumber.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.ManufacturingCountry);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.CustomsStatisticsNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.ExtendedWarranty.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.Unspsc.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.EndOfSupport.ToIso8601SecondsOnly());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.ErpAltPartNumber);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.TeleSalesFlag.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.ItemDefFulfillSource);
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.MeterEnabled.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.SwedishChemicalTaxReduction.ToStringWithEmptyIfNull());
            _sb.Append(FileSeparators.ColumnSeparator);
            _sb.Append(detail.WarrantyTime.ToStringWithEmptyIfNull());


            _sb.Append(Environment.NewLine);
            line = _sb.ToString();
            return true;

        }
    }
}