namespace PdfReportCreator.PdfCells
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class SummaryCell : PdfCell
    {
        public SummaryCell(PdfPTable table, string data, int columnSpan)
            : base(table, data, Color.WHITE, columnSpan)
        {
        }
    }
}
