﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPeSimpleParter.Api.Model {
    public class Section {
        private readonly List<Paragraph> _paragraphs;

        public Section() {
            _paragraphs = new List<Paragraph>();
        }

        internal class Paragraph {
            public string Id { get; set; }
            public string Text { get; set; }
        }

        public string Header { get; set; }

        public void AddParagraph(string text, string id) {
            _paragraphs.Add(new Paragraph{Text = text, Id = id});
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine(Header);
            foreach (var paragraph in _paragraphs.OrderBy(x=>x.Id)) {
                sb.AppendLine(paragraph.Text);
            }

            return sb.ToString();
        }
    }
}