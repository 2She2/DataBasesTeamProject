namespace VehicleVendorConsole.Client
{
    using MongoDB.Driver.Builders;
    using System.Collections.Generic;
    using System.Linq;
    using VehicleVendor.Data;
    using VehicleVendor.Data.Repositories;
    using VehicleVendor.Models;
    
    public class MongoLoader : IRepositoryLoader
    {
        private IVehicleVendorRepository repo;
        private IVehicleVendorMongoRepository repoMongo;

        public MongoLoader(IVehicleVendorRepository repo, IVehicleVendorMongoRepository repoMongo)
        {
            this.repo = repo;
            this.repoMongo = repoMongo;
        }

        public void LoadDiscountsInMongo(IDictionary<int, double> discountParameters)
        {
            foreach (var discount in discountParameters)
            {
                var company = this.repo.Dealers.First(d => d.Id == discount.Key).Company;
                var query = Query<Dealer>.EQ(d => d.Company, company);
                var update = Update<Dealer>.Set(d => d.Discount, discount.Value);
                this.repoMongo.Database.GetCollection<Dealer>("Dealers").Update(query, update);
            }
        }

        public void LoadRepository()
        {
            this.LoadCountries();
            this.LoadDealers();
            this.LoadVehicles();
        }

        private void LoadVehicles()
        {
            if (this.repo.Vehicles.Count() == 0)
            {
                var vehicles = this.repoMongo.GetDocument("Vehicles");
                foreach (var item in vehicles)
                {
                    this.repo.Add<Vehicle>(
                        new Vehicle()
                        {
                            Name = item["Name"].ToString(),
                            Price = (decimal)item["Price"].ToDouble(),
                            Category = (Category)item["Category"].ToInt32()
                        });
                }
            }
        }

        private void LoadCountries()
        {
            if (this.repo.Countries.Count() == 0)
            {
                var coutries = this.repoMongo.GetDocument("Countries");
                foreach (var item in coutries)
                {
                    this.repo.Add<Country>(
                        new Country()
                        {
                            Name = item["Country"].ToString(),
                            Region = (Region)item["Region"].ToInt32()
                        });
                }
            }
        }

        private void LoadDealers()
        {
            if (this.repo.Dealers.Count() == 0)
            {
                var dealers = this.repoMongo.GetDocument("Dealers");
                foreach (var item in dealers)
                {
                    var countryName = item["Country"].ToString();
                    var country = this.repo.Countries.Local.First(c => c.Name == countryName);
                    this.repo.Add<Dealer>(
                        new Dealer()
                        {
                            Company = item["Company"].ToString(),
                            Address = item["Address"].ToString(),
                            Country = country
                        });
                }
            }
        }
    }
}
