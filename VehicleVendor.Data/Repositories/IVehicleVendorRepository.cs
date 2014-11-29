namespace VehicleVendor.Data.Repositories
{
    using System.Data.Entity;
    using VehicleVendor.Models;

    public interface IVehicleVendorRepository
    {
        DbSet<Dealer> Dealers { get; }

        DbSet<Vehicle> Vehicles { get; }        

        DbSet<Country> Countries { get; }

        DbSet<Sale> Sales { get; }
        
        DbSet<SaleDetails> SalesDetails { get; }

        DbSet<Discount> Discounts { get; }

        void Add<T>(T entity) where T : class;
        
        void Update<T>(T old, T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        void SaveChanges();

        void Detach<T>(T entity) where T : class;
    }
}
