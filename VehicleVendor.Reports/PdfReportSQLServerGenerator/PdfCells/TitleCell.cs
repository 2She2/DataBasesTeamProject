namespace PdfReportCreator.PdfCells
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class TitleCell : PdfCell
    {
        public TitleCell(PdfPTable table, string data, int colSpan)
            : base(table, data, Color.WHITE, colSpan)
        {
            this.HorizontalAlignment = Element.ALIGN_CENTER;
        }
    }
}
