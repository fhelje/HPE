using System.Collections.Generic;
using HPeSimpleParser.lib.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    internal class BranchBuilder {
        private readonly List<Hierarchy> _branch = new List<Hierarchy>();

        private BranchBuilder() {
        }
        public static BranchBuilder With() {
            return new BranchBuilder();
        }
        public IReadOnlyList<Hierarchy> Build() {
            return _branch;
        }
    }
}