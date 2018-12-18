﻿using System.Collections.Generic;

namespace HPeSimpleParser.lib.Model
{
    public class Link
    {
        public Link()
        {
            Images = new List<Image>();
        }
        public string PartnerPartNumber { get; set; }
        public string PdfLinkDataSheet { get; set; }
        public string PdfLinkManual { get; set; }
        public List<Image> Images { get; set; }
    }
}