using System.Collections.Generic;
using System.Linq;
using HPeSimpleParter.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace HPeSimpleParter.Api.Controllers {
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductVariantsController : ControllerBase {
        private readonly IElasticClient _client;

        public ProductVariantsController(IElasticClient client) {
            _client = client;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<UpcCode>> Get() {
            var retVal = new List<UpcCode>();
            var result = _client.Search<ProductRoot>(s =>
                s.Index("item")
                    .Type("productroot")
                    .Size(0)
                    .MatchAll()
                    .Aggregations(a =>
                        a.Nested(
                            "agg_string_facet",
                            s1 =>
                                s1.Path("productVariants")
                                    .Aggregations(a1 =>
                                        a1.Terms("upcCodes", t => t.Field("productVariants.opt")
                                            .Size(1000)
                                            .Order(o => o.KeyAscending())
                                            .Aggregations(a2 =>
                                                a2.Terms("samples",
                                                    t2 => t2.Field("productVariants.description").Size(1000)))
                                        )
                                    )
                        )
                    )
            );
            if (!result.IsValid) {
                throw result.OriginalException;
            }

            var nestedAgg = result.Aggregations.Nested("agg_string_facet");
            var upcCodesAgg = nestedAgg.Terms("upcCodes");
            foreach (var data in upcCodesAgg.Buckets) {
                var samplesAggregate = data.Terms<string>("samples");
                var sample = samplesAggregate.Buckets.Select(b => new Sample { Key = b.Key, Count = b.DocCount });
                retVal.Add(new UpcCode { Code = data.Key, Sample = sample, VariantCount = data.DocCount });
            }

            return retVal;

        }
    }
}
