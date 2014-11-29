namespace VehicleVendor.Reports.JsonReportSQLServerGenerator
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using VehicleVendor.Data.Repositories;

    public class JsonReportSQLServerGenerator : IReportGenerator
    {
        private IVehicleVendorRepository repository;

        public JsonReportSQLServerGenerator(IVehicleVendorRepository vehicleVendorRepository)
        {
            this.repository = vehicleVendorRepository;
        }

        public void GenerateReport()
        {
            var countries = this.GetCountriesData();
            this.WriteToFileCountries(@"../../Reports/", countries);
            var dealers = this.GetDealersData();
            this.WriteToFileDealers(@"../../Reports/", dealers);
            var incomes = this.GetIncomesData();
            this.WriteToFileIncomes(@"../../Reports/", incomes);
        }

        private void WriteToFileIncomes(string path, IEnumerable<JsonIncomeReportModel> reportCollection)
        {
            foreach (var rep in reportCollection)
            {
                var data = new JsonIncomeReportModel
                {
                    Date = rep.Date,
                    DealerId = rep.DealerId,
                    Amount = rep.Amount
                };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                using (StreamWriter sw = new StreamWriter(path + rep.DealerId + rep.Date.Day + rep.Date.Month + rep.Date.Year + ".json"))
                {
                    sw.WriteLine(json);
                }
            }
        }

        private void WriteToFileDealers(string path, IEnumerable<JsonDealerReportModel> reportCollection)
        {
            foreach (var rep in reportCollection)
            {
                var data = new JsonDealerReportModel
                {
                    Company = rep.Company,
                    CountryId = rep.CountryId,
                    Address = rep.Address
                };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                using (StreamWriter sw = new StreamWriter(path + rep.Company + ".json"))
                {
                    sw.WriteLine(json);
                }
            }
        }

        private void WriteToFileCountries(string path, IEnumerable<JsonCountryReportModel> reportCollection)
        {
            foreach (var rep in reportCollection)
            {
                var data = new JsonCountryReportModel
                {
                    Country = rep.Country,
                    Region = rep.Region
                };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                using (StreamWriter sw = new StreamWriter(path + rep.Country + ".json"))
                {
                    sw.WriteLine(json);
                }
            }
        }

        private List<JsonIncomeReportModel> GetIncomesData()
        {
            var collection = this.repository.Sales
                .Join(this.repository.SalesDetails, h => h.Id, d => d.SaleId, (h, d) => new { h = h, d = d })
                .Join(this.repository.Dealers, s => s.h.DealerId, d => d.Id, (s, d) => new { s = s, d = d })
                .Join(this.repository.Countries, a => a.d.CountryId, c => c.Id, (a, c) => new { a = a, c = c })
                .Join(this.repository.Vehicles, i => i.a.s.d.VehicleId, p => p.Id, (i, p) => new { i = i, p = p })
                .Join(this.repository.Discounts, f => f.i.a.d.Id, fd => fd.DealerId, (f, fd) => new {f = f, fd = fd })
                .Select(f => new JsonIncomeReportModel()
                {
                    Date = f.f.i.a.s.h.SaleDate,
                    DealerId = f.f.i.a.d.Id,
                    Amount = f.f.i.a.s.d.Quantity * f.f.p.Price
                })
                .ToList();

            return collection;
        }

        private List<JsonDealerReportModel> GetDealersData()
        {
            var collection = this.repository.Dealers
                .Select(d => new JsonDealerReportModel()
                {
                    Company = d.Company,
                    Address = d.Address,
                    CountryId = d.CountryId
                })
                .ToList();

            return collection;
        }

        private List<JsonCountryReportModel> GetCountriesData()
        {
            var collection = this.repository.Countries
                .Select(c => new JsonCountryReportModel()
                {
                    Country = c.Name,
                    Region = c.Region
                })
                .ToList();

            return collection;
        }
    }
}
