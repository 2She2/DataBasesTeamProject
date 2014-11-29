namespace PdfReportCreator
{
    using System;

    using VehicleVendor.Models;

    internal class PdfReportModel
    {
        public string Vehicle { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
