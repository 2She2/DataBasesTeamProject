namespace VehicleVendor.Reports.XmlReportSqlServerGenerator
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using VehicleVendor.Data.Repositories;
    using VehicleVendor.Reports.XmlReportSqlServerGenerator.Classes;

    public class XmlReportGenerator : IReportGenerator
    {
        private IVehicleVendorRepository repo;
        private DateTime startDate;
        private DateTime endDate;

        private string dealer;
        private string country;

        public XmlReportGenerator(IVehicleVendorRepository repo, DateTime start, DateTime end)
        {
            this.repo = repo;
            this.startDate = start;
            this.endDate = end;
            this.country = null;
            this.dealer = null;
        }

        public XmlReportGenerator(
            IVehicleVendorRepository repo,
            DateTime start,
            DateTime end,
            string country,
            string dealer = null) : this(repo, start, end)
        {
            this.country = country;
            this.dealer = dealer;
        }

        public void GenerateReport()
        {
            this.GenerateReport(null);
        }

        public void GenerateReport(string pathToOutputDir)
        {
            var fileName = "Report" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xml";
            string outputDir;
            if (pathToOutputDir == null)
            {
                outputDir = @"../../Reports/";
            }
            else
            {
                outputDir = pathToOutputDir;
            }

            var data = this.GetReportData();

            var serializer = new XmlSerializer(typeof(XmlSalesReportModel));
            using (var writerStream = new StreamWriter(outputDir + fileName))
            {
                serializer.Serialize(writerStream, data);
            }
        }

        private XmlSalesReportModel GetReportData()
        {
            var reportData = this.repo.Sales.Where(sale => sale.SaleDate > this.startDate && sale.SaleDate < this.endDate);

            if (!string.IsNullOrEmpty(this.country))
            {
                reportData = reportData.Where(sale => sale.Dealer.Country.Name == this.country);
            }

            if (!string.IsNullOrEmpty(this.dealer))
            {
                reportData = reportData.Where(sale => sale.Dealer.Company == this.dealer);
            }

            var distinctCountries = reportData
                                              .Select(rd => new
                                              {
                                                  Name = rd.Dealer.Country.Name,
                                                  Id = rd.Dealer.CountryId
                                              })
                                              .Distinct();

            var distinctDealers = reportData
                                            .Select(rd => new
                                            {
                                                Company = rd.Dealer.Company,
                                                DealerId = rd.Dealer.CountryId,
                                                CountryId = rd.Dealer.CountryId
                                            })
                                            .Distinct();

            var extractedData = distinctCountries
                                                 .Select(country => new
                                                 {
                                                     CountryName = country.Name,
                                                     Dealers = distinctDealers
                                                                              .Where(dd => dd.CountryId == country.Id)
                                                                              .Select(d => new
                                                                              {
                                                                                  CompanyName = d.Company,
                                                                                  DaySales = reportData
                                                                                                       .Where(ds => d.DealerId == ds.DealerId)
                                                                                                       .Select(ds => new
                                                                                                       {
                                                                                                           Date = ds.SaleDate,
                                                                                                           Value = ds.SaleItems
                                                                                                                     .Select(si => (si.Quantity * si.Vehicle.Price))
                                                                                                                     .Sum()
                                                                                                       })
                                                                              })
                                                 });

            var countries = new List<XmlCountryModel>();
            foreach (var country in extractedData)
            {
                var dealers = new List<XmlDealerModel>();
                foreach (var dealer in country.Dealers)
                {
                    var daySales = new List<XmlDaySaleReportModel>();
                    foreach (var daySale in dealer.DaySales)
                    {
                        var currentDaySale = new XmlDaySaleReportModel() { Date = daySale.Date, Value = daySale.Value };
                        daySales.Add(currentDaySale);
                    }

                    var currentDealer = new XmlDealerModel() { Name = dealer.CompanyName, DaySale = daySales.ToArray() };
                    dealers.Add(currentDealer);
                }

                var currentCountry = new XmlCountryModel() { Name = country.CountryName, Dealer = dealers.ToArray() };
                countries.Add(currentCountry);
            }

            var data = new XmlSalesReportModel()
            {
                Country = countries.ToArray()
            };

            return data;
        }
    }
}