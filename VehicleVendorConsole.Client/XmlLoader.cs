namespace VehicleVendorConsole.Client
{
    using System.Collections.Generic;
    using VehicleVendor.Data.Repositories;
    using VehicleVendor.Models;

    public class XmlLoader : IRepositoryLoader
    {
        private IVehicleVendorRepository repo;
        private IDictionary<int, double> discountParameters;

        public XmlLoader(IVehicleVendorRepository repo, IDictionary<int, double> discountParameters)
        {
            this.repo = repo;
            this.discountParameters = discountParameters;
        }

        public void LoadRepository()
        {
            this.LoadDiscounts(this.discountParameters);
        }

        public void LoadDiscounts(IDictionary<int, double> discountParameters)
        {
            foreach (var discount in discountParameters)
            {
                this.repo.Add<Discount>(
                    new Discount()
                    {
                        Amount = discount.Value,
                        DealerId = discount.Key
                    });
            }
        }
    }
}
