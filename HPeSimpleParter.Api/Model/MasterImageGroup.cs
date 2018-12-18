using System.Collections.Generic;

namespace HPeSimpleParter.Api.Model {
    public class MasterImageGroup {
        public string Id { get; set; }
        public IEnumerable<CmgGroup> CmdGroups { get; set; }
    }
}