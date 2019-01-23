using System;
using System.Collections.Generic;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
    public static class LinqHelpers {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source) {
                if (seenKeys.Add(keySelector(element))) {
                    yield return element;
                }
            }
        }

        public static decimal? TryFindWeigthInSpecifications(this Specifications specifications, params string[] names) {
            if (specifications == null) {
                return null;
            }
            foreach (var name in names) {
                var spec = specifications.LabeledItems.Find(x => x.Name == name);
                if (spec != null) {
                    var (weight, uom) = DetailValueParser.TryParseWeight(spec.Value);
                    if (uom != WeightUnitOfMeasure.None) {
                        switch (uom) {
                            case WeightUnitOfMeasure.Kilogram:
                                return weight.Value;
                            case WeightUnitOfMeasure.Gram:
                                return weight.Value * 1000;
                            case WeightUnitOfMeasure.Pounds:
                                return weight.Value * 0.45359237M;
                            case WeightUnitOfMeasure.Ounces:
                                return weight.Value * 0.02834952M;
                        }
                    }
                }
            }
            return null;
        }

        public static bool TryFindDimensionsInSpecifications(this Specifications specifications, out Dimension? dimension, params string[] names) {
            dimension = null;
            if (specifications == null) {
                return false;
            }
            foreach (var name in names) {
                var spec = specifications.LabeledItems.Find(x => x.Name == name);
                if (spec != null) {
                    var dim = DetailValueParser.ParseDimensions(spec.Value);
                    if (dim.HasValues) {
                        dimension = dim;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}