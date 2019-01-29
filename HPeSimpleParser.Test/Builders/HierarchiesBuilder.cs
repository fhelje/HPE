using System.Collections.Generic;
using HPeSimpleParser.lib.HPE.Model;

namespace HPeSimpleParser.Test.Builders {
    public class HierarchiesBuilder {
        private List<Hierarchy> _data;

        private HierarchiesBuilder() {
            _data = new List<Hierarchy>();
        }
        public static HierarchiesBuilder With() {
            return new HierarchiesBuilder();
        }

        public IReadOnlyList<Hierarchy> Build() {
            return _data;
        }

        public HierarchiesBuilder AddDefaultNode() {
            _data.Add(new Hierarchy("name", "categoryId", "categoryName", "parentCategoryId", "HPE"));
            return this;
        }
    }
}