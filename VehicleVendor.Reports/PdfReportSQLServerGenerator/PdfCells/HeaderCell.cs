namespace PdfReportCreator.PdfCells
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class HeaderCell : PdfCell
    {
        public HeaderCell(PdfPTable table, string data, int columnSpan = 0)
            : base(table, data, Color.LIGHT_GRAY, columnSpan)
        {
        }
    }
}
