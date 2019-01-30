using System.Collections.Generic;
using System.Linq;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser.State;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser {
    public static class SpecificationParser {
        public static decimal? TryFindWeightInSpecifications(this IReadOnlyList<Specification> specifications, params string[] names) {
            if (specifications == null) {
                return null;
            }
            foreach (var name in names) {
                var spec = specifications.FirstOrDefault(x => x.Name == name);
                if (spec != null) {
                    var (weight, uom) = DetailValueParser.TryParseWeight(spec.Value);
                    if (uom != WeightUnitOfMeasure.None) {
                        spec.UpdateUnitOfMeasure("Kg");
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

        public static bool TryFindDimensionsInSpecifications(this IReadOnlyList<Specification> specifications, out Dimension? dimension, params string[] names) {
            dimension = null;
            if (specifications == null) {
                return false;
            }
            foreach (var name in names) {
                var spec = specifications.FirstOrDefault(x => x.Name == name);
                if (spec != null) {
                    var dim = DetailValueParser.ParseDimensions(spec.Value);
                    spec.UpdateLabel("Dimensions (H x W x D) mm");
                    spec.UpdateValue($"{dim.GetHeightInMillimeter()} x {dim.GetWidthInMillimeter()} x {dim.GetDepthInMillimeter()} mm");
                    spec.UpdateUnitOfMeasure("mm");
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