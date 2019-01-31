using System.Collections.Generic;
using System.Linq;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;
using FSSystem.ContentAdapter.HPEAndHPInc.Linq;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser {
    public static class ImageSelector {
        public static List<Image> FilterImages(IReadOnlyList<Image> linksImageLinks) {
            var selectedImages = new List<Image>();
            if (linksImageLinks == null || linksImageLinks.Count < 1) {
                return new List<Image>();
            }

            // Group by master object name
            var monGroups = linksImageLinks.GroupBy(x => x.GroupingKey2);
            foreach (var monGroup in monGroups) {
                // Group by SizeCategory
                var sizeGroups = monGroup.GroupBy(x => x.SizeCategory);
                foreach (var sizeGroup in sizeGroups) {
                    if (sizeGroup.Key == SizeCategoryEnum.Wrong || sizeGroup.Key == SizeCategoryEnum.XLarge
                        || sizeGroup.Key == SizeCategoryEnum.Small) {
                        continue;
                    }

                    var orientationGroups = sizeGroup.GroupBy(g => g.Orientation);
                    foreach (var orientationGroup in orientationGroups) {
                        selectedImages.Add(orientationGroup.OrderBy(x => x.GroupingKey1)
                            .ThenBy(x => x.ContentTypePriority)
                            .ThenBy(x => x.DocumentTypeDetailPriority)
                            .ThenByDescending(x => x.Height)
                            .ThenByDescending(x => x.Width)
                            .First());
                    }
                }
            }

            var images = selectedImages.Where(RemoveSpecificDocDetailTypes).ToList();
            if (selectedImages.Count > images.Count) {
                selectedImages = images.ToList();
            }

            return selectedImages.DistinctBy(x => x.ImageUrlHttp).ToList();
            // Order secondly by contentType png, jpg, gif
            // Pick images by size range
        }

        private static bool RemoveSpecificDocDetailTypes(Image arg) {
            switch (arg.TypeDetail) {
                case "product image hero":
                case "product image - not as shown":
                case "Concept Graphic":
                case "product image hero,product image":
                case "product in use":
                case "icon":
                    return false;
                default:
                    return true;
            }
        }
    }
}