using System;
using System.Collections.Generic;

namespace HPeSimpleParser.lib.Linq {
    public static class LinqHelpers {
        public static string GetContentType(this string url) {
            var x = url.AsSpan();
            var ending = x.Slice(x.Length - 3);
            return ending.ToString();
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source) {
                if (seenKeys.Add(keySelector(element))) {
                    yield return element;
                }
            }
        }
    }
}