using FluentAssertions;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser.State;
using Xunit;
namespace HPeSimpleParser.Test {
    public class DetailParserTests {
        // "INPUT" W, "Enhet"
        [Theory]
        [InlineData("0.45 kg", 0.45, WeightUnitOfMeasure.Kilogram)]
        [InlineData("13.04 kg minimum 16.27 kg maximum", 16.27, WeightUnitOfMeasure.Kilogram)]
        [InlineData("14.76 kg (minimum)", 14.76, WeightUnitOfMeasure.Kilogram)]
        [InlineData("7.73 kg with two power supply FRUs, without transceivers", 7.73, WeightUnitOfMeasure.Kilogram)]
        [InlineData("800 g", 800, WeightUnitOfMeasure.Gram)]
        [InlineData("27.2 g", 27.2, WeightUnitOfMeasure.Gram)]
        [InlineData("975.2 gm", 975.2, WeightUnitOfMeasure.Gram)]
        [InlineData("< 2 kg", 2, WeightUnitOfMeasure.Kilogram)]
        [InlineData("8 Kg", 8, WeightUnitOfMeasure.Kilogram)]
        [InlineData("165 - 272 g", 272, WeightUnitOfMeasure.Gram)]
        [InlineData("1.497kg", 1.497, WeightUnitOfMeasure.Kilogram)]
        [InlineData("0.06 lb", 0.06, WeightUnitOfMeasure.Pounds)]
        [InlineData("1.1 lb", 1.1, WeightUnitOfMeasure.Pounds)]
        [InlineData("3 lb", 3, WeightUnitOfMeasure.Pounds)]
        [InlineData("28.5 oz", 28.5, WeightUnitOfMeasure.Ounces)]
        [InlineData("0.3 lb", 0.3, WeightUnitOfMeasure.Pounds)]
        [InlineData("0.8 lbs", 0.8, WeightUnitOfMeasure.Pounds)]
        [InlineData("135 lbs (ES3 Expansion Shelf: 115 lbs)", 115, WeightUnitOfMeasure.Pounds)]
        [InlineData("27.1 lb to 30.8 lb, model dependent", 30.8, WeightUnitOfMeasure.Pounds)]
        [InlineData("about 30 lb", 30, WeightUnitOfMeasure.Pounds)]
        [InlineData(".4 lb", 0.4, WeightUnitOfMeasure.Pounds)]
        [InlineData("<1 lb", 1, WeightUnitOfMeasure.Pounds)]
        [InlineData("R0P12A - 16.7lb R0P13A - 17.9lb R0P14A -19.1 lb", 19.1, WeightUnitOfMeasure.Pounds)]
        [InlineData("Cooling Unit: 1423 lb; 42U rack: 395 lb; 48U rack 395 lb", 395, WeightUnitOfMeasure.Pounds)]
        [InlineData("Max 36 lb; Min 27.8 lb", 27.8, WeightUnitOfMeasure.Pounds)]
        [InlineData("0. 24 lb", 0.24, WeightUnitOfMeasure.Pounds)]
        [InlineData("0. 4 lb", 0.4, WeightUnitOfMeasure.Pounds)]
        [InlineData("1014 lbs", 1014, WeightUnitOfMeasure.Pounds)]
        [InlineData("102 lbs", 102, WeightUnitOfMeasure.Pounds)]
        public void ParseWeight(string input, decimal weight, WeightUnitOfMeasure unitOfMeasure) {

            var (actualWeight, uom) = DetailValueParser.TryParseWeight(input);
            actualWeight.Should().Be(weight);
            uom.Should().Be(unitOfMeasure);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Any input not including weight")]
        [InlineData("dependent on model")]
        public void ParseInvalidWeight(string input) {

            var (actualWeight, uom) = DetailValueParser.TryParseWeight(input);
            actualWeight.Should().Be(null);
            uom.Should().Be(WeightUnitOfMeasure.None);
        }

        [Theory]
        [InlineData("(WxDxH) 150 x 340 x 210 mm", 210, 150, 340, DimensionUnitOfMeasure.Millimeter)]
        [InlineData("203 mm x 203 mm x 54 mm", 203, 203, 54, DimensionUnitOfMeasure.Millimeter)]
        [InlineData("15mm(L) x 11mm(W) x .95mm(H)", 0.95, 11, 15, DimensionUnitOfMeasure.Millimeter)]
        [InlineData("22.23 x 0.64 x 14.3 cm", 22.23, 0.64, 14.3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("HxWxD 4.23 x 43.61 x 43.3 cm", 4.23, 43.61, 43.3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("1.73 in. × 17.32 in. × 15.75 in (H x W x D)", 1.73, 17.32, 15.75, DimensionUnitOfMeasure.Inches)]
        [InlineData("22.4 x 12.6 x 6.5 in", 22.4, 12.6, 6.5, DimensionUnitOfMeasure.Inches)]
        [InlineData("8.75 x 0.25 x 5.63 in", 8.75, 0.25, 5.63, DimensionUnitOfMeasure.Inches)]
        [InlineData("9.5 x 0.25 x 12.5 in", 9.5, 0.25, 12.5, DimensionUnitOfMeasure.Inches)]
        [InlineData("9.8 x 8.5 x 2.5 in", 9.8, 8.5, 2.5, DimensionUnitOfMeasure.Inches)]
        [InlineData("6.13 x 7.67 x 6 in", 6.13, 7.67, 6, DimensionUnitOfMeasure.Inches)]
        [InlineData("6 x 6.75 x 6.13 in", 6, 6.75, 6.13, DimensionUnitOfMeasure.Inches)]
        [InlineData("1.70 x 17.11 x 15.05 in (HxWxD)", 1.70, 17.11, 15.05, DimensionUnitOfMeasure.Inches)]
        [InlineData("(17.5 x 23.9 x 3.44) inches", 17.5, 23.9, 3.44, DimensionUnitOfMeasure.Inches)]
        [InlineData("26x29x11 in.", 26, 29, 11, DimensionUnitOfMeasure.Inches)]
        [InlineData("0.72 x 2.86 x 0.33 in for the transceiver part", 0.72, 2.86, 0.33, DimensionUnitOfMeasure.Inches)]
        [InlineData("0.54 x 2.19 x 0.47 Inch at transceiver end", 0.54, 2.19, 0.47, DimensionUnitOfMeasure.Inches)]
        [InlineData("2.19(d) x 0.54(w) x 0.47(h) in. (5.57 x 1.38 x 1.19cm)", 0.47, 0.54, 2.19, DimensionUnitOfMeasure.Inches)]
        [InlineData("18.2 (H) x 25.51 (D) x 6.85 (W) in", 18.2, 6.85, 25.51, DimensionUnitOfMeasure.Inches)]
        [InlineData("8.75 x 0.25 x5.63 in", 8.75, 0.25, 5.63, DimensionUnitOfMeasure.Inches)]
        [InlineData("HxWxD 1.66 x 17.17 x 17.05 in", 1.66, 17.17, 17.05, DimensionUnitOfMeasure.Inches)]
        [InlineData("(WxDxH) 5.9 x 13.4 x 8.3 in", 8.3, 5.9, 13.4, DimensionUnitOfMeasure.Inches)]
        [InlineData("6.85 (H) x 25.51 (D) x 17.52 (W) in", 6.85, 17.52, 25.51, DimensionUnitOfMeasure.Inches)]
        [InlineData("\"\"W17.5\" x D26.5\" x H7\"\"\"", 7, 17.5, 26.5, DimensionUnitOfMeasure.Inches)]
        [InlineData("(LxWxH) 25.47 x 17.4 x 3.4 in", 3.4, 17.4, 25.47, DimensionUnitOfMeasure.Inches)]
        [InlineData("Width: 17.32 in; Height: 3.41 in; Depth: 24 in", 3.41, 17.32, 24, DimensionUnitOfMeasure.Inches)]
        [InlineData("1.73 in × 17.32 in × 15.75 in", 1.73, 17.32, 15.75, DimensionUnitOfMeasure.Inches)]
        [InlineData("1.73 x 17.3 x 21.6 in (H x W x D)", 1.73, 17.3, 21.6, DimensionUnitOfMeasure.Inches)]
        [InlineData("79.125-82 x 29.0 x 43.25 inches", 79.125, 29.0, 43.25, DimensionUnitOfMeasure.Inches)]
        [InlineData("1 2 3", 1, 2, 3, DimensionUnitOfMeasure.None)]
        [InlineData("1 cm 2 3", 1, 2, 3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("1 2 cm 3", 1, 2, 3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("1 2 3 cm ", 1, 2, 3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("               1 2 3 cm ", 1, 2, 3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("\t1 2 3 cm ", 1, 2, 3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("\r1 2 3 cm ", 1, 2, 3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("\n1 2 3 cm ", 1, 2, 3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("\r\n1\r\n2\r\n3\r\ncm\r\n", 1, 2, 3, DimensionUnitOfMeasure.CentiMeter)]
        [InlineData("Cooling Unit: 2,007 x 600 x 1,660 mm; 42U rack: 2,007 x 600 x 1,660 mm; 48U rack: 2,295 x 600 x 1,660 mm", 2007, 600, 1660, DimensionUnitOfMeasure.Millimeter)]
        [InlineData("17.4 (H/4U) x 64.8 (D) x 44.5 (W) cm", 17.4, 44.5, 64.8, DimensionUnitOfMeasure.CentiMeter)]
        // Questionable!: Should probably take last 3 but takes 1, 2, 4
        [InlineData("2 x 8.3 x 10.6 x 3.4 cm", 2, 8.3, 3.4, DimensionUnitOfMeasure.CentiMeter)] 
        public void ParseDimension(string input, decimal height, decimal width, decimal depth, DimensionUnitOfMeasure uom) {
            var dim = DetailValueParser.ParseDimensions(input);
            dim.UnitOfMeasure.Should().Be(uom);
            dim.Height.Should().Be(height);
            dim.Width.Should().Be(width);
            dim.Depth.Should().Be(depth);
        }


        [Theory]
        [InlineData("17.4 (H/4U) x 64.8 (D) x 44.5 (W) cm", 17.4, 44.5, 64.8, DimensionUnitOfMeasure.CentiMeter)]
        //[InlineData("3 m cable", null, null, null, "null")]
        public void ParseDimensionDebug(string input, decimal height, decimal width, decimal depth, DimensionUnitOfMeasure uom) {
            var dim = DetailValueParser.ParseDimensions(input);
            dim.UnitOfMeasure.Should().Be(uom);
            dim.Height.Should().Be(height);
            dim.Width.Should().Be(width);
            dim.Depth.Should().Be(depth);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("1")]
        [InlineData("1, 3")]
        [InlineData("1, 3 cm")]
        [InlineData("dependent on model")]
        //[InlineData("79.125-82 x 29.0 x 43.25 inches")]
        [InlineData("3 m cable")]
        public void ParseDimensionBadInput(string input) {
            var dim = DetailValueParser.ParseDimensions(input);
            dim.UnitOfMeasure.Should().Be(DimensionUnitOfMeasure.None);
            dim.Height.Should().Be(null);
            dim.Width.Should().Be(null);
            dim.Depth.Should().Be(null);
        }


    }



}
