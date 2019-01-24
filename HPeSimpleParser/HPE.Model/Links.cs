using System.Collections.Generic;

namespace HPeSimpleParser.HPE.Model {
    public class Links {
        public Links(string pdfLinkDataSheet, string pdfLinkManual, IReadOnlyList<Image> selectedImages) {
            PdfLinkDataSheet = pdfLinkDataSheet;
            PdfLinkManual = pdfLinkManual;
            SelectedImages = selectedImages;
        }

        public string PdfLinkDataSheet { get; set; }
        public string PdfLinkManual { get; set; }
        public IReadOnlyList<Image> SelectedImages { get; set; }
    }

    public class Link {
        public string Title { get; set; }
        public string Url { get; set; }
        public  string ContentType { get; set; }
    }
}