namespace PdfReportCreator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    using VehicleVendor.Data.Repositories;
    using VehicleVendor.Reports;

    public class PdfReportSQLServerGenerator : IReportGenerator
    {
        private IVehicleVendorRepository repository;

        public PdfReportSQLServerGenerator(IVehicleVendorRepository vehicleVendorRepository)
        {
            this.repository = vehicleVendorRepository;
        }

        private List<IGrouping<DateTime, PdfReportModel>> GetData()
        {
            var collection = this.repository.SalesDetails
                .Select(t => new PdfReportModel
                {
                    Vehicle = t.Vehicle.Name,
                    Category = t.Vehicle.Category,
                    Price = t.Vehicle.Price,
                    Quantity = t.Quantity,
                    SaleDate = t.Sale.SaleDate
                })
                .GroupBy(x => x.SaleDate)
                .ToList();

            return collection;
        }

        public void GenerateReport()
        {
            // In case of DateTime usage
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var collection = GetData();
            decimal totalSum = 0;
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportSettings.Default.PDFFileName, FileMode.Create));

            document.Open();

            // It is important to set the columns count, so the appearance is ok
            int tableColumns = 4;

            PdfPTable table = new PdfPTable(tableColumns);
            CellsFactory cells = new CellsFactory(table, tableColumns);

            cells.TitleCell("Cars Report by Date!");

            foreach (var rows in collection)
            {
                cells.HeaderRow(4, "Date: " + rows.First().SaleDate.ToShortDateString());
                cells.HeaderRow(0, "Model", "Category", "Price", "Quantity");

                var groups = rows.GroupBy(x => x.Vehicle);

                foreach (var row in groups)
                {
                    string vehicleModel = row.First().Vehicle;
                    string category = row.First().Category.ToString();
                    string price = row.Sum(x => x.Price).ToString();
                    string quantity = row.Sum(x => x.Quantity).ToString();

                    cells.DataCellRow(vehicleModel, category, price, quantity);
                }

                var groupSum = rows.Sum(x => x.Price);
                totalSum += groupSum;
                cells.SummaryCell(string.Format("Group Price: {0}", groupSum.ToString()));
            }

            cells.TitleCell(string.Format("Total Price:  {0}", totalSum.ToString()));

            document.Add(table);
            document.Close();
        }
    }
}
