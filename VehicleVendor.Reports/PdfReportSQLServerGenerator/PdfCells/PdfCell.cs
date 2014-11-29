namespace PdfReportCreator.PdfCells
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public abstract class PdfCell : Cell
    {
        private Font defaultFont = FontFactory.GetFont(BaseFont.COURIER_BOLD);

        public PdfCell(PdfPTable table, string data, Color color, int columnSpan)
        {
            this.AddData(data, defaultFont);
            this.SetAllBorders();
            this.BackgroundColor = color;
            this.Colspan = columnSpan;

            table.AddCell(this.CreatePdfPCell());
        }

        private void SetAllBorders()
        {
            this.Border = PdfCell.BOTTOM_BORDER | PdfCell.LEFT_BORDER | PdfCell.RIGHT_BORDER | PdfCell.TOP_BORDER;
        }

        private void AddData(string data, Font font)
        {
            Phrase phrase = new Phrase();
            phrase.Add(new Chunk(data, font));
            this.AddElement(phrase);
        }
    }
}
