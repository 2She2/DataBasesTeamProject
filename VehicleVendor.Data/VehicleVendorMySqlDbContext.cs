namespace VehicleVendor.Data
{
    using System.Data.Entity;
    using VehicleVendor.Data.Migrations;
    using VehicleVendor.Models;

    public class VehicleVendorMySqlDbContext : DbContext, IVehicleVendorMySqlDbContext
    {
        public VehicleVendorMySqlDbContext()
            : base("Nissan")
        {
            Database.SetInitializer<VehicleVendorMySqlDbContext>(
                new MigrateDatabaseToLatestVersion<VehicleVendorMySqlDbContext, ConfigurationMySqlSingle>(
                    "Nissan"));
        }

        public DbSet<Dealer> Dealers { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<Country> Countries { get; set; }
        
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
