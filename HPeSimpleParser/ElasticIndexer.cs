using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HPeSimpleParser.HPE.Model;
using Nest;

namespace HPeSimpleParser {
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

    public class TextAndKeywordPropertyVisitor : NoopPropertyVisitor {
        public override IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) {
            if (propertyInfo.Name == "Items") {
                var name = new KeywordProperty { Name = "name" };
                var value = new KeywordProperty { Name = "value" };

                var retVal = new NestedProperty {
                    Properties = new Properties(new Dictionary<PropertyName, IProperty> {
                        {"name", name},
                        {"value", value},

                    })
                };
                return retVal;

            }
            if (propertyInfo.Name == "ProductVariants") {
                var name = new KeywordProperty { Name = "opt" };
                var value = new KeywordProperty { Name = "description" };

                var retVal = new NestedProperty {
                    Properties = new Properties(new Dictionary<PropertyName, IProperty> {
                        {"opt", name},
                        {"description", value},

                    })
                };
                return retVal;

            }
            if (propertyInfo.Name == "ImageLinks" || propertyInfo.Name == "SelectedImages" ) {
                var size = new KeywordProperty { Name = "size" };
                var contentType = new KeywordProperty { Name = "contentType" };
                var orientation = new KeywordProperty { Name = "orientation" };
                var documentType = new KeywordProperty { Name = "documentType" };
                var documentTypeDetail= new KeywordProperty { Name = "documentTypeDetail" };

                var retVal = new NestedProperty {
                    Properties = new Properties(new Dictionary<PropertyName, IProperty> {
                        {"size", size},
                        {"contentType", contentType},
                        {"orientation", orientation},
                        {"documentType", documentType},
                        {"documentTypeDetail", documentTypeDetail},

                    })
                };
                return retVal;

            }
            return base.Visit(propertyInfo, attribute);
        }
        //public override IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) {
        //    IProperty retVal;
        //    if (propertyInfo.Name == "Specs") {
        //        var name = new KeywordProperty { Name = "name" };
        //        var value = new KeywordProperty { Name = "value" };

        //        retVal = new NestedProperty {
        //            Properties = new Properties(new Dictionary<PropertyName, IProperty> {
        //                {"name", name},
        //                {"value", value},

        //            })
        //        };
                
        //    }
        //    else {
        //        retVal = new TextProperty {
        //            Name = propertyInfo.Name,
        //            Fields = new Properties(new Dictionary<PropertyName, IProperty> {
        //                {"keyword", new KeywordProperty {Name = "keyword"}}
        //            })
        //        };
        //    }
        //    return retVal;
        //}
    }
}
