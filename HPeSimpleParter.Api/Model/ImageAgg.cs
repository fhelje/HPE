using System.Collections.Generic;

namespace HPeSimpleParter.Api.Model {
    public class ImageAgg {
        public string Size { get; set; }
        public long? Count { get; set; }
        public IEnumerable<Sample> ContentType { get; set; }
        public IEnumerable<Sample> DocumentTypes { get; set; }
    }
}