namespace VehicleVendor.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using VehicleVendor.Models;

    public interface IVehicleVendorDbContext
    {
        DbSet<Dealer> Dealers { get; set; }

        DbSet<Vehicle> Vehicles { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Sale> Sales { get; set; }

        DbSet<SaleDetails> SalesDetails { get; set; }

        DbSet<Discount> Discounts { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
