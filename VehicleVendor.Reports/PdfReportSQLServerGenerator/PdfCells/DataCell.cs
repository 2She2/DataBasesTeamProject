namespace PdfReportCreator.PdfCells
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class DataCell : PdfCell
    {
        public DataCell(PdfPTable table, string data)
            : base(table, data, Color.WHITE, 0)
        {
        }
    }
}
