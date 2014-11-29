namespace VehicleVendor.Data.Repositories
{
    using System.Data.Entity;
    using VehicleVendor.Models;

    public interface IVehicleVendorMySqlRepository
    {
        DbSet<Dealer> Dealers { get; }

        DbSet<Income> Incomes { get; }

        DbSet<Country> Countries { get; }

        void Add<T>(T entity) where T : class;

        void Update<T>(T old, T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        void SaveChanges();

        void Detach<T>(T entity) where T : class;
    }
}
