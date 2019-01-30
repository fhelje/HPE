namespace HPeSimpleParser.lib.Parser.State {
    public class Branch {
        public (string Name, string Id) ProductType {get;set;}
        public (string Name, string Id) MarketingCategory {get;set;}
        public (string Name, string Id) MarketingSubCategory {get;set;}
        public (string Name, string Id) BigSeries {get;set;}
        public (string Name, string Id) SmallSeries {get;set;}
        public (string Name, string Id) Model {get;set;}
    }
}