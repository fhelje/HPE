using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using Nest;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public class ElasticIndexer {
        private ElasticClient _client;

        public void SetupClient() {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("item");
            _client = new ElasticClient(settings);

        }

        public void CreateIndex() {
            _client.CreateIndex("item", c => c.Mappings(ms => ms.Map<ProductRoot>(m => m.AutoMap(new TextAndKeywordPropertyVisitor()))));
        }
        public void DeleteIndex() {
            _client.DeleteIndex("item");
        }

        public async Task IndexMany(List<ProductRoot> items) {
            var result = await _client.IndexManyAsync(items, "item");
            if (!result.IsValid) {
                Console.WriteLine("Error indexing to elastic");
            }
        }
    }
}
