using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HPeSimpleParser.Parser {
    public class DetailValueParser {
        private static readonly Lazy<Regex> dimensionRegex = new Lazy<Regex>(new Regex(@"([(]*(?<dim1>l|h|d|w)\s*x\s*(?<dim2>l|h|d|w)\s*x\s*(?<dim3>l|h|d|w)[)]*)*\s*
([(]+(?<dim>\w*)[)]+|(?<dim>([WDH]|Width:\s*|Height:\s*|Depth:\s*)))*
(?<measure>[-]*\d*[\.,\,]?\d+)\s*
[)]*\s*(?<uom>cm|mm|m|in|inch|inches|"")*
([(]+(?<dim>\w*|\w*\/\d*\w*)[)]+)*
\s*([(]*(?<dim1>l|h|d|w)\s*x\s*(?<dim2>l|h|d|w)\s*x\s*(?<dim3>l|h|d|w)[)]*)*
\s*(?<uom>cm|mm|m|in|inch|inches|"")*", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant));
        private static readonly Lazy<Regex> weightRegex = new Lazy<Regex>(new Regex(@"(?<weight>\d*\.?\d+)\s*(?<unitOfMeasure>lbs?|oz?|kg?|g?)", RegexOptions.Compiled | RegexOptions.IgnoreCase));

        public static (decimal? weight, WeightUnitOfMeasure uom) TryParseWeight(string input) {
            try {
                if (input is null) {
                    return (null, WeightUnitOfMeasure.None);
                }
                var cleanedInput = CleanedInput(input);
                var rx = weightRegex.Value;
                // Find matches.
                var matches = rx.Matches(cleanedInput);
                var m = matches.LastOrDefault();
                if (m == null) {
                    return (null, WeightUnitOfMeasure.None);
                }
                var weight = m.Groups["weight"];
                var unitOfMeasure = m.Groups["unitOfMeasure"];
                var actualWeight = decimal.Parse(weight?.Value ?? "", NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
                return (actualWeight, GetWeightUnitOfMeasure(unitOfMeasure?.Value ?? ""));
            }
            catch (Exception ex) {
                Console.WriteLine("Unable to parse input: " + input);
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static Dimension ParseDimensions(string input) {
            try {

                //            var rgx = @"([(]*(?<dim1>l|h|d|w)\s*x\s*(?<dim2>l|h|d|w)\s*x\s*(?<dim3>l|h|d|w)[)]*)*\s*
                //([(]+(?<dim>\w*)[)]+|(?<dim>([WDH]|Width:\s*|Height:\s*|Depth:\s*)))*
                //(?<measure>\d*\.?\d+)\s*
                //\s*(?<uom>cm|mm|m|in|inch|inches|"")*
                //([(]+(?<dim>\w*)[)]+)*
                //\s*([(]*(?<dim1>l|h|d|w)\s*x\s*(?<dim2>l|h|d|w)\s*x\s*(?<dim3>l|h|d|w)[)]*)*
                //";
                if (string.IsNullOrEmpty(input)) {
                    return new Dimension(null, null, null, DimensionUnitOfMeasure.None);
                }
                var rx = dimensionRegex.Value;
                var matches = rx.Matches(input);

                if (matches.Count < 3) {
                    return new Dimension(null, null, null, DimensionUnitOfMeasure.None);
                }

                var measures = new string[3];
                var uom = "";
                var dims = new[] { "", "", "" };
                var dimsFound = new[] { false, false, false };
                var foundDim = false;
                var index = 0;

                var count = matches.Count;
                for (int i = 0; i < count; i++) {
                    var measureFound = false;
                    var measure = matches[i].Groups["measure"]?.Value;
                    if (!string.IsNullOrEmpty(measure) && !measure.StartsWith("-")) {
                        measures[index] = measure.Replace(",", "");
                        measureFound = true;
                    }

                    var u = matches[i].Groups["uom"]?.Value;
                    if (!string.IsNullOrEmpty(u)) {
                        uom = u;
                    }
                    var d1 = matches[i].Groups["dim1"]?.Value;
                    if (!string.IsNullOrEmpty(d1) && !dimsFound[0]) {
                        dims[0] = d1;
                        dimsFound[0] = true;
                        foundDim = true;
                        measureFound = true;
                    }
                    var d2 = matches[i].Groups["dim2"]?.Value;
                    if (!string.IsNullOrEmpty(d1) && !dimsFound[1]) {
                        dims[1] = d2;
                        dimsFound[1] = true;
                        foundDim = true;
                        measureFound = true;
                    }
                    var d3 = matches[i].Groups["dim3"]?.Value;
                    if (!string.IsNullOrEmpty(d3) && !dimsFound[2]) {
                        dims[2] = d3;
                        dimsFound[2] = true;
                        foundDim = true;
                        measureFound = true;
                    }
                    if (!foundDim && !dimsFound[index]) {
                        var dimString = matches[i].Groups["dim"]?.Value;

                        if (!string.IsNullOrEmpty(dimString) && !dimsFound[2]) {
                            dims[index] = dimString;
                            dimsFound[index] = true;
                            measureFound = true;
                        }
                    }
                    if (!measureFound) {
                        continue;
                    }
                    if (index < 2) index++;
                    if (FoundAll(measures, dimsFound, uom)) {
                        break;
                    }
                }
                var height = GetHeight(dims, measures);
                var width = GetWidth(dims, measures);
                var depth = GetDepth(dims, measures);
                var unitOfMeasure = GetDistansUnitOfMeasure(uom);
                return new Dimension(height, width, depth, unitOfMeasure);
            }
            catch (Exception ex) {
                Console.WriteLine("Unable to parse input: " + input);
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        private static bool FoundAll(string[] measures, bool[] dimsFound, string uom) {
            if (string.IsNullOrEmpty(measures[0]) && !dimsFound[0]) {
                return false;
            }
            if (string.IsNullOrEmpty(measures[1]) && !dimsFound[1]) {
                return false;
            }
            if (string.IsNullOrEmpty(measures[2]) && !dimsFound[2]) {
                return false;
            }
            if (string.IsNullOrEmpty(uom)) {
                return false;
            }
            return true;
        }

        private static DimensionUnitOfMeasure GetDistansUnitOfMeasure(string uom) {
            switch (uom.ToLower()) {
                case "mm":
                    return DimensionUnitOfMeasure.Millimeter;
                case "cm":
                    return DimensionUnitOfMeasure.CentiMeter;
                case "m":
                    return DimensionUnitOfMeasure.Meter;
                case "in":
                case "inch":
                case "inches":
                case "\"":
                    return DimensionUnitOfMeasure.Inches;
            }
            return DimensionUnitOfMeasure.None;
        }

        private static decimal? GetHeight(string[] dims, string[] measures) {
            var pos = 0;
            for (int i = 0; i < dims.Length; i++) {
                var d = dims[i].ToLower();
                switch (d) {
                    case "h":
                    case "h/1u":
                    case "h/2u":
                    case "h/4u":
                    case "h/8u":
                    case "height: ":
                        pos = i;
                        break;
                }
            }
            if (decimal.TryParse(measures[pos], NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var retVal)) {
                return retVal;
            }
            return null;
        }

        private static decimal? GetWidth(string[] dims, string[] measures) {
            var pos = 1;
            for (int i = 0; i < dims.Length; i++) {
                var d = dims[i].ToLower();
                switch (d) {
                    case "w":
                    case "w/1u":
                    case "w/2u":
                    case "w/4u":
                    case "w/8u":
                    case "width: ":
                        pos = i;
                        break;
                }
            }
            if (decimal.TryParse(measures[pos], NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var retVal)) {
                return retVal;
            }
            return null;
        }

        private static decimal? GetDepth(string[] dims, string[] measures) {
            var pos = 2;
            for (int i = 0; i < dims.Length; i++) {
                var d = dims[i].ToLower();
                switch (d) {
                    case "d":
                    case "d/1u":
                    case "d/2u":
                    case "d/4u":
                    case "d/8u":
                    case "l":
                    case "depth: ":
                        pos = i;
                        break;
                }
            }
            if (decimal.TryParse(measures[pos], NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var retVal)) {
                return retVal;
            }
            return null;
        }

        private static string CleanedInput(string input) {
            var sb = new StringBuilder();
            var state = CleanState.None;
            for (int i = 0; i < input.Length; i++) {
                if (input[i] == '.') {
                    state = CleanState.Dot;
                }
                if (state == CleanState.Dot && input[i] == ' ') {
                    continue;
                }
                sb.Append(input[i]);
            }
            return sb.ToString();
        }

        private enum CleanState {
            None,
            Dot
        }

        private static WeightUnitOfMeasure GetWeightUnitOfMeasure(string uom) {
            switch (uom.ToLower()) {
                case "kg":
                    return WeightUnitOfMeasure.Kilogram;
                case "g":
                case "gram":
                    return WeightUnitOfMeasure.Gram;
                case "lb":
                case "lbs":
                    return WeightUnitOfMeasure.Pounds;
                case "oz":
                    return WeightUnitOfMeasure.Ounces;
                default:
                    return WeightUnitOfMeasure.None;
            }
        }

    }
}
