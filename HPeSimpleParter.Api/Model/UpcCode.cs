using System.Collections.Generic;

namespace HPeSimpleParter.Api.Model {
    public class UpcCode {
        public string Code { get; set; }
        public IEnumerable<Sample> Sample { get; set; }
        public long? VariantCount { get; set; }
    }
}