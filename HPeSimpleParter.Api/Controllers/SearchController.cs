using System;
using System.Collections.Generic;
using System.Linq;
using HPeSimpleParter.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace HPeSimpleParter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IElasticClient _client;

        public SearchController(IElasticClient client)
        {
            _client = client;
        }
        [HttpGet("{id}")]
        public ActionResult<ProductResponse> GetItem(string id)
        {
            var result = _client.Get(DocumentPath<ProductRoot>.Id(id), x => x.Index("item").Type("productroot"));
            if (!result.IsValid)
            {
                ProblemDetails problem;
                if (result.ApiCall.HttpStatusCode == 404)
                {
                    problem = new ProblemDetails {
                        Detail = result.ApiCall.DebugInformation,
                        Title = "Product not found",
                    };
                }
                if (result?.ServerError?.Error != null)
                {
                    problem = new ProblemDetails {
                        Detail = result.ServerError.Error.Reason,
                        Title = "Product not found",
                    };
                }
                else
                {
                    problem = new ProblemDetails {
                        Detail = result.ApiCall.DebugInformation,
                        Title = "Product not found",
                    };
                }
                problem.Extensions.Add("StackTrace", result.ServerError.Error.StackTrace);
                return NotFound(problem);
            }
            return Ok(new ProductResponse {

                Description = result.Source.Product.Description,
                PartNumber = result.Source.PartNumber,
                PartnerPartNumber = result.Source.PartnerPartNumber,
                CategoryName = result.Source.Product.CategoryName,
                SelectedImages = result.Source.Links.SelectedImages,
                TotalImageCount = result.Source.Links.ImageLinks.Count,
                MasterImages = result.Source.Links.ImageLinks
                    .GroupBy(y => y.MasterObjectName)
                    .Select(y => new MasterImageGroup {
                        Id = y.Key,
                        CmdGroups = y.GroupBy(z => z.CmgAcronym).Select(z => new CmgGroup {
                            Id = z.Key,
                            Images = z.Select(i => new ProductImageResponse {
                                ContentType = i.ContentType,
                                PixelHeight = i.PixelHeight,
                                Orientation = i.Orientation,
                                PixelWidth = i.PixelWidth,
                                Action = i.Action,
                                ImageUrlHttp = i.ImageUrlHttp,
                                DocumentTypeDetail = i.DocumentTypeDetail,
                                DocumentType = i.DocumentType,
                                SearchKeyword = i.SearchKeyword,
                                FullTitle = i.FullTitle,
                                MasterObjectName = i.MasterObjectName
                            })
                        })
                    }),

            });
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponse>> Get()
        {
            var result = _client.Search<ProductRoot>(s =>
                s.Index("item")
                    .Type("productroot")
                    .Size(50)
                    .MatchAll());

            if (!result.IsValid)
            {
                var problem = new ProblemDetails {
                    Detail = result.ServerError.Error.Reason,
                    Title = "Search failed",
                };
                problem.Extensions.Add("StackTrace", result.ServerError.Error.StackTrace);
                return StatusCode(500, problem);
            }

            var response = result.Documents.Select(x => new ProductResponse {
                Description = x.Product.Description,
                PartNumber = x.PartNumber,
                PartnerPartNumber = x.PartnerPartNumber,
                CategoryName = x.Product.CategoryName,
                MasterImages = x.Links.ImageLinks
                    .GroupBy(y => y.MasterObjectName)
                    .Select(y => new MasterImageGroup {
                        Id = y.Key,
                        CmdGroups = y.GroupBy(z => z.CmgAcronym).Select(z => new CmgGroup {
                            Id = z.Key,
                            Images = z.Select(i => new ProductImageResponse {
                                ContentType = i.ContentType,
                                PixelHeight = i.PixelHeight,
                                Orientation = i.Orientation,
                                PixelWidth = i.PixelWidth,
                                Action = i.Action,
                                ImageUrlHttp = i.ImageUrlHttp,
                                DocumentTypeDetail = i.DocumentTypeDetail,
                                DocumentType = i.DocumentType,
                                SearchKeyword = i.SearchKeyword,
                                FullTitle = i.FullTitle,
                            })
                        })
                    })
            });
            return Ok(response);
        }
        [HttpPost]
        public IEnumerable<SearchQueryResponse> Post(SearchQueryRequest query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            var result = _client.Search<ProductRoot>(s =>
                s.Index("item")
                    .Type("productroot")
                    .Size(query.Size)
                    .Query(x => x.Match(m => m.Field(f => f.Product.Description).Query(query.Query))));

            var response = result.Documents.Select(x => new SearchQueryResponse {
                Id = x.Id,
                Description = x.Product.Description,
                PartNumber = x.PartNumber,
                PartnerPartNumber = x.PartnerPartNumber,
                CategoryName = x.Hierarchy.Last()?.CategoryName,
            });
            return response;
        }
    }

    public class SearchQueryResponse
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string PartNumber { get; set; }
        public string PartnerPartNumber { get; set; }
    }

    public class SearchQueryRequest
    {
        public SearchQueryRequest()
        {
            Query = string.Empty;
            Size = 50;

        }
        public string Query { get; set; }
        public int Size { get; set; }
    }
}