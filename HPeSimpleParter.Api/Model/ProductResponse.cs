using System;
using System.Collections.Generic;

namespace HPeSimpleParter.Api.Model {
    public class ProductResponse {
        public string Description { get; set; }
        public IEnumerable<MasterImageGroup> MasterImages { get; set; }
        public string PartNumber { get; set; }
        public string PartnerPartNumber { get; set; }
        public string CategoryName { get; set; }
        public Guid Id { get; set; }
        public Image[] SelectedImages { get; set; }
        public int TotalImageCount { get; set; }
    }
}