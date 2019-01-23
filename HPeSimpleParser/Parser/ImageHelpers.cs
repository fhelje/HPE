using System.Collections.Generic;
using System.Linq;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Parser {
    public static class ImageHelpers {
        public static Image[] FilterImages(ProductRoot item) {
            var selectedImages = new List<Image>();
            if (item?.Links?.ImageLinks == null || item.Links.ImageLinks.Count < 1) {
                return new Image[0];
            }

            // Group by master object name
            var monGroups = item.Links.ImageLinks.GroupBy(x => x.MasterObjectName);
            foreach (var monGroup in monGroups) {

                // Group by SizeCategory
                var sizeGroups = monGroup.GroupBy(x => x.SizeCategory);
                foreach (var sizeGroup in sizeGroups) {
                    if (sizeGroup.Key == SizeCategoryEnum.Wrong || sizeGroup.Key == SizeCategoryEnum.XLarge ||
                        sizeGroup.Key == SizeCategoryEnum.Small) {
                        continue;
                    }

                    var orientationGroups = sizeGroup.GroupBy(g => g.Orientation);
                    foreach (var orientationGroup in orientationGroups) {
                        selectedImages.Add(orientationGroup.OrderBy(x => x.CmgAcronym)
                            .ThenBy(x => x.ContentTypePriority)
                            .ThenBy(x => x.DocumentTypeDetailPriority)
                            .ThenByDescending(x => x.Height)
                            .ThenByDescending(x => x.Width)

                            .First());
                    }
                }
            }

            if (selectedImages.Count > selectedImages.Where(RemoveSpecificDocDetailTypes).Count()) {
                selectedImages = selectedImages.Where(RemoveSpecificDocDetailTypes).ToList();
            }

            return selectedImages.DistinctBy(x => x.ImageUrlHttp).ToArray();
            // Order secondly by contentType png, jpg, gif
            // Pick images by size range
        }

        private static bool RemoveSpecificDocDetailTypes(Image arg) {
            switch (arg.DocumentTypeDetail) {
                case "product image hero":
                case "product image - not as shown":
                case "Concept Graphic":
                case "product image hero,product image":
                case "icon":
                    return false;
                default:
                    return true;
            }
        }
    }
}