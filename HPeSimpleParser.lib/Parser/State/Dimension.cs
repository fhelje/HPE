namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser.State {
    public struct Dimension {
        public Dimension(decimal? height, decimal? width, decimal? depth, DimensionUnitOfMeasure unitOfMeasure) {
            Height = height;
            Width = width;
            Depth = depth;
            UnitOfMeasure = unitOfMeasure;
        }

        public decimal? Height { get; }
        public decimal? Width { get; }
        public decimal? Depth { get; }
        public DimensionUnitOfMeasure UnitOfMeasure { get; }

        public bool HasValues => Height.HasValue && Width.HasValue && Depth.HasValue;

        public decimal? GetHeightInMillimeter() => GetInMillimeter(Height, UnitOfMeasure);

        public decimal? GetWidthInMillimeter() => GetInMillimeter(Width, UnitOfMeasure);

        public decimal? GetDepthInMillimeter() => GetInMillimeter(Depth, UnitOfMeasure);

        private decimal? GetInMillimeter(decimal? value, DimensionUnitOfMeasure uom) {
            if (uom == DimensionUnitOfMeasure.None) return null;
            switch (uom) {
                case DimensionUnitOfMeasure.Millimeter:
                    return value;
                case DimensionUnitOfMeasure.CentiMeter:
                    return value * 10;
                case DimensionUnitOfMeasure.Meter:
                    return value * 1000;
                case DimensionUnitOfMeasure.Inches:
                    return value * 2.54M * 10;
                case DimensionUnitOfMeasure.Feet:
                    return value * 12 * 2.54M * 10;
                default:
                    return null;
            }
        }
    }
}