namespace VehicleVendor.Data.Repositories
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using VehicleVendor.Models;

    public class VehicleVendorMySqlRepository : IVehicleVendorMySqlRepository
    {
        private IVehicleVendorMySqlDbContext context;

        public VehicleVendorMySqlRepository(IVehicleVendorMySqlDbContext context)
        {
            this.context = context;
        }

        public DbSet<Dealer> Dealers
        {
            get 
            {
                return this.context.Dealers;
            }
        }

        public DbSet<Income> Incomes
        {
            get
            {
                return this.context.Incomes;
            }
        }

        public DbSet<Country> Countries
        {
            get
            {
                return this.context.Countries;
            }
        }

        public void Add<T>(T entity) where T : class
        {
            var entry = this.AttachIfDetached(entity, context);
            entry.State = EntityState.Added;
        }

        public void Update<T>(T old, T entity) where T : class
        {
            this.Detach<T>(old);
            var entry = this.AttachIfDetached<T>(entity, context);
            entry.State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            var entry = this.AttachIfDetached<T>(entity, context);
            entry.State = EntityState.Deleted;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Detach<T>(T entity) where T : class
        {
            var entry = context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        private DbEntityEntry AttachIfDetached<T>(T entity, IVehicleVendorMySqlDbContext context) where T : class
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
            }

            return entry;
        }
    }
}
