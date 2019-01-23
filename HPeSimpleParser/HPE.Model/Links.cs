using System.Collections.Generic;

namespace HPeSimpleParser.HPE.Model {
    public class Links {
        public Links() {
            ImageLinks = new List<Image>();
            SelectedImages = new Image[0];
        }

        public string PdfLinkDataSheet { get; set; }
        public string PdfLinkManual { get; set; }
        public List<Image> ImageLinks { get; set; }
        public List<Image> FilteredImages { get; set; }
        public Image[] SelectedImages { get; set; }
    }

    public class Link {
        public string Title { get; set; }
        public string Url { get; set; }
        public  string ContentType { get; set; }
    }
}