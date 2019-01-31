namespace FSSystem.ContentAdapter.GenericOutput.FileWriter {
    public class WriterConfiguration {
        public WriterConfiguration() {
            CsvDirectory = "csv";
            JsonDirectory = "json";
            JsonFileName = "all.json";
            DetailFileName = "detail.txt";
            LinkFileName = "link.txt";
            MarketingFileName = "marketing.txt";
            OptionFileName = "option.txt";
            ProductFileName = "product.txt";
            SpecificationFileName = "specification.txt";
            SupplierFileName = "supplier.txt";
            PureHierarchyFileName = "pure_hierarchy.txt";
        }

        public string OutputPath { get; set; }
        public string CsvDirectory { get; set; }
        public string JsonDirectory { get; set; }
        public string JsonFileName { get; set; }
        public string DetailFileName { get; set; }
        public string LinkFileName { get; set; }
        public string MarketingFileName { get; set; }
        public string OptionFileName { get; set; }
        public string ProductFileName { get; set; }
        public string SpecificationFileName { get; set; }
        public string PureHierarchyFileName { get; set; }
        public string ImportPath { get; set; }
        public string DeliveryFile { get; set; }
        public string CsvZipFileName { get; set; }
        public string JsonZipFileName { get; set; }
        public string SupplierFileName { get; set; }
    }
}