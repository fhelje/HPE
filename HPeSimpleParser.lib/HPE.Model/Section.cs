using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSSystem.ContentAdapter.HPEAndHPInc.HPE.Model
{
    public class Section
    {
        private readonly List<Paragraph> _paragraphs;

        public Section()
        {
            _paragraphs = new List<Paragraph>();
        }

        internal class Paragraph
        {
            public string Id { get; set; }
            public string Text { get; set; }
        }

        public string Header { get; set; }

        public void AddParagraph(string text, string id)
        {
            _paragraphs.Add(new Paragraph { Text = text, Id = id });
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Header)) {
                sb.Append("<h1>");
                sb.Append(Header.RemoveLineEndings());
                sb.Append("</h1>");

            }
            foreach (var paragraph in _paragraphs.OrderBy(x => x.Id)) {
                sb.Append("<p><span>");
                sb.Append(paragraph.Text.RemoveLineEndings());
                sb.Append("</span></p>");
            }

            return sb.ToString();
        }

    }
}