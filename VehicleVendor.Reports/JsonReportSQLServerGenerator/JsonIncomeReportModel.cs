using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleVendor.Models;

namespace VehicleVendor.Reports.JsonReportSQLServerGenerator
{
    public class JsonIncomeReportModel
    {
        //[JsonConverter(typeof(ObjectIdConverter))]
        //public ObjectId _id { get; set; }

        public int DealerId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        //public int total_quantity_sold { get; set; }

        //public decimal total_incomes { get; set; }
    }
}
