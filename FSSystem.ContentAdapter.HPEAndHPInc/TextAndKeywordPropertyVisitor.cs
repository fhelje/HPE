﻿using System.Collections.Generic;
using System.Reflection;
using Nest;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public class TextAndKeywordPropertyVisitor : NoopPropertyVisitor {
        public override IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) {
            if (propertyInfo.Name == "Items") {
                var name = new KeywordProperty {Name = "name"};
                var value = new KeywordProperty {Name = "value"};

                var retVal = new NestedProperty {
                    Properties = new Properties(new Dictionary<PropertyName, IProperty> {
                        {"name", name},
                        {"value", value}
                    })
                };
                return retVal;
            }

            if (propertyInfo.Name == "LabeledItems") {
                var name = new KeywordProperty {Name = "name"};
                var value = new KeywordProperty {Name = "value"};
                var label = new KeywordProperty {Name = "label"};

                var retVal = new NestedProperty {
                    Properties = new Properties(new Dictionary<PropertyName, IProperty> {
                        {"name", name},
                        {"value", value},
                        {"label", label}
                    })
                };
                return retVal;
            }

            if (propertyInfo.Name == "ProductVariants") {
                var name = new KeywordProperty {Name = "opt"};
                var value = new KeywordProperty {Name = "description"};

                var retVal = new NestedProperty {
                    Properties = new Properties(new Dictionary<PropertyName, IProperty> {
                        {"opt", name},
                        {"description", value}
                    })
                };
                return retVal;
            }

            if (propertyInfo.Name == "ImageLinks" || propertyInfo.Name == "SelectedImages") {
                var size = new KeywordProperty {Name = "size"};
                var contentType = new KeywordProperty {Name = "contentType"};
                var orientation = new KeywordProperty {Name = "orientation"};
                var documentType = new KeywordProperty {Name = "documentType"};
                var documentTypeDetail = new KeywordProperty {Name = "documentTypeDetail"};

                var retVal = new NestedProperty {
                    Properties = new Properties(new Dictionary<PropertyName, IProperty> {
                        {"size", size},
                        {"contentType", contentType},
                        {"orientation", orientation},
                        {"documentType", documentType},
                        {"documentTypeDetail", documentTypeDetail}
                    })
                };
                return retVal;
            }

            return base.Visit(propertyInfo, attribute);
        }
    }
}