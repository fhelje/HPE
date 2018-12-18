using System.Collections.Generic;
using System.Linq;
using HPeSimpleParter.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace HPeSimpleParter.Api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IElasticClient _client;

        public ImagesController(IElasticClient client)
        {
            _client = client;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<ImageResponse> Get()
        {
            var retVal = new List<ImageAgg>();
            var result = _client.Search<ProductRoot>(s =>
                s.Index("item")
                    .Type("productroot")
                    .Size(0)
                    .MatchAll()
                    .Aggregations(a =>
                        a.Nested(
                            "agg_string_facet",
                            s1 =>
                                s1.Path("links.imageLinks")
                                    .Aggregations(a1 =>
                                        a1.Terms("upcCodes", t => t.Field("links.imageLinks.size")
                                            .Size(1000)
                                            .Aggregations(a2 =>
                                                a2.Terms("samples",
                                                    t2 => t2.Field("links.imageLinks.contentType").Size(1000).Order(o => o.KeyAscending()))
                                                  .Terms("documentType",
                                                            t2 => t2.Field("links.imageLinks.documentTypeDetail").Size(10).Order(o => o.KeyAscending())))
                                        )
                                    )
                        )
                            .Nested(
                                "filters",
                                s1 =>
                                    s1.Path("links.imageLinks")
                                        .Aggregations(a1 =>
                                            a1.Terms("documentTypeDetail", t => t.Field("links.imageLinks.documentTypeDetail")
                                                .Size(20)
                                            )
                                        )
                            )
                    )
            );
            if (!result.IsValid)
            {
                throw result.OriginalException;
            }

            var nestedAgg = result.Aggregations.Nested("agg_string_facet");
            var filtersAgg = result.Aggregations.Nested("filters");
            var upcCodesAgg = nestedAgg.Terms("upcCodes");
            var filterTerms = filtersAgg.Terms("documentTypeDetail");
            foreach (var data in upcCodesAgg.Buckets)
            {
                var samplesAggregate = data.Terms<string>("samples");
                var documentTypeAggregate = data.Terms<string>("documentType");
                var sample = samplesAggregate.Buckets.Select(b => new Sample { Key = b.Key, Count = b.DocCount });
                var documentTypes = documentTypeAggregate.Buckets.Select(b => new Sample { Key = b.Key, Count = b.DocCount });
                retVal.Add(new ImageAgg { Size = data.Key, ContentType = sample, DocumentTypes = documentTypes, Count = data.DocCount });
            }

            return new ImageResponse {
                TotalCount = result.HitsMetadata.Total,
                Aggs = retVal,
                DocumentTypeDetail = filterTerms.Buckets.Select(x => new Sample { Key = x.Key, Count = x.DocCount })
            };

        }
    }

    public class ImageResponse
    {
        public List<ImageAgg> Aggs { get; set; }
        public long TotalCount { get; set; }
        public IEnumerable<Sample> DocumentTypeDetail { get; set; }
    }

    public class ImagesRetval
    {
    }
}