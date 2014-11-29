namespace VehicleVendor.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using VehicleVendor.Models;

    public interface IVehicleVendorMySqlDbContext
    {
        DbSet<Dealer> Dealers { get; set; }

        DbSet<Income> Incomes { get; set; }

        DbSet<Country> Countries { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
