namespace VehicleVendor.Data
{
    using VehicleVendor.Data.Repositories;
    using VehicleVendor.Models;

    public interface IVehicleVendorData
    {
        IGenericRepository<Vehicle> Vehicles { get; }
        
        IGenericRepository<Dealer> Dealers { get; }
        
        IGenericRepository<Country> Countries { get; }
        
        IGenericRepository<Sale> Sales { get; }

        IGenericRepository<SaleDetails> SaleDetails { get; }

        IGenericRepository<Discount> Discounts { get; }
        
        void SaveChanges();
    }
}
