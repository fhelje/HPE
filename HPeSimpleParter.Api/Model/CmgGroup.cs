using System.Collections.Generic;

namespace HPeSimpleParter.Api.Model {
    public class CmgGroup {
        public string Id { get; set; }
        public IEnumerable<ProductImageResponse> Images { get; set; }
    }
}