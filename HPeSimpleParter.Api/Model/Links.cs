using System.Collections.Generic;

namespace HPeSimpleParter.Api.Model {
    public class Links {
        public Links() {
            ImageLinks = new List<Image>();
        }

        public string PdfLinkDataSheet { get; set; }
        public string PdfLinkManual { get; set; }
        public List<Image> ImageLinks { get; set; }
        public Image[] SelectedImages { get; set; }
    }
}