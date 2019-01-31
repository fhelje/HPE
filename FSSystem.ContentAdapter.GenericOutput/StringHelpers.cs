using System;
using System.Globalization;
using System.Linq;
using System.Text;
using FSSystem.ContentAdapter.GenericOutput.FileWriter;
using FSSystem.ContentAdapter.Model;

namespace FSSystem.ContentAdapter.GenericOutput {
    public static class StringHelpers {
        public static string ToTitleCase(this string s) {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
        }

        public static string ToIso8601SecondsOnly(this in DateTime date) {
            return date.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static string ToIso8601SecondsOnly(this DateTime? date) {
            return date.HasValue ? date.Value.ToString("yyyy-MM-ddTHH:mm:ss") : string.Empty;
        }

        public static string ToStringWithEmptyIfNull<T>(this T? value) where T : struct {
            return value.HasValue ? value.ToString() : string.Empty;
        }

        public static string ToStringWithEmptyIfNull(this DateTime? date) {
            return date.HasValue ? date.Value.ToString("yyyy-MM-ddTHH:mm:ss") : string.Empty;
        }

        public static string ToStringWithEmptyIfNull(this decimal? date) {
            return date.HasValue ? date.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ToStringWithEmptyIfNull(this int? date) {
            return date.HasValue ? date.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ToDebugString(this object value) {
            switch (value) {
                case null:
                    return string.Empty;
                case MarketingType mt:
                    return ((int) mt).ToString();
                case DateTime d:
                    return d.ToIso8601SecondsOnly();
                case object[] arr:
                    if (arr.Length == 0) {
                        return string.Empty;
                    }

                    switch (arr[0]) {
                        case object[] _:
                            return string.Join(FileSeparators.MultiColumnColumnRowSeparator,
                                arr.Select(x => x.ToDebugString()));
                        default:
                            return string.Join(FileSeparators.MultiColumnColumnSeparator,
                                arr.Select(x => x.ToDebugString()));
                    }
                default:
                    return value.ToString();
            }
        }

        public static string RemoveLineEndings(this string text) {
            if (text == null) {
                return text;
            }

            var newText = new StringBuilder();
            for (var i = 0; i < text.Length; i++) {
                if (text[i] == '\t') {
                    continue;
                }

                if (text[i] == '\r') {
                    continue;
                }

                if (text[i] == '\n') {
                    continue;
                }

                if (!char.IsControl(text, i)) {
                    newText.Append(text[i]);
                }
            }

            return newText.ToString();
        }
    }
}