using System.Collections.Generic;

namespace HPeSimpleParser.lib.HPE.Model {
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
}