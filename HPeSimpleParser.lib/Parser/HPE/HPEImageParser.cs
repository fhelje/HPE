using System.Collections.Generic;
using FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Parser.HPE {
    public class HPEImageParser {
        private const int LargeHeight = 600;
        private const int LargeWidth = 800;
        private const int MediumHeight = 300;
        private const int MediumWidth = 400;
        private const int SmallHeight = 150;
        private const int SmallWidth = 200;

        private static readonly Dictionary<string, int> _contentTypePriority = new Dictionary<string, int> {
            {"jpg", 0},
            {"png", 1},
            {"gif", 2},
            {"bmp", 3}
        };

        private static readonly Dictionary<string, int> _typeDetailPriority = new Dictionary<string, int> {
            {"product image", 0},
            {"Product Only", 1},
            {"Person - Face", 2},
            {"product image hero,product image", 1000},
            {"icon", 1000},
            {"product image hero", 1000},
            {"product image - not as shown", 1000},
            {"Concept Graphic", 1000}
        };

        public int ParseIntWithDefault(string value, int @default = 0) {
            return int.TryParse(value, out var height) ? height : @default;
        }

        public int ParseTypeDetail(string typeDetail) {
            return _typeDetailPriority.TryGetValue(typeDetail, out var value) ? value : 3;
        }

        public int ParseContentTypePriority(string contentType) {
            return _contentTypePriority.TryGetValue(contentType, out var value) ? value : 1000;
        }

        private static bool IsLargeDimensions(int height, int width) {
            return height <= LargeHeight && width <= LargeWidth;
        }

        private static bool IsMediumDimensions(int height, int width) {
            return height <= MediumHeight && width <= MediumWidth;
        }

        private static bool IsSmallDimensions(int height, int width) {
            return height <= SmallHeight && width <= SmallWidth;
        }

        public SizeCategoryEnum GetSizeCategory(string pixelHeight, string pixelWidth) {
            if (!int.TryParse(pixelHeight, out var height)) {
                return SizeCategoryEnum.Wrong;
            }

            if (!int.TryParse(pixelWidth, out var width)) {
                return SizeCategoryEnum.Wrong;
            }

            if (IsSmallDimensions(height, width)) {
                return SizeCategoryEnum.Small;
            }

            if (IsMediumDimensions(height, width)) {
                return SizeCategoryEnum.Medium;
            }

            return IsLargeDimensions(height, width)
                ? SizeCategoryEnum.Large
                : SizeCategoryEnum.XLarge;
        }
    }
}