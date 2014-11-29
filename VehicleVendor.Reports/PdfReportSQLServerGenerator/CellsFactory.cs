namespace PdfReportCreator
{
    using iTextSharp.text.pdf;
    using PdfReportCreator.PdfCells;

    public class CellsFactory
    {
        private PdfPTable table;
        private int columnsCount;

        public CellsFactory(PdfPTable table, int columnsCount)
        {
            this.table = table;
            this.columnsCount = columnsCount;
        }

        public void DataCell(string data)
        {
            var dataCell = new DataCell(this.table, data);
        }

        public void SummaryCell(string data)
        {
            var summaryCell = new SummaryCell(this.table, data, this.table.NumberOfColumns);
        }

        public void HeaderCell(string data, int colSpan = 0)
        {
            var headerCell = new HeaderCell(this.table, data, colSpan);
        }

        public void TitleCell(string data)
        {
            var summaryCell = new TitleCell(this.table, data, this.columnsCount);
        }

        public void HeaderRow(int colSpan = 0, params string[] columnNames)
        {
            foreach (var name in columnNames)
            {
                this.HeaderCell(name, colSpan);
            }
        }

        public void DataCellRow(params string[] columnNames)
        {
            foreach (var name in columnNames)
            {
                this.DataCell(name);
            }
        }
    }
}
