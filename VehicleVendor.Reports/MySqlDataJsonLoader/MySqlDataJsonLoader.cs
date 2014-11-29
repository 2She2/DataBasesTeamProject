using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleVendor.Data;
using VehicleVendor.Data.Repositories;
using VehicleVendor.DataAceessData;
using VehicleVendor.DataAceessData.Repository;
using VehicleVendor.Reports.JsonReportSQLServerGenerator;
using VehicleVendor.Models;

namespace VehicleVendor.Reports.MySqlDataJsonGenerator
{
    public class MySqlDataJsonLoader
    {
        private IVehicleVendorRepository repository;
        private IVehicleVendorMySqlRepository mySqlRepository;

        public MySqlDataJsonLoader(IVehicleVendorRepository vehicleVendorRepository, IVehicleVendorMySqlRepository mySqlRepository)
        {
            this.repository = vehicleVendorRepository;
            this.mySqlRepository = mySqlRepository;
        }

        public void WriteJsonsReportsToMySql()
        {
            var jsonCountryReports = this.ParseAllJsonCountryReports();

            foreach (var model in jsonCountryReports)
            {
                var country = new DataAccessCountry()
                {
                    Name = model.Country,
                    Region = (int) model.Region
                };

                this.mySqlRepository.Add<DataAccessCountry>(country);
            }

            this.mySqlRepository.SaveChanges();

            var jsonDealerReports = this.ParseAllJsonDealerReports();

            foreach (var model in jsonDealerReports)
            {
                var dealer = new DataAccessDealer()
                {
                    Company = model.Company,
                    CountryId = model.CountryId,
                    Address = model.Address
                };

                this.mySqlRepository.Add<DataAccessDealer>(dealer);
            }

            this.mySqlRepository.SaveChanges();

            var jsonIncomeReports = this.ParseAllJsonIncomeReports();

            foreach (var model in jsonIncomeReports)
            {
                var income = new DataAccessIncome()
                {
                    DealerId = model.DealerId,
                    Date = model.Date,
                    Amount = model.Amount
                };

                this.mySqlRepository.Add<DataAccessIncome>(income);
            }

            this.mySqlRepository.SaveChanges();
        }

        private string ReadJsonReport(string path)
        {
            string json = string.Empty;

            using (StreamReader reader = new StreamReader(path))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }

        private JsonIncomeReportModel ConvertJsonIncomeReports(string json)
        {
            var reportModel = JsonConvert.DeserializeObject<JsonIncomeReportModel>(json);

            return reportModel;
        }

        private JsonCountryReportModel ConvertJsonCountryReports(string json)
        {
            var reportModel = JsonConvert.DeserializeObject<JsonCountryReportModel>(json);

            return reportModel;
        }

        private JsonDealerReportModel ConvertJsonDealerReports(string json)
        {
            var reportModel = JsonConvert.DeserializeObject<JsonDealerReportModel>(json);

            return reportModel;
        }

        private IList<DateTime> GetIncomeDates()
        {
            var collection = this.repository.Sales
                .Select(x => x.SaleDate)
                .GroupBy(x => x)
                .Select(x => x.Key)
                .ToList();

            return collection;
        }

        private IList<int> GetDealerIds()
        {
            var collection = this.repository.Dealers
                .Select(x => x.Id)
                .ToList();

            return collection;
        }

        private IList<string> GetDealerCompanies()
        {
            var collection = this.repository.Dealers
                .Select(x => x.Company)
                .ToList();

            return collection;
        }

        private IList<string> GetCountries()
        {
            var collection = this.repository.Countries
                .Select(x => x.Name)
                .ToList();

            return collection;
        }

        private List<JsonIncomeReportModel> ParseAllJsonIncomeReports()
        {
            var currJsonString = string.Empty;
            List<JsonIncomeReportModel> jsonIncomeModels = new List<JsonIncomeReportModel>();
            var dates = this.GetIncomeDates();
            var dealerIds = this.GetDealerIds();
            foreach (var id in dealerIds)
            {
                foreach (var date in dates)
                {
                    try
                    {
                        currJsonString = this.ReadJsonReport(@"../../Reports/" + id + date.Day + date.Month + date.Year + ".json");
                        var currJsonModel = this.ConvertJsonIncomeReports(currJsonString);
                        jsonIncomeModels.Add(currJsonModel);
                    }
                    catch(FileNotFoundException)
                    {
                        continue;
                    }
                }
            }
            
            return jsonIncomeModels;
        }

        private IList<JsonCountryReportModel> ParseAllJsonCountryReports()
        {
            string currJsonString = string.Empty;
            var countries = this.GetCountries();
            var jsonCountryModels = new List<JsonCountryReportModel>();
            foreach (var country in countries)
            {
                currJsonString = this.ReadJsonReport(@"../../Reports/" + country + ".json");
                var currJsonModel = this.ConvertJsonCountryReports(currJsonString);
                jsonCountryModels.Add(currJsonModel);
            }
            return jsonCountryModels;
        }

        private IList<JsonDealerReportModel> ParseAllJsonDealerReports()
        {
            var currJsonString = string.Empty;
            var dealers = this.GetDealerCompanies();
            var jsonDealerModels = new List<JsonDealerReportModel>();
            foreach (var dealer in dealers)
            {
                currJsonString = this.ReadJsonReport(@"../../Reports/" + dealer + ".json");
                var currJsonModel = this.ConvertJsonDealerReports(currJsonString);
                jsonDealerModels.Add(currJsonModel);
            }

            return jsonDealerModels;
        }
    }
}
