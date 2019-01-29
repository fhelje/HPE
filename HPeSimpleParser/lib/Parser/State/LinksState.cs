using System.Collections.Generic;
using HPeSimpleParser.lib.HPE.Model;

namespace HPeSimpleParser.lib.Parser.State {
    public class LinksState {
        public LinksState() {
            ImageLinks = new List<Image>();
            SelectedImages = new Image[0];
        }

        // TODO: Add constructor
        // TODO: Add Links
        public string PdfLinkDataSheet { get; set; }
        public string PdfLinkManual { get; set; }
        public List<Image> ImageLinks { get; }
        public Image[] SelectedImages { get; }
    }
}