using System.Collections.Generic;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    internal sealed class BranchBuilder {
        // ReSharper disable once CollectionNeverUpdated.Local
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